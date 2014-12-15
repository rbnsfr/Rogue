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
    enum ProtoStates
    {
        Standing,
        Walking
    }

    class Protagonist : Sprite
    {
        private ProtoStates state;

        public Protagonist(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity,
            float relativeSize) : base(location, texture, initialFrame, velocity, relativeSize)
        {
            AddFrame("walking", new Rectangle(60, 0, 58, 75));
            AddFrame("walking", new Rectangle(120, 0, 58, 75));

            this.State = ProtoStates.Standing;
            this.FrameTime = 0.6f;
        }

        public ProtoStates State
        {
            get { return this.state; }
            set
            {
                state = value;
                this.currentFrame = 0;

                switch (state)
                {
                    case ProtoStates.Standing:
                        this.CurrentAnimation = "default";
                        break;

                    case ProtoStates.Walking:
                        this.CurrentAnimation = "walking";
                        break;
                }
            }
        }

        KeyboardState ks;
        private bool participating;

        public void FireProjectile()
        {
            if (ks.IsKeyDown(Keys.Up))
            {

            }
        }

        public bool Participating
        {
            get { return participating; }
            set { participating = value; }
        }
    }
}