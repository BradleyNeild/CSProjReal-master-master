using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public abstract class Weapon:BaseObject
    {
        public float dmgPerShot;
        public Timer cooldown;
        public Texture2D texture;
        public MouseOneTap mouseOneTap = new MouseOneTap();
        public Character character = Game1.objectHandler.SearchFirst<Character>();
        public float direction;

        public abstract void OnClick();

        public abstract void OnRightClick();

    }
}
