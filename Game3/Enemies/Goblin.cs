using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Goblin : Enemy
    {
        AIHandler ai;
        ObjectHandler objectHandler = new ObjectHandler();
        
        public Color color = Color.White;
        public Texture2D texture;
        
        public float speed = 2;
        public float unaggroedSpeed = 2;
        public int health, maxHealth, power;
        public Rectangle spawnPoint;
        public bool frozen = true;
        public bool aggroed = false;

        public static int numAggroed = 0;
        public const int maxAggroed = 3;
        public Goblin(int enemyHealth, int enemyMaxHealth, int enemyPower, Texture2D enemyTexture, Rectangle enemyBounds, Rectangle enemySpawnPoint)
        {
            bounds = enemyBounds;
            health = enemyHealth;
            maxHealth = enemyMaxHealth;
            power = enemyPower;
            texture = enemyTexture;
            spawnPoint = enemySpawnPoint;
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

        private void Aggro()
        {
            Console.WriteLine("iowtruwhuowihiowio3jripogi0g");
            ai.Push(new MeleeAttack());
            numAggroed++;
            aggroed = true;

        }

        private void DeAggro()
        {
            //Console.WriteLine("Deaggroed");
            aggroed = false;
            numAggroed--;
        }



        public override void Update(GameTime gameTime)
        {
            ai.Update(gameTime);
            if (health <= 0)
            {
                destroy = true;
            }
            if (AggrosUnderMax() && !aggroed)
            {
                Aggro();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: color);
        }

        public override void OnCreate()
        {
            ai = new AIHandler(this);
            ai.Push(new Roaming());
        }

        public override void OnDestroy()
        {
            Coin.CreateCoin(parent, bounds.Location, Game1.random.Next(2, 4));
            Character.totalXP += 300;
        }

        public override void OnInteract(BaseObject caller)
        {
            if (caller is Walls)
            {
                bounds.X += -(int)vector.X;
                bounds.Y += -(int)vector.Y;
            }
            else if (caller is Doors)
            {
                bounds.X += -(int)vector.X;
                bounds.Y += -(int)vector.Y;
            }
            else if (caller is MagicMissile)
            {
                health--;
                caller.destroy = true;
            }
        }

    }
}


