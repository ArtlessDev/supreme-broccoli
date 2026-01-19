using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.InputHandler
{
    public static class BasicInputs
    {
        //public static Time 
        public static bool HandleKeyDown(Keys inputOne, Keys inputTwo)
        {
            if (Globals.keyb.IsKeyDown(inputOne) || Globals.keyb.IsKeyDown(inputTwo))
                return true;

            return false;
        }
    }
}
