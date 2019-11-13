using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public abstract class BaseAI
    {
        public AIHandler parent;

        public abstract void Update(GameTime gt);
        public abstract void OnCreate();

    }
}
