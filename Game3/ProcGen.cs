//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;

//namespace Game3
//{
//    public class ProcGen
//    {
//        static int roomOffset = 150;
//        static List<Room> rooms = new List<Room>();
//        static int[,] wall2DArray = new int[9, 15]
        
//        {
//            {1,1,1,1,1,1,1,2,1,1,1,1,1,1,1},
//            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//            {5,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
//            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//            {1,1,1,1,1,1,1,4,1,1,1,1,1,1,1},
//        };

//        static Room[,] roomNodes = new Room[9, 9];

//        static int[,] roomAllowedTypes = new int[10, 10]
//        {
//          //{0,1,2,3,4,5,6,7,8,9}
//          //{A,B,C,D,E,F,G,H,I,J}
//            {0,0,0,1,0,1,0,1,1,1},//A 0
//            {0,0,1,1,1,1,1,1,1,1},//B 1
//            {0,1,0,1,0,1,1,1,1,1},//C 2
//            {1,1,1,1,0,1,0,1,1,1},//D 3
//            {0,1,0,0,0,0,1,0,1,1},//E 4
//            {1,1,1,1,0,0,1,0,1,1},//F 5
//            {0,1,1,0,1,1,1,0,1,1},//G 6
//            {1,1,1,1,0,0,0,0,0,1},//H 7
//            {1,1,1,1,1,1,1,0,0,1},//I 8
//            {1,1,1,1,1,1,1,1,1,1},//J 9
//        };

//        //make sure generated room isnt outside of array, but also is null
//        static bool IsRoomNullAndInRange(Vector2 inputRoomLocation)
//        {
//            //potential bug > 0
//            try
//            {
//                if (roomNodes[(int)inputRoomLocation.X, (int)inputRoomLocation.Y] == null && inputRoomLocation.X >= 0 && inputRoomLocation.Y >= 0 && inputRoomLocation.X <= roomNodes.GetUpperBound(0) && inputRoomLocation.Y <= roomNodes.GetUpperBound(1))
//                    return true;
//                else
//                    return false;
//            }
//            catch (IndexOutOfRangeException)
//            {
//                return false;
//                throw;
//            }
            
//        }

//        static Tuple<Room, bool> FindRoomWithEmptyNeighbors()
//        {
//            for (int i = 0; i < rooms.Count; i++)
//            {
//                if (!rooms[i].FullNeighbors)
//                {
//                    return new Tuple<Room, bool>(rooms[i], false);
//                }
//            }
//            return new Tuple<Room, bool>(null, true);
//        }

//        static Vector2 FindEmptyAdjacentRoom(Room inputRoom)
//        {
//            if (IsRoomNullAndInRange(new Vector2 (inputRoom.Pos.X, inputRoom.Pos.Y - 1)))
//            {
//                return new Vector2(0, -1);
//            }
//            else if (IsRoomNullAndInRange(new Vector2(inputRoom.Pos.X + 1, inputRoom.Pos.Y)))
//            {
//                return new Vector2(1, 0);
//            }
//            else if (IsRoomNullAndInRange(new Vector2(inputRoom.Pos.X, inputRoom.Pos.Y + 1)))
//            {
//                return new Vector2(0, 1);
//            }
//            else if (IsRoomNullAndInRange(new Vector2(inputRoom.Pos.X - 1, inputRoom.Pos.Y)))
//            {
//                return new Vector2(-1, 0);
//            }
//            else
//                inputRoom.FullNeighbors = true;
//                return new Vector2(0, 0);
//        }

//        public static void GenerateDungeon()
//        {
//            rooms.Clear();
//            int dungeonWidth = roomNodes.GetUpperBound(0);
//            int dungeonHeight = roomNodes.GetUpperBound(1);
//            int middleWidth, middleHeight;
//            Room roomGenerationPointer, roomGenerationPointer2;
//            bool allRoomsDone = false;
//            if (dungeonWidth % 2 != 0)
//            {
//                middleWidth = dungeonWidth / 2;
//            }
//            else
//            {
//                middleWidth = (dungeonWidth - 1) / 2;
//            }
//            if (dungeonHeight % 2 != 0)
//            {
//                middleHeight = dungeonHeight / 2;
//            }
//            else
//            {
//                middleHeight = (dungeonHeight - 1) / 2;
//            }
//            //first room
//            rooms.Add(new Room(9, new Vector2(middleWidth, middleHeight), false));
//            roomNodes[4, 4] = rooms[0];
//            roomGenerationPointer = rooms[0];
//            roomGenerationPointer2 = roomGenerationPointer;
//            Vector2 pointer2Offset = new Vector2(0,0);
//            Room tempRoom;
//            //other rooms
//            while (!allRoomsDone)
//            {
//                string roomNodeDebugStr = "";
//                for (int i = 0; i < roomNodes.GetUpperBound(0); i++)
//                {
//                    for (int x = 0; x < roomNodes.GetUpperBound(1); x++)
//                    {
//                        if (roomNodes[i,x] == null)
//                        {
//                            roomNodeDebugStr += "#";
//                        }
//                        else
//                        {
//                            roomNodeDebugStr += roomNodes[i,x].Type.ToString();
//                        }
                        
//                    }
//                }
//                Console.WriteLine(roomNodeDebugStr);
//                if (FindEmptyAdjacentRoom(roomGenerationPointer)!= new Vector2(0,0))
//                {
//                    pointer2Offset = FindEmptyAdjacentRoom(roomGenerationPointer);
//                    roomGenerationPointer2.Pos = roomGenerationPointer.Pos + pointer2Offset;
//                    tempRoom = new Room(9, roomGenerationPointer2.Pos, false);
//                    rooms.Add(tempRoom);
//                    roomNodes[(int)tempRoom.Pos.X, (int)tempRoom.Pos.Y] = tempRoom;

//                }
//                else
//                {
//                    allRoomsDone = FindRoomWithEmptyNeighbors().Item2;
//                    roomGenerationPointer = FindRoomWithEmptyNeighbors().Item1;
//                }
//            }
            
//        }

//        public static void CreateWall(int wallPosX, int wallPosY)
//        {
//            Game1.walls.Add(new Walls(new Rectangle(wallPosY * 45 + roomOffset, wallPosX * 45 + roomOffset, 0, 0), Game1.wallTexture));
//        }

//        public static void CreateDoor(int wallPosX, int wallPosY, int tileNumber, int doorDirection)
//        {
//            Game1.doors.Add(new Doors(new Rectangle(wallPosY * 45 + roomOffset, wallPosX * 45 + roomOffset, 0, 0), Game1.doorTexture, doorDirection));
//            Console.WriteLine("Door created");
//        }

//        public static void spawnRoom(int RoomNodeNo)
//        {
//            for (int i = 0; i <= wall2DArray.GetUpperBound(0); i++)
//            {
//                for (int x = 0; x <= wall2DArray.GetUpperBound(1); x++)
//                {
//                    if (wall2DArray[i, x] != 0)
//                    {
//                        if (wall2DArray[i, x] == 1)
//                        {
//                            CreateWall(i, x);
//                        }

//                        else if (doorNumbers.Contains(wall2DArray[i, x]))
//                        {
//                            CreateDoor(i, x, wall2DArray[i, x], false, false, 0);
//                        }
//                        else
//                        {
//                            CreateWall(i, x);
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
