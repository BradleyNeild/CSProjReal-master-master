﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class Goblin:BaseObject
    {

        public Timer MoveTimer = new Timer(0.3f);
        public Color color = Color.White;
        public Texture2D texture;
        public Point CurrentTargetTile = Point.Zero;
        public Point PreviousTile = Point.Zero;
        public Point NextTile = Point.Zero;
        Collision collision = new Collision();
        public float speed = 2;
        public float unaggroedSpeed = 2;
        public int health, maxHealth, power;
        public Rectangle spawnPoint;
        public DateTime spawnTime = DateTime.Now;
        bool okToMove = true;
        public float waitTime = 0.5f;
        public bool frozen = true;
        public Vector2 roamingVector = new Vector2(Game1.random.Next(-100, 100), Game1.random.Next(-100, 100));
        public bool aggroed = false;
        public bool MoveComplete = true;
        List<Point> path = null;
        int lastSecond = 99999;
        public static int noAggroed = 0;
        public const int maxAggroed = 3;
        public Goblin(int enemyHealth, int enemyMaxHealth, int enemyPower, Texture2D enemyTexture, Rectangle enemyBounds, Rectangle enemySpawnPoint)
        {
            bounds = enemyBounds;
            health = enemyHealth;
            maxHealth = enemyMaxHealth;
            power = enemyPower;
            texture = enemyTexture;
            spawnPoint = enemySpawnPoint;
        }

        private bool AggrosUnderMax()
        {
            if (noAggroed < maxAggroed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void MoveTo(Point destination)
        {
            
        }

        private void Aggro()
        {
            //Console.WriteLine("Aggroed");
            noAggroed++;
            aggroed = true;

        }

        private void DeAggro()
        {
            //Console.WriteLine("Deaggroed");
            aggroed = false;
            noAggroed--;
        }



        public override void Update(GameTime gameTime)
        {
            MoveTimer.Update(gameTime);
            if (aggroed)
            {
                color = Color.Red;
            }
            else
            {
                color = Color.Green;
            }
            if (DateTime.Now > spawnTime.AddSeconds(waitTime) && frozen == true)
            {
                frozen = false;
            }
            if (aggroed)
            {
                if (!frozen)
                {
                    Character character = (Character)parent.SearchFirst<Character>();
                    if (PathFinding.ConvertThing(character.bounds.Location) != CurrentTargetTile)
                    {
                        //Console.WriteLine("uoaer");
                        CurrentTargetTile = PathFinding.ConvertThing(character.bounds.Location);
                        Point boundLocationTile = PathFinding.ConvertThing(bounds.Location);
                        path = PathFinding.FindPath(boundLocationTile, CurrentTargetTile);
                        PreviousTile = bounds.Location;
                        NextTile = bounds.Location;
                        MoveTimer.Triggered = true;
                    }

                    if (path != null)
                    {

                        if((MoveTimer.Triggered) && !MoveComplete)
                        {
                            MoveComplete = true;
                        }


                        if ((path.Count > 0) && (MoveTimer.Triggered) && MoveComplete)
                        {
                            MoveComplete = false;
                            MoveTimer.ResetTimer();
                            Point movingTo = path[path.Count - 1];
                            path.RemoveAt(path.Count - 1);
                            PreviousTile = bounds.Location;
                            NextTile = (movingTo.ToVector2() * 45).ToPoint() + new Point(RoomShower.roomOffset); 
                            //MoveTo(movingTo);
                        }

                        if (!MoveTimer.Triggered)
                        {
                            int NewX = (int)MathHelper.Lerp(PreviousTile.X, NextTile.X, MoveTimer.PercentageDone);
                            int NewY = (int)MathHelper.Lerp(PreviousTile.Y, NextTile.Y, MoveTimer.PercentageDone);

                            bounds.Location = new Point(NewX, NewY);
                        }







                    }
                    

                    //float temp = vector.X;
                    //vector.X = vector.Y;
                    //vector.Y = -temp;



                    if (vector != Vector2.Zero)
                    {
                        vector.Normalize();
                        vector *= speed;
                        bounds.Location += vector.ToPoint();
                    }
                }
            }
            else if (AggrosUnderMax())
            {
                Aggro();
            }
            else
            {
                if (DateTime.Now.Second % 2 == 0 && DateTime.Now.Second != lastSecond)
                {
                    roamingVector = new Vector2(Game1.random.Next(-100, 100), Game1.random.Next(-100, 100));
                    lastSecond = DateTime.Now.Second;
                }
                if (roamingVector != Vector2.Zero)
                {
                    roamingVector.Normalize();
                    roamingVector *= unaggroedSpeed;
                    bounds.Location += roamingVector.ToPoint();
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle: bounds, color: color);
        }

        public override void OnCreate()
        {

        }

        public override void OnDestroy()
        {

        }

        public override void OnInteract(BaseObject caller)
        {

        }

    }
}

