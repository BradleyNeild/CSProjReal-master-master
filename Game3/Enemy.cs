using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    class Enemy
    {
        public Texture2D texture;
        public int health, maxHealth, speed, power;
        public string aiType;
        public Enemy(int enemyHealth, int enemyMaxHealth, int enemySpeed, int enemyPower, string enemyAIType, Texture2D enemyTexture)
        {
            health = enemyHealth;
            maxHealth = enemyMaxHealth;
            speed = enemySpeed;
            power = enemyPower;
            aiType = enemyAIType;
            texture = enemyTexture;
        }
    }
}
