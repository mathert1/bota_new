﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MSCommon;
using System.IO;


namespace WpfTest
{
    public class CharControl : Controls.XNAControlGame
    {
        public Stream stream;
        SpriteBatch m_spriteBatch;
        SpriteFont m_spriteFont;
        public Rectangle SpriteBounds = new Rectangle(0, 0, 0, 0);
        int frameCount = 0;
        int delay = 10;
        public Texture2D tankTexture;
        public Texture2D skinTexture;
        public Texture2D avaHeadTexture;
        public Texture2D avaChestTexture;
        public Texture2D avaShoulderTexture;
        double m_passedTime;
        int m_counter;
        int m_prevCounter;
        float scale = 0.8f;
         
        public CharControl(string T, string S, string avaH, string avaS, string avaCh, IntPtr handle)
            : base(handle, 175, 175, "Content", true)
        {
            LoadContent(T, S, avaH, avaS, avaCh);
            //Content.RootDirectory = "Content";
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

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// protected override void LoadContent()
        /// No Longer overriding because LoadContent was being called before avatar parameters were being set
        /// it is now called directly from Game1 method
        protected void LoadContent(string T, string S, string avaH, string avaS, string avaCh)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            String basePath = AppDomain.CurrentDomain.BaseDirectory;
            Content.RootDirectory = basePath + "..\\..\\XNBContent";
            //stream = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Content\\" + T);
            tankTexture = Content.Load<Texture2D>(T);
            skinTexture = Content.Load<Texture2D>(S);
            avaHeadTexture = Content.Load<Texture2D>(avaH);
            avaShoulderTexture = Content.Load<Texture2D>(avaS);
            avaChestTexture = Content.Load<Texture2D>(avaCh);

            m_spriteFont = Content.Load<SpriteFont>("DebugFont");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (Variables.player.Tank == 0)
            //{
            //    if (frameCount / delay > 3)
            //        frameCount = 0;
            //    SpriteBounds = new Rectangle(frameCount / delay * 200, 0, 200, 200);
            //    frameCount++;
            //}
            //else
            //{
            //    SpriteBounds = new Rectangle(0, 0, 200, 200);
            //}
            SpriteBounds = new Rectangle(0, 0, 200, 200);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            m_spriteBatch.Begin();
            //m_spriteBatch.Draw(tankTexture, new Vector2((float)87.5, 175), Color.White);
            m_spriteBatch.Draw(tankTexture, new Vector2(0, 0), SpriteBounds, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            m_spriteBatch.Draw(skinTexture, new Vector2(0, 0), SpriteBounds, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            m_spriteBatch.Draw(avaChestTexture, new Vector2(0, 0), SpriteBounds, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            m_spriteBatch.Draw(avaShoulderTexture, new Vector2(0, 0), SpriteBounds, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            m_spriteBatch.Draw(avaHeadTexture, new Vector2(0, 0), SpriteBounds, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            //m_spriteBatch.Draw(tankTexture, new Vector2((float)87.5, 175), new Rectangle(0, 0, 175, 175), Color.White, 0, new Vector2(175 / 2, 175), 1.0f, SpriteEffects.None, 0);
            m_spriteBatch.End();
            DrawFPS(gameTime.ElapsedGameTime.TotalMilliseconds);
            base.Draw(gameTime);
        }

        void DrawFPS(double elapsedTime)
        {
            m_passedTime += elapsedTime;
            if (m_passedTime >= 1000)
            {
                m_passedTime = 0;
                m_prevCounter = m_counter;
                m_counter = 1;
            }
            else if (elapsedTime != 0)
                m_counter++;

            m_spriteBatch.Begin();
            m_spriteBatch.DrawString(m_spriteFont, m_prevCounter.ToString() + " FPS", new Vector2(10, 5), Color.White);
            m_spriteBatch.End();
        }
    }
}
