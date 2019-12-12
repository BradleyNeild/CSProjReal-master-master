using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game3
{
    public class RoomFeatures
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


        public static List<Slime> GenerateSlimes(Room room)
        {

            int number = Game1.random.Next(2);
            List<Slime> slimeSpawns = new List<Slime>();
            if (number == 0)
            {
                Game1.objectHandler.AddObject(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(1 * Walls.wallSize, 1 * Walls.wallSize), 0, true, room, null));
                Game1.objectHandler.AddObject(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(1 * Walls.wallSize, 2 * Walls.wallSize), 0, true, room, null));
                Game1.objectHandler.AddObject(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(2 * Walls.wallSize, 1 * Walls.wallSize), 0, true, room, null));
                Game1.objectHandler.AddObject(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(12 * Walls.wallSize, 1 * Walls.wallSize), 0, true, room, null));
                Game1.objectHandler.AddObject(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(13 * Walls.wallSize, 1 * Walls.wallSize), 0, true, room, null));
                Game1.objectHandler.AddObject(new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), new Point(13 * Walls.wallSize, 2 * Walls.wallSize), 0, true, room, null));
            }
            if (number == 1)
            {
                Game1.objectHandler.AddObject(new Slime(5, 5, 1, new Rectangle(0, 0, 64, 64), new Point(7 * Walls.wallSize, 4 * Walls.wallSize), Game1.random.Next(3, 5), true, room, new Slime(1, 1, 1, new Rectangle(0, 0, 32, 32), Point.Zero, 0, false, room, null)));
            }
            if (number == 2)
            {

            }
            return slimeSpawns;

        }
    }
}
