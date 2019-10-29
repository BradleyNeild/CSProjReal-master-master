using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    class Buttons
    {
        public Texture2D texture;
        public Rectangle bounds;
        public string text;

        public Buttons(Rectangle ButtonBounds, Texture2D buttonTexture, string buttonText)
        {
            bounds = ButtonBounds;
            texture = buttonTexture;
            text = buttonText;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(bounds.X / 2, bounds.Y / 2);
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White, origin: origin);
        }
    }
}
