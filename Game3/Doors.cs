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
    public class Doors
    {
        public Texture2D texture;
        public Rectangle bounds;
        public int direction;

        public Doors(Rectangle doorBounds, Texture2D doorTexture, int doorDirection)
        {
            bounds = doorBounds;
            bounds.Width = 47;
            bounds.Height = 47;
            texture = doorTexture;
            direction = doorDirection;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
        }
    }
}
