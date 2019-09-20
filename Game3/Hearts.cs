using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Game3
{
    class Hearts
    {
        public Texture2D texture;
        public Rectangle bounds;
        Vector2 Vector;
        public int fullness;
        
        public void moveBounds(int distance)
        {
            bounds.X += distance;
        }

        public Hearts(Rectangle HeartBounds, Texture2D heartTexture, int heartFullness)
        {
            bounds = HeartBounds;
            bounds.Width = 54;
            bounds.Height = 54;
            texture = heartTexture;
            fullness = heartFullness;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(bounds.X / 2, bounds.Y / 2);
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
        }
    }
}
