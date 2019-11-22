using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Game3
{
    public class HeartManager
    {
        ObjectHandler objectHandler = new ObjectHandler();
        List<Heart> hearts = new List<Heart>();
        //public void MoveBounds(int distance)
        //{
        //    bounds.X += distance;
        //}


        public void UpdateHearts()
        {
            hearts.Clear();
            Character character = Game1.objectHandler.SearchFirst<Character>();
            int lifeTracker = Character.life;
            Rectangle currentBound = new Rectangle(0, 0, 52, 52);
            for (int i = 0; i < Character.maxHearts; i++)
            {
                if (lifeTracker >= 2)
                {
                    hearts.Add(new Heart(currentBound, Game1.heartTextureFull));
                    lifeTracker -= 2;
                }
                else if (lifeTracker == 1)
                {
                    hearts.Add(new Heart(currentBound, Game1.heartTextureHalf));
                    lifeTracker--;
                }
                else
                {
                    hearts.Add(new Heart(currentBound, Game1.heartTextureEmpty));
                }
                currentBound.X += 55;
                if (currentBound.X > Game1.screenX - 55)
                {
                    currentBound.X = 0;
                    currentBound.Y += 55;
                }
            }
        }

        public void Update(GameTime gt)
        {
            UpdateHearts();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Heart heart in hearts)
            {
                heart.Draw(spritebatch);
            }
        }
    }
}
