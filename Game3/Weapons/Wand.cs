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
        Character character;
        public float direction;

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destinationRectangle: bounds, rotation: direction);
        }

        public override void OnClick()
        {
            
            var mouseState = Mouse.GetState();
            if (MagicMissile.noMissiles < MagicMissile.maxMissiles)
            {
                Game1.objectHandler.AddObject(new MagicMissile(new Rectangle(bounds.Location, Point.Zero), 1));
            }
        }

        public override void OnCreate()
        {
            dmgPerShot = 1;
            cooldown = new Timer(0f);
            texture = Game1.wandTexture;
            character = Game1.objectHandler.SearchFirst<Character>();
            bounds = character.bounds;
            bounds.Width = 6;
            bounds.Height = 32;
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
            var mouseState = Mouse.GetState();
            Vector2 vector = new Vector2(mouseState.X, mouseState.Y) - character.bounds.Center.ToVector2();
            direction = (float)Math.Atan2(vector.Y, vector.X) - 1.5f;
            bounds.Location = character.bounds.Center;
        }
    }
}
