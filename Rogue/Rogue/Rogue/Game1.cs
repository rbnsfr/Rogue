using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Rogue
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //RandomManager randomManager;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, spritesheet, tilesheet, cursortexture;
        Sprite protagonist, cursor;
        SpriteFont sf;
        KeyboardState ks;
        MouseState ms;
        Keys[] applicableKeys = { Keys.W, Keys.A, Keys.S, Keys.D };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>(@"Test\Background");
            tilesheet = Content.Load<Texture2D>(@"Test\Tilesheet");
            spritesheet = Content.Load<Texture2D>(@"Test\Spritesheet");
            cursortexture = Content.Load<Texture2D>(@"Test\Cursor");

            protagonist = new Sprite(new Vector2(300, 300), spritesheet, new Rectangle(8, 0, 57, 75), Vector2.Zero, 1);
            cursor = new Sprite(Vector2.Zero, cursortexture, new Rectangle(0, 0, 50, 50), Vector2.Zero, 1);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            ms = Mouse.GetState();

            if (ks.IsKeyDown(Keys.W))
            {
                protagonist.Location += new Vector2(0, -2);
            }
            if (ks.IsKeyDown(Keys.A))
            {
                //protagonist.Frame = 0;
                protagonist.AddFrame(new Rectangle(68, 0, 50, 75));
                protagonist.AddFrame(new Rectangle(120, 0, 50, 75));
                protagonist.Location += new Vector2(-2, 0);
            }
            if (ks.IsKeyDown(Keys.S))
            {
                protagonist.Location += new Vector2(0, 2);
            }
            if (ks.IsKeyDown(Keys.D))
            {
                protagonist.AddFrame(new Rectangle(178, 0, 50, 75));
                protagonist.AddFrame(new Rectangle(238, 0, 50, 75));
                protagonist.Location += new Vector2(2, 0);
            }
            if (!ks.IsKeyDown(Keys.W) && !ks.IsKeyDown(Keys.A)
                && !ks.IsKeyDown(Keys.S) && !ks.IsKeyDown(Keys.D))
                protagonist.Location += new Vector2(0, 0);
            cursor.Location = new Vector2(ms.X, ms.Y);
            protagonist.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            /*switch (randomManager.RandomNumber)
            {
                case 0:
                    // Draw stone tile
                    break;
                case 1:
                    // Draw grass tile
                    break;
                case 2:
                    // Draw hell tile
                    break;
                case 3:
                    // Draw hole
                    break;
            }*/
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            protagonist.Draw(spriteBatch);
            spriteBatch.Draw(cursor, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
