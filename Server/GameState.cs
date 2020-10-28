using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using MSCommon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Server
{
	class GameState
	{
		NetServer peer;
		Maps maps;
		public DatabaseConnection DBconn;
		public List<Game> gameList;
		int gameID;

		public GameState(NetServer p, DatabaseConnection db)
		{
			peer = p;
			DBconn = db;
			maps = new Maps();
			maps.LoadMap2dArrays();
			gameList = new List<Game>();
		}

		public void CreateGame(List<Player> pList)
		{
			Random rand = new Random();
			int map = rand.Next(0, maps.mapList.Count); //select random map
            Game g = new Game(maps.mapList[map], gameID, pList);
			//g.SetUpGame(pList);
			gameList.Add(g);			
			SendGameCreatedMessage(g, maps.GetMapName(map), maps.GetBackgroundName(map), gameID);
			gameID++;
		}

		private void SendGameCreatedMessage(Game g, string mapName, string backName, int gID)
		{
			List<NetConnection> conns = new List<NetConnection>();		
			foreach (Player p in g.playerList)
			{
				if(p.Connection != null) //in case there are bots
					conns.Add(p.Connection);
			}			

			foreach (NetConnection NC in conns)
			{
				NetOutgoingMessage StartGame = peer.CreateMessage();
				StartGame.Write((byte)MasterServerMessageType.StartGame);
				StartGame.Write(mapName);
				StartGame.Write(backName);
				StartGame.Write(gID);
                StartGame.Write(g.redLives);
                StartGame.Write(g.playerList[0].ID); //Starting player ID. Right now just defaults to first player in list since everyone starts with 0 energy
				StartGame.Write(g.playerList.Count - 1);
				foreach (Player p in g.playerList)
				{
					StartGame.Write(p.ID);
					StartGame.Write(p.Name);
                    StartGame.Write(p.Rank);
					StartGame.Write((int)p.team);					
					StartGame.Write(p.Tank);
					StartGame.Write(p.Skin);
					StartGame.Write(p.AvaHead);
					StartGame.Write(p.AvaShoulder);
					StartGame.Write(p.AvaChest);
                    StartGame.Write(100 + (int)p.Health);
				}
				peer.SendMessage(StartGame, NC, NetDeliveryMethod.ReliableOrdered, 0);
			}            
		}

        public void GameLoaded(NetIncomingMessage msg)
        {
            
            bool everyoneLoaded = true;
            int gameID = msg.ReadInt32();
            foreach (Game g in gameList)
            {
                if (g.ID == gameID)
                {
                    foreach (Player p in g.playerList)
                    {
                        if (p.Connection == msg.SenderConnection)
                        {
                            p.gameLoaded = true;
                        }
                        else
                        {
                            if (p.gameLoaded == false)
                                everyoneLoaded = false;
                        }
                    }                    
                }
                if (everyoneLoaded)
                    changeTurn(g);
            }
        }

		public void Move(NetIncomingMessage msg)
		{
			int gameID = msg.ReadInt32();
			int moveX = msg.ReadInt32();
			foreach (Game g in gameList)
			{
				if (g.ID == gameID)
				{
					foreach (Player p in g.playerList)
					{
						if (p.Connection == msg.SenderConnection && p.ID == g.currentPlayerTurnID) 
						{
                            for (int q = 0; q < 6; q++) //This is what restricts players from moving up steep cliffs.  The higher q is, the steeper the player is allowed to climb.
							{
								if (g.map2dArray[(int)p.Position.X + moveX, (int)p.Position.Y - q] == false)
								{
									p.Position.X += moveX;
									p.Position.Y -= q;
									CalcPlayerMid(g, p);
									break;
								}
							}
							CheckPlayerOffScreen(p, g);
						}
					}
					break;
				}				
			}
		}

		public void CalcPlayerMid(Game g, Player p)
		{
			int xLeft = (int)p.Position.X - 15;
			int xRight = (int)p.Position.X + 15;
			int yPos = (int)p.Position.Y;
			int yMax = 25;
			if (yPos < yMax)
				yMax = yPos;
			else if (yPos > (g.screenHeight - yMax))
				yMax = g.screenHeight - yPos - 1;
			float leftPoint = yMax;
			float rightPoint = yMax;

			for (int j = (yPos - yMax); j <= (yPos + yMax); j++)
			{
				if (g.map2dArray[xLeft, j])
				{
					leftPoint = yPos - j;
					break;
				}
				else if (j == (yPos + yMax) && !g.map2dArray[xLeft, j])
				{
					leftPoint = yPos - j;
					break;
				}
			}

			for (int j = (yPos - yMax); j <= (yPos + yMax); j++)
			{
				if (g.map2dArray[xRight, j])
				{
					rightPoint = yPos - j;
					break;
				}
				else if (j == (yPos + yMax) && !g.map2dArray[xRight, j])
				{
					rightPoint = yPos - j;
					break;
				}
			}
			float slope = (leftPoint - rightPoint) / 30;
			p.Rotate = (float)Math.Atan(slope);

			p.PlayerMid = NewPoint(p.Position, new Vector2(p.Position.X, p.Position.Y - 25), p.Rotate);
		}

		private Vector2 NewPoint(Vector2 point, Vector2 oldpoint, float angle)
		{
			Matrix myRotationMatrix = Matrix.CreateRotationZ(angle);
			Vector2 rotatedVector =
					Vector2.Transform(oldpoint - point, myRotationMatrix);
			return (rotatedVector + point);
		}

		private void CheckPlayerOffScreen(Player p, Game g)
		{
			//Not Finished
		}

		public void CreateRocket(NetIncomingMessage msg)
		{
            bool createRocket = false;
			Console.WriteLine("Rocket Fired");
			List<NetConnection> conns = new List<NetConnection>();
			int ID = msg.ReadInt32();
			int pow = msg.ReadInt32();
			float ang = msg.ReadFloat();
			Rocket r;
			NetOutgoingMessage oMessage = peer.CreateMessage();
			oMessage.Write((byte)MasterServerMessageType.CreateRocket);
			foreach (Game g in gameList)
			{
				if (g.ID == ID)
				{
					g.rocketFlying = true;
					foreach (Player p in g.playerList)
					{
						conns.Add(p.Connection);
                        if (p.Connection == msg.SenderConnection && p.ID == g.currentPlayerTurnID)
						{
                            createRocket = true;
							Vector2 rockPos = p.PlayerMid;
							r = new Rocket(rockPos, pow, ang);
							r.player = p.ID;
                            p.Energy += 100; //+ elapsedTurnTime
							g.rocketList.Add(r);
							oMessage.Write((int)r.Position.X);
							oMessage.Write((int)r.Position.Y);
						}
					}					
				}
			}
			oMessage.Write(pow);
			oMessage.Write(ang);
            if (createRocket)
                peer.SendMessage(oMessage, conns, NetDeliveryMethod.ReliableOrdered, 0);
            else
                oMessage = null;	
		}

		public Vector2 GetRandomLocation(Game g)
		{
			Random randomizer = new Random();
			return new Vector2(randomizer.Next(g.screenWidth), 5);
		}

		public void Update()
		{
			foreach (Game g in gameList)
			{
                if (g.gameOver)
                {
                    if (g.blueLives <= 0 && g.redLives > 0)
                        EndGame(g, TeamTypes.Red);
                    else if (g.redLives <= 0 && g.blueLives > 0)
                        EndGame(g, TeamTypes.Blue);
                    else
                        EndGame(g, TeamTypes.Both);
                    gameList.Remove(g);
                    break;
                }
				List<NetConnection> connsRoom = new List<NetConnection>();
				foreach (Player p in g.playerList)
				{
					if(p.Connection!=null)
						connsRoom.Add(p.Connection);
					//Make players fall when not touching map
					if (g.map2dArray[(int)p.Position.X, (int)p.Position.Y] == false)
					{
						p.Position.Y += 3;
						CalcPlayerMid(g, p);
					}

					//Handle when players fall off bottom of map
					if (p.Position.Y > g.screenHeight - 4)
					{
                        killPlayer(g, p, p);
						p.Position = GetRandomLocation(g);
					}
				}
				if (g.ChangeWind >= g.playerList.Count * 2)
				{
					g.ChangeWind = 0;
					List<NetConnection> connects = new List<NetConnection>();
					foreach (Player p in g.playerList)
						connects.Add(p.Connection);
					CalcWind(g);
					NetOutgoingMessage nOutM = peer.CreateMessage();
					nOutM.Write((byte)MasterServerMessageType.ChangeWind);
					nOutM.Write(g.WindAngle);
					nOutM.Write(g.WindPower);
					nOutM.Write(g.WindSpeed.X);
					nOutM.Write(g.WindSpeed.Y);
					peer.SendMessage(nOutM, connects, NetDeliveryMethod.ReliableOrdered, 0);
				}
				if (g.rocketFlying)
				{
					for (int i = 0; i < g.rocketList.Count; i++)
					{
						g.rocketList[i].UpdateRocket(g);
					}
					CheckRocketCollision(g);
				}
                if (g.turnExpired)
                {
                    changeTurn(g);
                }

				if (connsRoom.Count > 0)
				{
					//Console.WriteLine("Players Positions Sent for game: " + aGame.Name);
					NetOutgoingMessage omLobby = peer.CreateMessage();
					omLobby.Write((byte)MasterServerMessageType.Move);
					omLobby.Write(g.playerList.Count);
					for (int i = 0; i < g.playerList.Count; i++)
					{
						int asdfx = (int)g.playerList[i].Position.X;
						int asdfy = (int)g.playerList[i].Position.Y;
						omLobby.Write(asdfx);
						omLobby.Write(asdfy);
						omLobby.Write((int)g.playerList[i].PlayerMid.X);
						omLobby.Write((int)g.playerList[i].PlayerMid.Y);
						omLobby.Write(g.playerList[i].Rotate);
                        omLobby.Write(g.playerList[i].Energy); //Probably don't need to send energy updates with player position updates.
					}
					peer.SendMessage(omLobby, connsRoom, NetDeliveryMethod.ReliableOrdered, 0);
				}

			}
		}

		public void QuitGame(NetIncomingMessage msg)
		{
			List<NetConnection> conns = new List<NetConnection>();
			NetOutgoingMessage quitG = peer.CreateMessage();
			quitG.Write((byte)MasterServerMessageType.QuitGame);
			int ID = msg.ReadInt32();
			foreach (Game g in gameList)
			{
				if (g.ID == ID)
				{
					for (int j = 0; j < g.playerList.Count; j++)
					{
						conns.Add(g.playerList[j].Connection);
						if (g.playerList[j].Connection == msg.SenderConnection)
						{
							quitG.Write(j);
						}
					}
					gameList.Remove(g);
					break;
				}
			}
			peer.SendMessage(quitG, conns, NetDeliveryMethod.ReliableOrdered, 0);
		}

		private void CalcWind(Game g)
		{
			Random randomizer = new Random();
			g.WindPower = randomizer.Next(25) + 1;
			g.WindAngle = randomizer.Next(360);
			g.WindSpeed.X = (float)Math.Sin(MathHelper.ToRadians(g.WindAngle)) * g.WindPower;
			g.WindSpeed.Y = (float)Math.Cos(MathHelper.ToRadians(g.WindAngle)) * g.WindPower * -1;
			g.WindSpeed /= 500.0f;
		}

		private void CheckRocketCollision(Game g)
		{
            int rocketNum = 0;
            for (int i = 0; i < g.rocketList.Count; i++)
			{
                //If rocket off screen. 
                if (g.rocketList[i].Position.X <= 0 || g.rocketList[i].Position.X >= g.screenWidth || g.rocketList[i].Position.Y >= g.screenHeight)
				{
                    g.rocketList.Remove(g.rocketList[i]);
					SendRocketCollisionScreen(rocketNum, g);
					continue;
				}
				foreach (Player p in g.playerList)
				{
                    //If this is the player that shot the rocket. Should change to only skip player if rocket has not left his bounding circle yet.
					if (p.ID == g.rocketList[i].player)
					{
						continue;
					}
					if (BoundingCircle((int)g.rocketList[i].Position.X, (int)g.rocketList[i].Position.Y, g.rocketList[i].radius, (int)p.PlayerMid.X, (int)p.PlayerMid.Y, p.Radius))
					{
						g.ChangeWind++;
                        float dmg = CalcDmg(g, g.rocketList[i], p);
						Console.WriteLine("Damage: " + dmg);
						p.HP -= dmg;
                        damagePlayer(g, p, (int)dmg);
						if (p.HP <= 0)
						{
                            foreach (Player pl in g.playerList)
                            {
                                if(pl.ID == g.rocketList[i].player) //to determine which player gets credit for kill
                                    killPlayer(g, p, pl);
                            }
                            
							p.HP = 100 + p.Health;
							//if (g.rocketList[i].player == 0)
							//    g.playerList[0].Score++;
							//else
							//    g.playerList[1].Score++;
							//UpdateScore(g);
						}
						int x = (int)g.rocketList[i].Position.X;
						int y = (int)g.rocketList[i].Position.Y;
                        SendRocketCollisionPlayer(rocketNum, g, p, g.rocketList[i]);
                        g.rocketList.Remove(g.rocketList[i]);
						if (g.rocketList.Count == 0)
							g.rocketFlying = false;
						
						break;
					}
				}				
				if (g.rocketFlying && g.rocketList[i].Position.Y > 0) //Why check r.Position.Y > 0??
				{
					Vector2 rocketPos = g.rocketList[i].Position;
					//if (rocketPos.Y > ag.TerrainContourTop[(int)rocketPos.X])
					if (g.map2dArray[(int)rocketPos.X, (int)rocketPos.Y]) //if it collides with terrain
					{
						g.ChangeWind++;
						//Console.WriteLine("BOOOOM! on terrain");
						int tx = (int)rocketPos.X;
						int ty = (int)rocketPos.Y;
						AddCrater(g, tx, ty, 50);
                        g.rocketList.Remove(g.rocketList[i]);
						if (g.rocketList.Count == 0)
							g.rocketFlying = false;
						SendRocketCollisionLand(rocketNum, tx, ty, 50, g);
					}
				}
                rocketNum++;
			}
		}

        private void SendRocketCollisionScreen(int i, Game g)
        {
            List<NetConnection> conns = new List<NetConnection>();
            foreach (Player p in g.playerList)
                conns.Add(p.Connection);

            NetOutgoingMessage outMessage = peer.CreateMessage();
            outMessage.Write((byte)MasterServerMessageType.RocketCollisionScreen);
            outMessage.Write(i);

            peer.SendMessage(outMessage, conns, NetDeliveryMethod.ReliableOrdered, 0);

            changeTurn(g);
        }

		private void SendRocketCollisionPlayer(int i, Game g, Player coliPlayer, Rocket r)
		{
            Player rPlayer = new Player();
            List<NetConnection> conns = new List<NetConnection>();
            foreach (Player p in g.playerList)
            {
                conns.Add(p.Connection);
                if (p.ID == r.player)
                    rPlayer = p;
            }
            
            NetOutgoingMessage outMessage = peer.CreateMessage();
			outMessage.Write((byte)MasterServerMessageType.RocketCollisionPlayer);
			outMessage.Write(i);

			peer.SendMessage(outMessage, conns, NetDeliveryMethod.ReliableOrdered, 0);

            if (r.player == coliPlayer.ID)
                givePlayerMoney(g, rPlayer, -25); //-25 gold for hitting self
            else if (rPlayer.team == coliPlayer.team)
                givePlayerMoney(g, rPlayer, -25); //-25 gold for hitting team.  Could use this to check if hit self. Still -25
            else
                givePlayerMoney(g, rPlayer, 25); //25 gold for hitting enemy	

            changeTurn(g);
		}

		private void SendRocketCollisionLand(int rocketNum, int posX, int posY, int radius, Game g)
		{
			List<NetConnection> conns = new List<NetConnection>();
			foreach (Player p in g.playerList)
				conns.Add(p.Connection);

			NetOutgoingMessage outMessage = peer.CreateMessage();
			outMessage.Write((byte)MasterServerMessageType.RocketCollisionLand);
			outMessage.Write(rocketNum);
			outMessage.Write(posX);
			outMessage.Write(posY);
			outMessage.Write(radius);

			peer.SendMessage(outMessage, conns, NetDeliveryMethod.ReliableOrdered, 0);

            changeTurn(g);
		}

		private void AddCrater(Game g, int x, int y, int r)
		{
			for (int i = (x - r); i <= (x + r); i++)
			{
				for (int j = (y - r); j <= (y + r); j++)
				{
					if (x < 0 || j < 0)
						continue;
					//if(Program.radiatin2dArray[x,y]) this makes it more efficient, but for some reason it only will draw left half of the crater on server
					{
						double d = Math.Sqrt(Math.Pow((x - i), 2) + Math.Pow((y - j), 2));
						d = Math.Abs(d);
						if (d <= r)
							g.map2dArray[i, j] = false;
					}
				}
			}
		}

		private bool BoundingCircle(int x1, int y1, int radius1, int x2, int y2, int radius2)
		{
			Vector2 V1 = new Vector2(x1, y1);
			Vector2 V2 = new Vector2(x2, y2);

			Vector2 Distance = V1 - V2;

			if (Distance.Length() < radius1 + radius2)
				return true;

			return false;
		}

		private float CalcDmg(Game g, Rocket r, Player p)
		{
			bool cont = true;
			Vector2 oldDist;
			Vector2 Dist = new Vector2(1000, 1000);
			while (cont)
			{
				oldDist = Dist;
				Dist = r.Position - p.PlayerMid;
				r.UpdateRocket(g);
				if (oldDist.Length() < Dist.Length())
				{
					cont = false;
					return 50 - oldDist.Length(); //max possible dmg is 50
				}
			}
			return 0;
		}

        private void killPlayer(Game g, Player killedPlayer, Player playerKiller)
        {
            if (killedPlayer.team == TeamTypes.Red)
                g.redLives--;
            else
                g.blueLives--;

            if (killedPlayer.ID == playerKiller.ID) 
                givePlayerMoney(g, killedPlayer, -100); //player loses 100 gold for killing self. could just check team for this as well if player is deducted 100 for killing self and teammate
            else if (killedPlayer.team != playerKiller.team)
                givePlayerMoney(g, playerKiller, 100); //player receives 100 gold for killing opponent
            else
                givePlayerMoney(g, playerKiller, -100);  //player loses 100 gold for killing teammate

            List<NetConnection> conns = new List<NetConnection>();
            foreach (Player p in g.playerList)
                conns.Add(p.Connection);

            NetOutgoingMessage outMessage = peer.CreateMessage();
            outMessage.Write((byte)MasterServerMessageType.KillPlayer);
            outMessage.Write(killedPlayer.ID);
            outMessage.Write(playerKiller.ID);
            outMessage.Write(g.redLives);
            outMessage.Write(g.blueLives);

            peer.SendMessage(outMessage, conns, NetDeliveryMethod.ReliableOrdered, 0);            
        }

        private void givePlayerMoney(Game g, Player moneyPlayer, int money)
        {
            moneyPlayer.GoldEarned += money;

            List<NetConnection> conns = new List<NetConnection>();
            foreach (Player p in g.playerList)
                conns.Add(p.Connection);

            NetOutgoingMessage outMessage = peer.CreateMessage();
            outMessage.Write((byte)MasterServerMessageType.GivePlayerMoney);
            outMessage.Write(moneyPlayer.ID);
            outMessage.Write(money);
            peer.SendMessage(outMessage, conns, NetDeliveryMethod.ReliableOrdered, 0);
        }

        private void damagePlayer(Game g, Player damagedPlayer, int dmg)
        {
            List<NetConnection> conns = new List<NetConnection>();
            foreach (Player p in g.playerList)
                conns.Add(p.Connection);

            NetOutgoingMessage outMessage = peer.CreateMessage();
            outMessage.Write((byte)MasterServerMessageType.DamagePlayer);
            outMessage.Write(damagedPlayer.ID);
            outMessage.Write(dmg);

            peer.SendMessage(outMessage, conns, NetDeliveryMethod.ReliableOrdered, 0);
        }

        private void changeTurn(Game g)
        {
            g.turnExpired = false;
            int reducedEnergy = 0;
            int lowestEnergyPlayerID = 0;
            int lowestEnergy = 999999;
            foreach (Player p in g.playerList)
            {
                if (p.ID == g.currentPlayerTurnID)
                {                    
                    reducedEnergy = p.Energy; //should reduce the energy of everyone by what the new turn player's energy is no last turn player's energy
                    p.Energy += (int)(g.elapsedTimer.ElapsedMilliseconds / 1000) * 10;
                    g.elapsedTimer.Reset();
                    break;
                }
            }

            foreach(Player p in g.playerList)
            {
                if (p.Energy < lowestEnergy)
                {
                    lowestEnergy = p.Energy;
                    lowestEnergyPlayerID = p.ID;
                }
                p.Energy -= reducedEnergy;
            }

            g.currentPlayerTurnID = lowestEnergyPlayerID;

            List<NetConnection> conns = new List<NetConnection>();
            foreach (Player p in g.playerList)
            {
                conns.Add(p.Connection);
            }

            NetOutgoingMessage outMessage = peer.CreateMessage();
            outMessage.Write((byte)MasterServerMessageType.ChangeTurn);
            outMessage.Write(g.currentPlayerTurnID);
            peer.SendMessage(outMessage, conns, NetDeliveryMethod.ReliableOrdered, 0);
            g.StartTurnTimer();
        }

        private void EndGame(Game g, TeamTypes winningTeam) //This could be cleaned up a bit
        {
            List<NetConnection> connsRed = new List<NetConnection>();
            List<NetConnection> connsBlue = new List<NetConnection>();
            foreach (Player p in g.playerList)
            {
                if (p.team == TeamTypes.Red)
                {
                    connsRed.Add(p.Connection);
                    if (winningTeam == TeamTypes.Red)
                    {
                        p.GoldEarned += 100;
                        DBconn.UpdateCharacterStats(p, 1, 0);
                    }
                    else
                        DBconn.UpdateCharacterStats(p, 0, 1);
                    p.GoldEarned = 0;
                }
                else
                {
                    connsBlue.Add(p.Connection);
                    if (winningTeam == TeamTypes.Blue)
                    {
                        p.GoldEarned += 100;
                        DBconn.UpdateCharacterStats(p, 1, 0);
                    }
                    else
                        DBconn.UpdateCharacterStats(p, 0, 1);
                    p.GoldEarned = 0;
                }
            }

            NetOutgoingMessage outMessageRed = peer.CreateMessage();
            outMessageRed.Write((byte)MasterServerMessageType.EndGame);
            if (winningTeam == TeamTypes.Red)
                outMessageRed.Write(true);
            else //handles if blue won or both teams lost
                outMessageRed.Write(false);
            peer.SendMessage(outMessageRed, connsRed, NetDeliveryMethod.ReliableOrdered, 0);

            NetOutgoingMessage outMessageBlue = peer.CreateMessage();
            outMessageBlue.Write((byte)MasterServerMessageType.EndGame);
            if (winningTeam == TeamTypes.Blue)
                outMessageBlue.Write(true);
            else //handles if red won or both teams lost
                outMessageBlue.Write(false);
            peer.SendMessage(outMessageBlue, connsBlue, NetDeliveryMethod.ReliableOrdered, 0);
        }
	}
}
