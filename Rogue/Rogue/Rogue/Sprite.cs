using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Rogue
{
    class Sprite
    {
        public List<Rectangle> frames = new List<Rectangle>();
        private Vector2 location;
        private Vector2 velocity;
        private Texture2D texture;
        private Rectangle initialFrame;
        private bool isDead;
        private float relativeSize;
        private Color tintColor;
        KeyboardState ks;

        public Sprite(Texture2D texture, Vector2 location, Vector2 velocity, Rectangle initialFrame)
        {
            this.location = location;
            this.texture = texture;
            this.velocity = velocity;
            this.initialFrame = initialFrame;
        }

        public Vector2 Location
        {
            get { return location;}
            set { location = value; }
        }

        public bool IsWalking()
        {
            if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.A) ||
                ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.D))
                return true;
            else
                return false;
        }

        public bool IsAttacking()
        {
            if (ks.IsKeyDown(Keys.Space))
                return true;
            else
                return false;
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        public float RelativeSize
        {
            get { return relativeSize; }
            set { relativeSize = value; }
        }

        public Color TintColor
        {
            get { return tintColor; }
            set { tintColor = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Rectangle InitialFrame
        {
            get { return initialFrame; }
            set { initialFrame = value; }
        }

        public void AddFrame(Rectangle frameRectangle)
        {
            frames.Add(frameRectangle);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Vector2.Zero, InitialFrame, TintColor);
        }
    }
}
