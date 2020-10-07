using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WpfTest.Game
{
	public class Rocket //Simple class.  Just designed for other classed to create an instance of Rocket
	{
		public Texture2D RocketTexture;
		public Texture2D SmokeTexture;
		//public Texture2D ExplosionTexture;
		Random randomizer = new Random();
		public Color[,] RocketColorArray;
		public Color[,] ExplosionColorArray;
		public List<Vector2> SmokeList;
		//float RocketScaling = 0.1f;
		public Vector2 RocketPosition;
		public float RocketAngle;
		public Vector2 RocketDirection;

		public Rocket(Texture2D rocketTexture, Texture2D smokeTexture, Vector2 Position, int Power, float Angle)
		{
			RocketTexture = rocketTexture;
			SmokeTexture = smokeTexture;
			RocketPosition = Position;
			RocketAngle = Angle;
			Vector2 up = new Vector2(0, -1);
			Matrix rotMatrix = Matrix.CreateRotationZ(RocketAngle);
			RocketDirection = Vector2.Transform(up, rotMatrix);
			RocketDirection *= Power / 50.0f;
			SmokeList = new List<Vector2>();
			LoadContent();
		}

		public void LoadContent()
		{

		}

		public void UpdateRocket(Vector2 WindSpeed)
		{

			//map.cam.Pos = new Vector2(RocketPosition.X - 400, RocketPosition.Y - 200);
			//map.ScrollCamera(map.device.Viewport, RocketPosition);
			Vector2 gravity = new Vector2(0, 1);
			RocketDirection += gravity / 10.0f;
			RocketDirection += WindSpeed;
			RocketPosition += RocketDirection;
			RocketAngle = (float)Math.Atan2(RocketDirection.X, -RocketDirection.Y);

			for (int i = 0; i < 5; i++)
			{
				Vector2 smokePos = RocketPosition;
				smokePos.X += randomizer.Next(10) - 5;
				smokePos.Y += randomizer.Next(10) - 5;
				SmokeList.Add(smokePos);
				if (SmokeList.Count >20)
					SmokeList.RemoveAt(0);
			}			
		}

		public void Draw(SpriteBatch SB)
		{
			SB.Draw(RocketTexture, RocketPosition, null, Color.White, RocketAngle, new Vector2(42, 240), 0.1f, SpriteEffects.None, 1);
			foreach (Vector2 smokePos in SmokeList)
				SB.Draw(SmokeTexture, smokePos, null, Color.White, 0, new Vector2(40, 35), 0.2f, SpriteEffects.None, 1);
		}
	}
}
