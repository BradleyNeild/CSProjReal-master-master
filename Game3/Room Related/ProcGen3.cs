//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Game3
//{
//    public class ProcGen3
//    {
//        static List<int> roomsInDirs = new List<int>();


//        public static List<Room> roomList = new List<Room>();
//        static int currentDir;
//        static int numRooms;

//        static void AddRoom(int posX, int posY, bool safe)

//        {
//            roomList.Add(new Room(posX, posY, safe));
//            numRooms++;
//        }

//        static Room FindRoomByPos(int fposX, int fposY)
//        {
//            foreach (Room room in roomList)
//            {
//                if (room.posX == fposX && room.posY == fposY)
//                {
//                    return room;
//                }
//            }
//            return null;
//        }

//        public static void GenerateDungeon()
//        {
//            roomList.Clear();
//            RoomPointer roomPointer = new RoomPointer(0, 0);
//            int roomsToGen = 10;
//            int divided;
//            int remainder;
//            remainder = 10 % 4;
//            divided = (roomsToGen - remainder) / 4;
//            if (remainder > 0)
//            {
//                int randomInt = Game1.random.Next(4);
//                for (int i = 0; i < 4; i++)
//                {
//                    roomsInDirs.Add(divided);
//                }
                
//                switch (randomInt)
//                {
//                    case 0:
//                        {
//                            roomsInDirs[0] = remainder;
//                            break;
//                        }
//                    case 1:
//                        {
//                            roomsInDirs[0] = remainder;
//                            break;
//                        }
//                    case 2:
//                        {
//                            roomsInDirs[0] = remainder;
//                            break;
//                        }
//                    case 3:
//                        {
//                            roomsInDirs[0] = remainder;
//                            break;
//                        }
//                }
//            }
//            AddRoom(0, 0, true);
//            for (int i = 0; i < 4; i++)
//            {
//                currentDir = i;
//                roomPointer.posX = roomList[0].posX;
//                roomPointer.posY = roomList[0].posY;
//                if (i == 0)
//                {
//                    roomPointer.posY--;
//                }
//                else if (i == 1)
//                {
//                    roomPointer.posX++;
//                }
//                else if (i == 2)
//                {
//                    roomPointer.posY++;
//                }
//                else if (i == 3)
//                {
//                    roomPointer.posX--;
//                }
//                AddRoom(roomPointer.posX, roomPointer.posY, false);

//                while (roomsInDirs[i] > 0)
//                {
//                    if (FindRoomByPos(roomPointer.posX, roomPointer.posY - 1) != null)
//                    {
//                        roomPointer.roomN = true;
//                    }
//                    else
//                    {
//                        roomPointer.roomN = false;
//                    }

//                    if (FindRoomByPos(roomPointer.posX + 1, roomPointer.posY) != null)
//                    {
//                        roomPointer.roomE = true;
//                    }
//                    else
//                    {
//                        roomPointer.roomE = false;
//                    }

//                    if (FindRoomByPos(roomPointer.posX, roomPointer.posY + 1) != null)
//                    {
//                        roomPointer.roomS = true;
//                    }
//                    else
//                    {
//                        roomPointer.roomS = false;
//                    }

//                    if (FindRoomByPos(roomPointer.posX - 1, roomPointer.posY) != null)
//                    {
//                        roomPointer.roomW = true;
//                    }
//                    else
//                    {
//                        roomPointer.roomW = false;
//                    }

//                    int randomDir = Game1.random.Next(4);
//                    if (randomDir == 0 && !roomPointer.roomN)
//                    {
//                        roomPointer.posY--;
//                        AddRoom(roomPointer.posX, roomPointer.posY, false);
//                    }
//                    else if (randomDir == 1 && !roomPointer.roomE)
//                    {
//                        roomPointer.posX++;
//                        AddRoom(roomPointer.posX, roomPointer.posY, false);

//                    }

//                    else if (randomDir == 2 && !roomPointer.roomS)
//                    {
//                        roomPointer.posY++;
//                        AddRoom(roomPointer.posX, roomPointer.posY, false);

//                    }
//                    else if (randomDir == 3 && !roomPointer.roomW)
//                    {
//                        roomPointer.posX--;
//                        AddRoom(roomPointer.posX, roomPointer.posY, false);

//                    }
//                    else
//                    {
//                        FindRoomByPos(roomPointer.posX, roomPointer.posY).fullNeighbors = true;
//                        Room randRoom = roomList[Game1.random.Next(0, roomList.Count - 1)];
//                        roomPointer = new RoomPointer(randRoom.posX, randRoom.posY);
//                    }
//                }
                


//            }
//        }
//    }
//}

