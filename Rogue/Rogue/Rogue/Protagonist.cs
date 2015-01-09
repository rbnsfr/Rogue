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
    enum ProtagonistStates { Standing, Walking }

    class Protagonist : Sprite
    {
        private ProtagonistStates state;

        public Protagonist(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity,
            float relativeSize) : base(location, texture, initialFrame, velocity, relativeSize)
        {
            AddFrame("walking", new Rectangle(60, 0, 58, 75));
            AddFrame("walking", new Rectangle(120, 0, 58, 75));

            //this.State = ProtagonistStates.Standing;
            this.FrameTime = 0.1f;
        }

        public ProtagonistStates State
        {
            get { return this.state; }
            set
            {
                state = value;
                this.Frame = 0;

                switch (state)
                {
                    case ProtagonistStates.Standing:
                        this.CurrentAnimation = "default";
                        break;

                    case ProtagonistStates.Walking:
                        this.CurrentAnimation = "walking";
                        break;
                }
            }
        }

        private bool participating, sprinting;
        private float speed;

        public void FireProjectile()
        {
        }

        public bool Participating
        {
            get { return participating; }
            set { participating = value; }
        }

        public bool Sprinting
        {
            get { return sprinting; }
            set { sprinting = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
    }
}