using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rogue
{
    class RandomManager
    {
        private Random rand = new Random();
        private int randomnumber { get { return rand.Next(0, 4); } }
        public int RandomNumber { get { return randomnumber; } }
        // Random numbers cannot be explicitly set, only read.
    }
}
