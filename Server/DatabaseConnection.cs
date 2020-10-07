using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MSCommon;

namespace Server
{
	class DatabaseConnection
	{
		public SqlConnection sqlConn;

		public DatabaseConnection()
		{
			ConnectDB();
		}

		public void ConnectDB()
		{
            //GrandChamp-PC\\SQLEXPRESS
            sqlConn = new SqlConnection(//"user id=username;" +
                //"password=password;" +
				"server=DESKTOP-CUI4F07;" +
                //"Trusted_Connection=yes;" +
				"Integrated Security=true;" +
                "database=BOTA; " +
                "connection timeout=30");
            try
			{
				sqlConn.Open();
				Console.WriteLine("Successfully setup database.");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public int SelectCharIDbyName(string charName)
		{
			SqlDataReader myReader = null;
			SqlCommand myCommand = new SqlCommand(string.Format(
				"SELECT TOP 1 CharID "+
				"FROM dbo.CharacterInfo "+
				"WHERE CharName = '{0}'", charName), sqlConn);
			myReader = myCommand.ExecuteReader();
			if (myReader.HasRows)
			{
				myReader.Read();
				int charId = (int)myReader["CharID"];
				myReader.Close();
				return charId;
			}
			myReader.Close();
			return 0;
		}

		public List<Player> SelectPlayerInfo(String loginUsername, String pass)
		{
			List<Player> playerList = new List<Player>();
			SqlDataReader myReader = null;
			SqlCommand myCommand = new SqlCommand(string.Format(
				"SELECT CI.CharID,CI.CharName,CI.[Rank],T.ID As Tank,S.ID As Skin, " +
				"AVH.ID As AvaHead,AVS.ID As AvaShoulder,AVC.ID As AvaChest,CI.Gold, " +
				"(T.Attack + AVH.Attack + AVC.Attack + AVS.Attack + S.Attack) As Attack, " +
				"(T.Defense + AVH.Defense + AVC.Defense + AVS.Defense + S.Defense) As Defense, " +
				"(T.Health + AVH.Health + AVC.Health + AVS.Health + S.Health) As Health, " +
				"(T.Shield + AVH.Shield + AVC.Shield + AVS.Shield + S.Shield) As Shield, " +
				"(T.[Move] + AVH.[Move] + AVC.[Move] + AVS.[Move] + S.[Move]) As [Move], " +
				"(T.Popularity + AVH.Popularity + AVC.Popularity + AVS.Popularity + S.Popularity) As Popularity  " +
				"FROM dbo.CharacterInfo CI " +
				"LEFT JOIN CharacterAvatarHead CAH ON CI.CharAvaHeadID = CAH.ID " +
				"LEFT JOIN AvatarHead AVH ON CAH.AvatarID = AVH.ID " +
				"LEFT JOIN CharacterAvatarChest CAC ON CI.CharAvaChestID = CAC.ID " +
				"LEFT JOIN AvatarChest AVC ON CAC.AvatarID = AVC.ID " +
				"LEFT JOIN CharacterAvatarShoulder CAS ON CI.CharAvaShoulderID = CAS.ID " +
				"LEFT JOIN AvatarShoulder AVS ON CAS.AvatarID = AVS.ID " +
				"INNER JOIN CharacterTanks CT on CI.CharTankID = CT.ID " +
				"INNER JOIN Tanks T ON CT.TankID = T.ID " +
				"INNER JOIN CharacterSkins CS ON CI.CharSkinID = CS.ID " +
				"INNER JOIN Skins S ON CS.SkinID = S.ID " +
				"WHERE CI.CharName = '{0}' AND CI.Pass = '{1}'", loginUsername, pass), sqlConn);//", sqlConn);
			myReader = myCommand.ExecuteReader();
			if (myReader.HasRows)
			{
				while (myReader.Read())
				{
					Player p = new Player();
					p.ID = (int)myReader["CharID"];
					p.Name = ((string)myReader["CharName"]).Trim();
					p.Rank = (int)myReader["Rank"];
					p.Tank = (int)myReader["Tank"];
					p.Skin = (int)myReader["Skin"];
					if (!DBNull.Value.Equals(myReader["AvaHead"]))
						p.AvaHead = (int)myReader["AvaHead"];
					else
						p.AvaHead = 0;
					if (!DBNull.Value.Equals(myReader["AvaShoulder"]))
						p.AvaShoulder = (int)myReader["AvaShoulder"];
					else
						p.AvaShoulder = 0;
					if (!DBNull.Value.Equals(myReader["AvaChest"]))
						p.AvaChest = (int)myReader["AvaChest"];
					else
						p.AvaChest = 0;			
					p.Gold = (int)myReader["Gold"];
					p.Attack = (int)myReader["Attack"];
					p.Defense = (int)myReader["Defense"];
					p.Health = (int)myReader["Health"];
					p.Shield = (int)myReader["Shield"];
					p.Move = (int)myReader["Move"];
					p.Popularity = (int)myReader["Popularity"];
					
					playerList.Add(p);
				}				
			}			
			myReader.Close();

			return playerList;
		}

		public Player SelectPlayerInfoNoPassByID(int id)
		{
			SqlDataReader myReader = null;
			SqlCommand myCommand = new SqlCommand(string.Format(
				"SELECT CI.CharID,CI.CharName,CI.[Rank],CI.Wins,CI.Losses,CT.TankID As Tank,CS.SkinID As Skin, " +
                "CAH.AvatarID As AvaHead,CAS.AvatarID As AvaShoulder,CAC.AvatarID As AvaChest,CI.Gold, " +
				"(T.Attack + AVH.Attack + AVC.Attack + AVS.Attack + S.Attack) As Attack, " +
				"(T.Defense + AVH.Defense + AVC.Defense + AVS.Defense + S.Defense) As Defense, " +
				"(T.Health + AVH.Health + AVC.Health + AVS.Health + S.Health) As Health, " +
				"(T.Shield + AVH.Shield + AVC.Shield + AVS.Shield + S.Shield) As Shield, " +
				"(T.[Move] + AVH.[Move] + AVC.[Move] + AVS.[Move] + S.[Move]) As [Move], " +
				"(T.Popularity + AVH.Popularity + AVC.Popularity + AVS.Popularity + S.Popularity) As Popularity  " +
				"FROM dbo.CharacterInfo CI " +
				"LEFT JOIN CharacterAvatarHead CAH ON CI.CharAvaHeadID = CAH.ID " +
				"LEFT JOIN AvatarHead AVH ON CAH.AvatarID = AVH.ID " +
				"LEFT JOIN CharacterAvatarChest CAC ON CI.CharAvaChestID = CAC.ID " +
				"LEFT JOIN AvatarChest AVC ON CAC.AvatarID = AVC.ID " +
				"LEFT JOIN CharacterAvatarShoulder CAS ON CI.CharAvaShoulderID = CAS.ID " +
				"LEFT JOIN AvatarShoulder AVS ON CAS.AvatarID = AVS.ID " +
				"INNER JOIN CharacterTanks CT on CI.CharTankID = CT.ID " +
				"INNER JOIN Tanks T ON CT.TankID = T.ID " +
				"INNER JOIN CharacterSkins CS ON CI.CharSkinID = CS.ID " +
				"INNER JOIN Skins S ON CS.SkinID = S.ID " +
				"WHERE CI.CharID = '{0}'", id), sqlConn);
			myReader = myCommand.ExecuteReader();
			if (myReader.HasRows)
			{
				myReader.Read();
				
				Player p = new Player();
				p.ID = (int)myReader["CharID"];
				p.Name = ((string)myReader["CharName"]).Trim();
				p.Rank = (int)myReader["Rank"];
                p.Wins = (int)myReader["Wins"];
                p.Losses = (int)myReader["Losses"];
				p.Tank = (int)myReader["Tank"];
				p.Skin = (int)myReader["Skin"];
				if (!DBNull.Value.Equals(myReader["AvaHead"]))
					p.AvaHead = (int)myReader["AvaHead"];
				else
					p.AvaHead = 0;
				if (!DBNull.Value.Equals(myReader["AvaShoulder"]))
					p.AvaShoulder = (int)myReader["AvaShoulder"];
				else
					p.AvaShoulder = 0;
				if (!DBNull.Value.Equals(myReader["AvaChest"]))
					p.AvaChest = (int)myReader["AvaChest"];
				else
					p.AvaChest = 0;
				p.Gold = (int)myReader["Gold"];
				p.Attack = (int)myReader["Attack"];
				p.Defense = (int)myReader["Defense"];
				p.Health = (int)myReader["Health"];
				p.Shield = (int)myReader["Shield"];
				p.Move = (int)myReader["Move"];
				p.Popularity = (int)myReader["Popularity"];
				myReader.Close();
				return p;
			}
			else
				myReader.Close();

			return null;
		}

		public Player SelectPlayerInfoNoPassByName(String loginUsername)
		{		
			SqlDataReader myReader = null;
			SqlCommand myCommand = new SqlCommand(string.Format(
				"SELECT TOP 1 * "+
				"FROM dbo.CharacterInfo "+
				"WHERE CharName = '{0}'",loginUsername), sqlConn);//", sqlConn);
			myReader = myCommand.ExecuteReader();
			if (myReader.HasRows)
			{
				myReader.Read();				

				Player p = new Player();
				p.ID = (int)myReader["CharID"];
				p.Name = ((string)myReader["CharName"]).Trim();				
				p.Rank = (int)myReader["Rank"];				
				myReader.Close();
				return p;
			}
			else
				myReader.Close();

			return null;
		}

		public void UpdateCharacterInfo(Player p)
		{
			SqlCommand myCommand = new SqlCommand(string.Format(
                "UPDATE dbo.CharacterInfo " +
                "SET CharTankID = (SELECT TOP 1 ID FROM CharacterTanks WHERE CharID = {5} AND TankID = {0})," +
                "CharSkinID = (SELECT TOP 1 ID FROM CharacterSkins WHERE CharID = {5} AND SkinID = {1})," +
                "CharAvaHeadID = (SELECT TOP 1 ID FROM CharacterAvatarHead WHERE CharID = {5} AND AvatarID = {2})," +
                "CharAvaShoulderID = (SELECT TOP 1 ID FROM CharacterAvatarSHoulder WHERE CharID = {5} AND AvatarID = {3})," +
                "CharAvaChestID = (SELECT TOP 1 ID FROM CharacterAvatarChest WHERE CharID = {5} AND AvatarID = {4}) " +
                "WHERE CharID = {5}", p.Tank, p.Skin, p.AvaHead, p.AvaShoulder, p.AvaChest, p.ID), sqlConn);
				myCommand.ExecuteNonQuery();
		}

        public void UpdateCharacterStats(Player p, int win, int lose)
        {
            SqlCommand myCommand = new SqlCommand(string.Format(
                "UPDATE dbo.CharacterInfo " +
                "SET Gold = Gold + {0}, " +
                "Wins = Wins + {1}, " +
                "Losses = Losses + {2} " +
                "WHERE CharID = {3}", p.GoldEarned, win, lose, p.ID), sqlConn);
            myCommand.ExecuteNonQuery();
        }

		public List<CharTank> SelectCharacterTanks(int id)
		{
			List<CharTank> tempTankList = new List<CharTank>();
			SqlDataReader myReader = null;
			SqlCommand myCommand = new SqlCommand(string.Format(
				"SELECT * " +
				"FROM dbo.CharacterTanks " +
				"WHERE CharID = '{0}'", id), sqlConn);//", sqlConn);
			myReader = myCommand.ExecuteReader();
			if (myReader.HasRows)
			{
				while (myReader.Read())
				{
					CharTank cT = new CharTank();
					cT.ID = (int)myReader["TankID"];
					cT.Rank = (int)myReader["Rank"];
					cT.shot1 = ((int)myReader["Shot1"]);
					if (!DBNull.Value.Equals(myReader["Shot2"]))
						cT.shot2 = ((int)myReader["Shot2"]);
					else
						cT.shot2 = 0;
					if (!DBNull.Value.Equals(myReader["Shot3"]))
						cT.shot3 = ((int)myReader["Shot3"]);
					else
						cT.shot3 = 0;
					if (!DBNull.Value.Equals(myReader["Item1"]))
						cT.item1 = ((int)myReader["Item1"]);
					else
						cT.item1 = 0;
					if (!DBNull.Value.Equals(myReader["Item2"]))
						cT.item2 = ((int)myReader["Item2"]);
					else
						cT.item2 = 0;
					if (!DBNull.Value.Equals(myReader["Item3"]))
						cT.item3 = ((int)myReader["Item3"]);
					else
						cT.item3 = 0;
					tempTankList.Add(cT);
				}

				myReader.Close();
				return tempTankList;
			}
			else
				myReader.Close();
			return null;
		}

		public List<Tuple<int,int>> SelectCharacterAvatar(int id)
		{
			List<Tuple<int, int>> tempAvatarList = new List<Tuple<int, int>>();
			SqlDataReader myReader = null;
			SqlCommand myCommand = new SqlCommand(string.Format(
				"SELECT CAH.AvatarID AS avaID, " +
				"1 AS Type " +
				"FROM CharacterAvatarHead CAH " +
				"WHERE CAH.CharID = {0} " +
				"UNION " +
				"SELECT CAS.AvatarID AS avaID, " +
				"2 AS Type " +
				"FROM CharacterAvatarShoulder CAS " +
				"WHERE CAS.CharID = {0} " +
				"UNION " +
				"SELECT CAC.AvatarID AS avaID, " +
				"3 AS Type " +
				"FROM CharacterAvatarChest CAC " +
				"WHERE CAC.CharID = {0} " +
				"UNION " +
				"SELECT CS.SkinID AS avaID, " +
				"4 AS Type " +
				"FROM CharacterSkins CS " +
				"WHERE CS.CharID = {0}", id), sqlConn);
			myReader = myCommand.ExecuteReader();
			if (myReader.HasRows)
			{
				while (myReader.Read())
				{
					int avaID = 0;
					int type = 0;
					if (!DBNull.Value.Equals(myReader["avaID"]))
						avaID = ((int)myReader["avaID"]);
					type = ((int)myReader["Type"]);
					tempAvatarList.Add(new Tuple<int, int>(avaID, type));
				}

				myReader.Close();
				return tempAvatarList;
			}
			else
				myReader.Close();
			return null;
		}

		public int GetGoldCost(string type, int id)
		{
			SqlDataReader myReader = null;
			SqlCommand myCommand = new SqlCommand(string.Format(
				"SELECT TOP 1 GoldCost " +
				"FROM dbo.{0} " +
				"WHERE ID = '{1}'", type, id), sqlConn);
			myReader = myCommand.ExecuteReader();
			myReader.Read();
			int goldCost = (int)myReader["GoldCost"];
			myReader.Close();
			return goldCost;
		}

		public void PurchaseItem(string type, int itemID, int playerID, int playerGold)
		{
			SqlCommand myCommand = new SqlCommand(string.Format(
				"INSERT INTO dbo.Character{0} " +				
				"Values ('{1}', '{2}', '{3}')", type, playerID, itemID), sqlConn);
			myCommand.ExecuteNonQuery();

			myCommand = new SqlCommand(string.Format(
				"UPDATE dbo.CharacterInfo " +
				"SET Gold = {0} " +
				"WHERE CharID = {1}", playerGold, playerID), sqlConn);
			myCommand.ExecuteNonQuery();
		}

		public void CreateNewPlayer(String userName, String pass)
		{
			int charId = 0;

			SqlCommand myCommand = new SqlCommand(string.Format( 
				"INSERT INTO dbo.CharacterInfo (CharName, Pass, Rank, Wins, Losses, CharTankID, CharSkinID, CharAvaHeadID, CharAvaChestID, CharAvaShoulderID, Gold)" +
				"Values ('{0}', '{1}', 0, 0, 0, 0, 0, 0, 0, 0, 0)", userName, pass), sqlConn);
			myCommand.ExecuteNonQuery();

			//Gets newly created ID for character from CharacterInfo
			SqlDataReader myReader = null;
			myCommand = new SqlCommand(string.Format(
				"SELECT CharID " +
				"FROM dbo.CharacterInfo " +
				"WHERE CharName = '{0}'", userName), sqlConn);
			myReader = myCommand.ExecuteReader();
			myReader.Read();
			charId = (int)myReader["CharID"];
            myReader.Close();			

			if(charId > 0)
			{
                //TODO: Fix comment
				//Add entry to character status table and set status = 0 (offline) plus other stuff
				myCommand = new SqlCommand(string.Format(
					"INSERT INTO dbo.CharacterStatuses (CharID, Status) " +
					"Values ('{0}', 0) " +
					"INSERT INTO dbo.CharacterTanks (CharID, TankID, Rank, Shot1, Shot2, Shot3, Item1, Item2, Item3) " +
					"Values ('{0}', 1, 1, 1, null, null, null, null, null) " +
                    "INSERT INTO dbo.CharacterSkins (CharID, SkinID) " +
                    "Values ('{0}', 1) " +
					"INSERT INTO dbo.CharacterAvatarHead (CharID, AvatarID) " +
                    "Values ('{0}', 0) " +
					"INSERT INTO dbo.CharacterAvatarShoulder (CharID, AvatarID) " +
                    "Values ('{0}', 0) " +
					"INSERT INTO dbo.CharacterAvatarChest (CharID, AvatarID) " +
                    "Values ('{0}', 0) ", charId), sqlConn);
				myCommand.ExecuteNonQuery();

                myCommand = new SqlCommand(string.Format(
                    "SELECT TOP 1 CT.ID As TankID, " +
	                "CAH.ID As CharAvaHeadID, " +
	                "CAS.ID As CharAvaShoulderID, " +
	                "CAC.ID As CharAvaChestID, " +
	                "CS.ID As CharSkinID " +
                    "FROM CharacterInfo CI " +
                    "INNER JOIN CharacterTanks CT on CI.CharID = CT.CharID " +
                    "INNER JOIN CharacterAvatarHead CAH on CI.CharID = CAH.CharID " +
                    "INNER JOIN CharacterAvatarShoulder CAS on CI.CharID = CAS.CharID " +
                    "INNER JOIN CharacterAvatarChest CAC on CI.CharID = CAC.CharID " +
                    "INNER JOIN CharacterSkins CS on CI.CharID = CS.CharID " +
                    "WHERE CI.CharID = {0}", charId), sqlConn);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                int tankId = (int)myReader["TankID"];
                int charAvaHeadId = (int)myReader["CharAvaHeadID"];
                int charAvaShoulderId = (int)myReader["CharAvaShoulderID"];
                int charAvaChestId = (int)myReader["CharAvaChestID"];
                int CharSkinId = (int)myReader["CharSkinID"];
                myReader.Close();
				
                myCommand = new SqlCommand(string.Format(
                    "UPDATE dbo.CharacterInfo " +
                    "SET CharTankID = {0}, " +
                    "CharSkinID = {1}, " +
                    "CharAvaHeadID = {2}, " +
                    "CharAvaShoulderID = {3}, " +
                    "CharAvaChestID = {4} " +
                    "WHERE CharID = {5}", tankId, CharSkinId, charAvaHeadId,
                    charAvaShoulderId, charAvaChestId, charId), sqlConn);
                myCommand.ExecuteNonQuery();
			}		
		}

		public void ChangePlayerStatus(int charId, int status)
		{
			SqlCommand myCommand = new SqlCommand(string.Format(
				"UPDATE dbo.CharacterStatuses "+
				"SET Status = {0} "+
				"WHERE CharID = {1}", status, charId), sqlConn);
			myCommand.ExecuteNonQuery();
		}

		public void AddNewBuddy(int charID, int buddyID)
		{
			SqlCommand myCommand = new SqlCommand(string.Format(
				"INSERT INTO dbo.Buddies "+
				"Values ('{0}', '{1}')", charID, buddyID), sqlConn);
			myCommand.ExecuteNonQuery();
		}

		public List<string> SelectBuddiesByCharID(int charID)
		{
			List<string> buddyList = new List<string>();

			SqlDataReader myReader = null;
			SqlCommand myCommand = new SqlCommand(string.Format("SELECT CI.CharName FROM dbo.Buddies B " +
				"INNER JOIN dbo.CharacterInfo CI ON B.BuddyID = CI.CharID " +
				"INNER JOIN dbo.CharacterStatuses CS ON B.BuddyID = CS.CharID " +
				"WHERE B.CharID = {0} AND CS.Status = 1", charID), sqlConn);
			myReader = myCommand.ExecuteReader();
			if (myReader.HasRows)
			{
				while (myReader.Read())
				{
					buddyList.Add(((string)myReader["CharName"]).Trim());
				}
			}
			myReader.Close();
			return buddyList;
		}

        public List<int> SelectOnlineBuddiesByBuddyID(int buddyID)
        {
            List<int> buddyList = new List<int>();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(string.Format(
				"SELECT B.CharID FROM dbo.Buddies B " +
                "INNER JOIN dbo.CharacterStatuses CS ON B.CharID = CS.CharID " +
                "WHERE B.BuddyID = {0} AND CS.Status = 1", buddyID), sqlConn);
            myReader = myCommand.ExecuteReader();
            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    buddyList.Add((int)myReader["CharID"]);
                }
            }
            myReader.Close();
            return buddyList;
        }

		public bool CheckBuddyExist(int charID, int buddyID)
		{
			SqlDataReader myReader = null;
			SqlCommand myCommand = new SqlCommand(string.Format(
				"SELECT B.CharID FROM dbo.Buddies B " +
				"WHERE B.CharID = {0} AND B.BuddyID = {1}", charID, buddyID), sqlConn);
			myReader = myCommand.ExecuteReader();
			if (myReader.HasRows)
			{
				myReader.Close();
				return true;
			}
			else
			{
				myReader.Close();
				return false;
			}
		}

		public void DisconnectAllPlayers()
		{
			SqlCommand myCommand = new SqlCommand("UPDATE dbo.CharacterStatuses SET Status = 0 WHERE 1=1", sqlConn);
			myCommand.ExecuteNonQuery();
		}
	}
}
