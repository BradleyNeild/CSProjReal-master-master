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
        public static Color color = new Color(0, 0, 0);
        Color thisColor;
        Texture2D texture = Game1.whitePixelTexture;
        public FloorTile(Rectangle tileBounds)
        {
            bounds = tileBounds;
            color.R += 2;
            color.G += 2;
            color.B += 2;
            thisColor = color;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (Game1.greyTiles)
            {
                sb.Draw(texture, destinationRectangle: bounds, color: new Color(Game1.random.Next(255), Game1.random.Next(255), Game1.random.Next(255)));

            }
            else
            {

                sb.Draw(texture, destinationRectangle: bounds, color: Color.MediumPurple);
            }
            
        }

        public override void OnCreate()
        {
            bounds.Width = Walls.wallSize;
            bounds.Height = Walls.wallSize;
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
