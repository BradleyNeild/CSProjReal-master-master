using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public abstract class Enemy : BaseObject
    {
        public AIHandler ai;
        public Texture2D texture;
        public Timer animTimer;
        public int animFrame;
        public int frameIncrement = 1;
        public Rectangle srcRectangle;
        public float life, maxLife;
        public int damage;
        public Point spawnPoint;
    }
}
