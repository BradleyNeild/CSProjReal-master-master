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
    public class Walls
    {
        public Texture2D texture;
        public Rectangle bounds;

        public Walls(Rectangle wallBounds, Texture2D wallTexture)
        {
            bounds = wallBounds;
            bounds.Width = 45;
            bounds.Height = 45;
            texture = wallTexture;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
        }
    }
}
