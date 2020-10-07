using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using MSCommon;

namespace Server
{
	class LobbyState
	{
		NetServer peer;
		public DatabaseConnection DBconn;
		public List<Player> lobbyPlayers;

		public List<Player> OnevOneList;
		public List<Player> TwovTwoList;
		public List<Player> ThreevThreeList;
		public List<Player> FourvFourList;

		public LobbyState(NetServer p, DatabaseConnection db)
		{
			peer = p;
			DBconn = db;
			lobbyPlayers = new List<Player>();
			OnevOneList = new List<Player>();
			TwovTwoList = new List<Player>();
			ThreevThreeList = new List<Player>();
			FourvFourList = new List<Player>();
		}

		public void HandleLoginRequest(NetIncomingMessage msg)
		{
			string loginUsername = msg.ReadString();
			string pass = msg.ReadString();
			Console.WriteLine("Login Request by: " + loginUsername);

			int tempId = 0;
			NetConnection NC = null;

			NetOutgoingMessage omLogin = peer.CreateMessage();
			omLogin.Write((byte)MasterServerMessageType.RequestLogin);

			List<Player> playerList = DBconn.SelectPlayerInfo(loginUsername, pass);
			if (playerList.Count == 0) //Couldn't find player in database
			{
				omLogin.Write(false);
			}
			else if (playerList.Count == 1) //Found one instance of player.  --Supposed to happen
			{
				bool exist = false;
				Player p = GetPlayerByName(loginUsername); //Check to see if user is already logged in.
				if (p != null)
					exist = true;
				if (exist)
					omLogin.Write(false);
				else
				{
					omLogin.Write(true);
					Player tempPlayer = playerList[0]; //if the code reaches this point, there will only ever be 1 player in list hence hardcoded 0.
					tempPlayer.Connection = msg.SenderConnection;
					lobbyPlayers.Add(tempPlayer);
					DBconn.ChangePlayerStatus(tempPlayer.ID, 1);

					tempId = tempPlayer.ID;
					NC = msg.SenderConnection;
				}
			}
			else //catching
				omLogin.Write(false);

			peer.SendMessage(omLogin, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);

			if (tempId != 0 || NC != null)
			{
				//SendBuddyList(tempId, NC); Causes client to try to update buddylist before screen is loaded.  Now client requests buddylist after it is loaded.
				NotifyPlayersOfBuddyStatusChange(tempId);
			}
		}

