using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Server
{
	public class Rocket
	{
		public int player;
		//public Texture2D RocketTexture;
		public Texture2D SmokeTexture;
		//public Texture2D ExplosionTexture;
		Random randomizer = new Random();
		//public Color[,] RocketColorArray;
		//public Color[,] ExplosionColorArray;
		public List<Vector2> SmokeList;
		//float RocketScaling = 0.1f;
		public Vector2 Position;
		public float Angle;
		public Vector2 Direction;
		public int width = 86;
		public int height = 287;
		public int radius = 3;

		public Rocket(Vector2 pos, int Power, float ang)
		{
			Position = pos;
			Angle = ang;
			Vector2 up = new Vector2(0, -1);
			Matrix rotMatrix = Matrix.CreateRotationZ(Angle);
			Direction = Vector2.Transform(up, rotMatrix);
			Direction *= Power / 50.0f;
			LoadContent();
		}

		public void LoadContent()
		{

		}

		public void UpdateRocket(Game g)
		{

			//map.cam.Pos = new Vector2(RocketPosition.X - 400, RocketPosition.Y - 200);
			//map.ScrollCamera(map.device.Viewport, RocketPosition);
			Vector2 gravity = new Vector2(0, 1);
			Direction += gravity / 10.0f;
			Direction += g.WindSpeed;
			Position += Direction;

			Angle = (float)Math.Atan2(Direction.X, -Direction.Y);

			//for (int i = 0; i < 5; i++)
			//{
			//    Vector2 smokePos = RocketPosition;
			//    smokePos.X += randomizer.Next(10) - 5;
			//    smokePos.Y += randomizer.Next(10) - 5;
			//    SmokeList.Add(smokePos);
			//    if (SmokeList.Count > 100)
			//        SmokeList.RemoveAt(randomizer.Next(100));
			//}			
		}
	}
}
