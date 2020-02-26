using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game3
{
    class TileMovingAI : BaseAI
    {
        Point NextTile, PreviousTile;
        Timer MoveTimer = new Timer(0.3f);

        public TileMovingAI(Point start, Point end)
        {
            PreviousTile = start;
            NextTile = new Point(end.X, end.Y);
        }


        public override void OnCreate()
        {
            
        }

        public override void Update(GameTime gt)
        {
            MoveTimer.Update(gt);
            if (MoveTimer.Triggered)
            {
                parent.Pop();
                return;
            }

            int NewX = (int)MathHelper.Lerp(PreviousTile.X, NextTile.X, MoveTimer.PercentageDone);
            int NewY = (int)MathHelper.Lerp(PreviousTile.Y, NextTile.Y, MoveTimer.PercentageDone);

            parent.slave.bounds.Location = new Point(NewX, NewY);



        }
    }
}
