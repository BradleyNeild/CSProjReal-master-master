using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Wand : Weapon
    {

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destinationRectangle: bounds, rotation: direction, origin: new Vector2(1, 0));
        }

        public override void OnClick()
        {
            if (cooldown.Triggered)
            {
                if (MagicMissile.noMissiles < MagicMissile.maxMissiles)
                {
                    Game1.objectHandler.AddObject(new MagicMissile(new Rectangle(character.bounds.Center, Point.Zero), 1));
                }
                cooldown.ResetTimer();
            }

        }

        public override void OnCreate()
        {
            dmgPerShot = 1;
            cooldown = new Timer(0.2f);
            texture = Game1.wandTexture;
            bounds = character.bounds;
            bounds.Width = 6;
            bounds.Height = 32;
        }

        public override void OnDestroy()
        {

        }

        public override void OnHold()
        {
            if (cooldown.Triggered)
            {
                if (MagicMissile.noMissiles < MagicMissile.maxMissiles)
                {
                    Game1.objectHandler.AddObject(new MagicMissile(new Rectangle(character.bounds.Center, Point.Zero), 1));
                }
                cooldown.ResetTimer();
            }
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
            MouseState mouseState = Mouse.GetState();
            Vector2 vector = new Vector2(mouseState.X, mouseState.Y) - character.bounds.Center.ToVector2();
            direction = (float)Math.Atan2(vector.Y, vector.X) - 1.5f;
            bounds.Location = character.bounds.Center;
        }
    }
}
