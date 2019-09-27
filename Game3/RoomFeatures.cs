using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game3
{
    class RoomFeatures
    {
        public List<Goblin> goblinSpawns;


        public RoomFeatures(List<Goblin> roomGoblinSpawns)
        {
            int randType = Game1.random.Next(5);
            if (randType == 0)
            {

            }
        }
    }
}
