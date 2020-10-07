using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WpfTest.Game
{
	public class Character
	{
		public int ID;
		public string Name;
		public Texture2D tankTexture;
		public Texture2D skinTexture;
		public Texture2D avaHeadTexture;
		public Texture2D avaShoulderTexture;
		public Texture2D avaChestTexture;		
		public Vector2 Position;
		public Vector2 PlayerMid;
		public float Rotate;
		public int Health;
		public int Energy;
		public String Facing = "right";
		public String Direction = "stand";
		public Rectangle SpriteBounds = new Rectangle(0, 0, 200, 200);
		float scale = 0.7f;

		public void Draw(SpriteBatch SB)
		{
			if (Facing == "right")
			{
				SB.Draw(tankTexture, Position, SpriteBounds, Color.White, Rotate, new Vector2(SpriteBounds.Width / 2, SpriteBounds.Height), scale, SpriteEffects.FlipHorizontally, 0);
				SB.Draw(skinTexture, Position, SpriteBounds, Color.White, Rotate, new Vector2(SpriteBounds.Width / 2, SpriteBounds.Height), scale, SpriteEffects.FlipHorizontally, 0);
				SB.Draw(avaChestTexture, Position, SpriteBounds, Color.White, Rotate, new Vector2(SpriteBounds.Width / 2, SpriteBounds.Height), scale, SpriteEffects.FlipHorizontally, 0);
				SB.Draw(avaShoulderTexture, Position, SpriteBounds, Color.White, Rotate, new Vector2(SpriteBounds.Width / 2, SpriteBounds.Height), scale, SpriteEffects.FlipHorizontally, 0);
				SB.Draw(avaHeadTexture, Position, SpriteBounds, Color.White, Rotate, new Vector2(SpriteBounds.Width / 2, SpriteBounds.Height), scale, SpriteEffects.FlipHorizontally, 0);
			}
			else
			{
				SB.Draw(tankTexture, Position, SpriteBounds, Color.White, Rotate, new Vector2(SpriteBounds.Width / 2, SpriteBounds.Height), scale, SpriteEffects.None, 0);
				SB.Draw(skinTexture, Position, SpriteBounds, Color.White, Rotate, new Vector2(SpriteBounds.Width / 2, SpriteBounds.Height), scale, SpriteEffects.None, 0);
				SB.Draw(avaChestTexture, Position, SpriteBounds, Color.White, Rotate, new Vector2(SpriteBounds.Width / 2, SpriteBounds.Height), scale, SpriteEffects.None, 0);
				SB.Draw(avaShoulderTexture, Position, SpriteBounds, Color.White, Rotate, new Vector2(SpriteBounds.Width / 2, SpriteBounds.Height), scale, SpriteEffects.None, 0);
				SB.Draw(avaHeadTexture, Position, SpriteBounds, Color.White, Rotate, new Vector2(SpriteBounds.Width / 2, SpriteBounds.Height), scale, SpriteEffects.None, 0);
			}
							
		}
	}
}
