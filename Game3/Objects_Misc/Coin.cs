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
    class Coin:BaseObject
    {
        Texture2D texture;
        bool Bounce;
        bool FinishedBounce;
        int Bounces;
        float YOff;
        float BounceHeight;
        float BounceVelocity;
        float LerpValue;
        float vectorXNextDouble;
        float vectorYNextDouble;


        public override string ToString()
        {
            string Str = "";
            Str += "BoundingRect:" + bounds.ToString() + "\n";
            Str += "YOff:" + YOff.ToString() + "\n";
            Str += "Vector:" + vector.ToString();
            return Str;
        }

        public static void CreateCoin(ObjectHandler objectHandler, Point position, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                objectHandler.AddObject(new Coin(new Rectangle(position, Point.Zero), RoomShower.playerRoom));
            }
            
        }

        public Coin(Rectangle coinBounds, Room coinRoom)
        {
            room = coinRoom;
            bounds = coinBounds;
            bounds.Width = 16;
            bounds.Height = 22;
            texture = Game1.coinTexture;


            if (Game1.random.Next(0, 2) == 0)
            {
                vectorXNextDouble = -(float)Game1.random.NextDouble();
                vector.X = vectorXNextDouble;
                ////Console.WriteLine(vectorXNextDouble);

            }
            else
            {
                vectorXNextDouble = (float)Game1.random.NextDouble();
                vector.X = vectorXNextDouble;
                //Console.WriteLine(vectorXNextDouble);
            }

            if (Game1.random.Next(0, 2) == 0)
            {
                vectorYNextDouble = -(float)Game1.random.NextDouble();
                vector.Y = vectorYNextDouble;
                //Console.WriteLine(vectorYNextDouble);
            }
            else
            {
                vectorYNextDouble = (float)Game1.random.NextDouble();
                vector.Y = vectorYNextDouble;
                //Console.WriteLine(vectorYNextDouble);
            }

            vector.Normalize();
            vector *= (new Vector2(4) * (float)Game1.random.NextDouble());
            FinishedBounce = false;
            Bounce = false;
            YOff = 0;
            BounceHeight = 10;
            LerpValue = 0;
            BounceVelocity = Game1.random.Next(2, 5);
            Bounces = Game1.random.Next(2, 5);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Point DrawPos = bounds.Location + new Point(0, (int)YOff);
            Point DrawBounds = bounds.Size;

            if (enabled)
            {
                spriteBatch.Draw(texture, destinationRectangle: new Rectangle(DrawPos, DrawBounds), Color.White);
            }
            
        }

        public override void Update(GameTime gameTime)
        {

            if (ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY] == room)
            {
                enabled = true;
            }
            else
            {
                enabled = false;
            }

            if (enabled)
            {


                if (!FinishedBounce)
                {
                    bounds.Location += vector.ToPoint();

                    if (Bounce)
                    {
                        LerpValue += (BounceHeight != 0) ? 1 / BounceHeight : 0.1f;
                    }
                    else
                    {
                        LerpValue -= (BounceHeight != 0) ? 1 / (BounceHeight / (2 * (float)Game1.random.NextDouble())) : 0.1f;
                    }

                    if (LerpValue >= 1)
                    {
                        Bounce = false;
                    }

                    if (LerpValue < 0)
                    {
                        Bounce = true;
                        BounceHeight -= 1;
                        Bounces -= 1;
                        if (BounceHeight <= 0)
                        {
                            BounceHeight = 0;
                        }

                        if (Bounces <= 0)
                        {
                            FinishedBounce = true;
                        }
                        LerpValue = 0;
                    }

                    YOff = BounceVelocity * (-MathHelper.Lerp(-3, BounceHeight, LerpValue));
                }
                Character character = Game1.objectHandler.SearchFirst<Character>();
                Vector2 magnetismVector;
                magnetismVector = new Vector2(character.bounds.Location.X - bounds.Location.X, character.bounds.Location.Y - bounds.Location.Y);
                magnetismVector.Normalize();
                magnetismVector *= 2;
                bounds.X += (int)magnetismVector.X;
                bounds.Y += (int)magnetismVector.Y;
            }
        }
        public override void OnCreate()
        {

        }

        public override void OnDestroy()
        {

        }

        public override void OnInteract(BaseObject caller)
        {
            if (caller is Doors || caller is Walls)
            {
                bounds.X += (int)-vector.X;
                bounds.Y += (int)-vector.Y;
                vector = -vector;
            }
        }
    }
}
