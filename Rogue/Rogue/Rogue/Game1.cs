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
        Texture2D background, spritesheet, tilesheet, cursortexture, ui;
        Sprite cursor, projectile;
        Protagonist firstPlayer, secondPlayer, thirdPlayer;
        SpriteFont consbold, consnrml;
        KeyboardState ks;
        MouseState ms;
        CommandManager mgrCommands = new CommandManager();
        RandomManager mgrRandom = new RandomManager();
        BoundaryManager mgrBoundary = new BoundaryManager();
        Keys[,] movementKeys = { { Keys.W, Keys.A, Keys.S, Keys.D },
                                 { Keys.T, Keys.F, Keys.G, Keys.H },
                                 { Keys.I, Keys.J, Keys.K, Keys.L } };
        bool firstPlayerParticipating = true,
            secondPlayerParticipating = false,
            thirdPlayerParticipating = false;

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
            ui = Content.Load<Texture2D>(@"Test\Interface");
            consbold = Content.Load<SpriteFont>(@"Test\ConsoleBold");
            consnrml = Content.Load<SpriteFont>(@"Test\Console");

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

            mgrCommands.CheckCommands(); UpdateMovement(); UpdateParticipation(); CheckBoundaries();

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

            if (ks.IsKeyDown(movementKeys[0, 0]) && firstPlayerParticipating)
            {
                firstPlayer.Location += new Vector2(0, -playerSpeed);
            }
            if (ks.IsKeyDown(movementKeys[1, 0]) && secondPlayerParticipating)
            {
                secondPlayer.Location += new Vector2(0, -playerSpeed);
            }
            if (ks.IsKeyDown(movementKeys[2, 0]) && thirdPlayerParticipating)
            {
                thirdPlayer.Location += new Vector2(0, -playerSpeed);
            }

            if (ks.IsKeyDown(movementKeys[0, 1]) && firstPlayerParticipating)
            {
                firstPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                firstPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                firstPlayer.Location += new Vector2(-playerSpeed, 0);
                firstPlayer.FlipHorizontal = false;
            }
            if (ks.IsKeyDown(movementKeys[1, 1]) && secondPlayerParticipating)
            {
                secondPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                secondPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                secondPlayer.Location += new Vector2(-playerSpeed, 0);
                secondPlayer.FlipHorizontal = false;
            }
            if (ks.IsKeyDown(movementKeys[2, 1]) && secondPlayerParticipating)
            {
                thirdPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                thirdPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                thirdPlayer.Location += new Vector2(-playerSpeed, 0);
                thirdPlayer.FlipHorizontal = false;
            }

            if (ks.IsKeyDown(movementKeys[0, 2]) && firstPlayerParticipating)
            {
                firstPlayer.Location += new Vector2(0, playerSpeed);
            }
            if (ks.IsKeyDown(movementKeys[1, 2]) && secondPlayerParticipating)
            {
                secondPlayer.Location += new Vector2(0, playerSpeed);
            }
            if (ks.IsKeyDown(movementKeys[2, 2]) && thirdPlayerParticipating)
            {
                thirdPlayer.Location += new Vector2(0, playerSpeed);
            }

            if (ks.IsKeyDown(movementKeys[0, 3]) && firstPlayerParticipating)
            {
                firstPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                firstPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                firstPlayer.Location += new Vector2(playerSpeed, 0);
                firstPlayer.FlipHorizontal = true;
            }
            if (ks.IsKeyDown(movementKeys[1, 3]) && secondPlayerParticipating)
            {
                secondPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                secondPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                secondPlayer.Location += new Vector2(playerSpeed, 0);
                secondPlayer.FlipHorizontal = true;
            }
            if (ks.IsKeyDown(movementKeys[2, 3]) && thirdPlayerParticipating)
            {
                thirdPlayer.AddFrame(new Rectangle(60, 0, 58, 75));
                thirdPlayer.AddFrame(new Rectangle(120, 0, 58, 75));
                thirdPlayer.Location += new Vector2(playerSpeed, 0);
                thirdPlayer.FlipHorizontal = true;
            }

            if (!ks.IsKeyDown(movementKeys[0, 0]) && !ks.IsKeyDown(movementKeys[0, 1])
                && !ks.IsKeyDown(movementKeys[0, 2]) && !ks.IsKeyDown(movementKeys[0, 3])
                && firstPlayerParticipating)
            {
                firstPlayer.Location += new Vector2(0, 0);
            }
            if (!ks.IsKeyDown(movementKeys[1, 0]) && !ks.IsKeyDown(movementKeys[1, 1])
                && !ks.IsKeyDown(movementKeys[1, 2]) && !ks.IsKeyDown(movementKeys[1, 3])
                && secondPlayerParticipating)
            {
                secondPlayer.Location += new Vector2(0, 0);
            }
            if (!ks.IsKeyDown(movementKeys[2, 0]) && !ks.IsKeyDown(movementKeys[2, 1])
                && !ks.IsKeyDown(movementKeys[2, 2]) && !ks.IsKeyDown(movementKeys[2, 3])
                && thirdPlayerParticipating)
            {
                thirdPlayer.Location += new Vector2(0, 0);
            }
        }

        protected void UpdateParticipation()
        {
            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.D1) && !firstPlayerParticipating)
                firstPlayerParticipating = true;
            else if (ks.IsKeyDown(Keys.D1) && firstPlayerParticipating)
                firstPlayerParticipating = false;

            if (ks.IsKeyDown(Keys.D2) && !secondPlayerParticipating)
                secondPlayerParticipating = true;
            else if (ks.IsKeyDown(Keys.D2) && secondPlayerParticipating)
                secondPlayerParticipating = false;

            if (ks.IsKeyDown(Keys.D3) && !thirdPlayerParticipating)
                thirdPlayerParticipating = true;
            else if (ks.IsKeyDown(Keys.D3) && thirdPlayerParticipating)
                thirdPlayerParticipating = false;
        }

        protected void CheckBoundaries()
        {
            if (firstPlayer.Location.X < mgrBoundary.MinX)
                firstPlayer.Location = new Vector2(mgrBoundary.MinX, firstPlayer.Location.Y);
            if (firstPlayer.Location.X > mgrBoundary.MaxX)
                firstPlayer.Location = new Vector2(mgrBoundary.MaxX, firstPlayer.Location.Y);
            if (firstPlayer.Location.Y < mgrBoundary.MinY)
                firstPlayer.Location = new Vector2(firstPlayer.Location.X, mgrBoundary.MinY);
            if (firstPlayer.Location.Y > mgrBoundary.MaxY)
                firstPlayer.Location = new Vector2(firstPlayer.Location.X, mgrBoundary.MaxY);

            if (secondPlayer.Location.X < mgrBoundary.MinX)
                secondPlayer.Location = new Vector2(mgrBoundary.MinX, secondPlayer.Location.Y);
            if (secondPlayer.Location.X > mgrBoundary.MaxX)
                secondPlayer.Location = new Vector2(mgrBoundary.MaxX, secondPlayer.Location.Y);
            if (secondPlayer.Location.Y < mgrBoundary.MinY)
                secondPlayer.Location = new Vector2(secondPlayer.Location.X, mgrBoundary.MinY);
            if (secondPlayer.Location.Y > mgrBoundary.MaxY)
                secondPlayer.Location = new Vector2(secondPlayer.Location.X, mgrBoundary.MaxY);

            if (thirdPlayer.Location.X < mgrBoundary.MinX)
                thirdPlayer.Location = new Vector2(mgrBoundary.MinX, thirdPlayer.Location.Y);
            if (thirdPlayer.Location.X > mgrBoundary.MaxX)
                thirdPlayer.Location = new Vector2(mgrBoundary.MaxX, thirdPlayer.Location.Y);
            if (thirdPlayer.Location.Y < mgrBoundary.MinY)
                thirdPlayer.Location = new Vector2(thirdPlayer.Location.X, mgrBoundary.MinY);
            if (thirdPlayer.Location.Y > mgrBoundary.MaxY)
                thirdPlayer.Location = new Vector2(thirdPlayer.Location.X, mgrBoundary.MaxY);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            if (firstPlayerParticipating)
                firstPlayer.Draw(spriteBatch);
            secondPlayer.TintColor = Color.OrangeRed;
            if (secondPlayerParticipating)
                secondPlayer.Draw(spriteBatch);
            thirdPlayer.TintColor = Color.CornflowerBlue;
            if (thirdPlayerParticipating)
                thirdPlayer.Draw(spriteBatch);
            cursor.Draw(spriteBatch);
            if (mgrCommands.DebugMode)
            {
                spriteBatch.Draw(ui, new Rectangle(0, 0, 225, 300), Color.White);
                spriteBatch.DrawString(consbold, "Player 1", new Vector2(7, 5), Color.White);
                spriteBatch.DrawString(consnrml, "Participating: " + Convert.ToString(firstPlayerParticipating), new Vector2(7, 25), Color.White);
                spriteBatch.DrawString(consnrml, "Frame: " + Convert.ToString(firstPlayer.Frame), new Vector2(7, 45), Color.White);
                spriteBatch.DrawString(consnrml, "Location: " + Convert.ToString(firstPlayer.Location), new Vector2(7, 65), Color.White);

                spriteBatch.DrawString(consbold, "Player 2", new Vector2(7, 105), Color.White);
                spriteBatch.DrawString(consnrml, "Participating: " + Convert.ToString(secondPlayerParticipating), new Vector2(7, 125), Color.White);
                spriteBatch.DrawString(consnrml, "Frame: " + Convert.ToString(secondPlayer.Frame), new Vector2(7, 145), Color.White);
                spriteBatch.DrawString(consnrml, "Location: " + Convert.ToString(secondPlayer.Location), new Vector2(7, 165), Color.White);

                spriteBatch.DrawString(consbold, "Player 3", new Vector2(7, 205), Color.White);
                spriteBatch.DrawString(consnrml, "Participating: " + Convert.ToString(thirdPlayerParticipating), new Vector2(7, 225), Color.White);
                spriteBatch.DrawString(consnrml, "Frame: " + Convert.ToString(thirdPlayer.Frame), new Vector2(7, 245), Color.White);
                spriteBatch.DrawString(consnrml, "Location: " + Convert.ToString(thirdPlayer.Location), new Vector2(7, 265), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
