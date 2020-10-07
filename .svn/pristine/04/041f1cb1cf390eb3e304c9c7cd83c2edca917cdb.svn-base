using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WpfTest.Game
{
	public class Layer
	{
		public Texture2D[] Textures { get; private set; }
		public float ScrollRate { get; private set; }

		public Layer(ContentManager content, string basePath, float scrollRate)
		{
			// Assumes each layer only has 3 segments.
			Textures = new Texture2D[1];
			Textures[0] = content.Load<Texture2D>(basePath);
			//for (int i = 0; i < 3; ++i)
			//    Textures[i] = content.Load<Texture2D>(basePath + "_" + i);

			ScrollRate = scrollRate;
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition)
		{
			// Assume each segment is the same width.
			int segmentWidth = Textures[0].Width;
			int segmentHeight = Textures[0].Height;

			// Calculate which segments to draw and how much to offset them.
			float x = cameraPosition.X * ScrollRate;
			float y = (cameraPosition.Y + 300) * ScrollRate;
			int leftSegment = (int)Math.Floor(x / segmentWidth);
			int topSegment = (int)Math.Floor(y / segmentHeight);
			x = (x / segmentWidth - leftSegment) * -segmentWidth;
			y = (y / segmentHeight - topSegment) * -segmentHeight;
			spriteBatch.Draw(Textures[leftSegment % Textures.Length], new Vector2(x, y - 300), Color.White);
		}
	}
}
