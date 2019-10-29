using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class Path
    {
        Room room1;
        Room room2;
        int length; 
        public Path(Room inRoom1, Room inRoom2, int inLength)
        {
            room1 = inRoom1;
            room2 = inRoom2;
            length = inLength;
        }
    }
}
