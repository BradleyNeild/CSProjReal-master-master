﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Goblin
    {
            public Rectangle bounds;
            public Vector2 vector;
            public Texture2D texture;
            public float speed;
            public int health, maxHealth, power;
            public Goblin(int enemyHealth, int enemyMaxHealth, float enemySpeed, int enemyPower, Texture2D enemyTexture, Rectangle enemyBounds)
            {
                bounds = enemyBounds;
                health = enemyHealth;
                maxHealth = enemyMaxHealth;
                speed = enemySpeed;
                power = enemyPower;
                texture = enemyTexture;
            }

            public void Update(GameTime gameTime)
            {
                vector = Game1.characters[0].bounds.Center.ToVector2() - bounds.Center.ToVector2();

                if (vector != Vector2.Zero)
                {
                    vector.Normalize();
                    vector *= speed;
                    bounds.Location += vector.ToPoint();
                }


            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
            }
    }
}
