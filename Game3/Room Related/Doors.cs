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
    public class Doors:BaseObject
    {
        public Texture2D texture;
        public int direction;
        public bool enterable;

        public Doors(Rectangle doorBounds, int doorDirection)
        {
            bounds = doorBounds;
            bounds.X -= 2;
            bounds.Y -= 2;
            bounds.Width = Walls.wallSize + 6;
            bounds.Height = Walls.wallSize + 6;
            texture = Game1.doorTexture;
            direction = doorDirection;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: new Rectangle(bounds.X+5, bounds.Y+5, Walls.wallSize, Walls.wallSize), color: Color.White);
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
            if (Game1.objectHandler.SearchFirstEnabled<Slime>() != null)
            {
                texture = Game1.wallTexture;
                enterable = false;
            }
            else
            {
                texture = Game1.doorTexture;
                enterable = true;
            }
        }
    }
}
