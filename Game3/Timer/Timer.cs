using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Timer
    {
        float TargetTime = 0.0f;
        float CurrentTime = 0.0f;
        public bool Triggered = false;
        public float PercentageDone
        {
            get
            {
                if (TargetTime != 0)
                {
                    if (CurrentTime > TargetTime)
                        return 1;
                    else
                        return CurrentTime / TargetTime;
                }
                return 0;
            }
            set
            {

            }
        }
        
        
        //Target Time is the number of seconds it takes to trigger the timer.
        public Timer(float Target)
        {
            TargetTime = Target;
        }

        public void ResetTimer()
        {
            Triggered = false;
            CurrentTime = 0;
        }

        public void Update(GameTime gt)
        {
            if (!Triggered)
            {
                if(CurrentTime < TargetTime)
                {
                    CurrentTime += (float)gt.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    Triggered = true;
                    CurrentTime = 0;
                }
            }
        }
    }
}
