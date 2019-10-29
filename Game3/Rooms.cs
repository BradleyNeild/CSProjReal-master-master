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
    
    public class Room
    {
        public int type, posX, posY;
        public bool fullNeighbors, doorN, doorE, doorS, doorW;
        public bool isShop;
        public List<Goblin> gobinsContained;
        public int cost = Game1.random.Next(10);
        public int totalCost = int.MaxValue;
        public bool dijsktraVisited = false;
        public List<Room> tentativeRooms = new List<Room>();
        public Room dijkstraFrom;
        public bool isExplored = false;
        public bool isBoss = false;

        //public enum RoomType
        //{
        //    Normal,
        //    Enemy,
        //};

        //public RoomType Type { get; private set; }
        public Room[] Neighbors = new Room[4];

        public Room(int roomPosX, int roomPosY, bool roomDoorN, bool roomDoorE, bool roomDoorS, bool roomDoorW, bool roomFullNeighbors, List<Goblin> roomGoblinsContained, bool roomIsShop)
        {
            posX = roomPosX;
            posY = roomPosY;
            doorN = roomDoorN;
            doorE = roomDoorE;
            doorS = roomDoorS;
            doorW = roomDoorW;
            fullNeighbors = roomFullNeighbors;
            gobinsContained = roomGoblinsContained;
            isShop = roomIsShop;
        }
    }
}
