using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class KeyboardOneTap
    {
        KeyboardState lastKey;
        KeyboardState currentKey;

        public bool IsPressed(Keys Key)
        {
            return (lastKey.IsKeyDown(Key) ^ currentKey.IsKeyDown(Key)) && currentKey.IsKeyDown(Key);
        }

        public void Update(GameTime gameTime)
        {
            lastKey = currentKey;
            currentKey = Keyboard.GetState();
        }
    }
}
