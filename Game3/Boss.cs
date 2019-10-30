using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Boss
    {
        int health;
        int maxHealth;
        public Rectangle bounds;
        bool inAir = false;

        public Boss(int bossHealth, int bossMaxHealth)
        {
            health = bossHealth;
            maxHealth = bossMaxHealth;
        }

        private void Jump(Vector2 destination)
        {
            inAir = true;

            inAir = false;
        }

        public void Update(GameTime gameTime)
        {
            
        }
        }
}
