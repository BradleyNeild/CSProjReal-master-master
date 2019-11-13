using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class MeleeAttack : BaseAI
    {
        public Point CurrentTargetTile = Point.Zero;
        public Point PreviousTile = Point.Zero;
        public Point NextTile = Point.Zero;
        public float speed = 2f;
        List<Point> path = null;
        public bool MoveComplete = true;
        public override void OnCreate()
        {
            
        }

        public override void Update(GameTime gt)
        {
            Character character = Game1.objectHandler.SearchFirst<Character>();
            
            if (PathFinding.ConvertThing(character.bounds.Location) != CurrentTargetTile)
            {
                CurrentTargetTile = PathFinding.ConvertThing(character.bounds.Location);
                Point boundLocationTile = PathFinding.ConvertThing(parent.slave.bounds.Location);
                path = PathFinding.FindPath(boundLocationTile, CurrentTargetTile);
            }
            else
            {
                parent.Pop();
            }

            if (path != null)
            {
                if (path.Count > 0)
                {
                    Point movingTo = path[path.Count - 1];
                    path.RemoveAt(path.Count - 1);
                    PreviousTile = parent.slave.bounds.Location;


                    int offsetSize = (45 - parent.slave.bounds.Width) / 2;


                    NextTile = (movingTo.ToVector2() * 45).ToPoint() + new Point(RoomShower.roomOffset) + new Point(offsetSize);

                    parent.Push(new TileMovingAI(PreviousTile, NextTile));
                }
            }


            //float temp = vector.X;
            //vector.X = vector.Y;
            //vector.Y = -temp;



            if (parent.slave.vector != Vector2.Zero)
            {
                parent.slave.vector.Normalize();
                parent.slave.vector *= speed;
                parent.slave.bounds.Location += parent.slave.vector.ToPoint();
            }
        }
    }
}
