using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Pickup:BaseObject
    {
        public string name;
        public int effID;
        public Texture2D texture;
        List<Texture2D> textures = new List<Texture2D>()
        {
            Game1.heartTextureEmpty,
            Game1.kiteTexture,
            Game1.medkitTexture,
            Game1.magazineTexture,
            Game1.rapidTexture,
            Game1.diamondTexture
        };

        public static List<string> names = new List<string>()
        {
            "Heart Container",
            "Kite",
            "Medkit",
            "Extended Mag",
            "Rapid Fire",
            "Diamond"
        };

        public Pickup(int effectID, Point pickupPosition, Room pickupRoom)
        {
            name = names[effectID];
            room = pickupRoom;
            effID = effectID;
            texture = textures[effectID];
            bounds.Location = pickupPosition;
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
                    break;
                case 1:
                    //kite
                    Character.moveSpeed += 0.3f;
                    break;
                case 2:
                    //medkit
                    Character.life += 2;
                    break;
                case 3:
                    //extended mag
                    Character.persistentEffects.Add(3);
                    break;
                case 4:
                    //rapid fire
                    Character.persistentEffects.Add(4);
                    break;
                case 5:
                    //diamond
                    Game1.coinCount += 50;
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
