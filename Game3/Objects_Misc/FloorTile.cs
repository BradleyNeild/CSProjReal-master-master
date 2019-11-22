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
    public class FloorTile:BaseObject
    {
        Texture2D texture = Game1.floorTexture;
        public FloorTile(Rectangle tileBounds)
        {
            bounds = tileBounds;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destinationRectangle: bounds, layerDepth: 1f);
        }

        public override void OnCreate()
        {
            bounds.Width = 45;
            bounds.Height = 45;
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
