using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Ghost:BaseObject
    {
        public Texture2D texture;
        public int type, power, health, maxHealth;
        public static Room currentRoom;
        static List<Room> openRooms = new List<Room>();
        static Room[,] dupedArray = ProcGen2.roomNodes;
        static List<Room> tentativeRooms = new List<Room>();
        static List<Path> paths = new List<Path>();
        static DateTime lastMoved;

        public Ghost(Texture2D enemyTexture, int ghostType, int ghostHealth, int ghostMaxHealth)
        {
            texture = enemyTexture;
            type = ghostType;
            health = ghostHealth;
            maxHealth = ghostMaxHealth;
        }
        public static void SpawnSelf()
        {
            foreach (Room room in ProcGen2.roomNodes)
            {
                if (room != null)
                {
                    if (room.posX != RoomShower.playerRoomX && room.posY != RoomShower.playerRoomY)
                    {
                        openRooms.Add(room);
                    }
                }
            }
            Room roomToSpawnIn = openRooms[Game1.random.Next(openRooms.Count)];
            currentRoom = roomToSpawnIn;
            lastMoved = DateTime.Now;
            Console.WriteLine("Ghost spawned in" + currentRoom.posX + currentRoom.posY);
        }

        private static void MoveTowardsPlayer()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (DateTime.Now.AddSeconds(-10) > lastMoved)
            {
                MoveTowardsPlayer();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }

        public override void OnCreate()
        {

        }

        public override void OnDestroy()
        {

        }

        public override void OnInteract(BaseObject caller)
        {

        }

    }
}
