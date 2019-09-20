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
    class RoomShower
    {
        static int roomOffset = 150;
        public static Room playerRoom = ProcGen2.roomNodes[50, 50];
        public static int playerRoomX = 50, playerRoomY = 50;
        public static Room nextRoom;
        public static int nextRoomX = 50, nextRoomY = 50;
        public static List<int> doorNums = new List<int>();
        static int[,] wall2DArray = new int[9, 15]
        {
            {1,1,1,1,1,1,1,2,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {5,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,4,1,1,1,1,1,1,1},
        };

        public static void CreateWall(int wallPosX, int wallPosY)
        {
            Game1.walls.Add(new Walls(new Rectangle(wallPosY * 45 + roomOffset, wallPosX * 45 + roomOffset, 0, 0), Game1.wallTexture));
        }

        public static void CreateDoor(int wallPosX, int wallPosY, int doorDirection)
        {
            Game1.doors.Add(new Doors(new Rectangle(wallPosY * 45 + roomOffset, wallPosX * 45 + roomOffset, 0, 0), Game1.doorTexture, doorDirection));
            Console.WriteLine("Door created");
        }

        public static void spawnRoom()
        {
            if (playerRoom.doorN)
            {
                doorNums.Add(2);
            }
            if (playerRoom.doorE)
            {
                doorNums.Add(3);
            }
            if (playerRoom.doorS)
            {
                doorNums.Add(4);
            }
            if (playerRoom.doorW)
            {
                doorNums.Add(5);
            }

            for (int i = 0; i <= wall2DArray.GetUpperBound(0); i++)
            {
                for (int x = 0; x <= wall2DArray.GetUpperBound(1); x++)
                {
                    if (wall2DArray[i, x] != 0)
                    {
                        if (wall2DArray[i, x] == 1)
                        {
                            CreateWall(i, x);
                        }

                        else if (doorNums.Contains(wall2DArray[i, x]))
                        {
                            int doorDir = wall2DArray[i, x];
                            CreateDoor(i, x, doorDir);
                        }
                        else
                        {
                            CreateWall(i, x);
                        }
                    }
                }
            }
        }

    }
}
