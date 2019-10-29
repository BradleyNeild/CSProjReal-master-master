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
    class Items
    {
        bool buyable;
        int price, itemID;
        string name;
        Texture2D texture;
        public Items(bool itemBuyable, int itemPrice, int itemItemID, string itemName, Texture2D itemTexture)
        {
            buyable = itemBuyable;
            price = itemPrice;
            itemID = itemItemID;
            name = itemName;
        }

    }
}
