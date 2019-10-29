using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Pickup
    {
        string name;
        public int effID;
        Texture2D texture;
        public Rectangle bounds;

        public Pickup(string pickupName, int effectID, Texture2D pickupTexture, Rectangle pickupBounds)
        {
            name = pickupName;
            effID = effectID;
            texture = pickupTexture;
            bounds = pickupBounds;
        }

        public static void Effect(int effectID)
        {
            if (effectID == 0)
            {
                Ghost.SpawnSelf();
                Game1.AddHeart(0, 1);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
        }
    }
}
