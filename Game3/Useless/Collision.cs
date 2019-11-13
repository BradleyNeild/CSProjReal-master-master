using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game3
{
    class Collision
    {
        bool debug = false;
        public bool CollisionCheck(Rectangle collider1, Rectangle collider2, string colliderName1, string colliderName2)
        {
            if (collider1.Intersects(collider2) && collider1 != collider2)
            {
                
                if (debug)
                {
                    Console.WriteLine(colliderName1 + " COLLIDED WITH " + colliderName2 + collider1 + collider2);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
