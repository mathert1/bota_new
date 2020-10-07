using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using MSCommon;

namespace WpfTest
{
	class ServerConnection
	{
		public static NetClient m_client;

		public static void Connect()
		{
			NetPeerConfiguration config = new NetPeerConfiguration("game");
			config.ConnectionTimeout = 20.0f;

			m_client = new NetClient(config);
			m_client.Start();
			m_client.Connect("localhost", CommonConstants.MasterServerPort);
		}

		public static void TryLogin(String userName, String password)
		{
			NetOutgoingMessage loginRequest = m_client.CreateMessage();
			loginRequest.Write((byte)MasterServerMessageType.RequestLogin);
			loginRequest.Write(userName);
			loginRequest.Write(password);
			m_client.SendMessage(loginRequest, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void CreateAccount(String userName, String password)
		{
			NetOutgoingMessage CreateAccount = m_client.CreateMessage();
			CreateAccount.Write((byte)MasterServerMessageType.CreateAccount);
			CreateAccount.Write(userName);
			CreateAccount.Write(password);
			m_client.SendMessage(CreateAccount, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void SendMessage(String name, String message)
		{
			NetOutgoingMessage sendMsg = m_client.CreateMessage();
			sendMsg.Write((byte)MasterServerMessageType.SendMessage);
			sendMsg.Write(name);
			sendMsg.Write(message);
			m_client.SendMessage(sendMsg, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void AddBuddy(String bName)
		{
			NetOutgoingMessage addBuddy = m_client.CreateMessage();
			addBuddy.Write((byte)MasterServerMessageType.AddBuddy);
			addBuddy.Write(bName);
			m_client.SendMessage(addBuddy, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void Disconnect()
		{
			NetOutgoingMessage DC = m_client.CreateMessage();
			DC.Write((byte)MasterServerMessageType.Disconnect);
			m_client.SendMessage(DC, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void RequestBuddyList()
		{
			NetOutgoingMessage BL = m_client.CreateMessage();
			BL.Write((byte)MasterServerMessageType.SendBuddyList);
			m_client.SendMessage(BL, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void RequestCharacterInfo()
		{
			NetOutgoingMessage CI = m_client.CreateMessage();
			CI.Write((byte)MasterServerMessageType.CharacterInfo);
			m_client.SendMessage(CI, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void RequestMyTanks()
		{
			NetOutgoingMessage MT = m_client.CreateMessage();
			MT.Write((byte)MasterServerMessageType.RequestTanks);
			m_client.SendMessage(MT, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void RequestMyAvatar()
		{
			NetOutgoingMessage MA = m_client.CreateMessage();
			MA.Write((byte)MasterServerMessageType.RequestAvatar);
			m_client.SendMessage(MA, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void UpdateCharacterInfo(Player p)
		{
			NetOutgoingMessage CI = m_client.CreateMessage();
			CI.Write((byte)MasterServerMessageType.UpdateCharacterInfo);
			CI.Write(p.Tank);
			CI.Write(p.Skin);
			CI.Write(p.AvaHead);
			CI.Write(p.AvaShoulder);
			CI.Write(p.AvaChest);
			m_client.SendMessage(CI, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void PurchaseItem(int type, int ID)
		{
			NetOutgoingMessage PI = m_client.CreateMessage();
			PI.Write((byte)MasterServerMessageType.PurchaseItem);
			PI.Write(type);
			PI.Write(ID);
			m_client.SendMessage(PI, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void StartGame(bool bots)
		{
			NetOutgoingMessage SG = m_client.CreateMessage();
			SG.Write((byte)MasterServerMessageType.StartGame);
			SG.Write(bots);
			SG.Write(Variables.numPlayers);
			m_client.SendMessage(SG, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public static void CancelStartGame()
		{
			NetOutgoingMessage CSG = m_client.CreateMessage();
			CSG.Write((byte)MasterServerMessageType.CancelStartGame);
			m_client.SendMessage(CSG, NetDeliveryMethod.ReliableOrdered, 0);
		}

        public static void GameLoaded(int gameID)
        {
            NetOutgoingMessage GL = m_client.CreateMessage();
            GL.Write((byte)MasterServerMessageType.GameLoaded);
            GL.Write(gameID);
            m_client.SendMessage(GL, NetDeliveryMethod.ReliableOrdered, 0);
        }
	}
}
