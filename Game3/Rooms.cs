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
        public List<Goblin> gobinsContained;
        //public enum RoomType
        //{
        //    Normal,
        //    Enemy,
        //};

        //public RoomType Type { get; private set; }
        public Room[] Neighbors = new Room[4];

        public Room(int roomPosX, int roomPosY, bool roomDoorN, bool roomDoorE, bool roomDoorS, bool roomDoorW, bool roomFullNeighbors, List<Goblin> roomGoblinsContained)
        {
            posX = roomPosX;
            posY = roomPosY;
            doorN = roomDoorN;
            doorE = roomDoorE;
            doorS = roomDoorS;
            doorW = roomDoorW;
            fullNeighbors = roomFullNeighbors;
            gobinsContained = roomGoblinsContained;
        }
    }
}
