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
    public class TrapDoor : BaseObject
    {
        Texture2D texture;
        public bool opened = false;
        Rectangle srcRectangle;
        public TrapDoor(Rectangle trapDoorBounds)
        {
            bounds = trapDoorBounds;
            bounds.Width = 64;
            bounds.Height = 128;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, bounds, srcRectangle, color: Color.White);

        }

        public void EnterTrapDoor()
        {
            if (opened)
            {

                Game1.win = true;
            }
            
        }

        public override void OnCreate()
        {
            texture = Game1.trapDoorTexture;
        }

        public override void OnDestroy()
        {
            
        }

        public override void OnInteract(BaseObject caller)
        {
            
        }

        public override void Update(GameTime gt)
        {
            if (Game1.objectHandler.SearchFirst<Enemy>() == null)
            {
                opened = true;
            }
            else
            {
                opened = false;
            }
            if (!opened)
            {
                srcRectangle = new Rectangle(0, 0, 32, 64);
            }
            else
            {
                srcRectangle = new Rectangle(32, 0, 32, 64);
            }
        }
    }
}
