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
    class BoundaryManager
    {
        private int minX = 52;
        private int maxX = 684;
        private int minY = 50;
        private int maxY = 446;

        public int MinX { get { return minX; } }
        public int MaxX { get { return maxX; } }
        public int MinY { get { return minY; } }
        public int MaxY { get { return maxY; } }
    }
}
