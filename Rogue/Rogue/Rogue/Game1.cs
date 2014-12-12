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
        Sprite cursor, projectile;
        Protagonist protagonist;
        SpriteFont sf;
        KeyboardState ks;
        MouseState ms;
        ManageCommands mgrcommands = new ManageCommands();
        ManageRandom mgrrandom = new ManageRandom();
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
            sf = Content.Load<SpriteFont>(@"DrawnString");

            protagonist = new Protagonist(new Vector2(300, 300), spritesheet, new Rectangle(8, 0, 57, 75), Vector2.Zero, 1);
            cursor = new Sprite(Vector2.Zero, cursortexture, new Rectangle(0, 0, 50, 50), Vector2.Zero, 0.4f);
            projectile = new Sprite(Vector2.Zero, spritesheet, new Rectangle(228, 9, 15, 15), Vector2.Zero, 1);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            ms = Mouse.GetState();

            float playerSpeed;
            if (ks.IsKeyDown(Keys.LeftShift))
                playerSpeed = 4;
            else
                playerSpeed = 2;

            if (ks.IsKeyDown(applicableKeys[0]))
            {
                protagonist.Location += new Vector2(0, -playerSpeed);
            }
            if (ks.IsKeyDown(applicableKeys[1]))
            {
                protagonist.AddFrame(new Rectangle(60, 0, 58, 75));
                protagonist.AddFrame(new Rectangle(120, 0, 58, 75));
                protagonist.Location += new Vector2(-playerSpeed, 0);
                protagonist.FlipHorizontal = false;
            }
            if (ks.IsKeyDown(applicableKeys[2]))
            {
                protagonist.Location += new Vector2(0, playerSpeed);
            }
            if (ks.IsKeyDown(applicableKeys[3]))
            {
                protagonist.AddFrame(new Rectangle(60, 0, 58, 75));
                protagonist.AddFrame(new Rectangle(120, 0, 58, 75));
                protagonist.Location += new Vector2(playerSpeed, 0);
                protagonist.FlipHorizontal = true;
            }
            if (!ks.IsKeyDown(applicableKeys[0]) && !ks.IsKeyDown(applicableKeys[1])
                && !ks.IsKeyDown(applicableKeys[2]) && !ks.IsKeyDown(applicableKeys[3]))
            {
                protagonist.AddFrame(new Rectangle(0, 0, 58, 75));
                protagonist.Location += new Vector2(0, 0);
            }
            if (protagonist.Location.Y < 50)
                protagonist.Location = new Vector2(protagonist.Location.X, 50);
            if (protagonist.Location.Y > 446)
                protagonist.Location = new Vector2(protagonist.Location.X, 446);

            mgrcommands.CheckCommands();

            cursor.Location = new Vector2(ms.X, ms.Y);
            protagonist.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            switch (mgrrandom.RandomNumber)
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
            }
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            protagonist.Draw(spriteBatch);
            cursor.Draw(spriteBatch);
            if (mgrcommands.DebugMode)
            {
                spriteBatch.DrawString(sf, "Frame: " + Convert.ToString(protagonist.Frame), new Vector2(7, 5), Color.LightGreen);
                spriteBatch.DrawString(sf, "Location: " + Convert.ToString(protagonist.Location), new Vector2(7, 25), Color.IndianRed);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
