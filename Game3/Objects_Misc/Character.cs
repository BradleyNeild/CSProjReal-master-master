using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game3
{
    public class Character : BaseObject
    {
        public Timer iFrames = new Timer(1f);
        public static int life = 6, maxHearts = 3;
        public int moveSpeed = 4;
        private Texture2D texture = Game1.playerTexture;
        public static Weapon weaponHeld;
        public int level;
        public static float totalXP;
        bool showLevelUp = false;
        DateTime showTime;


        public void Damage(int damage)
        {
            //Console.WriteLine("jerwighuoergnjpoerngoehup9otnjkdtgihpejgip");
            if (iFrames.Triggered)
            {
                life -= damage;
                iFrames.ResetTimer();
            }
        }

        public void Heal(int heal)
        {
            if (life < maxHearts*2)
            {
                life += heal;
            }
            
        }

        public List<float> levelBrackets = new List<float>()
        {


        };



        public void GenerateBrackets()
        {
            float num = 2000;
            for (int i = 0; i < 100; i++)
            {
                levelBrackets.Add(num);
                num = num * 2 + 125;
            }
        }


        public Character()
        {
            bounds = new Rectangle(475, 330, 32, 32);
        }

        public override void OnInteract(BaseObject caller)
        {
            if (caller is Goblin)
            {
                Damage(1);
            }
            else if (caller is Walls)
            {
                bounds.X += -(int)vector.X;
                bounds.Y += -(int)vector.Y;
            }
            else if (caller is Doors)
            { 
                Goblin goblinQuestionmark = parent.SearchFirst<Goblin>();
                if (goblinQuestionmark == null)
                {
                    ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY].objectsContained.AddRange(parent.SearchArray<Goblin>());
                    ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY].objectsContained.AddRange(parent.SearchArray<Coin>());
                    ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY].objectsContained.AddRange(parent.SearchArray<Balloon>());


                    foreach (Goblin goblin in parent.SearchArray<Goblin>())
                    {
                        //Console.WriteLine("theres a goblin in goblins");
                    }
                    if (((Doors)caller).direction == 2)
                    {
                        RoomShower.playerRoomY -= 1;
                        bounds = new Rectangle(475, 475, bounds.Width, bounds.Height);

                    }
                    if (((Doors)caller).direction == 3)
                    {
                        RoomShower.playerRoomX += 1;
                        bounds = new Rectangle(200, 330, bounds.Width, bounds.Height);

                    }
                    if (((Doors)caller).direction == 4)
                    {
                        RoomShower.playerRoomY += 1;
                        bounds = new Rectangle(475, 200, bounds.Width, bounds.Height);

                    }
                    if (((Doors)caller).direction == 5)
                    {
                        RoomShower.playerRoomX -= 1;
                        bounds = new Rectangle(744, 330, bounds.Width, bounds.Height);
                    }

                    RoomShower.SpawnRoom();
                    parent.RemoveObject<MagicMissile>();
                    parent.RemoveObject<Coin>();
                    parent.RemoveObject<Particle>();
                    parent.RemoveObject<Balloon>();
                }
                else
                {
                    bounds.Location -= vector.ToPoint();
                }
            }
            else if (caller is Coin)
            {
                caller.destroy = true;
                Game1.coinCount++;
                totalXP += 200;
            }
        }

        public override void OnCreate()
        {

        }

        public override void OnDestroy()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
            if (showLevelUp)
            {
                spriteBatch.DrawString(Game1.debugTextFont, "Level Up!", new Vector2(bounds.X - 5, bounds.Y - 15), Color.White);
            }

        }

        public void CheckLevel()
        {
            if (totalXP > levelBrackets[level])
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            // Game1.AddHeart(2, 1);
            totalXP = levelBrackets[level];
            level++;
            showTime = DateTime.Now;
        }


        public override void Update(GameTime gameTime)
        {
   
            iFrames.Update(gameTime);
            if (DateTime.Now > showTime.AddSeconds(3))
            {
                showLevelUp = false;
            }
            else
                showLevelUp = true;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                vector.Y = -1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                vector.Y = 1;
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.S) && !Keyboard.GetState().IsKeyDown(Keys.W))
            {
                vector.Y = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                vector.X = -1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                vector.X = 1;
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.D))
            {
                vector.X = 0;
            }

            if (vector != Vector2.Zero)
            {

                vector.Normalize();
                vector *= moveSpeed;
                bounds.Location += vector.ToPoint();
            }

        }
    }
}
