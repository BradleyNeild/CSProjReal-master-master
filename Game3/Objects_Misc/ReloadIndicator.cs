using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class ReloadIndicator : BaseObject
    {
        int yOff = 0;
        Texture2D texture = Game1.whitePixelTexture;
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, bounds, Color.White);
        }

        public override void OnCreate()
        {
            bounds.Width = 8;
        }

        public override void OnDestroy()
        {
            
        }

        public override void OnInteract(BaseObject caller)
        {
            
        }

        public override void Update(GameTime gt)
        {
            MouseState mouseState = Mouse.GetState();
            Pistol pistol = Game1.objectHandler.SearchFirst<Pistol>();
            if (pistol != null)
            {
                if (pistol.reloadTimer.Triggered)
                {
                    bounds.Height = 0;
                }
                else
                {
                    bounds.Height = (int)MathHelper.Lerp(50f, 0, pistol.reloadTimer.PercentageDone);
                    yOff = (int)MathHelper.Lerp(50f, 0, pistol.reloadTimer.PercentageDone);
                }
                
            }
            else
            {
                bounds.Height = 0;
            }
            
            bounds.X = mouseState.X + 5;
            bounds.Y = mouseState.Y - yOff;
        }
    }
}
