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
    public class Camera
    {

        Vector2 screenCentre;
        Vector2 position;
        float rotation = 0;
        float zoom = 1;

        public Camera(Vector2 cameraCentre, Vector2 cameraPosition)
        {
            screenCentre = cameraCentre;
            position = cameraPosition;
        }

        public Matrix WorldMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-position.X, -position.Y, 0) * Matrix.CreateScale(zoom,zoom, 0) * Matrix.CreateTranslation(screenCentre.X, screenCentre.Y, 0) * Matrix.CreateRotationZ(rotation);
            }
        }



        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Vector2 ScreenCentre
        {
            get
            {
                return screenCentre;
            }
            set
            {
                screenCentre = value;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = MathHelper.WrapAngle(value);
            }
        }

        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                if (value > 0)
                {
                    zoom = value;
                }
            }
        }

    }
}
