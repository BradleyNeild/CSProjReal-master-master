using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Goblin
    {
        public Color color = Color.White;
        public Rectangle bounds;
        public Vector2 vector;
        public Texture2D texture;
        public float speed = 4;
        public int health, maxHealth, power;
        public Rectangle spawnPoint;
        public DateTime spawnTime = DateTime.Now;
        public float waitTime = 0.5f;
        public bool frozen = true;
        public bool aggroed = false;
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
            if (Game1.characters[0].aggroed.Count <= Game1.characters[0].maxAggroed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckAggros()
        {
            bool found = false;
            int i = 0;

                do
                {
                    if (Game1.characters[0].aggroed[i] != null)
                    {
                        Game1.characters[0].aggroed[i].DeAggro();
                        found = true;
                    }
                    i++;
                }
                while (!found);


            
        }

        private void Aggro()
        {
            Console.WriteLine("Aggroed");
            Game1.characters[0].aggroed.Add(this);
            aggroed = true;
            
        }

        private void DeAggro()
        {
            Console.WriteLine("Deaggroed");
            aggroed = false;
            Game1.characters[0].aggroed.Remove(this);
        }

        public void Update(GameTime gameTime)
        {
            if (aggroed)
            {
                color = Color.Red;
            }
            else
            {
                color = Color.Green;
            }
            if (DateTime.Now > spawnTime.AddSeconds(waitTime) && frozen == true)
            {
                frozen = false;
            }
            if (aggroed)
            {
                if (!frozen)
                {
                    vector = Game1.characters[0].bounds.Center.ToVector2() - bounds.Center.ToVector2();

                    if (vector != Vector2.Zero)
                    {
                        vector.Normalize();
                        vector *= speed;
                        bounds.Location += vector.ToPoint();
                    }
                }
            }
            else if (AggrosUnderMax())
            {
                Aggro();
            }
            
            


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: color);
        }
    }
}
