using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class RoomPointer
    {
        public int posX, posY;
        public bool roomN, roomE, roomS, roomW;
        public RoomPointer(int roomPosX, int roomPosY)
        {
            posX = roomPosX;
            posY = roomPosY;
        }
    }
}
