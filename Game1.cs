﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace mygame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D ship_Sprite;
        Texture2D asteroid_Sprite;
        Texture2D space_Sprite;

        SpriteFont gameFont;
        SpriteFont timerFont;

        Ship player = new Ship();

        Controller gameController = new Controller();

        double m_iElapsedMilliseconds = 0; 
        int m_iFrameCount = 0;
        int m_iFPS = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ship_Sprite = Content.Load<Texture2D>("ship");
            asteroid_Sprite = Content.Load<Texture2D>("asteroid");
            space_Sprite = Content.Load<Texture2D>("space");

            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("timerFont");
        }

        protected override void Update(GameTime gameTime)
        {
            m_iElapsedMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
           
            if (m_iElapsedMilliseconds > 1000)
            {
                m_iElapsedMilliseconds -= 1000;
                m_iFPS = m_iFrameCount;
                m_iFrameCount = 0;
            }
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();            
            player.shipUpdate(gameTime, gameController);     
            gameController.conUpdate(gameTime);      
            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                gameController.asteroids[i].asteroidUpdate(gameTime);

                if (gameController.asteroids[i].position.X < (0 - gameController.asteroids[i].radius))
                {
                    gameController.asteroids[i].offscreen = true;
                }

                int sum = gameController.asteroids[i].radius + 30;
                if (Vector2.Distance(gameController.asteroids[i].position, player.position) < sum)
                {
                    player.lifeBar --;
                    if (player.lifeBar <= 0)
                    {
                        gameController.inGame = false;
                        player.position = Ship.defaultPosition;
                        player.lifeBar = Ship.defaultLifeBar;
                        i = gameController.asteroids.Count;
                        gameController.asteroids.Clear();
                    }
                    
                }
            }
            gameController.asteroids.RemoveAll(a => a.offscreen);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);
            m_iFrameCount++;
            _spriteBatch.Begin();
            float num = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _spriteBatch.Draw(space_Sprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(ship_Sprite, new Vector2(player.position.X - 34, player.position.Y - 50), Color.White); 
            if (gameController.inGame == false)
            {
                string menuMessage = "Press Enter to Begin!";
                Vector2 sizeOfText = gameFont.MeasureString(menuMessage);
                _spriteBatch.DrawString(gameFont, menuMessage, new Vector2(640 - sizeOfText.X/2, 200), Color.White);
            }
            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                Vector2 tempPos = gameController.asteroids[i].position;
                int tempRadius = gameController.asteroids[i].radius;
                _spriteBatch.Draw(asteroid_Sprite, new Vector2(tempPos.X - tempRadius, tempPos.Y - tempRadius), Color.White);
            }
            //_spriteBatch.DrawString(gameFont, "Frame Rate: " + m_iFPS.ToString() +" fps", new Vector2(10,10), Color.White);
            _spriteBatch.DrawString(timerFont, "Time:" + Math.Floor(gameController.totalTime).ToString(), new Vector2(3,3), Color.White); 
            _spriteBatch.DrawString(gameFont, "Life: " + player.lifeBar.ToString(), new Vector2(1050,650), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
