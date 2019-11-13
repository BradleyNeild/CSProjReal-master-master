using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class MinimapRoom
    {

        Texture2D texture = Game1.miniRoomTexture;
        Texture2D overlay = Game1.questionTexture;
        int posX, posY, rPosX, rPosY;
        Color textureColor = Color.White;
        Color overlayColor = Color.Transparent;
        Vector2 overlaySize = new Vector2(16, 16);
        public MinimapRoom(int miniPosX, int miniPosY, int miniRPosX, int miniRPosY)
        {
            posX = miniPosX;
            posY = miniPosY;
            rPosX = miniRPosX;
            rPosY = miniRPosY;
        }

        public void Update(GameTime gameTime)
        {
            if (RoomShower.playerRoomX == rPosX && RoomShower.playerRoomY == rPosY)
            {
                textureColor = Color.Lime;
            }
            else
            {
                textureColor = Color.White;
            }

            if(ProcGen2.roomNodes[rPosX, rPosY].gobinsContained.Count > 0 || !ProcGen2.roomNodes[rPosX, rPosY].isExplored)
            {
                overlay = Game1.questionTexture;
                overlayColor = Color.White;
            }

            else if (ProcGen2.roomNodes[rPosX, rPosY].isShop)
                {
                overlay = Game1.coinTexture;
                overlaySize = new Vector2(11, 16);
                overlayColor = Color.White;
            }
            else if (ProcGen2.roomNodes[rPosX, rPosY].isBoss)
            {
                overlay = Game1.skullTexture;
                overlaySize = new Vector2(16, 16);
                overlayColor = Color.White;
            }
            else
            {
                overlayColor = Color.Transparent;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(posX * 16, posY * 16 + 250, 16, 16), textureColor);
            spriteBatch.Draw(overlay, new Rectangle(posX * 16, posY * 16 + 250, (int)overlaySize.X, (int)overlaySize.Y), overlayColor);
        }


    }
}
