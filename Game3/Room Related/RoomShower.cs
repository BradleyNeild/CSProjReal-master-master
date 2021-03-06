﻿using Microsoft.Xna.Framework;
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
        public static int roomOffset = 0;
        public static Room playerRoom;
        public static int playerRoomX = 50, playerRoomY = 50;
        public static int nextRoomX = 50, nextRoomY = 50;
        public static List<int> doorNums = new List<int>();
        public static int[,] wall2DArray = new int[9, 15]
        {
            {1,1,1,1,1,1,1,2,1,1,1,1,1,1,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
            {5,0,0,1,1,1,0,0,0,1,1,1,0,0,3},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,1,0,0,1,0,0,1},
            {1,0,0,0,0,0,0,0,1,0,0,1,0,0,1},
            {1,1,1,1,1,1,1,4,1,1,1,1,1,1,1},
        };



        public static void ClearRoom()
        {
            Game1.objectHandler.RemoveObject<Walls>();
            Game1.objectHandler.RemoveObject<Doors>();
        }

        public static void CreateWall(int wallPosX, int wallPosY)
        {
            Game1.objectHandler.AddObject(new Walls(new Rectangle(wallPosY * Walls.wallSize + roomOffset, wallPosX * Walls.wallSize + roomOffset, 0, 0), Game1.wallTexture));
        }

        public static void CreateDoor(int wallPosX, int wallPosY, int doorDirection)
        {
            Game1.objectHandler.AddObject(new Doors(new Rectangle(wallPosY * Walls.wallSize + roomOffset - 3, wallPosX * Walls.wallSize + roomOffset - 3, 0, 0), doorDirection));
            //Console.Write("Door created");
        }

        public static void StartingThing()
        {
            Room playerRoom = ProcGen2.roomNodes[50,50, 0];
            
        }

        public static void SpawnRoom()
        {
            playerRoom = ProcGen2.roomNodes[playerRoomX, playerRoomY, Game1.currentFloor];
            ProcGen2.roomNodes[playerRoomX, playerRoomY, Game1.currentFloor].isExplored = true;
            ClearRoom();
            if (ProcGen2.roomNodes[playerRoomX, playerRoomY, Game1.currentFloor].doorN)
            {
                doorNums.Add(2);
            }
            if (ProcGen2.roomNodes[playerRoomX, playerRoomY, Game1.currentFloor].doorE)
            {
                doorNums.Add(3);
            }
            if (ProcGen2.roomNodes[playerRoomX, playerRoomY, Game1.currentFloor].doorS)
            {
                doorNums.Add(4);
            }
            if (ProcGen2.roomNodes[playerRoomX, playerRoomY, Game1.currentFloor].doorW)
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
            
            if (playerRoom.isShop)
            {

            }
            //Minimap.MinimapDebug();
            Game1.ResetSlimes();
            doorNums.Clear();
        }

    }
}
