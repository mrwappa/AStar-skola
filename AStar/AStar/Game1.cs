﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AStar.GameObjects.Tools___Accessories;
using AStar.GameObjects;

namespace AStar
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState mouse;
        KeyboardState keyboard;

        Camera camera;
        int monitorWidth;
        int monitorHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            monitorWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            monitorHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = monitorWidth;
            graphics.PreferredBackBufferHeight = monitorHeight;
            Window.Position = new Point(0, 0);
            Window.IsBorderless = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            camera = new Camera(monitorWidth, monitorHeight);
            GameObject.InitGame(graphics, camera, Content);
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            new Solid(100, 100);
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();
            GameObject.Keyboard = keyboard;
            GameObject.Mouse = mouse;
            Camera.Mouse = mouse;
            // TODO: Add your update logic here
            foreach (GameObject obj in GameObject.GameObjects.ToList())
            {
                obj.Update();
            }
            GameObject.PreviousKeyboardState = keyboard;
            GameObject.PreviousMouseState = mouse;
            camera.Update(new Vector2(0, 0));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);
            foreach (GameObject obj in GameObject.GameObjects.ToList())
            {
                obj.Draw(spriteBatch);
            }

            spriteBatch.Draw(GameObject.Box, GameObject.GridSnap, new Rectangle(0, 0, GameObject.Box.Width, GameObject.Box.Height), Color.Black, 0,
            new Vector2((GameObject.Box.Width / 2), (GameObject.Box.Height / 2)), new Vector2(1, 1), SpriteEffects.None, 1);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
