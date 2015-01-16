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
        private bool dead, attacking;

        public float AttackDamage
        {
            get { return attackDamage; }
            set { attackDamage = value; }
        }
        
        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }

        public bool Attacking
        {
            get { return attacking; }
            set { attacking = value; }
        }
    }
}
