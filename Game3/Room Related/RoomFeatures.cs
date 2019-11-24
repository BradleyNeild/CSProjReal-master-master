using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game3
{
    class RoomFeatures
    {



        public RoomFeatures()
        {

        }

        private void GenerateFeatures()
        {

        }

        private void SpawnShop(Room room)
        {

        }


        public static List<Slime> GenerateSlimes()
        {
            int number = Game1.random.Next(2);
            List<Slime> slimeSpawns = new List<Slime>();
            if (number == 0)
            {
                slimeSpawns.Add(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(197, 197), 0, null, true));
                slimeSpawns.Add(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(250, 197), 0, null, true));
                slimeSpawns.Add(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(197, 250), 0, null, true));
                slimeSpawns.Add(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(747, 250), 0, null, true));
                slimeSpawns.Add(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(747, 197), 0, null, true));
                slimeSpawns.Add(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(675, 197), 0, null, true));
            }
            if (number == 1)
            {
                slimeSpawns.Add(new Slime(5, 5, 1, new Rectangle(0, 0, 64, 64), new Point(440, 310), Game1.random.Next(3,5), new Slime(1, 1, 1, new Rectangle(0, 0, 32, 32), Point.Zero, 0, null, false), true));
            }
            if (number == 2)
            {
                
            }
            return slimeSpawns;

        }
    }
}
