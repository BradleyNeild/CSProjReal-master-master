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

        public void ChangeWeapon(Weapon weapon)
        {
            Character.weaponHeld = weapon;
        }

        public void Update(GameTime gt)
        {
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
        }
    }
}
