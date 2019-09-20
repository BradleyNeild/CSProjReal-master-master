using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game3
{
    public class Character
    {
        public Rectangle bounds;
        public int moveSpeed;
        private Texture2D texture;
        public Vector2 vector;
        List<Hearts> hearts = new List<Hearts>();
        
        public Character(Rectangle playerBounds, Texture2D playerTexture, int playerMoveSpeed)
        {
            texture = playerTexture;
            bounds = playerBounds;
            bounds.Width = 32;
            bounds.Height = 32;
            moveSpeed = playerMoveSpeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
        }



        public void Update(GameTime gameTime)
        {

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
