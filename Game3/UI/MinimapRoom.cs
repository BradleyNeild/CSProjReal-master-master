using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class MinimapRoom
    {
        int offsetX = 300;
        Texture2D texture = Game1.miniRoomTexture;
        Texture2D overlay = Game1.questionTexture;
        int posX, posY, rPosX, rPosY;
        Color textureColor = Color.White;
        Color overlayColor = Color.Transparent;
        Vector2 overlaySize = new Vector2(16, 16);
        List<Slime> slimes;
        public MinimapRoom(int miniPosX, int miniPosY, int miniRPosX, int miniRPosY)
        {
            posX = miniPosX;
            posY = miniPosY;
            rPosX = miniRPosX;
            rPosY = miniRPosY;
        }


        public void CheckStatus()
        {
            slimes = Game1.objectHandler.SearchArray<Slime>();
            foreach (Slime slime in slimes)
            {
                if (slime.room == ProcGen2.roomNodes[rPosX, rPosY])
                {
                    overlay = Game1.questionTexture;
                    overlayColor = Color.White;
                    break;
                }
                else
                {
                    overlayColor = Color.Transparent;
                }

            }
            if (ProcGen2.roomNodes[rPosX, rPosY].isShop)
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

            if (RoomShower.playerRoomX == rPosX && RoomShower.playerRoomY == rPosY)
            {
                textureColor = Color.Lime;
            }
            else
            {
                textureColor = Color.White;
            }
        }

        public void Update(GameTime gameTime)
        {
            
                CheckStatus();
            


            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: new Rectangle(posX * 16, posY * 16, 16, 16), color: textureColor, layerDepth: 0.1f);
            spriteBatch.Draw(overlay, destinationRectangle: new Rectangle(posX * 16, posY * 16, (int)overlaySize.X, (int)overlaySize.Y), color: overlayColor, layerDepth: 0f);
        }


    }
}
