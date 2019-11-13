using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class AIHandler
    {
        public BaseObject slave;
        Stack<BaseAI> aiStack = new Stack<BaseAI>();

        public AIHandler(BaseObject obj)
        {
            slave = obj;
        }

        public BaseAI Pop()
        {
            if (aiStack.Count > 0)
            {
                return aiStack.Pop();
            }
            return null;
        }

        public void Push(BaseAI baseAI)
        {
            baseAI.parent = this;
            baseAI.OnCreate();
            aiStack.Push(baseAI);
        }

        public BaseAI Peek()
        {
            return aiStack.Peek();
        }

        public void Update(GameTime gt)
        {
            if (aiStack.Count > 0)
            {
                Peek().Update(gt);
            }

        }


    }
}
