using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Roaming : BaseAI
    {
        Timer timer = new Timer(2f);
        Vector2 vector;
        int speed = 2;

        public override void OnCreate()
        {
            vector = new Vector2(Game1.random.Next(-100, 100), Game1.random.Next(-100, 100));
            vector.Normalize();
        }

        private void CheckPath()
        {
            Character character = Game1.objectHandler.SearchFirst<Character>();
            if (PathFinding.FindPath(PathFinding.ConvertToTile(parent.slave.bounds.Center), PathFinding.ConvertToTile(character.bounds.Center)) != null)
            {
                parent.Push(new MeleeAttack());
            }
          
        }

        public override void Update(GameTime gt)
        {
            CheckPath();
            timer.Update(gt);
            if (timer.Triggered)
            {
                vector = new Vector2(Game1.random.Next(-100, 100), Game1.random.Next(-100, 100));
                timer.ResetTimer();
            }


            if (vector != Vector2.Zero)
            {
                vector.Normalize();
                vector *= speed;
                parent.slave.bounds.Location += vector.ToPoint();
                parent.slave.vector = vector;
            }

        }
    }
}
