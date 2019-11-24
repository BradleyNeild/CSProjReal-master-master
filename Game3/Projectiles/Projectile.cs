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
    public abstract class Projectile : BaseObject
    {
        public bool isInert = false;
        public Texture2D texture;
        public float damage;
    }
}
