using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Confetti : Projectile
    {
        Timer deathTimer = new Timer(1.5f);
        Color color = new Color(Game1.random.Next(100,255), Game1.random.Next(100, 255), Game1.random.Next(100, 255));
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destinationRectangle: bounds, color: color);
        }

        public Confetti(Vector2 confettiVector, Point confettiSpawn, float confettiPower)
        {
            damage = confettiPower;
            vector = confettiVector;
            bounds.Location = confettiSpawn;
            bounds.Width = 8;
            bounds.Height = 8;
        }

        public override void OnCreate()
        {
            vector.Normalize();
            Console.WriteLine(vector);
            texture = Game1.whitePixelTexture;
        }

        public override void OnDestroy()
        {
            
        }

        public override void OnInteract(BaseObject caller)
        {
            if (caller is Walls || caller is Doors)
            {
                bounds.X = (int)-vector.X;
                bounds.Y = (int)-vector.Y;
                vector = Vector2.Zero;
            }
        }

        public override void Update(GameTime gt)
        {
            deathTimer.Update(gt);
            if (deathTimer.Triggered)
            {
                destroy = true;
                deathTimer.ResetTimer();
            }
            if (vector != Vector2.Zero)
            {
                vector.Normalize();
                vector *= 5;
                bounds.Location += vector.ToPoint();
            }

        }
    }
}