		public void HandleCreateAccountRequest(NetIncomingMessage msg)
		{
			string userName = msg.ReadString();
			string password = msg.ReadString();

			NetOutgoingMessage CreateAccount = peer.CreateMessage();
			CreateAccount.Write((byte)MasterServerMessageType.CreateAccount);
			if (userName.Length < 3 || password.Length < 3)
			{
				Console.WriteLine("Invalid account creation. Invalid Username/Pass. " + userName + ":" + password);
				CreateAccount.Write(false);
			}
			else
			{
				Player player = new Player();
				player = DBconn.SelectPlayerInfoNoPassByName(userName);

				if (player != null)
				{
					Console.WriteLine("Invalid account creation. Username already exists. " + userName + ":" + password);
					CreateAccount.Write(false);
				}
				else
				{
					DBconn.CreateNewPlayer(userName, password);
					CreateAccount.Write(true);
				}
				peer.SendMessage(CreateAccount, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
			}
		}

		public void HandleSendMessageRequest(NetIncomingMessage msg)
		{
			

			string chatName = msg.ReadString();
			string message = msg.ReadString();

			NetConnection destinationConn = null;
			string msgSender = null;

			List<NetConnection> conns = new List<NetConnection>();
			if (chatName == "General")
			{
				foreach (Player p in lobbyPlayers)
				{
					if (p.Connection == msg.SenderConnection)
						msgSender = p.Name;
					else
						conns.Add(p.Connection);
				}
				if (!msgSender.Equals(null) || conns.Count != 0)
				{					
					foreach (NetConnection NC in conns)
					{
						NetOutgoingMessage sendMsg = peer.CreateMessage();
						sendMsg.Write((byte)MasterServerMessageType.SendMessage);
						sendMsg.Write(chatName);
						sendMsg.Write(msgSender);
						sendMsg.Write(message);
						peer.SendMessage(sendMsg, NC, NetDeliveryMethod.ReliableOrdered, 0);
					}
				}

			}
			else
			{
				foreach (Player p in lobbyPlayers)
				{
					if (p.Name == chatName)
					{
						destinationConn = p.Connection;
					}
					if (p.Connection == msg.SenderConnection)
					{
						msgSender = p.Name;
					}
				}

				if (destinationConn != null && !msgSender.Equals(null))
				{
					NetOutgoingMessage sendMsg = peer.CreateMessage();
					sendMsg.Write((byte)MasterServerMessageType.SendMessage);
					sendMsg.Write(msgSender);
					sendMsg.Write(msgSender);
					sendMsg.Write(message);
					peer.SendMessage(sendMsg, destinationConn, NetDeliveryMethod.ReliableOrdered, 0);
				}
			}
		}

		public void HandleAddBuddyRequest(NetIncomingMessage msg)
		{
			string Name = msg.ReadString();
			int requestingID = 0;

			NetOutgoingMessage addBud = peer.CreateMessage();
			addBud.Write((byte)MasterServerMessageType.AddBuddy);

			requestingID = GetPlayerbyConnection(msg.SenderConnection).ID;
			//int targetID = DBconn.SelectCharIDbyName(Name);
			int targetID = GetPlayerByName(Name).ID;

			if (targetID == 0 || requestingID == 0 || targetID == requestingID)
			{
				addBud.Write(false); //to let client know requesting/target ID is invalid
			}
			else
			{
				bool bExists = DBconn.CheckBuddyExist(requestingID, targetID);
				if (bExists)
				{
					addBud.Write(false);//to let client know is unsuccessful because buddy already is added.
				}
				else
				{
					addBud.Write(true); //to let client know buddy added successfully
					DBconn.AddNewBuddy(requestingID, targetID);
					SendBuddyList(requestingID, msg.SenderConnection);
				}
			}
			peer.SendMessage(addBud, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public void HandleDisconnect(NetIncomingMessage msg)
		{
			Player p = GetPlayerbyConnection(msg.SenderConnection);
			lobbyPlayers.Remove(p);
			DBconn.ChangePlayerStatus(p.ID, 0);
			NotifyPlayersOfBuddyStatusChange(p.ID);			
		}

		public void HandleSendBuddyListRequest(NetIncomingMessage msg)
		{
			int requestingID = 0;
			requestingID = GetPlayerbyConnection(msg.SenderConnection).ID;

			if (requestingID != 0)
			{
				SendBuddyList(requestingID, msg.SenderConnection);
			}
		}

		public void HandleSendCharacterInfo(NetIncomingMessage msg)
		{
			Player tempP = new Player();
			tempP = GetUpdatedPlayerbyConnection(msg.SenderConnection);

			if (!tempP.Equals(null))
			{
				NetOutgoingMessage sendCharacterInfo = peer.CreateMessage();
				sendCharacterInfo.Write((byte)MasterServerMessageType.CharacterInfo);
                sendCharacterInfo.Write(tempP.ID);
				sendCharacterInfo.Write(tempP.Name);
				sendCharacterInfo.Write(tempP.Rank);
                sendCharacterInfo.Write(tempP.Wins);
                sendCharacterInfo.Write(tempP.Losses);
				sendCharacterInfo.Write(tempP.Tank);
				sendCharacterInfo.Write(tempP.Skin);
				sendCharacterInfo.Write(tempP.AvaHead);
				sendCharacterInfo.Write(tempP.AvaShoulder);
				sendCharacterInfo.Write(tempP.AvaChest);
				sendCharacterInfo.Write(tempP.Gold);
				peer.SendMessage(sendCharacterInfo, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
			}
		}

		public void HandleUpdateCharacterInfo(NetIncomingMessage inc)
		{
			Player tempP = new Player();
			tempP.Tank = inc.ReadInt32();
			tempP.Skin = inc.ReadInt32();
			tempP.AvaHead = inc.ReadInt32();
			tempP.AvaShoulder = inc.ReadInt32();
			tempP.AvaChest = inc.ReadInt32();
			tempP.ID = GetPlayerbyConnection(inc.SenderConnection).ID;
			DBconn.UpdateCharacterInfo(tempP);
			
			Player p = GetPlayerByID(tempP.ID);			
			p.Tank = tempP.Tank;
			p.Skin = tempP.Skin;
			p.AvaHead = tempP.AvaHead;
			p.AvaShoulder = tempP.AvaShoulder;
			p.AvaChest = tempP.AvaChest;
		}

		public void HandleRequestTanks(NetIncomingMessage msg)
		{
			int id = GetPlayerbyConnection(msg.SenderConnection).ID;
			List<CharTank> tankList = DBconn.SelectCharacterTanks(id);
			NetOutgoingMessage sendTankList = peer.CreateMessage();
			sendTankList.Write((byte)MasterServerMessageType.RequestTanks);
			sendTankList.Write(tankList.Count);
			foreach (CharTank CT in tankList)
			{
				sendTankList.Write(CT.ID);
				sendTankList.Write(CT.Rank);
				sendTankList.Write(CT.shot1);
				sendTankList.Write(CT.shot2);
				sendTankList.Write(CT.shot3);
				sendTankList.Write(CT.item1);
				sendTankList.Write(CT.shot2);
				sendTankList.Write(CT.shot3);
			}
			peer.SendMessage(sendTankList, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public void HandleRequestAvatar(NetIncomingMessage msg)
		{
			int id = GetPlayerbyConnection(msg.SenderConnection).ID;
			List<Tuple<int, int>> avatarList = DBconn.SelectCharacterAvatar(id);
			NetOutgoingMessage sendAvatarList = peer.CreateMessage();
			sendAvatarList.Write((byte)MasterServerMessageType.RequestAvatar);
			sendAvatarList.Write(avatarList.Count);
			foreach (Tuple<int, int> T in avatarList)
			{
				sendAvatarList.Write(T.Item1);
				sendAvatarList.Write(T.Item2);
			}
			peer.SendMessage(sendAvatarList, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public void HandlePurchaseItemRequest(NetIncomingMessage msg)
		{
			int type = msg.ReadInt32();
			int itemID = msg.ReadInt32();

			int goldCost = 99999999;

			NetOutgoingMessage purchaseItem = peer.CreateMessage();
			purchaseItem.Write((byte)MasterServerMessageType.PurchaseItem);
			foreach (Player p in lobbyPlayers)
			{
				if (p.Connection == msg.SenderConnection)
				{
					switch (type)
					{
						case 1:
							goldCost = DBconn.GetGoldCost("Tanks", itemID);
							if (goldCost < p.Gold)
							{
								p.Gold -= goldCost;
								DBconn.PurchaseItem("Tanks", itemID, p.ID, p.Gold);
								purchaseItem.Write(true);
								purchaseItem.Write(goldCost);
							}
							else
								purchaseItem.Write(false);
							peer.SendMessage(purchaseItem, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
							break;
						case 2:
							goldCost = DBconn.GetGoldCost("AvatarHead", itemID);
							if (goldCost < p.Gold)
							{
								p.Gold -= goldCost;
								DBconn.PurchaseItem("AvatarHead", itemID, p.ID, p.Gold);
								purchaseItem.Write(true);
								purchaseItem.Write(goldCost);
							}
							else
								purchaseItem.Write(false);
							peer.SendMessage(purchaseItem, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
							break;
						case 3:
							goldCost = DBconn.GetGoldCost("AvatarShoulder", itemID);
							if (goldCost < p.Gold)
							{
								p.Gold -= goldCost;
								DBconn.PurchaseItem("AvatarShoulder", itemID, p.ID, p.Gold);
								purchaseItem.Write(true);
								purchaseItem.Write(goldCost);
							}
							else
								purchaseItem.Write(false);
							peer.SendMessage(purchaseItem, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
							break;
						case 4:
							goldCost = DBconn.GetGoldCost("AvatarChest", itemID);
							if (goldCost < p.Gold)
							{
								p.Gold -= goldCost;
								DBconn.PurchaseItem("AvatarChest", itemID, p.ID, p.Gold);
								purchaseItem.Write(true);
								purchaseItem.Write(goldCost);
							}
							else
								purchaseItem.Write(false);
							peer.SendMessage(purchaseItem, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
							break;
						case 5:
							goldCost = DBconn.GetGoldCost("Skins", itemID);
							if (goldCost < p.Gold)
							{
								p.Gold -= goldCost;
								DBconn.PurchaseItem("Skins", itemID, p.ID, p.Gold);
								purchaseItem.Write(true);
								purchaseItem.Write(goldCost);
							}
							else
								purchaseItem.Write(false);
							peer.SendMessage(purchaseItem, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
							break;
					}
				}
			}
		}

		//For random bots
		public List<Player> HandleStartGameRequestWithBots(NetIncomingMessage msg)
		{
			Random rand = new Random();
			int numPlayers = msg.ReadInt32();
			numPlayers = numPlayers * 2 + 1;
			List<Player> players = new List<Player>();

			int id = GetPlayerbyConnection(msg.SenderConnection).ID;
			Player p = DBconn.SelectPlayerInfoNoPassByID(id);
			NetOutgoingMessage StartGame = peer.CreateMessage();
			StartGame.Write((byte)MasterServerMessageType.StartGame);
			StartGame.Write("radiatin");
			StartGame.Write("dragon_b");
			StartGame.Write(1); //Lives
            StartGame.Write(1); //ID
            StartGame.Write(p.ID); //Starting player ID. In this case it will always be the player
			players.Add(p);
			StartGame.Write(numPlayers);
			StartGame.Write(p.ID);
			StartGame.Write(p.Name);
			StartGame.Write(p.Rank);
            StartGame.Write(1);
			StartGame.Write(p.Tank);
			StartGame.Write(p.Skin);
			StartGame.Write(p.AvaHead);
			StartGame.Write(p.AvaShoulder);
			StartGame.Write(p.AvaChest);
            StartGame.Write(100 + (int)p.Health);

			for (int x = 0; x <= numPlayers; x++)
			{
				Player tempP = new Player();
				tempP.ID = p.ID + x + 1;
				tempP.Name = ("Bot" + x);
				tempP.Rank = rand.Next(0, 100);
				tempP.Tank = rand.Next(1, 6);
				tempP.Skin = rand.Next(1, 4);
				tempP.AvaHead = rand.Next(1, 3);
				tempP.AvaShoulder = rand.Next(1, 3);
				tempP.AvaChest = rand.Next(1, 3);
				StartGame.Write(tempP.ID);
				StartGame.Write(tempP.Name);
				StartGame.Write(tempP.Rank);
				StartGame.Write(tempP.Tank);
				StartGame.Write(tempP.Skin);
				StartGame.Write(tempP.AvaHead);
				StartGame.Write(tempP.AvaShoulder);
				StartGame.Write(tempP.AvaChest);
                StartGame.Write(100); //health
				players.Add(tempP);
			}
			peer.SendMessage(StartGame, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
			return players;
		}

		public void HandleStartGameRequest(NetIncomingMessage msg)
		{
			Random rand = new Random();
			int numPlayers = msg.ReadInt32();
			Player p = GetPlayerbyConnection(msg.SenderConnection);

			switch (numPlayers)
			{
				case 0:
					OnevOneList.Add(p);
					break;
				case 1:
					TwovTwoList.Add(p);
					break;
				case 2:
					ThreevThreeList.Add(p);
					break;
				case 3:
					FourvFourList.Add(p);
					break;
			}

			NetOutgoingMessage FindingGame = peer.CreateMessage();
			FindingGame.Write((byte)MasterServerMessageType.FindingGame);
			peer.SendMessage(FindingGame, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public void HandleCancelStartGameRequest(NetIncomingMessage msg)
		{
			foreach (Player p in OnevOneList)
			{
				if (p.Connection == msg.SenderConnection)
				{
					OnevOneList.Remove(p);
					break;
				}					
			}
			foreach (Player p in TwovTwoList)
			{
				if (p.Connection == msg.SenderConnection)
				{
					TwovTwoList.Remove(p);
					break;
				}
			}
			foreach (Player p in ThreevThreeList)
			{
				if (p.Connection == msg.SenderConnection)
				{
					ThreevThreeList.Remove(p);
					break;
				}
			}
			foreach (Player p in FourvFourList)
			{
				if (p.Connection == msg.SenderConnection)
				{
					FourvFourList.Remove(p);
					break;
				}
			}
		}

		public List<Player> TryMatchPlayersForGame()
		{
			if (OnevOneList.Count > 1)
			{
				List<Player> pList = new List<Player>();
				for (int i = 0; i <= 1; i++)
				{
					if (i % 2 != 0) //if i is odd
						OnevOneList[i].team = TeamTypes.Blue;
					else
						OnevOneList[i].team = TeamTypes.Red;
					pList.Add(OnevOneList[i]);
				}
				foreach (Player p in pList)
				{
					OnevOneList.Remove(p);
				}
				return pList;
			}
			if (TwovTwoList.Count > 3)
			{
				List<Player> pList = new List<Player>();
				for (int i = 0; i <= 3; i++)
				{
					if (i % 2 != 0) //if i is odd
                        TwovTwoList[i].team = TeamTypes.Blue;
					else
                        TwovTwoList[i].team = TeamTypes.Red;
					pList.Add(TwovTwoList[i]);
				}
				foreach (Player p in pList)
				{
					TwovTwoList.Remove(p);
				}
				return pList;
			}
			if (ThreevThreeList.Count > 5)
			{
				List<Player> pList = new List<Player>();
				for (int i = 0; i <= 5; i++)
				{
					if (i % 2 != 0) //if i is odd
                        ThreevThreeList[i].team = TeamTypes.Blue;
					else
                        ThreevThreeList[i].team = TeamTypes.Red;
					pList.Add(ThreevThreeList[i]);
				}
				foreach (Player p in pList)
				{
					ThreevThreeList.Remove(p);
				}
				return pList;
			}
			if (FourvFourList.Count > 7)
			{
				List<Player> pList = new List<Player>();
				for (int i = 0; i <= 7; i++)
				{
					if (i % 2 != 0) //if i is odd
                        FourvFourList[i].team = TeamTypes.Blue;
					else
                        FourvFourList[i].team = TeamTypes.Red;
					pList.Add(FourvFourList[i]);
				}
				foreach (Player p in pList)
				{
					FourvFourList.Remove(p);
				}
				return pList;
			}
			return null;
		}

		private Player GetPlayerByID(int ID)
		{
			foreach (Player p in lobbyPlayers)
			{
				if (p.ID == ID)
					return p;
			}
			return null;
		}

		private Player GetPlayerByName(string name)
		{
			foreach (Player p in lobbyPlayers)
			{
				if (p.Name == name)
					return p;
			}
			return null;
		}

		private Player GetPlayerbyConnection(NetConnection NC)
		{
			foreach (Player p in lobbyPlayers)
			{
				if (p.Connection == NC)
				{
					return p;
				}
			}
			return null;
		}

        private Player GetUpdatedPlayerbyConnection(NetConnection NC)
        {
            foreach (Player p in lobbyPlayers)
            {
                if (p.Connection == NC)
                {
                    Player tempP = DBconn.SelectPlayerInfoNoPassByID(p.ID);
                    p.Gold = tempP.Gold;
                    p.Wins = tempP.Wins;
                    p.Losses = tempP.Losses;
                    return p;
                }
            }
            return null;
        }

		private void NotifyPlayersOfBuddyStatusChange(int buddyID)
		{
			List<int> buddyList = new List<int>();
			buddyList = DBconn.SelectOnlineBuddiesByBuddyID(buddyID);

			if (buddyList != null)
			{
				foreach (int b in buddyList)
				{
					Player p = GetPlayerByID(b);					
					SendBuddyList(p.ID, p.Connection);
				}
			}
		}

		private void SendBuddyList(int ID, NetConnection conn)
		{
			List<string> buddyList = DBconn.SelectBuddiesByCharID(ID);

			NetOutgoingMessage sendBuddyList = peer.CreateMessage();
			sendBuddyList.Write((byte)MasterServerMessageType.SendBuddyList);
			sendBuddyList.Write(buddyList.Count);
			foreach (string b in buddyList)
			{
				sendBuddyList.Write(b);
			}
			peer.SendMessage(sendBuddyList, conn, NetDeliveryMethod.ReliableOrdered, 0);

		}		
	}
}
