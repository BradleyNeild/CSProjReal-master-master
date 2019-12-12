using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Pickup:BaseObject
    {
        public bool shoppable;
        public bool randomDroppable;
        public int price;
        public string name;
        public int effID;
        public Texture2D texture;
        static int rerollerprice = 10;

        public static List<Pickup> pickups = new List<Pickup>()
        {
            new Pickup("Heart Container", 0, 30, Game1.heartTextureEmpty, true, true),
            new Pickup("Kite", 1, 40, Game1.kiteTexture, true, true),
            new Pickup("Medkit", 2, 20, Game1.medkitTexture, true, true),
            new Pickup("Extended Mag", 3, 40, Game1.magazineTexture, true, true),
            new Pickup("Rapid Fire", 4, 40, Game1.rapidTexture, true, true),
            new Pickup("Diamond", 5, 0, Game1.diamondTexture, false, true),
            new Pickup("Reroller", 6, rerollerprice, Game1.rerollerTexture, false, false)
        };

        public static void CreatePickup(int i, Point position, Room room)
        {
            Pickup newPickup = new Pickup(pickups[i].name, pickups[i].effID, pickups[i].price, pickups[i].texture, pickups[i].shoppable, pickups[i].randomDroppable);
            newPickup.bounds.Location = position;
            newPickup.room = room;
            Game1.objectHandler.AddObject(newPickup);
        }

        public Pickup(string pickupName, int effectID, int pickupPrice, Texture2D pickupTexture, bool pickupShoppable, bool randomDroppable)
        {
            price = pickupPrice;
            shoppable = pickupShoppable;
            name = pickupName;
            effID = effectID;
            texture = pickupTexture;
            bounds.Width = 32;
            bounds.Height = 32;
        }

        public void Effect(int effectID)
        {
            switch (effectID)
            {
                case 0:
                    //heart container
                    Character.maxHearts++;
                    destroy = true;
                    break;
                case 1:
                    //kite
                    Character.moveSpeed += 0.3f;
                    destroy = true;
                    break;
                case 2:
                    //medkit
                    Game1.objectHandler.SearchFirst<Character>().Heal(2);
                    destroy = true;
                    break;
                case 3:
                    //extended mag
                    Character.persistentEffects.Add(3);
                    destroy = true;
                    break;
                case 4:
                    //rapid fire
                    Character.persistentEffects.Add(4);
                    destroy = true;
                    break;
                case 5:
                    //diamond
                    Game1.coinCount += 50;
                    destroy = true;
                    break;
                case 6:
                    //reroller
                    ProcGen2.GeneratePurchasables(true);
                    break;
                default:
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (enabled)
            {
                spriteBatch.Draw(texture, destinationRectangle: bounds, color: Color.White);
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
            if (enabled)
            {
                if (caller is Character)
                {
                    Effect(effID);
                    destroy = true;
                }
            }
            
        }

        public override void Update(GameTime gt)
        {
            if (ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY] == room)
            {
                enabled = true;
            }
            else
            {
                enabled = false;
            }
            if (enabled)
            {

            }
        }
    }
}
