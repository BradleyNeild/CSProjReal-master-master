using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Pickup:BaseObject
    {
        string name;
        public int effID;
        Texture2D texture;

        public Pickup(string pickupName, int effectID, Texture2D pickupTexture, Point pickupPosition)
        {
            name = pickupName;
            effID = effectID;
            texture = pickupTexture;
            bounds.Location = pickupPosition;
            bounds.Width = 40;
            bounds.Height = 40;
        }

        public void Effect(int effectID)
        {
            if (effectID == 0)
            {
                Character.maxHearts++;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
        }

        public override void OnCreate()
        {

        }

        public override void OnDestroy()
        {

        }

        public override void OnInteract(BaseObject caller)
        {
            if (caller is Character)
            {
                Effect(effID);
                destroy = true;
            }
        }

        public override void Update(GameTime gt)
        {

        }
    }
}
