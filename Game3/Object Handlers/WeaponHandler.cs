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
        public void Update(GameTime gt)
        {
            Character.weaponHeld = new Wand();
        }
    }
}
