using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game3
{
    public class Character:BaseObject
    {
        public int life, maxLife;
        public int moveSpeed;
        private Texture2D texture;
        List<Hearts> hearts = new List<Hearts>();
        public int level;
        public float totalXP;
        bool showLevelUp = false;
        DateTime showTime;
        
        public List<float> levelBrackets = new List<float>()
        {
            
        
        };



        public void GenerateBrackets()
        {
            float num = 2000;
            for (int i = 0; i < 100; i++)
            {
                levelBrackets.Add(num);
                num = num *2 * 1.1f;
            }
        }

        
        public Character(Rectangle playerBounds, Texture2D playerTexture, int playerMoveSpeed)
        {
            texture = playerTexture;
            bounds = playerBounds;
            bounds.Width = 32;
            bounds.Height = 32;
            moveSpeed = playerMoveSpeed;
        }

        public override void OnInteract(BaseObject caller)
        {
            if (caller is Goblin)
            {

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
