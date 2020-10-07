using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Server
{
	public class Game
	{
		public int ID;
		public List<Player> playerList;
		public List<Rocket> rocketList;
		public int ChangeWind;
        public bool rocketFlying = false;
        public float playerScaling;
        public int screenWidth;// = 1920;
        public int screenHeight;// = 696;
        public bool[,] map2dArray;
        public int currentPlayerTurnID = 0;
        public Timer turnTimer;
        public Stopwatch elapsedTimer;
        public int currentElapsedTurnTime = 0;
        public bool turnExpired = false;
        public bool gameOver = false;

        private int _redLives;
        public int redLives
        {
            get { return _redLives; }
            set
            {
                _redLives = value;
                if (_redLives == 0)
                {
                    gameOver = true;//Add code that ends game (shows score screen)
                }
            }
        }
        private int _blueLives;
        public int blueLives
        {
            get { return _blueLives; }
            set
            {
                _blueLives = value;
                if (_blueLives == 0)
                {
                    gameOver = true;//Add code that ends game (shows score screen)
                }
            }
        }
		
        public Vector2 WindSpeed = new Vector2(0, 0);
		public int WindPower = 0;
		public float WindAngle = 0;

		public Game(bool[,] map, int gameID, List<Player> pList)
		{
			screenWidth = map.GetLength(0);
			screenHeight = map.GetLength(1);
			map2dArray = new bool[screenWidth, screenHeight];
			Array.Copy(map, map2dArray, map.Length);
			ID = gameID;
            playerList = pList;
            //playerList = new List<Player>(np);
			rocketList = new List<Rocket>();
            SetUpGame();
		}

		private void SetUpGame()
		{
			//playerScaling = 50.0f / 100;
			//ChangeWind = 0;
			switch (playerList.Count)
            {
                case 2:
                    redLives = 1;                    
                    break;
                case 4:
                    redLives = 3;                    
                    break;
                case 6:
                    redLives = 4;
                    break;
                case 8:
                    redLives = 5;
                    break;                    
            }
            blueLives = redLives;
			for(int i = 0; i<playerList.Count;i++)
			{
				//playerList[i].Height = 100;
				//playerList[i].Width = 100;
                if (i % 2 != 0) //if odd
                {
                    playerList[i].team = TeamTypes.Red;
                }
                else
                {
                    playerList[i].team = TeamTypes.Blue;
                }
				playerList[i].Position.X = (screenWidth+100) / (playerList.Count + 1) * (i + 1);
				playerList[i].Position.Y = 1;
				playerList[i].Radius = 40;
				playerList[i].HP = 100 + playerList[i].Health;
			}		
		}

        public void StartTurnTimer()
        {
            if (turnTimer != null)
            {
                turnTimer.Stop();
                turnTimer.Close();
                turnTimer.Dispose();
            }
                
            turnTimer = new Timer(20000);
            turnTimer.AutoReset = false;
            turnTimer.Interval = (20000);
            turnTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            turnTimer.Enabled = true;

            if (elapsedTimer == null)
                elapsedTimer = Stopwatch.StartNew();
            else
                elapsedTimer.Start();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            turnExpired = true;
        }
	}
}
