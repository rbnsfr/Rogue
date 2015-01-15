using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rogue
{
    class Sprite
    {
        public Texture2D Texture;

        protected Dictionary<string, List<Rectangle>> frames = new Dictionary<string,List<Rectangle>>();
        private int frameWidth = 0;
        private int frameHeight = 0;
        protected int currentFrame;
        private float frameTime = 0.1f;
        private float timeForCurrentFrame = 0.0f;

        private Color tintColor = Color.White;
        private float rotation = 0.0f;
        private bool flipHorizontal;

        public int CollisionRadius = 0;
        public int BoundingXPadding = 0;
        public int BoundingYPadding = 0;

        protected Vector2 location = Vector2.Zero;
        protected Vector2 velocity = Vector2.Zero;
        protected float relativeSize;

        private String currentAnimation = "default";

        public String CurrentAnimation
        {
            get
            {
                return currentAnimation;
            }
            set
            {
                if (frames.ContainsKey(value))
                {
                    currentAnimation = value;
                }
            }
        }

        public Sprite(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity,
            float relativeSize)
        {
            this.location = location;
            Texture = texture;
            this.velocity = velocity;
            this.relativeSize = relativeSize;

            frames.Add("default", new List<Rectangle>());
            frames[currentAnimation].Add(initialFrame);
            frameWidth = initialFrame.Width;
            frameHeight = initialFrame.Height;
        }

        public Sprite(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity)
        {
            this.location = location;
            Texture = texture;
            this.velocity = velocity;
            this.relativeSize = 1;

            frames.Add("default", new List<Rectangle>());
            frames[currentAnimation].Add(initialFrame);
            frameWidth = initialFrame.Width;
            frameHeight = initialFrame.Height;
        }

        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Color TintColor
        {
            get { return tintColor; }
            set { tintColor = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value % MathHelper.TwoPi; }
        }

        public int Frame
        {
            get { return currentFrame; }
            set
            {
                currentFrame = (int)MathHelper.Clamp(value, 0,
                frames[currentAnimation].Count - 1);
            }
        }

        public float FrameTime
        {
            get { return frameTime; }
            set { frameTime = MathHelper.Max(0, value); }
        }

        public int NumberOfFrames
        {
            get { return frames[currentAnimation].Count; }
        }

        public Rectangle Source
        {
            get { return frames[currentAnimation][currentFrame]; }
        }

        public Rectangle Destination
        {
            get
            {
                return new Rectangle(
                    (int)location.X,
                    (int)location.Y,
                    frameWidth,
                    frameHeight);
            }
        }

        public Vector2 Center
        {
            get
            {
                return location +
                    new Vector2(frameWidth / 2, frameHeight / 2);
            }
        }

        public Rectangle BoundingBoxRect
        {
            get
            {
                return new Rectangle(
                    (int)location.X + BoundingXPadding,
                    (int)location.Y + BoundingYPadding,
                    frameWidth - (BoundingXPadding * 2),
                    frameHeight - (BoundingYPadding * 2));
            }
        }

        public bool IsBoxColliding(Rectangle OtherBox)
        {
            return BoundingBoxRect.Intersects(OtherBox);
        }

        public bool FlipHorizontal
        {
            get { return flipHorizontal; }
            set { flipHorizontal = value; }
        }

        public bool IsCircleColliding(Vector2 otherCenter, float otherRadius)
        {
            if (Vector2.Distance(Center, otherCenter) <
                (CollisionRadius + otherRadius))
                return true;
            else
                return false;
        }

        public float RelativeSize
        {
            get { return relativeSize; }
            set { relativeSize = value; }
        }

        public void AddFrame(String animationKey, Rectangle frameRectangle)
        {
            if (!frames.ContainsKey(animationKey))
                frames.Add(animationKey, new List<Rectangle>());

            frames[animationKey].Add(frameRectangle);
        }

        public virtual void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            timeForCurrentFrame += elapsed;

            if (timeForCurrentFrame >= FrameTime)
            {
                currentFrame = (currentFrame + 1) % (frames[currentAnimation].Count);
                timeForCurrentFrame = 0.0f;
            }

            location += (velocity * elapsed);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Center,
                Source,
                tintColor,
                rotation,
                new Vector2(frameWidth / 2, frameHeight / 2),
                relativeSize,
                flipHorizontal ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0.0f);
        }

    }
}
