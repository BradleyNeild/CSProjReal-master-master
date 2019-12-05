using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class Bullet : Projectile
    {
        Point shotAt;
        float direction;
        
        public Bullet(Point bulletShotAt, Rectangle bulletBounds)
        {
            bounds = bulletBounds;

            shotAt = bulletShotAt;
            vector = new Vector2(bulletShotAt.X, bulletShotAt.Y) - bounds.Center.ToVector2();
            direction = (float)Math.Atan2(vector.Y, vector.X);
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destinationRectangle: bounds);

        }

        public override void OnCreate()
        {
            texture = Game1.whitePixelTexture;
            damage = 1;
            vector = new Vector2(shotAt.X, shotAt.Y) - bounds.Center.ToVector2();
        }

        public override void OnDestroy()
        {

        }

        public override void OnInteract(BaseObject caller)
        {

                if (caller is Walls || caller is Doors)
                {
                    destroy = true;
                }
                        
        }

        public override void Update(GameTime gt)
        {

            

            if (vector != Vector2.Zero)
            {
                vector.Normalize();
                vector *= 8;
                bounds.Location += vector.ToPoint();
            }
            else
            {
                destroy = true;
            }
        }
    }
}
