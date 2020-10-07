using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WpfTest.Game
{
	public class Environment
	{
		private Layer[] layers;
		public Texture2D MapTexture { get; private set; }
		private Texture2D BackgroundTexture;
		private Color[] foregroundColorArray;

        public int WindPower = 0;
        public float WindAngle = 0;
        public Vector2 WindSpeed = new Vector2(0, 0);


		public Environment(ContentManager Content, string Map, string Background)
		{
			MapTexture = Content.Load<Texture2D>("Game\\Maps\\" + Map);

			layers = new Layer[1];
			layers[0] = new Layer(Content, "Game\\Backgrounds\\" + Background, .2f);

			foregroundColorArray = new Color[MapTexture.Width * MapTexture.Height];
			MapTexture.GetData<Color>(foregroundColorArray, 0, MapTexture.Width * MapTexture.Height);
		}

		public void DrawBackground(SpriteBatch SB, Vector2 camPos)
		{
			layers[0].Draw(SB, camPos);
		}

		public void DrawMap(SpriteBatch SB)
		{
			Rectangle screenRectangle = new Rectangle(0, 0, MapTexture.Width, MapTexture.Height);
			SB.Draw(MapTexture, screenRectangle, Color.White);
		}

		public void AddCrater(int x, int y, int r)
		{
			for (int i = (x - r); i <= (x + r); i++)
			{
				for (int j = (y - r); j <= (y + r); j++)
				{
					if (x < 0 || j < 0)
						continue;
					int index = j * MapTexture.Width + i;
					if (foregroundColorArray[index].A > 0)
					{
						double d = Math.Sqrt(Math.Pow((x - i), 2) + Math.Pow((y - j), 2));
						d = Math.Abs(d);
						if (d <= r)
						{
							foregroundColorArray[index].A = 0;
							foregroundColorArray[index].R = 0;
							foregroundColorArray[index].B = 0;
							foregroundColorArray[index].G = 0;
						}
					}
				}
			}
			MapTexture.SetData(foregroundColorArray, 0, MapTexture.Width * MapTexture.Height); //Possible to only set data for changed pixels?
		}
	}
}
