using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.CustomPhysics
{
    public static class CorePhysics
    {
        public static float GravitationalForce = 9.68f;


        public static Vector2 CalculateJump(AnyObject anyObject)
        {

            //anyObject.mass;

            return new Vector2 (0, 0);
        }
    }
}
