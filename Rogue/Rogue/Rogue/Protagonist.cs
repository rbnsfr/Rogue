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
    class Protagonist : Sprite
    {
        public Protagonist(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity,
            float relativeSize) : base(location, texture, initialFrame, velocity, relativeSize)
        {
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