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

        public Doors(Rectangle doorBounds, Texture2D doorTexture, int doorDirection)
        {
            bounds = doorBounds;
            bounds.Width = 49;
            bounds.Height = 49;
            texture = doorTexture;
            direction = doorDirection;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.currentDoorTexture, destinationRectangle: bounds, color: Color.White);
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
