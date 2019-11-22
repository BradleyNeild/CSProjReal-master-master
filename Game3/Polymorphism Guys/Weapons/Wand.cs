using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Wand : Weapon
    {
        bool start = false;

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destinationRectangle: bounds);
        }

        public override void OnClick()
        {
            
            var mouseState = Mouse.GetState();
            if (MagicMissile.noMissiles < MagicMissile.maxMissiles)
            {
                Game1.objectHandler.AddObject(new MagicMissile(new Rectangle(mouseState.Position, Point.Zero), 1));
            }
        }

        public override void OnCreate()
        {
            dmgPerShot = 1;
            cooldown = new Timer(0f);
            texture = Game1.wandTexture;
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
            if (mouseOneTap.IsLeftPressed())
            {
                OnClick();
            }
            if (mouseOneTap.IsRightPressed())
            {
                OnRightClick();
            }

        }
    }
}
