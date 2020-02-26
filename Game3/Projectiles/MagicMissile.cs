using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace Game3
{
    class MagicMissile : Projectile
    {
        public Vector2 mousePos;
        float direction;
        public Timer deathTimer = new Timer(2f);
        public static int noMissiles;
        public static int maxMissiles;

        private void MoveMissileCursor(Vector2 vector)
        {
            if (!bounds.Contains(mousePos))
            {
                vector.Normalize();
                vector *= 5;
                bounds.Location += vector.ToPoint();
            }
        }

        public void MoveMissile(Vector2 vector)
        {
            vector.Normalize();
            vector *= 5;
            bounds.Location += vector.ToPoint();
        }

        public MagicMissile(Rectangle missileBounds, int missilePower)
        {
            bounds = missileBounds;
            bounds.Width = 18;
            bounds.Height = 9;
            texture = Game1.missileTexture;
            damage = missilePower;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: new Rectangle(bounds.X, bounds.Y, 36, 9), color: Color.HotPink, rotation: direction, origin: new Vector2(texture.Width / 2, texture.Height / 2));
        }



        public override void Update(GameTime gameTime)
        {
            deathTimer.Update(gameTime);
            if (deathTimer.Triggered)
            {
                destroy = true;
            }
            var mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);
            vector = new Vector2(mouseState.X, mouseState.Y) - bounds.Center.ToVector2();

            if (vector != Vector2.Zero)
            {
                vector.Normalize();
                vector *= 8;
                bounds.Location += vector.ToPoint();
            }
            direction = (float)Math.Atan2(vector.Y, vector.X);

        }
        public override void OnCreate()
        {
            noMissiles++;
        }

        public override void OnDestroy()
        {
            noMissiles--;
        }

        public override void OnInteract(BaseObject caller)
        {
            if (caller is MagicMissile || caller is Walls || caller is Doors)
            {
                destroy = true;
            }
        }
    }
}
