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
        Texture2D background, spritesheet, tilesheet;
        Sprite protagonist;
        SpriteFont sf;

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

            sf = Content.Load<SpriteFont>("DrawnString");
            protagonist = new Sprite(spritesheet, new Vector2(Window.ClientBounds.Center.X, Window.ClientBounds.Center.Y), Vector2.Zero, new Rectangle(0, 0, 0, 0));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
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
            // <<< START TEST STUFF >>>
            spriteBatch.DrawString(sf, protagonist.IsAttacking().ToString(), new Vector2(Window.ClientBounds.Center.X, Window.ClientBounds.Center.Y), Color.White);
            spriteBatch.DrawString(sf, protagonist.IsWalking().ToString(), new Vector2(Window.ClientBounds.Center.X, Window.ClientBounds.Center.Y), Color.LightGreen);
            // <<< END TEST STUFF >>>
            protagonist.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
