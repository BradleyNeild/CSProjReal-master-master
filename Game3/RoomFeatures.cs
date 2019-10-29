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

        public static List<Goblin> GenerateGoblins(int number)
        {
            List<Goblin> goblinSpawns = new List<Goblin>();
            if (number == 0)
            {
                goblinSpawns.Add(new Goblin(2, 2, 1, Game1.enemyTexture, new Rectangle(0, 0, 30, 30), new Rectangle(197, 197, 1, 1)));
                goblinSpawns.Add(new Goblin(2, 2, 1, Game1.enemyTexture, new Rectangle(0, 0, 30, 30), new Rectangle(747, 197, 1, 1)));
            }
            if (number == 1)
            {
                goblinSpawns.Add(new Goblin(3, 3, 1, Game1.enemyTexture, new Rectangle(0, 0, 40, 40), new Rectangle(495, 350, 1, 1)));
            }
            if (number == 2)
            {
                
            }
            return goblinSpawns;

        }
    }
}
