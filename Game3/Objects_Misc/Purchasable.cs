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
    public class Purchasable : BaseObject
    {
        public Timer cooldown = new Timer(0.5f);
        Character character = Game1.objectHandler.SearchFirst<Character>();
        Texture2D texture;
        public int price;
        public Pickup pickup;
        public bool bought = false;

        public Purchasable(Pickup purchPickup, Room purchRoom)
        {
            bounds = purchPickup.bounds;
            texture = purchPickup.texture;
            price = purchPickup.price;
            room = purchRoom;
            pickup = purchPickup;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (enabled)
            {
                string nameString = "";
                if (character.bounds.Intersects(new Rectangle(bounds.X -10, bounds.Y - 10, bounds.Width + 20, bounds.Height + 20)))
                {
                    nameString = pickup.name;
                }
                sb.Draw(texture, destinationRectangle: bounds);
                sb.DrawString(Game1.debugTextFont, price.ToString() + "\n" + nameString, new Vector2(bounds.Location.X, bounds.Location.Y + 40), Color.Yellow);

            }
        }

        public override void OnCreate()
        {

        }

        public override void OnDestroy()
        {

        }

        public override void OnInteract(BaseObject caller)
        {

        }

        public override void Update(GameTime gt)
        {
            cooldown.Update(gt);
            if (room == RoomShower.playerRoom)
            {
                enabled = true;
            }
            else
            {
                enabled = false;
            }
        }
    }
}
