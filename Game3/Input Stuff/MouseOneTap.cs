using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class MouseOneTap
    {
        MouseState lastClick;
        MouseState currentClick;

        public bool IsLeftPressed()
        {
            if (currentClick.LeftButton == ButtonState.Pressed && lastClick.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRightPressed()
        {
            if (currentClick.RightButton == ButtonState.Pressed && lastClick.RightButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update(GameTime gameTime)
        {
            lastClick = currentClick;
            currentClick = Mouse.GetState();
        }
    }
}
