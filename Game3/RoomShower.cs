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
        public static Room playerRoom;
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

        public static void ClearRoom()
        {
            Game1.walls.Clear();
            Game1.doors.Clear();
        }

        public static void CreateWall(int wallPosX, int wallPosY)
        {
            Game1.walls.Add(new Walls(new Rectangle(wallPosY * 45 + roomOffset, wallPosX * 45 + roomOffset, 0, 0), Game1.wallTexture));
        }

        public static void CreateDoor(int wallPosX, int wallPosY, int doorDirection)
        {
            Game1.doors.Add(new Doors(new Rectangle(wallPosY * 45 + roomOffset - 1, wallPosX * 45 + roomOffset - 1, 0, 0), Game1.doorTexture, doorDirection));
            //Console.Write("Door created");
        }

        public static void StartingThing()
        {
            Room playerRoom = ProcGen2.roomNodes[50,50];
        }

        public static void SpawnRoom()
        {
            //Console.WriteLine(playerRoomX.ToString() + playerRoomY.ToString());
            playerRoom = ProcGen2.roomNodes[playerRoomX, playerRoomY];
            //Console.WriteLine(playerRoom.doorN.ToString() + playerRoom.doorE.ToString() + playerRoom.doorS.ToString() + playerRoom.doorW.ToString());
            ClearRoom();
            if (ProcGen2.roomNodes[playerRoomX, playerRoomY].doorN)
            {
                doorNums.Add(2);
            }
            if (ProcGen2.roomNodes[playerRoomX, playerRoomY].doorE)
            {
                doorNums.Add(3);
            }
            if (ProcGen2.roomNodes[playerRoomX, playerRoomY].doorS)
            {
                doorNums.Add(4);
            }
            if (ProcGen2.roomNodes[playerRoomX, playerRoomY].doorW)
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
            Game1.goblins = ProcGen2.roomNodes[playerRoomX, playerRoomY].gobinsContained;
            foreach (Goblin goblin in ProcGen2.roomNodes[playerRoomX, playerRoomY].gobinsContained)
            {
                Console.WriteLine("theres a goblin stored");
            }
            Game1.ResetGoblins();
            doorNums.Clear();
        }

    }
}
