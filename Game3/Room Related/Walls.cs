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
    public class Walls:BaseObject
    {
        public static int wallSize = 64;
        public Texture2D texture;

        public Walls(Rectangle wallBounds, Texture2D wallTexture)
        {
            bounds = wallBounds;
            bounds.Width = wallSize;
            bounds.Height = wallSize;
            texture = wallTexture;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
        }

        public override void OnCreate()
        {
            
        }

        public override void OnDestroy()
        {
            
        }

        public override void OnInteract(BaseObject caller)
        {

        }

        public override void Update(GameTime gt)
        {
            
        }
    }
}
