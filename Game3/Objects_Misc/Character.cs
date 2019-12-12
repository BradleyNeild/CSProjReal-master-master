using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game3
{
    public class Character : BaseObject
    {
        bool wPressed, aPressed, dPressed, sPressed;
        SpriteEffects spriteEffect = SpriteEffects.None;
        public bool flipSprite = false;
        public Timer iFrames = new Timer(1f);
        public static int life = 6, maxHearts = 3;
        public static float moveSpeed = 4.6f;
        private Texture2D texture = Game1.wizardTexture;
        public static Weapon weaponHeld;
        public int level;
        public static float totalXP;
        bool showLevelUp = false;
        DateTime showTime;
        public static List<int> persistentEffects = new List<int>();


        public void Damage(int damage)
        {
            if (life > 0)
            {
                int oldLife = life;
                //Console.WriteLine("jerwighuoergnjpoerngoehup9otnjkdtgihpejgip");
                if (iFrames.Triggered)
                {
                    life -= damage;
                    iFrames.ResetTimer();
                    Game1.hurtSfx.Play(1, 0, 0);
                }
            }

        }

        public void Heal(int heal)
        {
            if (life < maxHearts * 2)
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
            bounds = new Rectangle(7 * Walls.wallSize + 13, 4 * Walls.wallSize, 28, 48);
        }

        public override void OnInteract(BaseObject caller)
        {
            if (caller is Slime)
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
                Slime goblinQuestionmark = parent.SearchFirst<Slime>();
                if (((Doors)caller).enterable == true)
                {

                    foreach (Slime goblin in parent.SearchArray<Slime>())
                    {
                        //Console.WriteLine("theres a goblin in goblins");
                    }
                    if (((Doors)caller).direction == 2)
                    {
                        RoomShower.playerRoomY -= 1;
                        bounds = new Rectangle(7 * Walls.wallSize + 13, 7 * Walls.wallSize, bounds.Width, bounds.Height);

                    }
                    if (((Doors)caller).direction == 3)
                    {
                        RoomShower.playerRoomX += 1;
                        bounds = new Rectangle(1 * Walls.wallSize + 5, 4 * Walls.wallSize, bounds.Width, bounds.Height);

                    }
                    if (((Doors)caller).direction == 4)
                    {
                        RoomShower.playerRoomY += 1;
                        bounds = new Rectangle(7 * Walls.wallSize + 13, 1 * Walls.wallSize + 5, bounds.Width, bounds.Height);

                    }
                    if (((Doors)caller).direction == 5)
                    {
                        RoomShower.playerRoomX -= 1;
                        bounds = new Rectangle(13 * Walls.wallSize + 20, 4 * Walls.wallSize, bounds.Width, bounds.Height);
                    }

                    parent.RemoveObject<Projectile>();
                    parent.RemoveObject<Particle>();
                    parent.RemoveObject<Balloon>();

                    RoomShower.SpawnRoom();

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
                Game1.coinPickupSfx.Play(0.5f, 0, 0);
            }
            else if (caller is TrapDoor)
            {
                ((TrapDoor)caller).EnterTrapDoor();
            }
            else if (caller is Purchasable)
            {
                if (Game1.coinCount >= ((Purchasable)caller).price && Game1.Key.IsPressed(Keys.E) && ((Purchasable)caller).cooldown.Triggered)
                {
                    Game1.coinCount -= ((Purchasable)caller).price;
                    ((Purchasable)caller).cooldown.ResetTimer();
                    ((Purchasable)caller).pickup.Effect(((Purchasable)caller).pickup.effID);
                    if (((Purchasable)caller).pickup.name != "Reroller")
                    {
                        caller.destroy = true;
                    }
                    
                }

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
            spriteBatch.Draw(texture, destinationRectangle: bounds, null, Color.White, 0f, Vector2.Zero, spriteEffect, 0f);
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

            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                wPressed = true;
            }
            else
            {
                wPressed = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                sPressed = true;
            }
            else
            {
                sPressed = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                aPressed = true;
            }
            else
            {
                aPressed = false;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                dPressed = true;
            }
            else
            {
                dPressed = false;
            }

            //sprite flipping
            if (aPressed)
            {

            }
            else if (dPressed)
            {
                flipSprite = false;
            }

            //X movement
            if (aPressed && dPressed)
            {
                vector.X = 0;
            }
            else if (aPressed)
            {
                vector.X = -1;
                flipSprite = true;
            }
            else if (dPressed)
            {
                vector.X = 1;
                flipSprite = false;
            }
            else
            {
                vector.X = 0;
            }

            if (wPressed && sPressed)
            {
                vector.Y = 0;
            }
            else if (wPressed)
            {
                vector.Y = -1;
            }
            else if (sPressed)
            {
                vector.Y = 1;
            }
            else
            {
                vector.Y = 0;
            }



            if (vector != Vector2.Zero)
            {

                vector.Normalize();
                vector *= moveSpeed;
                bounds.Location += vector.ToPoint();
            }

            if (flipSprite)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffect = SpriteEffects.None;
            }


        }
    }
}
