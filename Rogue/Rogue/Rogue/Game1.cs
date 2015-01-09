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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, spritesheet, tilesheet, cursortexture, ui;
        Sprite cursor, projectile;
        SpriteFont consbold, consnrml;
        KeyboardState oldks, ks;
        MouseState ms;
        Mode mode = new Mode();
        RandomManager mgrRandom = new RandomManager();
        BoundaryManager mgrBoundary = new BoundaryManager();
        Protagonist[] protagonists = new Protagonist[3];
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
            oldks = Keyboard.GetState();
            protagonists[0].Participating = true;
            protagonists[0].Sprinting = false;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>(@"Test\Caverns");
            tilesheet = Content.Load<Texture2D>(@"Test\Tilesheet");
            spritesheet = Content.Load<Texture2D>(@"Test\Spritesheet");
            cursortexture = Content.Load<Texture2D>(@"Test\Cursor");
            ui = Content.Load<Texture2D>(@"Test\Interface");
            consbold = Content.Load<SpriteFont>(@"Test\ConsoleBold");
            consnrml = Content.Load<SpriteFont>(@"Test\Console");

            protagonists[0] = new Protagonist(new Vector2(200, 300), spritesheet, new Rectangle(8, 0, 57, 75), Vector2.Zero, 1);
            protagonists[1] = new Protagonist(new Vector2(300, 300), spritesheet, new Rectangle(8, 0, 57, 75), Vector2.Zero, 1);
            protagonists[2] = new Protagonist(new Vector2(400, 300), spritesheet, new Rectangle(8, 0, 57, 75), Vector2.Zero, 1);

            cursor = new Sprite(Vector2.Zero, cursortexture, new Rectangle(0, 0, 50, 50), Vector2.Zero, 0.4f);
            projectile = new Sprite(Vector2.Zero, spritesheet, new Rectangle(228, 9, 15, 15), Vector2.Zero, 1);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            CheckCommands();
            UpdateMovement();
            CheckBoundaries();

            ms = Mouse.GetState();
            cursor.Location = new Vector2(ms.X, ms.Y);

            for (int i = 0; i < protagonists.Length; i++)
                protagonists[i].Update(gameTime);

            base.Update(gameTime);
        }

        protected void UpdateMovement()
        {
            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.LeftShift))
            {
                protagonists[0].Sprinting = !protagonists[0].Sprinting;
            }

            if (protagonists[0].Sprinting)
            {
                protagonists[0].Speed = 4;
            }
            else
            {
                protagonists[0].Speed = 2;
            }

            for (int i = 0; i < protagonists.Length; i++)
            {
                protagonists[i].State = ProtagonistStates.Standing;

                if (protagonists[i].Participating)
                {
                    if (ks.IsKeyDown(movementKeys[i, 0]))
                    {
                        protagonists[i].Location += new Vector2(0, -protagonists[0].Speed);
                    }

                    if (ks.IsKeyDown(movementKeys[i, 1]))
                    {
                        protagonists[i].State = ProtagonistStates.Walking;
                        protagonists[i].Location += new Vector2(-protagonists[0].Speed, 0);
                        protagonists[i].FlipHorizontal = false;
                    }

                    if (ks.IsKeyDown(movementKeys[i, 2]))
                    {
                        protagonists[i].Location += new Vector2(0, protagonists[0].Speed);
                    }

                    if (ks.IsKeyDown(movementKeys[i, 3]))
                    {
                        protagonists[i].State = ProtagonistStates.Walking;
                        protagonists[i].Location += new Vector2(protagonists[0].Speed, 0);
                        protagonists[i].FlipHorizontal = true;
                    }

                    if (!ks.IsKeyDown(movementKeys[i, 0]) && !ks.IsKeyDown(movementKeys[i, 1])
                        && !ks.IsKeyDown(movementKeys[i, 2]) && !ks.IsKeyDown(movementKeys[i, 3]))
                    {
                        protagonists[i].Location += new Vector2(0, 0);
                    }
                }
            }
        }

        protected void CheckCommands()
        {
            if (ks.IsKeyDown(Keys.LeftControl) && ks.IsKeyDown(Keys.LeftShift)
                && ks.IsKeyDown(Keys.OemTilde)) { /* Checks to see if keys have just been released */ }
            else if (oldks.IsKeyDown(Keys.LeftControl) && oldks.IsKeyDown(Keys.LeftShift)
                && oldks.IsKeyDown(Keys.OemTilde)) { mode.DebugMode = !mode.DebugMode; }
            if (ks.IsKeyDown(Keys.D1)) { }
            else if (oldks.IsKeyDown(Keys.D1)) { protagonists[0].Participating = !protagonists[0].Participating; }
            if (ks.IsKeyDown(Keys.D2)) { }
            else if (oldks.IsKeyDown(Keys.D2)) { protagonists[1].Participating = !protagonists[1].Participating; }
            if (ks.IsKeyDown(Keys.D3)) { }
            else if (oldks.IsKeyDown(Keys.D3)) { protagonists[2].Participating = !protagonists[2].Participating; }

            oldks = ks;
        }

        protected void CheckBoundaries()
        {
            for (int i = 0; i < protagonists.Length; i++)
            {
                if (protagonists[i].Location.X < mgrBoundary.MinX)
                    protagonists[i].Location = new Vector2(mgrBoundary.MinX, protagonists[i].Location.Y);
                if (protagonists[i].Location.X > mgrBoundary.MaxX)
                    protagonists[i].Location = new Vector2(mgrBoundary.MaxX, protagonists[i].Location.Y);
                if (protagonists[i].Location.Y < mgrBoundary.MinY)
                    protagonists[i].Location = new Vector2(protagonists[i].Location.X, mgrBoundary.MinY);
                if (protagonists[i].Location.Y > mgrBoundary.MaxY)
                    protagonists[i].Location = new Vector2(protagonists[i].Location.X, mgrBoundary.MaxY);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            protagonists[1].TintColor = Color.OrangeRed;
            protagonists[2].TintColor = Color.CornflowerBlue;
            for (int i = 0; i < protagonists.Length; i++)
                if (protagonists[i].Participating)
                    protagonists[i].Draw(spriteBatch);
            if (mode.DebugMode)
            {
                cursor.Draw(spriteBatch);
                spriteBatch.Draw(ui, new Rectangle(0, 0, 225, 340), Color.White);
                for (int i = 0; i < protagonists.Length; i++)
                {
                    spriteBatch.DrawString(consbold, "Player " + (i + 1), new Vector2(7, 5 + (110 * i)), Color.White);
                    spriteBatch.DrawString(consnrml, "Participating: " + Convert.ToString(protagonists[i].Participating), new Vector2(7, 25 + (110 * i)), Color.White);
                    spriteBatch.DrawString(consnrml, "Sprinting: " + Convert.ToString(protagonists[i].Sprinting), new Vector2(7, 45 + (110 * i)), Color.White);
                    spriteBatch.DrawString(consnrml, "Frame: " + Convert.ToString(protagonists[i].Frame), new Vector2(7, 65 + (110 * i)), Color.White);
                    spriteBatch.DrawString(consnrml, "Location: " + Convert.ToString(protagonists[i].Location), new Vector2(7, 85 + (110 * i)), Color.White);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
