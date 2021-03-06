﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Forms;
using MSCommon;
using Lidgren.Network;

namespace WpfTest
{
	public class Program
	{
		//public WpfTest.App app;

		ScreenManager screenMan;
		
		static void Main()
		{
			Program program = new Program();
		}
		public Program()
		{
			ServerConnection.Connect();
			screenMan = new ScreenManager();
			screenMan.OpenLoginWindow();

			Application.Idle += new EventHandler(AppIdle);
			Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
			Application.Run();
		}		
		
		public void AppIdle(object sender, EventArgs e)
		{
			while (NativeMethods.AppStillIdle)
			{
				NetIncomingMessage inc;
				while ((inc = ServerConnection.m_client.ReadMessage()) != null)
				{
					switch (inc.MessageType)
					{
						case NetIncomingMessageType.VerboseDebugMessage:
						case NetIncomingMessageType.DebugMessage:
						case NetIncomingMessageType.WarningMessage:
						case NetIncomingMessageType.ErrorMessage:
							inc.ReadString();
							break;
						case NetIncomingMessageType.UnconnectedData:
							break;
						case NetIncomingMessageType.Data:
							switch ((MasterServerMessageType)inc.ReadByte())
							{
								case MasterServerMessageType.RequestLogin:
									bool login = inc.ReadBoolean();
									if (login)
									{
										screenMan.CloseLoginWindow();
										
										screenMan.OpenMainScreen();
                                        //Thread.Sleep(500); This was to stop buddylist from trying to be updated before screen was loaded.
									}
									else
									{
										MessageBox.Show("Login Failed.");
									}
									break;		
								case MasterServerMessageType.CreateAccount:
									bool success = inc.ReadBoolean();
									if (success)
									{
										MessageBox.Show("Account Successfully Created.", "Account");
									}
									else
									{
										MessageBox.Show("Invalid Username.", "Oops!");
									}
									break;
								case MasterServerMessageType.SendMessage:
									string chat = inc.ReadString();
									string name = inc.ReadString();
									string msg = inc.ReadString();

									screenMan.AddMessage(chat, name, msg);
									break;
								case MasterServerMessageType.SendBuddyList:
									int buddyListCount = inc.ReadInt32();
									List<string> buddyList = new List<string>();
									for (int i = 1; i <= buddyListCount; i++)
									{
										buddyList.Add(inc.ReadString());
									}
									screenMan.RefreshBuddyList(buddyList);
									break;
								case MasterServerMessageType.AddBuddy:
									bool added = inc.ReadBoolean();
									if (added)									
										MessageBox.Show("Buddy added successfully.", "Success!");									
									else
										MessageBox.Show("Error adding buddy.", "Failure!");	
									break;
								case MasterServerMessageType.CharacterInfo:
                                    Player p = new Player();
                                    p.ID = inc.ReadInt32();
                                    p.UserName = inc.ReadString();
									p.Rank = inc.ReadInt32();
                                    p.Wins = inc.ReadInt32();
                                    p.Losses = inc.ReadInt32();
									p.Tank = inc.ReadInt32();
									p.Skin = inc.ReadInt32();
									p.AvaHead = inc.ReadInt32();
									p.AvaShoulder = inc.ReadInt32();
									p.AvaChest = inc.ReadInt32();									
									p.Gold = inc.ReadInt32();																	
									screenMan.SetCharacterInfo(p);
									break;
								case MasterServerMessageType.RequestTanks:
									int tankListCount = inc.ReadInt32();
									List<CharTank> tankList = new List<CharTank>();
									for (int i = 1; i <= tankListCount; i++)
									{
										CharTank tempTank = new CharTank();
										tempTank.ID = inc.ReadInt32();
										tempTank.Rank = inc.ReadInt32();
										tempTank.shot1 = inc.ReadInt32();
										tempTank.shot2 = inc.ReadInt32();
										tempTank.shot3 = inc.ReadInt32();
										tempTank.item1 = inc.ReadInt32();
										tempTank.item2 = inc.ReadInt32();
										tempTank.item3 = inc.ReadInt32();
										tankList.Add(tempTank);
									}
									screenMan.SetTankList(tankList);
									break;
								case MasterServerMessageType.RequestAvatar:
									int avaListCount = inc.ReadInt32();
									List<int> avaList = new List<int>();
									for (int i = 1; i <= avaListCount; i++)
									{
										int avaID = inc.ReadInt32();
										int type = inc.ReadInt32();
										avaList.Add(avaID);
										avaList.Add(type);
									}
									screenMan.SetAvatarList(avaList);
									break;
								case MasterServerMessageType.PurchaseItem:
									bool purchased = inc.ReadBoolean();
									if (purchased)
									{
										MessageBox.Show("Item Purchased!", "WOO!");
										int goldCost = inc.ReadInt32();
                                        screenMan.localPlayer.Gold -= goldCost;
										//Variables.player.Gold -= goldCost;
										screenMan.SetCharacterInfo(screenMan.localPlayer); //This is terrible. Needs to be fixed!! <<<<>>>>>>
									}
									else
										MessageBox.Show("Insufficient funds.", "Boo!");
									break;
								case MasterServerMessageType.FindingGame:
									//TODO Show dialog box saying 'Searching for game' with something animated and cancel button
									screenMan.ShowGameSearchOverlay();
									break;
								case MasterServerMessageType.StartGame:		
									List<Player> pList = new List<Player>();
									string map = inc.ReadString();
									string background = inc.ReadString();
									int gameID = inc.ReadInt32();
                                    int teamLives = inc.ReadInt32();
                                    int startPlayerID = inc.ReadInt32();
									int count = inc.ReadInt32();
									for (int x = 0; x <= count; x++)
									{
										Player tempP = new Player();
										tempP.ID = inc.ReadInt32();
										tempP.UserName = inc.ReadString();
										tempP.Rank = inc.ReadInt32();
										tempP.team = inc.ReadInt32();
										tempP.Tank = inc.ReadInt32();
										tempP.Skin = inc.ReadInt32();
										tempP.AvaHead = inc.ReadInt32();
										tempP.AvaShoulder = inc.ReadInt32();
										tempP.AvaChest = inc.ReadInt32();
                                        tempP.Health = inc.ReadInt32();
										pList.Add(tempP);
									}
									screenMan.SetGamePlayerList(pList, map, background, gameID, teamLives, startPlayerID);

									screenMan.OpenGameWindow();
									break;
								case MasterServerMessageType.Move:
									int playerCount = inc.ReadInt32();
									for (int i = 0; i < playerCount; i++)
									{
                                        int playerPosX = inc.ReadInt32();
                                        int playerPosY = inc.ReadInt32();
                                        int playerMidX = inc.ReadInt32();
                                        int playerMidY = inc.ReadInt32();
                                        float rotate = inc.ReadFloat();
                                        int playerEnergy = inc.ReadInt32();
										screenMan.UpdatePlayerPositions(i, playerPosX, playerPosY, playerMidX, playerMidY, rotate, playerEnergy);										
									}
									break;
								case MasterServerMessageType.CreateRocket:
									int rocketX = inc.ReadInt32();
									int rocketY = inc.ReadInt32();
									int pow = inc.ReadInt32();
									float ang = inc.ReadFloat();
									screenMan.CreateRocket(rocketX, rocketY, pow, ang);
									break;
								case MasterServerMessageType.RocketCollisionLand:
									int colli = inc.ReadInt32();
									int collx = inc.ReadInt32();
									int colly = inc.ReadInt32();
									int collr = inc.ReadInt32();
									screenMan.RocketCollisionLand(colli, collx, colly, collr);
									break;
                                case MasterServerMessageType.RocketCollisionScreen:
                                    int rcoli = inc.ReadInt32();
                                    screenMan.RocketCollisionPlayer(rcoli); //For now can use same method as rocket collision player since they will do the same thing
                                    break;
								case MasterServerMessageType.RocketCollisionPlayer:
									int coli = inc.ReadInt32();
									screenMan.RocketCollisionPlayer(coli);
									break;
                                case MasterServerMessageType.ChangeWind:
                                    float angle = inc.ReadFloat();
                                    int power = inc.ReadInt32();
                                    float windSpeedX = inc.ReadFloat();
                                    float windSpeedY = inc.ReadFloat();
                                    screenMan.ChangeWind(angle, power, windSpeedX, windSpeedY);
                                    break;
                                case MasterServerMessageType.KillPlayer:
                                    int killedPlayerID = inc.ReadInt32();
                                    int playerKillerID = inc.ReadInt32();
                                    int redLives = inc.ReadInt32();
                                    int blueLives = inc.ReadInt32();
                                    screenMan.KillPlayer(killedPlayerID, playerKillerID, redLives, blueLives);
                                    break;
                                case MasterServerMessageType.DamagePlayer:
                                    int damagedPlayerID = inc.ReadInt32();
                                    int damage = inc.ReadInt32();
                                    screenMan.DamagePlayer(damagedPlayerID, damage);
                                    break;
                                case MasterServerMessageType.GivePlayerMoney:
                                    int moneyPlayerID = inc.ReadInt32();
                                    int money = inc.ReadInt32();
                                    screenMan.GivePlayerMoney(moneyPlayerID, money);
                                    break;
                                case MasterServerMessageType.ChangeTurn:
                                    int playerTurnID = inc.ReadInt32();
                                    screenMan.ChangeTurn(playerTurnID);
                                    break;
                                case MasterServerMessageType.EndGame:
                                    bool win = inc.ReadBoolean();
                                    screenMan.EndGame(win);
                                    break;
							}

							break;
						case NetIncomingMessageType.NatIntroductionSuccess:
							string token = inc.ReadString();
							MessageBox.Show("Nat introduction success to " + inc.SenderEndpoint + " token is: " + token);
							break;
					}                    
				}
			}
		}

		private void OnApplicationExit(object sender, EventArgs e)
		{
			ServerConnection.Disconnect();
		}
	}
}
