using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Game3
{
    public class TileHandler
    {
        public static void GenerateTiles()
        {
            int x = 45 + RoomShower.roomOffset;
            int y = 45 + RoomShower.roomOffset;
            for (int i = 0; i < RoomShower.wall2DArray.GetUpperBound(0)-1; i++)
            {
                for (int j = 0; j < RoomShower.wall2DArray.GetUpperBound(1) - 1; j++)
                {
                    Game1.objectHandler.AddObject(new FloorTile(new Rectangle(x, y, 45, 45)));
                    x += 45;
                    
                }
                x = 45 + RoomShower.roomOffset;
                y += 45;
            }
        }
    }
}
