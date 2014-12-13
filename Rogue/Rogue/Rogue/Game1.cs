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
        Protagonist firstPlayer, secondPlayer, thirdPlayer;
        SpriteFont sf;
        KeyboardState ks;
        MouseState ms;
        ManageCommands mgrcommands = new ManageCommands();
        ManageRandom mgrrandom = new ManageRandom();
        Keys[,] movementKeys = { { Keys.W, Keys.A, Keys.S, Keys.D },
                                 { Keys.T, Keys.F, Keys.G, Keys.H },
                                 { Keys.I, Keys.J, Keys.K, Keys.L } };

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

            firstPlayer = new Protagonist(new Vector2(200, 300), spritesheet, new Rectangle(8, 0, 57, 75), Vector2.Zero, 1);
            secondPlayer = new Protagonist(new Vector2(300, 300), spritesheet, new Rectangle(8, 0, 57, 75), Vector2.Zero, 1);
            thirdPlayer = new Protagonist(new Vector2(400, 300), spritesheet, new Rectangle(8, 0, 57, 75), Vector2.Zero, 1);
            cursor = new Sprite(Vector2.Zero, cursortexture, new Rectangle(0, 0, 50, 50), Vector2.Zero, 0.4f);
            projectile = new Sprite(Vector2.Zero, spritesheet, new Rectangle(228, 9, 15, 15), Vector2.Zero, 1);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (firstPlayer.Location.Y < 50)
                firstPlayer.Location = new Vector2(firstPlayer.Location.X, 50);
            if (firstPlayer.Location.Y > 446)
                firstPlayer.Location = new Vector2(firstPlayer.Location.X, 446);

            mgrcommands.CheckCommands();
            UpdateMovement();

            ms = Mouse.GetState();
            cursor.Location = new Vector2(ms.X, ms.Y);

            firstPlayer.Update(gameTime);
            secondPlayer.Update(gameTime);
            thirdPlayer.Update(gameTime);
            base.Update(gameTime);
        }

        protected void UpdateMovement()
        {
            ks = Keyboard.GetState();

            float playerSpeed;
            if (ks.IsKeyDown(Keys.LeftShift))
                playerSpeed = 4;
            else
                playerSpeed = 2;

            if (ks.IsKeyDown(movementKeys[0, 0]))
            {
                firstPlayer.Location += new Vector2(0, -playerSpeed);
            }
            if (ks.IsKeyDown(movementKeys[1, 0]))
            {
                secondPlayer.Location += new Vector2(0, -playerSpeed);
            }
            if (ks.IsKeyDown(movementKeys[2, 0]))
            {
                thirdPlayer.Location += new Vector2(0, -playerSpeed);
            }

            if (ks.IsKeyDown(movementKeys[0, 1]))
            {
                firstPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                firstPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                firstPlayer.Location += new Vector2(-playerSpeed, 0);
                firstPlayer.FlipHorizontal = false;
            }
            if (ks.IsKeyDown(movementKeys[1, 1]))
            {
                secondPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                secondPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                secondPlayer.Location += new Vector2(-playerSpeed, 0);
                secondPlayer.FlipHorizontal = false;
            }
            if (ks.IsKeyDown(movementKeys[2, 1]))
            {
                thirdPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                thirdPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                thirdPlayer.Location += new Vector2(-playerSpeed, 0);
                thirdPlayer.FlipHorizontal = false;
            }

            if (ks.IsKeyDown(movementKeys[0, 2]))
            {
                firstPlayer.Location += new Vector2(0, playerSpeed);
            }
            if (ks.IsKeyDown(movementKeys[1, 2]))
            {
                secondPlayer.Location += new Vector2(0, playerSpeed);
            }
            if (ks.IsKeyDown(movementKeys[2, 2]))
            {
                thirdPlayer.Location += new Vector2(0, playerSpeed);
            }

            if (ks.IsKeyDown(movementKeys[0, 3]))
            {
                firstPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                firstPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                firstPlayer.Location += new Vector2(playerSpeed, 0);
                firstPlayer.FlipHorizontal = true;
            }
            if (ks.IsKeyDown(movementKeys[1, 3]))
            {
                secondPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                secondPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                secondPlayer.Location += new Vector2(playerSpeed, 0);
                secondPlayer.FlipHorizontal = true;
            }
            if (ks.IsKeyDown(movementKeys[2, 3]))
            {
                thirdPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                thirdPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                thirdPlayer.Location += new Vector2(playerSpeed, 0);
                thirdPlayer.FlipHorizontal = true;
            }

            if (!ks.IsKeyDown(movementKeys[0, 0]) && !ks.IsKeyDown(movementKeys[0, 1])
                && !ks.IsKeyDown(movementKeys[0, 2]) && !ks.IsKeyDown(movementKeys[0, 3]))
            {
                firstPlayer.Location += new Vector2(0, 0);
            }
            if (!ks.IsKeyDown(movementKeys[1, 0]) && !ks.IsKeyDown(movementKeys[1, 1])
                && !ks.IsKeyDown(movementKeys[1, 2]) && !ks.IsKeyDown(movementKeys[1, 3]))
            {
                secondPlayer.Location += new Vector2(0, 0);
            }
            if (!ks.IsKeyDown(movementKeys[2, 0]) && !ks.IsKeyDown(movementKeys[2, 1])
                && !ks.IsKeyDown(movementKeys[2, 2]) && !ks.IsKeyDown(movementKeys[2, 3]))
            {
                thirdPlayer.Location += new Vector2(0, 0);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            firstPlayer.Draw(spriteBatch);
            secondPlayer.TintColor = Color.OrangeRed;
            secondPlayer.Draw(spriteBatch);
            thirdPlayer.TintColor = Color.CornflowerBlue;
            thirdPlayer.Draw(spriteBatch);
            cursor.Draw(spriteBatch);
            if (mgrcommands.DebugMode)
            {
                spriteBatch.DrawString(sf, "PLAYER 1 Frame: " + Convert.ToString(firstPlayer.Frame), new Vector2(7, 5), Color.LightGreen);
                spriteBatch.DrawString(sf, "PLAYER 1 Location: " + Convert.ToString(firstPlayer.Location), new Vector2(7, 25), Color.IndianRed);
                spriteBatch.DrawString(sf, "PLAYER 2 Frame: " + Convert.ToString(secondPlayer.Frame), new Vector2(7, 45), Color.LightGreen);
                spriteBatch.DrawString(sf, "PLAYER 2 Location: " + Convert.ToString(secondPlayer.Location), new Vector2(7, 65), Color.IndianRed);
                spriteBatch.DrawString(sf, "PLAYER 3 Frame: " + Convert.ToString(thirdPlayer.Frame), new Vector2(7, 85), Color.LightGreen);
                spriteBatch.DrawString(sf, "PLAYER 3 Location: " + Convert.ToString(thirdPlayer.Location), new Vector2(7, 105), Color.IndianRed);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
