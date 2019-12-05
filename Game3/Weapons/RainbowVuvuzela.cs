using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class RainbowVuvuzela : Weapon
    {
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destinationRectangle: bounds, rotation: direction, origin: new Vector2(0, 5));
        }

        public void ShootConfetti(int amount)
        {
            MouseState mouseState = Mouse.GetState();
            for (int i = 0; i < amount; i++)
            {
                int randomX = Game1.random.Next(-100, 100);
                int randomY = Game1.random.Next(-100, 100);
                Game1.objectHandler.AddObject(new Confetti(new Vector2(randomX, randomY), character.bounds.Center, dmgPerShot));
            }
        }

        public override void OnClick()
        {
            if (cooldown.Triggered)
            {
                ShootConfetti(15);
                cooldown.ResetTimer();
            }
            
        }

        public override void OnCreate()
        {
            dmgPerShot = 0.5f;
            cooldown = new Timer(0.2f);
            texture = Game1.rainbowVuvuzelaTexture;
            bounds = character.bounds;
            bounds.Width = 32;
            bounds.Height = 20;
        }

        public override void OnDestroy()
        {
            
        }

        public override void OnInteract(BaseObject caller)
        {
            
        }

        public override void OnRightClick()
        {
            
        }

        public override void Update(GameTime gt)
        {
            cooldown.Update(gt);
            var mouseState = Mouse.GetState();
            Vector2 vector = new Vector2(mouseState.X, mouseState.Y) - character.bounds.Center.ToVector2();
            direction = (float)Math.Atan2(vector.Y, vector.X);
            bounds.Location = character.bounds.Center;
        }

        public override void OnHold()
        {
        }
    }
}
