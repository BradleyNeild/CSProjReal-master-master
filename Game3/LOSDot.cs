using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class LOSDot
    {
        public Vector2 position;
        public Goblin connectedGoblin;
        public Rectangle bounds;
        public LOSDot(Goblin LOSConnctedGoblin)
        {
            connectedGoblin = LOSConnctedGoblin;
            position.X = LOSConnctedGoblin.bounds.Center.X;
            position.Y = LOSConnctedGoblin.bounds.Center.Y;
        }
    }
}
