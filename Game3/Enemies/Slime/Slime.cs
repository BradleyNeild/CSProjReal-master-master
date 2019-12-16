using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Slime : Enemy
    {

        public Color color = Color.White;

        public bool frozen = true;
        public bool aggroed = false;
        Timer freezeTime = new Timer(0.8f);
        public static int numAggroed = 0;
        public bool disableForever = false;
        public const int maxAggroed = 3;
        public int numSplitoffs = 0;
        public Slime splitSlime;
        public bool spawnFrozen = true;
        public Slime(float enemyLife, float enemyMaxLife, int enemyDamage, Rectangle enemyBounds, Point enemySpawnPoint, int enemySplitOffs, bool slimeSpawnFrozen, Room slimeRoom, Slime enemySplitSlime)
        {
            bounds = enemyBounds;
            life = enemyLife;
            maxLife = enemyMaxLife;
            damage = enemyDamage;
            texture = Game1.slimeSprites;
            spawnPoint = enemySpawnPoint;
            numSplitoffs = enemySplitOffs;
            splitSlime = enemySplitSlime;
            spawnFrozen = slimeSpawnFrozen;
            room = slimeRoom;
            floor = room.floor;
        }

        private bool AggrosUnderMax()
        {
            if (numAggroed < maxAggroed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void MoveTo(Point destination)
        {

        }

        public void Aggro()
        {
            //Console.WriteLine("iowtruwhuowihiowio3jripogi0g");
            ai.Push(new MeleeAttack());
            numAggroed++;
            aggroed = true;

        }

        public void DeAggro()
        {
            //Console.WriteLine("Deaggroed");
            aggroed = false;
            numAggroed--;
        }



        public override void Update(GameTime gameTime)
        {
            if (room == RoomShower.playerRoom && floor == Game1.currentFloor)
            {
                enabled = true;
            }
            else
            {
                enabled = false;
            }
                
            if (enabled)
            {
                freezeTime.Update(gameTime);
                animTimer.Update(gameTime);
                if (animTimer.Triggered)
                {
                    animTimer.ResetTimer();
                    if (animFrame == 2)
                    {
                        frameIncrement = -1;
                    }
                    else if (animFrame == 0)
                    {
                        frameIncrement = 1;
                    }
                    animFrame += frameIncrement;

                    srcRectangle = new Rectangle(animFrame * 16, 0, 16, 16);
                }
                if (freezeTime.Triggered || !spawnFrozen)
                {

                    ai.Update(gameTime);
                    if (life <= 0)
                    {
                        destroy = true;
                    }
                    if (AggrosUnderMax() && !aggroed)
                    {
                        Aggro();
                    }
                }
            }


        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (enabled)
            {


                spriteBatch.Draw(texture, bounds, srcRectangle, color: color);
            }
        }

        public override void OnCreate()
        {
            animFrame = Game1.random.Next(0, 3);
            frameIncrement = 1;
            srcRectangle = new Rectangle(0, 0, 16, 16);
            animTimer = new Timer(0.2f);
            ai = new AIHandler(this);
            ai.Push(new Roaming());
        }

        public override void OnDestroy()
        {
            Game1.hurt2Sfx.Play();
            for (int i = 0; i < numSplitoffs; i++)
            {
                Game1.objectHandler.AddObject(new Slime(splitSlime.life, splitSlime.maxLife, splitSlime.damage, new Rectangle(bounds.X, bounds.Y, splitSlime.bounds.Width, splitSlime.bounds.Height), bounds.Location, splitSlime.numSplitoffs, false, room, splitSlime.splitSlime));
            }

            for (int i = 0; i < 20; i++)
            {
                int num = Game1.random.Next(0, 2);
                if (num == 0)
                {
                    Game1.particleHandler.CreateParticles(1, 1, bounds.Center, new Color(66, 65, 49), 1f, 30);
                }
                else
                {
                    Game1.particleHandler.CreateParticles(1, 1, bounds.Center, new Color(87, 86, 66), 1f, 30);
                }

            }

            Coin.CreateCoin(parent, bounds.Location, Game1.random.Next(2, 4));
            if (Game1.random.Next(100) > 95)
            {
                Pickup.CreatePickup(5, bounds.Location, RoomShower.playerRoom);
            }
            Character.totalXP += 300;
        }

        public override void OnInteract(BaseObject caller)
        {
            if (caller is Walls || caller is Doors || caller is Slime)
            {
                bounds.X += -(int)vector.X;
                bounds.Y += -(int)vector.Y;
            }
            else if (caller is Projectile && !((Projectile)caller).isInert)
            {
                life -= ((Projectile)caller).damage;
                caller.destroy = true;
            }
        }

    }
}


