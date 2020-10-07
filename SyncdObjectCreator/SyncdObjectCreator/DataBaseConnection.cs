using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SyncdObjectCreator
{
	class DataBaseConnection
	{
		public SqlConnection sqlConn;

		public DataBaseConnection()
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
			sqlConn.Open();
		}

		public List<GameObject> LoadObjectList(string ObjectName)
		{
			List<GameObject> gOList = new List<GameObject>();

			SqlDataReader myReader = null;
			SqlCommand myCommand = new SqlCommand(string.Format(
				"SELECT * " +
				"FROM dbo.{0} ",ObjectName), sqlConn);
			myReader = myCommand.ExecuteReader();
			if (myReader.HasRows)
			{
				while (myReader.Read())
				{
					GameObject gO = new GameObject();
					gO.ID = (int)myReader["ID"];
					//gO.Type = (int)myReader["Type"];
					if (!DBNull.Value.Equals(myReader["Name"]))
					{
						gO.Name = (string)myReader["Name"];
						gO.Name = gO.Name.Trim();
					}
					else
						gO.Name = "";
					if (!DBNull.Value.Equals(myReader["FilePath"]))
					{
						gO.FilePath = (string)myReader["FilePath"];
						gO.FilePath = gO.FilePath.Trim();
					}
					else
						gO.FilePath = "";
					gO.GoldCost = (int)myReader["GoldCost"];
					gO.Attack = (int)myReader["Attack"];
					gO.Defense = (int)myReader["Defense"];
					gO.Health = (int)myReader["Health"];
					gO.Shield = (int)myReader["Shield"];
					gO.Move = (int)myReader["Move"];
					gO.Energy = (int)myReader["Energy"];
					gO.Popularity = (int)myReader["Popularity"];
					gOList.Add(gO);
				}
			}				
			myReader.Close();

			return gOList;
		}

		public void SaveObjectList(List<GameObject> objectList, string FileName)
		{
			SqlCommand myCommand = new SqlCommand(string.Format(
				"TRUNCATE TABLE dbo.{0}", FileName), sqlConn);
			myCommand.ExecuteNonQuery();

			if (FileName.Equals("AvatarHead") || FileName.Equals("AvatarShoulder") || FileName.Equals("AvatarChest"))
			{
                myCommand = new SqlCommand(string.Format(
                "DBCC CHECKIDENT ({0}, reseed, 0)", FileName), sqlConn);
                myCommand.ExecuteNonQuery();
            }
			else
			{

                myCommand = new SqlCommand(string.Format(
                "DBCC CHECKIDENT ({0}, reseed, 1)", FileName), sqlConn);
                myCommand.ExecuteNonQuery();
            }			

			foreach (GameObject gO in objectList)
			{
				string s = string.Format(
				"SET IDENTITY_INSERT {0} ON  INSERT INTO {0} ([ID], [Name], [FilePath], [GoldCost], [Attack], [Defense], [Health], [Shield], [Move], [Energy], [Popularity]) VALUES({1}, '{2}', '{3}', {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11})", 
				FileName,gO.ID, gO.Name, gO.FilePath, gO.GoldCost, gO.Attack, gO.Defense, gO.Health, gO.Shield, gO.Move, gO.Energy, gO.Popularity);				
				myCommand = new SqlCommand(s, sqlConn);
				myCommand.ExecuteNonQuery();
			}
		}
	}
}
