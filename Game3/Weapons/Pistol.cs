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
    public class Pistol : Weapon
    {
        bool reloading = false;
        public int ammo = 8, maxAmmo = 8;
        public Timer reloadTimer = new Timer(1f);
        Vector2 mousePos;
        int currentRapidFires = 0;
        SpriteEffects spriteEffect = SpriteEffects.None;
        public override void Draw(SpriteBatch sb)
        {
            Vector2 HoldPos = new Vector2(0, texture.Height / 2);
            spriteEffect = SpriteEffects.None;
            if (direction > MathHelper.PiOver2 || direction < -MathHelper.PiOver2)
            {
                //HoldPos = new Vector2(texture.Width,texture.Height/2);
                spriteEffect = SpriteEffects.FlipVertically;
            }
            sb.Draw(texture, bounds, null, Color.White, direction, HoldPos, spriteEffect, 0f);
        }

        public void Reload()
        {
            if (ammo < maxAmmo)
            {
                if (!reloading)
                {
                    Game1.reloadSfx.Play();
                    reloading = true;
                    reloadTimer.ResetTimer();
                }
            }

        }

        public override void OnClick()
        {
            if (ammo > 0)
            {
                if (cooldown.Triggered)
                {
                    Game1.shootSfx.Play();
                    int rapidFires = 0;
                    foreach (int effect in Character.persistentEffects)
                    {
                        if (effect == 4)
                        {
                            rapidFires+= 1;
                        }
                    }
                    if (currentRapidFires != rapidFires && rapidFires != 0)
                    {
                        cooldown.Target = 0.2f / (rapidFires + 1);
                        currentRapidFires = rapidFires;
                    }
                    cooldown.ResetTimer();
                    var mouseState = Mouse.GetState();
                    mousePos = new Vector2(mouseState.X, mouseState.Y);
                    Game1.objectHandler.AddObject(new Bullet(new Point((int)mousePos.X, (int)mousePos.Y), new Rectangle(character.bounds.Center.X, character.bounds.Center.Y, 10, 10)));
                    ammo--;
                }
            }
            else if (!reloading && cooldown.Triggered)
            {

                Reload();
            }

        }

        public override void OnCreate()
        {

            cooldown = new Timer(0.2f);
            texture = Game1.pistolTexture;
            bounds.Width = 26;
            bounds.Height = 17;
        }



        public override void OnDestroy()
        {

        }

        public override void OnHold()
        {
            OnClick();
        }

        public override void OnInteract(BaseObject caller)
        {

        }

        public override void OnRightClick()
        {

        }

        public override void Update(GameTime gt)
        {

            int extendedMags = 0;
            foreach (int effect in Character.persistentEffects)
            {
                if (effect == 3)
                {
                    extendedMags++;
                }
            }
            maxAmmo = (8 + extendedMags * 4);// - (currentRapidFires * 2);
            cooldown.Update(gt);
            reloadTimer.Update(gt);
            MouseState mouseState = Mouse.GetState();
            Vector2 vector = new Vector2(mouseState.X, mouseState.Y) - character.bounds.Center.ToVector2();
            direction = (float)Math.Atan2(vector.Y, vector.X);
            bounds.X = character.bounds.Center.X;
            bounds.Y = character.bounds.Center.Y + 10;
            if (reloadTimer.Triggered && reloading)
            {
                Game1.reload1Sfx.Play(1f, 0.5f, 0);
                reloading = false;
                ammo = maxAmmo;
            }
        }
    }
}