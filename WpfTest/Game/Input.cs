using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WpfTest.Game
{
	public class Input
	{
		KeyboardState keyboardState;
		MouseState mouseState;
		MouseState oldMouseState;
		public Boolean ScrollLocked = false;
		private Game game;

		public Input(Game g)
		{
			game = g;
		}

		public void GetInput(Character ch)
		{
			mouseState = Mouse.GetState();
			if (mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Pressed)
			{
				ScrollLocked = true;
				Vector2 MousePos = new Vector2(mouseState.X, mouseState.Y);
				Vector2 oldMousePos = new Vector2(oldMouseState.X, oldMouseState.Y);
				Vector2 Distance = MousePos - oldMousePos;
				//Distance*=-1;
				if (Distance.Length() > 0)
					game.cam.ScrollCamera(game.cam.scrolledPos + Distance);
			}
			else if (mouseState.RightButton == ButtonState.Released && oldMouseState.RightButton == ButtonState.Pressed)
			{
				ScrollLocked = false;
			}
			oldMouseState = mouseState;
			keyboardState = Keyboard.GetState();

			if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
			{
				if (ch.Facing == "right")
                    game.hudInfo.Angle -= .01f;
				else
                    game.hudInfo.Angle += .01f;
			}
			if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
			{
				if (ch.Facing == "left")
                    game.hudInfo.Angle -= .01f;
				else
                    game.hudInfo.Angle += .01f;
			}
            if (game.hudInfo.Angle > MathHelper.PiOver2)
                game.hudInfo.Angle = MathHelper.PiOver2;
            else if (game.hudInfo.Angle < -MathHelper.PiOver2)
                game.hudInfo.Angle = -MathHelper.PiOver2;
            else if (ch.Facing == "right" && game.hudInfo.Angle < 0)
                game.hudInfo.Angle = 0;
            else if (ch.Facing == "left" && game.hudInfo.Angle > 0)
                game.hudInfo.Angle = 0;
            if (keyboardState.IsKeyDown(Keys.Space) && game.hudInfo.Power <= 998)
            {
                if (game.myTurn)
                    game.hudInfo.Power += 1.75;
            }
                
			if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
			{

				if (ch.Position.X - 15 <= 1)
				{ }
				else
				{
					if (ch.Facing == "right")
                        game.hudInfo.Angle *= -1;
					ch.Facing = "left";
                    if (game.myTurn)
                    {
                        if (game.frameCount / game.delay > 0)
                            game.frameCount = 0;
                        ch.SpriteBounds = new Rectangle(game.frameCount / game.delay * 200, 0, 200, 200);
                        game.Move(-1);
                    }					
				}
			}
			if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
			{
				//if (Characters[localPlayer].Position.X + 15 >= screenWidth -1)
				if (ch.Position.X + 15 >= game.mapWidth - 1)
				{ }
				else
				{
					if (ch.Facing == "left")
                        game.hudInfo.Angle *= -1;
					ch.Facing = "right";
                    if (game.myTurn)
                    {
                        if (game.frameCount / game.delay > 0)
                            game.frameCount = 0;
                        ch.SpriteBounds = new Rectangle(game.frameCount / game.delay * 200, 0, 200, 200);
                        game.Move(1);
                    }					
				}
			}
            if ((keyboardState.IsKeyUp(Keys.Space) && game.hudInfo.Power > 0) || game.hudInfo.Power > 998)
			{
				if (!game.RocketFlying && game.myTurn) //Do we even need to check this? shouldn't be allowed to fire since power won't be > 0
				{
                    game.hudInfo.PrevAngle = game.hudInfo.ActualAngle;
                    game.hudInfo.PrevPower = (int)game.hudInfo.Power;
                    game.CreateRocket(ch.Position, (int)game.hudInfo.Power, game.hudInfo.Angle + ch.Rotate);
				}
                game.hudInfo.Power = 0;
			}
			game.frameCount++;
		}		
	}
}
