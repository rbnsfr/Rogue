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
    class CommandManager
    {
        private bool debugMode;
        private KeyboardState ks;

        public bool DebugMode
        {
            get { return debugMode; }
            set { debugMode = value; }
        }

        public void CheckCommands()
        {
            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.LeftControl) && ks.IsKeyDown(Keys.LeftShift) && ks.IsKeyDown(Keys.OemTilde))
                DebugMode = !DebugMode;
        }
    }
}
