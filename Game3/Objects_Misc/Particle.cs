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
    public class Particle : BaseObject
    {
        Texture2D texture;
        Color color;
        Timer lifeTime;
        float gravity;
        int yFloor;
        public Particle(Texture2D particleTexture, Color particleColor, Rectangle particleBounds, Vector2 particleVector, Timer particleLifeTime, float particleGravity)
        {
            texture = particleTexture;
            color = particleColor;
            bounds = particleBounds;
            int randSize = Game1.random.Next(7, 10);
            bounds.Width = randSize;
            bounds.Height = randSize;
            vector = particleVector;
            lifeTime = particleLifeTime;
            yFloor = bounds.Y + 30;
            gravity = particleGravity;
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
            
        }

        public override void OnInteract(BaseObject caller)
        {
            if(caller is Walls || caller is Doors)
            {
                bounds.X -= (int)vector.X;
                bounds.Y -= (int)vector.Y;
                //vector = -vector;
            }
        }

        public override void Update(GameTime gt)
        {
            lifeTime.Update(gt);
            vector.Y += gravity;
            bounds.X += (int)vector.X;
            
            if (bounds.Y < yFloor)
            {
                bounds.Y += (int)vector.Y;
            }
            else
            {
                vector.X = 0;
            }
            if(lifeTime.Triggered)
            {
                destroy = true;
            }
        }
    }
}
