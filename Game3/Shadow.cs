using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Shadow
    {
        bool started = false;
        bool linkedToBoss;
        bool linkedToGoblin;
        Texture2D texture = Game1.shadowTexture;
        Rectangle bounds;
        Boss linkedBoss;
        Goblin linkedGoblin;

        public Shadow(Boss shadowLinkedBoss, Goblin shadowLinkedGoblin)
        {
            linkedBoss = shadowLinkedBoss;
            linkedGoblin = shadowLinkedGoblin;
            if (linkedBoss != null)
            {
                linkedToBoss = true;
            }
            if (linkedGoblin != null)
            {
                linkedToGoblin = true;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!started)
            {
                started = true;
                if (linkedBoss != null)
                {
                    bounds = linkedBoss.bounds;
                }
                else
                    bounds = linkedGoblin.bounds;
                
            }
            if (linkedBoss != null)
            {
                bounds.Location = linkedBoss.bounds.Location;
            }
            if (linkedGoblin != null)
            {
                bounds.Location = linkedGoblin.bounds.Location;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.goblins.Contains(linkedGoblin))
            {

            }
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
        }
    }
}
