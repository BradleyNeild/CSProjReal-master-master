using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{

    public class ProcGen2
    {
        public static Room[,] roomNodes = new Room[100, 100];
        public static List<Room> roomList = new List<Room>();
        public static Room shopRoom;
        public static Slime bossSlime;

        static int numRooms = 1;

        public static void ClearDungeon()
        {
            foreach (Room room in roomNodes)
            {
                if (room.posX == 50 && room.posY == 50)
                {
                    roomNodes[room.posX, room.posY] = null;
                }
            }
            roomList.Clear();
            shopRoom = null;
            numRooms = 1;
            GenerateDungeon();
        }

        public static void SpawnBossRoom()
        {
            List<Room> validRooms = new List<Room>();
            foreach (Room room in roomNodes)
            {
                int doorCount = 0;
                if (room != null)
                {
                    if (room.posX != 50 && room.posY != 50 && !room.isShop)
                    {
                        if (room.doorN)
                            doorCount++;
                        if (room.doorE)
                            doorCount++;
                        if (room.doorS)
                            doorCount++;
                        if (room.doorW)
                            doorCount++;
                        if (doorCount <= 2)
                        {
                            validRooms.Add(room);
                        }
                    }
                }
            }
            Room randomRoom = validRooms[Game1.random.Next((validRooms.Count - 1))];
            roomNodes[randomRoom.posX, randomRoom.posY].isExplored = true;
            roomNodes[randomRoom.posX, randomRoom.posY].isBoss = true;
            Console.WriteLine("boss generated");
            foreach (Slime slime in Game1.objectHandler.SearchArray<Slime>())
            {
                if (slime.room == randomRoom)
                {
                    slime.room = null;
                }
            }
            Game1.objectHandler.AddObject(new TrapDoor(new Rectangle(Walls.wallSize * 7, Walls.wallSize * 4, 64, 128), randomRoom));

            bossSlime = new Slime(12, 12, 2, new Rectangle(Walls.wallSize * 7, Walls.wallSize * 4, 128, 128), new Point(Walls.wallSize * 7, Walls.wallSize * 4), 4, true, randomRoom, new Slime(5, 5, 1, new Rectangle(0, 0, 64, 64), Point.Zero, 4, false, randomRoom, new Slime(2, 2, 1, new Rectangle(0, 0, 32, 32), Point.Zero, 0, false, randomRoom, null)));
            Console.WriteLine(bossSlime.room.posX + ", " + bossSlime.room.posY);
            Game1.objectHandler.AddObject(bossSlime);


        }

        public static void SpawnShop()
        {
            List<Room> validRooms = new List<Room>();
            foreach (Room room in roomNodes)
            {
                int doorCount = 0;
                if (room != null)
                {
                    if (room.posX != 50 && room.posY != 50 && !room.isBoss)
                    {


                        if (room.doorN)
                            doorCount++;
                        if (room.doorE)
                            doorCount++;
                        if (room.doorS)
                            doorCount++;
                        if (room.doorW)
                            doorCount++;
                        if (doorCount == 2)
                        {
                            validRooms.Add(room);
                        }
                    }
                }
            }

            Room randomRoom = validRooms[Game1.random.Next(validRooms.Count - 1)];
            foreach (Slime slime in Game1.objectHandler.SearchArray<Slime>())
            {
                if (slime.room == randomRoom)
                {
                    slime.room = null;
                }
            }

            
            shopRoom = randomRoom;

            GeneratePurchasables(false);





            randomRoom.isExplored = true;
            randomRoom.isShop = true;
            //Console.WriteLine("jigjpigjegjepige");

        }

        public static void GeneratePurchasables(bool clear)
        {
            if (clear)
            {
                Game1.objectHandler.RemoveObject<Purchasable>();
            }
            List<Purchasable> shopPurchasables = new List<Purchasable>();
            while (shopPurchasables.Count < 3)
            {
                Pickup pickupToAdd = Pickup.pickups[0];
                bool valid = false;
                while (!valid)
                {
                    pickupToAdd = Pickup.pickups[Game1.random.Next(Pickup.pickups.Count)];
                    if (pickupToAdd.shoppable)
                    {
                        valid = true;
                    }
                }
                pickupToAdd.bounds.X = Walls.wallSize * (6 + shopPurchasables.Count);
                pickupToAdd.bounds.Y = Walls.wallSize * 4;
                shopPurchasables.Add(new Purchasable(pickupToAdd, shopRoom));
            }
            Purchasable rerollMachine = new Purchasable(Pickup.pickups[6], shopRoom);
            rerollMachine.bounds = new Rectangle(Walls.wallSize * 7, Walls.wallSize * 6, 64, 64);

            Game1.objectHandler.AddObject(rerollMachine);

            Game1.objectHandler.AddObjects(shopPurchasables);
        }


        static void CheckRooms()
        {
            foreach (Room room in roomNodes)
            {
                if (room != null)
                {
                    if (room.posY - 1 >= 0)
                    {
                        if (roomNodes[room.posX, room.posY - 1] != null)
                        {
                            room.doorN = true;
                        }
                        else
                        {
                            room.doorN = false;
                        }
                    }

                    if (room.posY + 1 <= roomNodes.GetUpperBound(1))
                    {
                        if (roomNodes[room.posX, room.posY + 1] != null)
                        {
                            room.doorS = true;
                        }
                        else
                        {
                            room.doorS = false;
                        }
                    }

                    if (room.posX - 1 >= 0)
                    {
                        if (roomNodes[room.posX - 1, room.posY] != null)
                        {
                            room.doorW = true;
                        }
                        else
                        {
                            room.doorW = false;
                        }
                    }

                    if (room.posX + 1 <= roomNodes.GetUpperBound(0))
                    {
                        if (roomNodes[room.posX + 1, room.posY] != null)
                        {
                            room.doorE = true;
                        }
                        else
                        {
                            room.doorE = false;
                        }
                    }

                    if (room.doorN && room.doorE && room.doorS && room.doorW)
                    {
                        room.fullNeighbors = true;
                    }
                    else
                    {
                        room.fullNeighbors = false;
                    }
                }

            }
        }

        static void CheckRoom(Room checkedRoom)
        {
            if (checkedRoom.posY - 1 >= 0)
            {
                if (roomNodes[checkedRoom.posX, checkedRoom.posY - 1] != null)
                {
                    checkedRoom.doorN = true;
                }
                else
                {
                    checkedRoom.doorN = false;
                }
            }

            if (checkedRoom.posY + 1 >= roomNodes.GetUpperBound(1))
            {
                if (roomNodes[checkedRoom.posX, checkedRoom.posY + 1] != null)
                {
                    checkedRoom.doorS = true;
                }
                else
                {
                    checkedRoom.doorS = false;
                }
            }

            if (checkedRoom.posX - 1 >= 0)
            {
                if (roomNodes[checkedRoom.posX, checkedRoom.posX - 1] != null)
                {
                    checkedRoom.doorW = true;
                }
                else
                {
                    checkedRoom.doorW = false;
                }
            }

            if (checkedRoom.posX + 1 >= roomNodes.GetUpperBound(0))
            {
                if (roomNodes[checkedRoom.posX, checkedRoom.posX + 1] != null)
                {
                    checkedRoom.doorE = true;
                }
                else
                {
                    checkedRoom.doorE = false;
                }
            }

            if (checkedRoom.doorN && checkedRoom.doorE && checkedRoom.doorS && checkedRoom.doorW)
            {
                checkedRoom.fullNeighbors = true;
            }
            else
            {
                checkedRoom.fullNeighbors = false;
            }
        }

        static bool CheckRoomIsNull(int x, int y)
        {
            if (roomNodes[x, y] == null)
            {
                return true;
            }

            else return false;
        }

        static Room FindRandomRoom()
        {
            int randRoom = Game1.random.Next(roomList.Count);
            Room tempRoom = roomList[randRoom];
            return roomNodes[tempRoom.posX, tempRoom.posY];
        }

        public static void DebugMap(int playerPosX, int playerPosY)
        {
            CheckRooms();
        }



        static void AddRoom(int addPosX, int addPosY, bool enemies)
        {
            Room roomToAdd = new Room(addPosX, addPosY, false, false, false, false, false, new List<Slime>(), false);
            if (enemies)
            {
                List<Slime> slimes = new List<Slime>();
                slimes = RoomFeatures.GenerateSlimes(roomToAdd);
                Game1.objectHandler.AddObjects(slimes);
            }




            roomNodes[addPosX, addPosY] = roomToAdd;
            roomList.Add(roomToAdd);
            //Console.WriteLine("Added Room at " + addPosX + "," + addPosY);
            numRooms += 1;
            CheckRooms();
        }

        public static void GenerateDungeon()
        {
            int maxRooms;
            bool maxroomsOK = false;
            do
            {
                maxRooms = 20;
                //maxRooms = Game1.random.Next(40, 60);
                if (maxRooms % 4 == 0)
                {
                    maxroomsOK = true;
                }
            }

            while (!maxroomsOK);


            int roomsPerPath = maxRooms / 4;
            bool goodRandom = false;
            List<Room> fourPaths = new List<Room>();
            bool canGoN, canGoE, canGoS, canGoW;
            int possibleDirections = 0;
            int newRoomX, newRoomY, pointer1X, pointer1Y;
            Room roomPointer1;
            AddRoom(50, 50, false);
            AddRoom(49, 50, true);
            AddRoom(50, 49, true);
            AddRoom(51, 50, true);
            AddRoom(50, 51, true);
            fourPaths.Add(roomList[1]);
            fourPaths.Add(roomList[2]);
            fourPaths.Add(roomList[3]);
            fourPaths.Add(roomList[4]);
            roomPointer1 = FindRandomRoom();
            pointer1X = roomPointer1.posX;
            pointer1Y = roomPointer1.posY;
            for (int i = 0; i < 4; i++)
            {
                roomPointer1 = roomNodes[fourPaths[i].posX, fourPaths[i].posY];
                for (int x = 0; x < roomsPerPath; x++)
                {
                    goodRandom = false;

                    if (CheckRoomIsNull(roomPointer1.posX, roomPointer1.posY - 1))
                    {
                        canGoN = true;
                        possibleDirections++;
                    }
                    else
                    {
                        canGoN = false;
                    }

                    if (CheckRoomIsNull(roomPointer1.posX + 1, roomPointer1.posY))
                    {
                        canGoE = true;
                        possibleDirections++;
                    }
                    else
                    {
                        canGoE = false;
                    }

                    if (CheckRoomIsNull(roomPointer1.posX, roomPointer1.posY + 1))
                    {
                        canGoS = true;
                        possibleDirections++;
                    }
                    else
                    {
                        canGoS = false;
                    }

                    if (CheckRoomIsNull(roomPointer1.posX - 1, roomPointer1.posY))
                    {
                        canGoW = true;
                        possibleDirections++;
                    }
                    else
                    {
                        canGoW = false;
                    }

                    if (possibleDirections == 0)
                    {
                        break;
                    }

                    newRoomX = roomPointer1.posX;
                    newRoomY = roomPointer1.posY;
                    while (!goodRandom)
                    {
                        int randomDir = Game1.random.Next(4);
                        if (randomDir == 0 && canGoN)
                        {
                            newRoomX = roomPointer1.posX;
                            newRoomY = roomPointer1.posY - 1;
                            goodRandom = true;
                        }
                        else if (randomDir == 1 && canGoE)
                        {
                            newRoomX = roomPointer1.posX + 1;
                            newRoomY = roomPointer1.posY;
                            goodRandom = true;
                        }
                        else if (randomDir == 2 && canGoS)
                        {
                            newRoomX = roomPointer1.posX;
                            newRoomY = roomPointer1.posY + 1;
                            goodRandom = true;
                        }
                        else if (randomDir == 3 && canGoW)
                        {
                            newRoomX = roomPointer1.posX - 1;
                            newRoomY = roomPointer1.posY;
                            goodRandom = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                    AddRoom(newRoomX, newRoomY, true);
                    roomPointer1 = roomNodes[newRoomX, newRoomY];
                    CheckRooms();
                }
            }
            SpawnShop();
            SpawnBossRoom();
        }
    }
}
