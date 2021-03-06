﻿using Microsoft.Xna.Framework;
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
        static int minimaproomint;

        public static void MinimapDebug()
        {
            minimapRooms.Clear();
            minimaproomint = 0;
            string debugRoomStr = "";
            for (int y = lowestY; y <= highestY; y++)
            {
                for (int x = lowestX; x <= highestX; x++)
                {
                    if (ProcGen2.roomNodes[x, y, Game1.currentFloor] == null)
                    {
                        debugRoomStr += " ";
                    }
                    else if (x == RoomShower.playerRoomX && y == RoomShower.playerRoomY)
                    {
                        debugRoomStr += "X";
                        minimapRooms.Add(new MinimapRoom(x - lowestX, y - lowestY, x, y, ProcGen2.genFloor-1));
                        minimaproomint++;
                    }
                    else
                    {
                        debugRoomStr += "#";
                        minimapRooms.Add(new MinimapRoom(x - lowestX, y - lowestY, x, y, ProcGen2.genFloor-1));
                        minimaproomint++;
                    }
                    
                    
                }
                debugRoomStr += "\n";
            }
            Game1.minirooms.AddRange( minimapRooms);
        }
        public static void GenerateMinimap()
        {
            
            foreach (Room room in ProcGen2.roomNodes)
            {
                if (room != null && room.floor == ProcGen2.genFloor-1)
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
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
