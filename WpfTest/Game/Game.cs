using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using MSCommon;
using Lidgren.Network;

namespace WpfTest.Game
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game : Controls.XNAControlGame
	{
		//GraphicsDeviceManager graphics;
		NetClient m_client;

		public int gameID;
        public int redLives;
        public int blueLives;
        public int currentPlayerTurnID;
        public bool myTurn = false;
		SpriteBatch spriteBatch;
		public List<Character> Characters = new List<Character>();
		public List<Rocket> Rockets;
        public List<textPopup> textPopups = new List<textPopup>();
		public Boolean RocketFlying = false; //Do we need this? Can we just check RocketList.Count>0 ?
		public Boolean ScrollLocked = false;
		public int localPlayer;
		public Camera cam;
		public Environment env;
        public HudInfo hudInfo;
		Input input;		
		Random randomizer = new Random();		
        Timer timer;
		Stopwatch stopWatch;
        Stopwatch turnTimer;
		readonly TimeSpan TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 75);
		readonly TimeSpan MaxElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 10);
		TimeSpan accumulatedTime;
		TimeSpan lastTime;

		public int frameCount = 0;
		public int delay = 6;

		public SpriteFont font;
        public SpriteFont dmgFont;
        public SpriteFont debugFont;
		public Texture2D arrowTexture;
		public Texture2D rocketTexture;
		public Texture2D smokeTexture;
		public Texture2D explosionTexture;
		public Texture2D powerbarTexture;		

		int screenWidth = 800; //800;
		int screenHeight = 600; //600;
		public int mapWidth;
		public int mapHeight;

		public Game(List<GamePlayer> gamePlayers, string Map, string background, int gameid, int tmLives, int startPlayerID, int locPlayer, IntPtr handle, NetClient NC) : base(handle, 800, 600, "Content", false)
		{
			//graphics = new GraphicsDeviceManager(this);
			spriteBatch = new SpriteBatch(GraphicsDevice);
            String basePath = AppDomain.CurrentDomain.BaseDirectory;
            Content.RootDirectory = basePath + "..\\..\\XNBContent";
			LoadContent(gamePlayers, Map, background);

			localPlayer = locPlayer;
			gameID = gameid;
            redLives = tmLives;
            blueLives = tmLives;
            currentPlayerTurnID = startPlayerID;
			m_client = NC;

            ChangeTurn(startPlayerID);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			base.Initialize();
		}
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		#region Load Content

		protected void LoadContent(List<GamePlayer> gamePlayers, string Map, string Background)
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			SetUpPlayers(gamePlayers);

			env = new Environment(Content, Map, Background);
			input = new Input(this);
            hudInfo = new HudInfo();
			mapWidth = env.MapTexture.Width;
			mapHeight = env.MapTexture.Height;

			font = Content.Load<SpriteFont>("hudFont");
            dmgFont = Content.Load<SpriteFont>("dmgFont");
            debugFont = Content.Load<SpriteFont>("DebugFont");
			arrowTexture = Content.Load<Texture2D>("Game\\Content\\pointer");
			rocketTexture = Content.Load<Texture2D>("Game\\Content\\rocket");
			powerbarTexture = Content.Load<Texture2D>("Game\\Content\\powerbar");
			smokeTexture = Content.Load<Texture2D>("Game\\Content\\smoke");
			explosionTexture = Content.Load<Texture2D>("Game\\Content\\explosion");
			// TODO: use this.Content to load your game content here			
			Rockets = new List<Rocket>();			
			
			screenWidth = 800;
			screenHeight = 600;
			
			cam = new Camera(screenWidth, screenHeight, mapWidth, mapHeight);

			timer = new Timer();
			timer.Interval = (int)TargetElapsedTime.TotalMilliseconds;
			timer.Tick += Tick;
			timer.Start();
			//Application.Idle += TickWhileIdle;

			stopWatch = Stopwatch.StartNew();
		}

		#endregion		
				
		private void Tick(object sender, EventArgs e)
		{
			TimeSpan currentTime = stopWatch.Elapsed;
			TimeSpan elapsedTime = currentTime - lastTime;
			lastTime = currentTime;

			if (elapsedTime > MaxElapsedTime)
			{
				elapsedTime = MaxElapsedTime;
			}

			accumulatedTime += elapsedTime;

			bool updated = false;

			while (accumulatedTime >= TargetElapsedTime)
			{
				UpdateGame();

				accumulatedTime -= TargetElapsedTime;
				updated = true;
			}

			if (updated)
			{
				
				//Invalidate();
			}
			base.Tick();
		}

		//void TickWhileIdle(object sender, EventArgs e)
		//{
		//    NativeMethods.PeekMsg message;

		//    while (!NativeMethods.PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
		//    {
		//        Tick(sender, e);
		//    }
		//}		

		#region Updating

		void UpdateGame()
		{
			CalcAngle();

			//SetCharactersRotation();

			if (Rockets.Count == 0)
				RocketFlying = false;

			if (RocketFlying && !ScrollLocked)
			{
				cam.ScrollCamera(Rockets[0].RocketPosition);
				foreach (Rocket r in Rockets)
				{
					r.UpdateRocket(env.WindSpeed);
					//Vector2 gravity = new Vector2(0, 1);
					//r.RocketDirection += gravity / 10.0f;
					//r.RocketDirection += WindSpeed;
					//r.RocketPosition += r.RocketDirection;
					//r.RocketAngle = (float)Math.Atan2(r.RocketDirection.X, -r.RocketDirection.Y);

					//for (int i = 0; i < 5; i++)
					//{
					//    Vector2 smokePos = r.RocketPosition;
					//    smokePos.X += randomizer.Next(10) - 5;
					//    smokePos.Y += randomizer.Next(10) - 5;
					//    SmokeList.Add(smokePos);
					//    if (SmokeList.Count > 100)
					//        SmokeList.RemoveAt(randomizer.Next(100));
					//}
				}
			}
			else if (!ScrollLocked)
				cam.ScrollCamera(Characters[localPlayer].Position);
				//ScrollCamera(new Vector2(Characters[localPlayer].Position.X, Characters[localPlayer].Position.Y));

			//Get Input
			input.GetInput(Characters[localPlayer]);
		}

		#endregion

		#region Drawing
		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin();
			env.DrawBackground(spriteBatch, cam.Pos);
			spriteBatch.End();

			Matrix m = cam.get_transformation();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, m);
			env.DrawMap(spriteBatch);
			DrawPlayers(spriteBatch);
			DrawRockets(spriteBatch);
			spriteBatch.Draw(arrowTexture, Characters[localPlayer].PlayerMid, null, Color.White, hudInfo.Angle + Characters[localPlayer].Rotate, new Vector2(11, 50), 2.0f, SpriteEffects.None, 1);
            DrawTextPopups(spriteBatch);            
			spriteBatch.End();

			spriteBatch.Begin();            
			DrawPowerbar(spriteBatch);
            DrawTimer(spriteBatch);
            DrawEnergies(spriteBatch);
			DrawHUD(spriteBatch);
			DrawWind(spriteBatch);
			spriteBatch.End();
			base.Draw(gameTime);
		}

        public void DrawTextPopups(SpriteBatch SB)
        {
            if (textPopups.Count > 0)
            {
                for (int i = 0; i < textPopups.Count; i++)
                {
                    if (textPopups[i].elapsed)
                        textPopups.Remove(textPopups[i]);
                    else
                    {
                        if(textPopups[i].textType == TextPopupTypes.Damage)
                            SB.DrawString(dmgFont, textPopups[i].intText.ToString(), new Vector2(textPopups[i].Position.X, textPopups[i].Position.Y - 150), Color.Red);
                        else if(textPopups[i].textType == TextPopupTypes.Money)
                            SB.DrawString(dmgFont, textPopups[i].intText.ToString(), new Vector2(textPopups[i].Position.X, textPopups[i].Position.Y - 150), Color.Gold);
                    }
                }
            }
        }

        public void DrawTimer(SpriteBatch SB)
        {
            if (myTurn)
            {
                int secondsLeft = (int)(20 - (turnTimer.ElapsedMilliseconds / 1000));
                if (secondsLeft < 0)
                    secondsLeft = 0;
                SB.DrawString(dmgFont, secondsLeft.ToString(), new Vector2(screenWidth - 100, 100), Color.Black);
            }
        }

        public void DrawEnergies(SpriteBatch SB)
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                SB.DrawString(debugFont, Characters[i].Name + ": " + Characters[i].Energy, new Vector2(10, screenHeight - 100 - (i * 20)), Color.Black);
            }
        }

        public void DrawPlayers(SpriteBatch SB)
		{
			foreach (Character ch in Characters)
			{
				ch.Draw(SB);
                //Player Names
                //If player is red team, make name red. Same for blue.
                SB.DrawString(debugFont, ch.Name, new Vector2((int)ch.Position.X - 25, (int)ch.Position.Y + 10), Color.Black);

				//HealthBars
				SB.Draw(powerbarTexture, new Rectangle((int)ch.Position.X - 25, (int)ch.Position.Y + 30, 50, 10), new Rectangle(0, 45, powerbarTexture.Width, 44), Color.Gray);
				SB.Draw(powerbarTexture, new Rectangle((int)ch.Position.X - 25, (int)ch.Position.Y + 30, (int)(ch.Health / 2), 10), new Rectangle(0, 45, powerbarTexture.Width, 44), Color.Red);
				SB.Draw(powerbarTexture, new Rectangle((int)ch.Position.X - 25, (int)ch.Position.Y + 30, 50, 10), new Rectangle(0, 0, powerbarTexture.Width, 44), Color.White);
			}
		}

		public void DrawRockets(SpriteBatch spriteBatch)
		{
			if (RocketFlying)
			{
				foreach (Rocket r in Rockets)
					r.Draw(spriteBatch);
			}
		}

		public void DrawHUD(SpriteBatch SB)
		{
			SB.DrawString(font, "Angle:\n   " + hudInfo.ActualAngle.ToString(), new Vector2(21, screenHeight - 70), Color.LimeGreen);
            SB.DrawString(font, "Angle:\n   " + hudInfo.ActualAngle.ToString(), new Vector2(20, screenHeight - 71), Color.White);
            SB.DrawString(font, "Prev Angle:\n   " + hudInfo.PrevAngle.ToString(), new Vector2(101, screenHeight - 70), Color.LimeGreen);
            SB.DrawString(font, "Prev Angle:\n   " + hudInfo.PrevAngle.ToString(), new Vector2(100, screenHeight - 71), Color.White);

            SB.DrawString(font, "Red Lives: " + redLives, new Vector2(screenWidth - 250, screenHeight - 30), Color.LimeGreen);
            SB.DrawString(font, "Red Lives: " + redLives, new Vector2(screenWidth - 249, screenHeight - 31), Color.White);
            SB.DrawString(font, "Blue Lives: " + blueLives, new Vector2(screenWidth - 125, screenHeight - 30), Color.LimeGreen);
            SB.DrawString(font, "Blue Lives: " + blueLives, new Vector2(screenWidth - 123, screenHeight - 31), Color.White);
		}

		public void DrawPowerbar(SpriteBatch SB)
		{
			SB.Draw(powerbarTexture, new Rectangle(screenWidth - (powerbarTexture.Width + 30), screenHeight - 70, powerbarTexture.Width, 40), new Rectangle(0, 45, powerbarTexture.Width, 44), Color.Gray);
            SB.Draw(powerbarTexture, new Rectangle(screenWidth - (powerbarTexture.Width + 30), screenHeight - 70, (int)(powerbarTexture.Width * ((double)hudInfo.PrevPower / 1000)), 40), new Rectangle(0, 45, powerbarTexture.Width, 44), Color.Green);
            SB.Draw(powerbarTexture, new Rectangle(screenWidth - (powerbarTexture.Width + 30), screenHeight - 70, (int)(powerbarTexture.Width * ((double)hudInfo.Power / 1000)), 40), new Rectangle(0, 45, powerbarTexture.Width, 44), Color.Red);
			SB.Draw(powerbarTexture, new Rectangle(screenWidth - (powerbarTexture.Width + 30), screenHeight - 70, powerbarTexture.Width, 40), new Rectangle(0, 0, powerbarTexture.Width, 44), Color.White);
		}

		public void DrawWind(SpriteBatch SB)
		{
			SB.Draw(arrowTexture, new Vector2(screenWidth / 2, 50), null, Color.White, MathHelper.ToRadians(env.WindAngle), new Vector2(11, 50), 1.0f / 2, SpriteEffects.None, 1);
			SB.DrawString(font, env.WindPower.ToString(), new Vector2(screenWidth / 2, 50), Color.White);
		}

		#endregion

		#region Private Functions

		private void CalcAngle()
		{
            int currentAngle = (int)MathHelper.ToDegrees(hudInfo.Angle);
            hudInfo.ActualAngle = 0;
			Character CurrentChar = Characters[localPlayer];
			float rot = (int)MathHelper.ToDegrees(CurrentChar.Rotate);
			if (CurrentChar.Facing == "right")
			{
                hudInfo.ActualAngle = (90 - Math.Abs(currentAngle) - rot);
			}
			else
			{
                hudInfo.ActualAngle = (Math.Abs(currentAngle) - 90 - rot) * -1;
			}
            if (hudInfo.ActualAngle > 90)
                hudInfo.ActualAngle = 90 - (hudInfo.ActualAngle - 90);
		}

		private void SetUpPlayers(List<GamePlayer> gamePlayers)
		{
			foreach (GamePlayer gp in gamePlayers)
			{
				Character ch = new Character();
				ch.ID = gp.ID;
				ch.Name = gp.Name;
				ch.tankTexture = Content.Load<Texture2D>(gp.Tank);
				ch.skinTexture = Content.Load<Texture2D>(gp.Skin);
				ch.avaHeadTexture = Content.Load<Texture2D>(gp.AvaHead);
				ch.avaShoulderTexture = Content.Load<Texture2D>(gp.AvaShoulder);
				ch.avaChestTexture = Content.Load<Texture2D>(gp.AvaChest);
                ch.Health = gp.Health;
				//ch.Position = new Vector2(gp.Position.Item1, gp.Position.Item2);
				//ch.PlayerMid = new Vector2(gp.PlayerMid.Item1, gp.PlayerMid.Item2);
				//ch.Rotate = gp.Rotate;
				//.Health = gp.Health;
				//ch.Energy = gp.Energy;

				Characters.Add(ch);
			}
		}

        private void StartTurn()
        {
            if (turnTimer == null)
                turnTimer = Stopwatch.StartNew();
            else
                turnTimer.Start();
            myTurn = true;
        }

		public void Move(int x)
		{
			NetOutgoingMessage move = m_client.CreateMessage();
			move.Write((byte)MasterServerMessageType.Move);
			move.Write(gameID);
			move.Write(x);
			m_client.SendMessage(move, NetDeliveryMethod.ReliableOrdered, 0);
		}

		public void CreateRocket(Vector2 Pos, int Pow, float Ang)
		{
			NetOutgoingMessage createRocket = m_client.CreateMessage();
			createRocket.Write((byte)MasterServerMessageType.CreateRocket);
			createRocket.Write(gameID);
			createRocket.Write(Pow);
			createRocket.Write(Ang);
			m_client.SendMessage(createRocket, NetDeliveryMethod.ReliableOrdered, 0);
		}

		#endregion		

		#region Public Functions

		public void CreateRocket(int x, int y, int pow, float ang)
		{
			RocketFlying = true;
			Rocket r = new Rocket(rocketTexture, smokeTexture, new Vector2(x, y), pow, ang);
			Rockets.Add(r);
		}

		public void UpdatePlayerHealth(int dmg, int ID)
		{
			foreach (Character ch in Characters)
			{
				if (ch.ID == ID)
					ch.Health -= dmg; //Need to eventually change all health values to int or float, not both.
			}
		}

        public void KillPlayer(int killedPlayerID, int playerKillerID, int rLives, int bLives)
        {
            redLives = rLives;
            blueLives = bLives;
            foreach (Character ch in Characters)
            {
                if (ch.ID == killedPlayerID)
                {
                    ch.Health = 100;                    
                }
            }
        }

        public void DamagePlayer(int damagedPlayerID, int dmg) //Can combine this method with GivePlayerMoney by passing TextPopupType
        {
            bool added = false;
            foreach (textPopup t in textPopups)
            {
                if (t.PlayerID == damagedPlayerID && t.textType == TextPopupTypes.Damage) //If player already has a textPopup for money, add this to it
                {
                    t.intText += dmg;
                    added = true;
                }
            }
            if (!added)
            {
                foreach (Character ch in Characters)
                {
                    if (ch.ID == damagedPlayerID)
                    {
                        textPopup tp = new textPopup(damagedPlayerID, (int)ch.Position.X -15, (int)ch.Position.Y - 40, dmg, 5000, TextPopupTypes.Damage);
                        textPopups.Add(tp);
                    }
                }
            }
        }

        public void GivePlayerMoney(int moneyPlayerID, int money) //Can combine this method with DamagePlayer by passing TextPopupType
        {
            bool added = false;
            foreach (textPopup t in textPopups)
            {
                if (t.PlayerID == moneyPlayerID && t.textType == TextPopupTypes.Money) //If player already has a textPopup for money, add this to it
                {
                    t.intText += money;
                    added = true;
                }
            }
            if (!added)
            {
                foreach (Character ch in Characters)
                {
                    if (ch.ID == moneyPlayerID)
                    {
                        textPopup tp = new textPopup(moneyPlayerID, (int)ch.Position.X + 15, (int)ch.Position.Y - 40, money, 5000, TextPopupTypes.Money);
                        textPopups.Add(tp);
                    }
                }                
            }
        }

        public void ChangeTurn(int playerTurnID)
        {
            currentPlayerTurnID = playerTurnID;
            if (turnTimer != null)
                turnTimer.Reset();

            if (currentPlayerTurnID == Characters[localPlayer].ID)
                StartTurn();
            else
            {
                myTurn = false;                
            }
        }
		#endregion
	}
}
