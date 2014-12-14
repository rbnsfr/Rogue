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
    class Enemy : Sprite
    {
        public Enemy(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity,
            float relativeSize) : base(location, texture, initialFrame, velocity, relativeSize)
        {
        }

        private float attackDamage;
        private bool isDead;

        public float AttackDamage
        {
            get { return attackDamage; }
            set { attackDamage = value; }
        }
        
        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }
    }
}
