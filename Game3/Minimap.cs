using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    class Minimap
    {
        public static int lowestX = 10000, lowestY = 10000, highestX = 0, highestY = 0;
        public static List<MinimapRoom> minimapRooms = new List<MinimapRoom>();
        static Texture2D texture = Game1.wallTexture;
        static int offsetY = 0, offsetX = 0;
        static int minimaproomint;

        public static void MinimapDebug()
        {
            minimaproomint = 0;
            string debugRoomStr = "";
            for (int y = lowestY; y <= highestY; y++)
            {
                for (int x = lowestX; x <= highestX; x++)
                {
                    if (ProcGen2.roomNodes[x, y] == null)
                    {
                        debugRoomStr += " ";
                    }
                    else if (x == RoomShower.playerRoomX && y == RoomShower.playerRoomY)
                    {
                        debugRoomStr += "X";
                        minimapRooms.Add(new MinimapRoom(x - lowestX, y - lowestY, x, y));
                        minimaproomint++;
                    }
                    else
                    {
                        debugRoomStr += "#";
                        minimapRooms.Add(new MinimapRoom(x - lowestX, y - lowestY, x, y));
                        minimaproomint++;
                    }
                    
                    
                }
                debugRoomStr += "\n";
            }
            Game1.minirooms = minimapRooms;
            Console.WriteLine("minirooms " + minimaproomint);
            //Console.WriteLine(debugRoomStr);
        }
        public static void GenerateMinimap()
        {

            foreach (Room room in ProcGen2.roomNodes)
            {
                if (room != null)
                {
                    if (room.posX > highestX)
                        highestX = room.posX;
                    if (room.posX < lowestX)
                        lowestX = room.posX;
                    if (room.posY > highestY)
                        highestY = room.posY;
                    if (room.posY < lowestY)
                        lowestY = room.posY;
                }
            }
            string debugString = "Highest X = " + highestX.ToString() + " Lowest X = " + lowestX.ToString() + " Highest Y = " + highestY.ToString() + " Lowest Y = " + lowestY.ToString();
            Console.WriteLine(debugString);
            
           


            //Console.WriteLine("minirooms = " + minimaproomint);

            //offsetX = 0;
            //offsetY = 300;
            //for (int y = 0; y < minimapRooms.GetUpperBound(1); y++)
            //{
            //    offsetX = 0;
            //    offsetY += 16;
            //    for (int x = 0; x < minimapRooms.GetUpperBound(0); x++)
            //    {
            //        offsetX += 16;
            //        if (minimapRooms[x, y] != null)
            //        {
            //            Game1.minirooms.Add(new MinimapRoom(texture, offsetX, offsetY, x, y));
            //        }

            //    }
            //}
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
