using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace Game3
{
    public abstract class BaseObject
    {
        public Rectangle bounds;
        public Vector2 vector;
        public ObjectHandler parent;
        public Room room;
        public int floor;
        public bool enabled = true;
        public bool destroy = false;
        public abstract void Update(GameTime gt);
        public abstract void Draw(SpriteBatch sb);
        public abstract void OnCreate();
        public abstract void OnInteract(BaseObject caller);
        public abstract void OnDestroy();

    }
}
