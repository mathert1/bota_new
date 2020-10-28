using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using MSCommon;
using System.Data.SqlClient;

namespace Server
{
	class GameServer
	{
		public NetServer peer;
		public DatabaseConnection DBconn;

		double nextSendUpdates;
		GameState gameState;
		LobbyState lobbyState;

		static void Main(string[] args)
		{
			GameServer server = new GameServer();
		}

		public GameServer()
		{
			InitServer();
			DBconn = new DatabaseConnection();
			DBconn.DisconnectAllPlayers();

			gameState = new GameState(peer, DBconn);
			lobbyState = new LobbyState(peer, DBconn);

			nextSendUpdates = NetTime.Now;
			GameLoop();
		}

		private void InitServer()
		{
			NetPeerConfiguration config = new NetPeerConfiguration("game");
			config.AcceptIncomingConnections = true;
			config.ConnectionTimeout = 20.0f;
			config.Port = CommonConstants.MasterServerPort;
			config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

			peer = new NetServer(config);
			peer.Start();
		}

		public void GameLoop()
		{
			while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Escape)
			{
				ReceiveMessages();

				// send position updates 30 times per second
				double now = NetTime.Now;
				if (now > nextSendUpdates)
				{
					//Try to find players for game. Might need to move this later
					List<Player> pList = new List<Player>();
					pList = lobbyState.TryMatchPlayersForGame();
					if (pList != null)
						gameState.CreateGame(pList);

					gameState.Update();
					// Yes, it's time to send position updates

					// schedule next update
					//nextSendUpdates += (1.0 / 30.0);
					nextSendUpdates += (1.0 / 75.0);
				}
			}

			peer.Shutdown("shutting down");
		}

		public void ReceiveMessages()
		{
			NetIncomingMessage msg;
			while ((msg = peer.ReadMessage()) != null)
			{
				switch (msg.MessageType)
				{
					case NetIncomingMessageType.UnconnectedData:
						break;
					case NetIncomingMessageType.Data:
						switch ((MasterServerMessageType)msg.ReadByte())
						{
                            //-----------LOBBY STATE-----------------------
							case MasterServerMessageType.RequestLogin: //Login Request
								lobbyState.HandleLoginRequest(msg);
								break;
							case MasterServerMessageType.CreateAccount:
								lobbyState.HandleCreateAccountRequest(msg);
								break;
							case MasterServerMessageType.SendMessage:
								lobbyState.HandleSendMessageRequest(msg);
								break;
							case MasterServerMessageType.AddBuddy:
								lobbyState.HandleAddBuddyRequest(msg);
								break;
							case MasterServerMessageType.Disconnect:
								lobbyState.HandleDisconnect(msg);
								break;
							case MasterServerMessageType.SendBuddyList:
								lobbyState.HandleSendBuddyListRequest(msg);
								break;
							case MasterServerMessageType.CharacterInfo:
								lobbyState.HandleSendCharacterInfo(msg);
								break;
							case MasterServerMessageType.UpdateCharacterInfo:
								lobbyState.HandleUpdateCharacterInfo(msg);
								break;
							case MasterServerMessageType.RequestTanks:
								lobbyState.HandleRequestTanks(msg);
								break;
                            case MasterServerMessageType.RequestAvatar:
                                lobbyState.HandleRequestAvatar(msg);
                                break;
							case MasterServerMessageType.PurchaseItem:
								lobbyState.HandlePurchaseItemRequest(msg);
								break;
							case MasterServerMessageType.StartGame:
								bool bots = msg.ReadBoolean();
								if (bots)
								{
									List<Player> pList = lobbyState.HandleStartGameRequestWithBots(msg);
									gameState.CreateGame(pList);
								}
								else
									lobbyState.HandleStartGameRequest(msg);
								break;
                            case MasterServerMessageType.GameLoaded:
                                gameState.GameLoaded(msg);
                                break;
							case MasterServerMessageType.CancelStartGame:
								lobbyState.HandleCancelStartGameRequest(msg);
								break;
                            //---------------GAME STATE-------------------
							case MasterServerMessageType.Move:
								gameState.Move(msg);
								break;
							case MasterServerMessageType.CreateRocket:
								gameState.CreateRocket(msg);
								break;
						}
						break;
					case NetIncomingMessageType.ConnectionApproval:
						msg.SenderConnection.Approve();
						Console.WriteLine(msg.SenderConnection.RemoteEndpoint.ToString() + " just connected");
						//Used to add player to lobbyPlayers on connection, now adds after successful login.
						//GamePlayer newp = new GamePlayer(msg.SenderConnection);
						//lobbyPlayers.Add(newp);
						break;
					case NetIncomingMessageType.DebugMessage:
					case NetIncomingMessageType.VerboseDebugMessage:
					case NetIncomingMessageType.WarningMessage:
					case NetIncomingMessageType.ErrorMessage:
						// print diagnostics message
						Console.WriteLine(msg.ReadString());
						break;
				}
			}
		}
	}
}
