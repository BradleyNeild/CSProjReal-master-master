using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3
{
    public class Balloon : BaseObject
    {
        Texture2D texture = Game1.balloonTexture;
        int health = 3;
        Color color;

        public Balloon(Rectangle balloonBounds, Color balloonColor)
        {
            bounds = balloonBounds;
            color = balloonColor;
            bounds.Width = 36;
            bounds.Height = 64;
        }
        public static void CreateBalloon(Point position)
        {
            Game1.objectHandler.AddObject(new Balloon(new Rectangle(position, Point.Zero), Color.Red));
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destinationRectangle: bounds, color: color);
        }

        public override void OnCreate()
        {
            
        }

        public override void OnDestroy()
        {
            Game1.particleHandler.CreateRainbowParticles(40, 3, bounds.Center, 0.2f);
            //Game1.objectHandler.AddObject(new Pickup("Heart Container", 0, bounds.Location));
        }

        public override void OnInteract(BaseObject caller)
        {
            if (caller is MagicMissile)
            {
                health--;
                caller.destroy = true;
            }
        }

        public override void Update(GameTime gt)
        {
            if (health <= 0)
            {
                destroy = true;
            }
            bounds.X += (int)vector.X;
            bounds.Y += (int)vector.Y;
        }
    }
}
