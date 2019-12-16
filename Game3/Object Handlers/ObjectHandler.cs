using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class ObjectHandler
    {
        List<BaseObject> objects = new List<BaseObject>();

        public void RemoveObject<T>()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] is T)
                {
                    objects[i].destroy = true;
                }
            }
        }

        public void RemoveObject(BaseObject inObject)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] == inObject)
                {
                    objects[i].destroy = true;
                    break;
                }
            }
        }

        public T SearchFirst<T>() where T:BaseObject
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] is T)
                {
                    return objects[i] as T;
                }
            }
            return null;
        }

        public T SearchFirstEnabled<T>() where T : BaseObject
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] is T && objects[i].enabled && objects[i].floor == Game1.currentFloor)
                {
                    return objects[i] as T;
                }
            }
            return null;
        }

        public List<T> SearchArray<T>() where T:BaseObject
        {
            List<T> outList = new List<T>();
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] is T)
                {
                        outList.Add(objects[i] as T); 
                }
            }
            return outList;
        }

        public List<T> SearchArrayEnabled<T>() where T : BaseObject
        {
            List<T> outList = new List<T>();
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] is T && objects[i].enabled && objects[i].floor == Game1.currentFloor)
                {
                    outList.Add(objects[i] as T);
                }
            }
            return outList;
        }

        public void AddObject(BaseObject inObject)
        {
            if (inObject != null)
            {

                inObject.parent = this;
                inObject.OnCreate();
                objects.Add(inObject);
                
            }
        }

        public void AddObjects<T>(List<T> inObjects) where T:BaseObject
        {
            if (inObjects != null)
            {
                for (int i = 0; i < inObjects.Count; i++)
                {
                    inObjects[i].parent = this;
                    inObjects[i].OnCreate();
                    objects.Add(inObjects[i]);
                }
                
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (!objects[i].destroy)
                {
                    objects[i].Draw(sb);
                }
            }
        }
        public void Update(GameTime gt)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (!objects[i].destroy)
                {
                    objects[i].Update(gt);
                }
            }
            for (int i = 0; i < objects.Count; i++)
            {
                for (int x = 0; x < objects.Count; x++)
                {
                    if (!objects[i].destroy && !objects[x].destroy)
                    {
                        if (i != x)
                        {
                            if (objects[i].bounds.Intersects(objects[x].bounds))
                            {
                                if (objects[i].enabled && objects[x].enabled)
                                {
                                    objects[i].OnInteract(objects[x]);
                                }
                                
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].destroy)
                {
                    objects[i].OnDestroy();
                    objects.RemoveAt(i);
                }
            }
        }
        
    }
}
