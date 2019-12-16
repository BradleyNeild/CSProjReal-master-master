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
        public int posX, posY;
        public int floor;
        public bool fullNeighbors, doorN, doorE, doorS, doorW;
        public bool isShop;
        public bool isExplored = false;
        public bool isBoss = false;
        public Room[] neighbors = new Room[4];

        public Room(int roomPosX, int roomPosY, bool roomDoorN, bool roomDoorE, bool roomDoorS, bool roomDoorW, bool roomFullNeighbors, List<Slime> roomGoblinsContained, bool roomIsShop, int roomFloor)
        {
            posX = roomPosX;
            posY = roomPosY;
            doorN = roomDoorN;
            doorE = roomDoorE;
            doorS = roomDoorS;
            doorW = roomDoorW;
            fullNeighbors = roomFullNeighbors;
            isShop = roomIsShop;
            floor = roomFloor;
        }
    }
}
