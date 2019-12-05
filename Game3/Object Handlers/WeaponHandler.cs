using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class WeaponHandler
    {
        bool start = false;
        MouseState mouseState;
        KeyboardState keyboardState;

        public void ChangeWeapon(Weapon weapon)
        {
            Character.weaponHeld = weapon;
        }

        public void Update(GameTime gt)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            if (!start)
            {
                Character.weaponHeld = new Wand();
                start = true;
            }
            List<Weapon> weaponList = Game1.objectHandler.SearchArray<Weapon>();
            //clean weapons not held out
            for (int i = 0; i < weaponList.Count - 1; i++)
            {
                if (weaponList[i] != Character.weaponHeld)
                {
                    weaponList[i].destroy = true;
                }
            }
            if (!weaponList.Contains(Character.weaponHeld))
            {
                Game1.objectHandler.AddObject(Character.weaponHeld);
            }
            if (Game1.objectHandler.SearchFirst<Weapon>() != Character.weaponHeld)
            {
                Game1.objectHandler.SearchFirst<Weapon>().destroy = true;
            }
            foreach (Weapon weapon in Game1.objectHandler.SearchArray<Weapon>())
            {
                if (Game1.mouseOneTap.IsLeftPressed() || Game1.Key.IsPressed(Keys.Space))
                {
                    if (weapon != null)
                    {
                        weapon.OnClick();
                    }
                }

                if (mouseState.LeftButton == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Space))
                {
                    weapon.OnHold();
                }
            }
        }
    }
}
