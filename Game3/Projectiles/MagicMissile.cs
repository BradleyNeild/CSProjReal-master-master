using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace Game3
{
    class MagicMissile:BaseObject
    {
        Texture2D texture;
        public Vector2 mousePos;
        float mouseGapX, mouseGapY, direction;
        public int ID, power;
        public DateTime spawnTime;
        public static int noMissiles;
        public static int maxMissiles;

        private void MoveMissileCursor(Vector2 vector)
        {
            if (!bounds.Contains(mousePos))
            {
                vector.Normalize();
                vector *= 5;
                bounds.Location += vector.ToPoint();
                //Console.WriteLine("vector done" + vector);
            }
        }

        public void MoveMissile(Vector2 vector)
        {
            vector.Normalize();
            vector *= 5;
            bounds.Location += vector.ToPoint();
            //Console.WriteLine("vector done" + vector);
        }

        public void IncrementID(int IDRemoved, bool Decrement)
        {
            if (ID > IDRemoved)
            {
                if (Decrement)
                {
                    ID--;
                }
                else ID++;
            }
        }

        public MagicMissile(Rectangle missileBounds, Texture2D missileTexture, int missileID, int missilePower, DateTime missileSpawnTime)
        {
            bounds = missileBounds;
            bounds.Width = 36;
            bounds.Height = 9;
            texture = missileTexture;
            ID = missileID;
            power = missilePower;
            spawnTime = missileSpawnTime;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White, rotation: direction, origin: new Vector2(texture.Width / 2, texture.Height / 2));
        }



        public override void Update(GameTime gameTime)
        {

            var mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);
            //Console.WriteLine("mousepos = " + mousePos);
            //Console.WriteLine("vector = " + vector);
            vector = new Vector2(mouseState.X, mouseState.Y) - bounds.Center.ToVector2();

            if (vector != Vector2.Zero)
            {
                vector.Normalize();
                vector *= 8;
                bounds.Location += vector.ToPoint();
            }
            direction = (float)Math.Atan2(vector.Y, vector.X);
            //Console.WriteLine(direction);


            //if (vector.Y != 0 && vector.X != 0)
            //{
            //    MoveMissileCursor(vector);
            //}
            //else if (vector.Y == 0 && vector.X > 0)
            //{
            //    vector = new Vector2(1, 0);
            //    MoveMissileCursor(vector);
            //}
            //else if (vector.Y == 0 && vector.X < 0)
            //{
            //    vector = new Vector2(-1, 0);
            //    MoveMissileCursor(vector);
            //}
            //else if (vector.Y < 0 && vector.X == 0)
            //{
            //    vector = new Vector2(0, -1);
            //    MoveMissileCursor(vector);
            //}
            //else if (vector.Y > 0 && vector.X == 0)
            //{
            //    vector = new Vector2(0, 1);
            //    MoveMissileCursor(vector);
            //}
            //else
            //{
            //    vector = Vector2.Zero;
            //}
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
