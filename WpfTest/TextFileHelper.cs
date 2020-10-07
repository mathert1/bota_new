﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MSCommon;

namespace WpfTest
{
	public static class TextFileHelper
	{
		public static string GetObjNameByID(int ID, string ObjectType)
		{			
			TextReader tr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + ObjectType + ".txt");
			string s = "";
			while ((s = tr.ReadLine()) != null)
			{
				string[] words = s.Split('|');
				if (Convert.ToInt32(words[0]) == ID)
				{
					return words[2];
				}
			}
			tr.Close();
			return "";
		}

		public static UpgradeObject GetUpgradeObjectFromTextFile(int ID, string ObjectType)
		{
			UpgradeObject tempObj = new UpgradeObject();
			TextReader tr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + ObjectType + ".txt");
			string s = "";
			while ((s = tr.ReadLine()) != null)
			{
				string[] words = s.Split('|');
				if (Convert.ToInt32(words[0]) == ID)
				{
					tempObj.ID = Convert.ToInt32(words[0]);
					tempObj.Type = Convert.ToInt32(words[1]);
					tempObj.Name = words[2];
					tempObj.FilePath = words[3];
					tempObj.GoldCost = Convert.ToInt32(words[4]);
					tempObj.Attack = Convert.ToInt32(words[5]);
					tempObj.Defense = Convert.ToInt32(words[6]);
					tempObj.Health = Convert.ToInt32(words[7]);
					tempObj.Shield = Convert.ToInt32(words[8]);
					tempObj.Move = Convert.ToInt32(words[9]);
					tempObj.Energy = Convert.ToInt32(words[10]);
					tempObj.Popularity = Convert.ToInt32(words[11]);
					break;
				}
			}
			tr.Close();
			return tempObj;
		}

		public static String GetFilePathByIDandType(int ID, string ObjectType)
		{
			string fPath = "";
			TextReader tr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + ObjectType + ".txt");
			string s = "";
			while ((s = tr.ReadLine()) != null)
			{
				string[] words = s.Split('|');
				if (Convert.ToInt32(words[0]) == ID)
				{
					fPath = words[3];
					break;
				}
			}
			tr.Close();
			return fPath;
		}

		public static ObservableCollection<UpgradeObject> GetUpgradeObjectListFormattedFilePathFromTextFile(string ObjectType)
		{
			String basePath = AppDomain.CurrentDomain.BaseDirectory;
			ObservableCollection<UpgradeObject> tempObjList = new ObservableCollection<UpgradeObject>();
			TextReader tr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + ObjectType + ".txt");
			string s = "";
			while ((s = tr.ReadLine()) != null)
			{
				string[] words = s.Split('|');
				UpgradeObject upgradeObj = new UpgradeObject();
				upgradeObj.ID = Convert.ToInt32(words[0]);
				upgradeObj.Type = Convert.ToInt32(words[1]);
				upgradeObj.Name = words[2];
				upgradeObj.FilePath = Path.Combine(basePath, "..\\..\\Content\\", words[3]);
				upgradeObj.GoldCost = Convert.ToInt32(words[4]);
				upgradeObj.Attack = Convert.ToInt32(words[5]);
				upgradeObj.Defense = Convert.ToInt32(words[6]);
				upgradeObj.Health = Convert.ToInt32(words[7]);
				upgradeObj.Shield = Convert.ToInt32(words[8]);
				upgradeObj.Move = Convert.ToInt32(words[9]);
				upgradeObj.Energy = Convert.ToInt32(words[10]);
				upgradeObj.Popularity = Convert.ToInt32(words[11]);
				tempObjList.Add(upgradeObj);
			}
			tr.Close();
			return tempObjList;
		}		
	}
}