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
    public class Heart
    {
        public Texture2D texture;
        public Rectangle bounds;
        public Heart(Rectangle heartBounds, Texture2D heartTexture)
        {
            bounds = heartBounds;
            texture = heartTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
        }
        public void Update(GameTime gt)
        {
            
        }

    }
}
