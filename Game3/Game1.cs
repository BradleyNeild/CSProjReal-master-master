using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static int totalScore;
        public static Random random = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Texture2D playerTexture, heartTextureFull, heartTextureHalf, heartTextureEmpty, coinTexture, missileTexture, wallTexture, doorTexture, enemyTexture;
        private SpriteFont debugTextFont, roomNodeFont;
        KeyboardOneTap Key;
        Collision collision = new Collision();
        MouseOneTap mouseOneTap = new MouseOneTap();
        List<Coin> coins = new List<Coin>();
        public static List<Character> characters = new List<Character>();
        List<MagicMissile> missiles = new List<MagicMissile>();
        List<Hearts> hearts = new List<Hearts>();
        public static List<Walls> walls = new List<Walls>();
        public static List<Doors> doors = new List<Doors>();
        public static List<Goblin> goblins = new List<Goblin>();
        Array input = Keyboard.GetState().GetPressedKeys();
        public static Doors previousDoor;
        DateTime hitTime;
        int[,] wall2DArray = new int[1, 1]
        {
            {1},
        };
        Random rnd = new Random();
        private int coinCount = 0, missileCount = 0, life, maxlife = 6, heartContainers, sortTemp, totalFullness, maxMissiles = 3;
        public static int cursorTileX, cursorTileY;
        bool started = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }



        private void CreateCoin(int amount, Rectangle location)
        {
            for (int i = 0; i < amount; i++)
            {
                coins.Add(new Coin(location, coinTexture));
            }
        }

        private void SpawnCharacter(Rectangle location)
        {
            var mouseState = Mouse.GetState();
            characters.Add(new Character(location, playerTexture, 5));
        }

        public static void SpawnGoblin(Rectangle position)
        {
            var mouseState = Mouse.GetState();
            goblins.Add(new Goblin(3, 3, (float)4.5, 1, enemyTexture, position, position));
            Console.WriteLine("GOBLIN SPAWNED");
        }

        public static void SpawnGoblinAtMouse()
        {
            var mouseState = Mouse.GetState();
            goblins.Add(new Goblin(3, 3, (float)4.5, 1, enemyTexture, new Rectangle(mouseState.Position, new Point(30)), new Rectangle(mouseState.Position, new Point(30))));
            Console.WriteLine("GOBLIN SPAWNED");
        }

        public static void ResetGoblins()
        {
            foreach (Goblin goblin in goblins)
            {
                goblin.spawnTime = DateTime.Now;
                goblin.frozen = true;
                goblin.bounds.Location = goblin.spawnPoint.Location;
                
            }
        }

        private void RemoveCoin(int coinIndex)
        {
            coins.RemoveAt(coinIndex);
        }

        private void RemoveMissile(int missileIndex)
        {
            missiles.RemoveAt(missileIndex);
        }

        private void RemoveGoblin(int goblinIndex)
        {
            goblins.RemoveAt(goblinIndex);
        }

        private void SpawnMissile(Vector2 position)
        {
            if (missiles.Count < maxMissiles)
            {
                missiles.Add(new MagicMissile(new Rectangle((int)position.X, (int)position.Y, 1, 1), missileTexture, missileCount, 1, DateTime.Now));
                missileCount++;
            }

        }

        private void DamagePlayer(int damage)
        {
            if (DateTime.Now > hitTime.AddSeconds(1))
            {
                for (int i = 0; i < heartContainers; i++)
                {
                    if (hearts[i].fullness == 2)
                    {
                        hearts[i].fullness -= damage;
                        hitTime = DateTime.Now;
                        break;
                    }
                    else if (hearts[i].fullness == 1)
                    {
                        hearts[i].fullness -= damage;
                        hitTime = DateTime.Now;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            
        }
        private void HealPlayer(int healing)
        {
            for (int i = heartContainers - 1; i >= 0; i--)
            {
                if (hearts[i].fullness == 0)
                {
                    hearts[i].fullness += healing;
                    break;
                }
                else if (hearts[i].fullness == 1)
                {
                    hearts[i].fullness += healing;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        private void AddHeart(int fullness, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                foreach (Hearts heart in hearts)
                {
                    heart.moveBounds(57);
                }
                if (fullness == 2)
                {
                    hearts.Insert((hearts.Count), (new Hearts(new Rectangle(0, 0, 0, 0), heartTextureFull, fullness)));
                }
                else if (fullness == 1)
                {
                    hearts.Insert((hearts.Count), (new Hearts(new Rectangle(0, 0, 0, 0), heartTextureHalf, fullness)));
                }
                else
                {
                    hearts.Insert((hearts.Count), (new Hearts(new Rectangle(0, 0, 0, 0), heartTextureEmpty, fullness)));
                }
            }
            

        }

        private void RemoveHeart(int heartsToRemove)
        {
            for (int i = 0; i < heartsToRemove; i++)
            {
                maxlife -= 2;
                hearts.RemoveAt(0);
            }
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            Key = new KeyboardOneTap();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 720;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            wallTexture = Content.Load<Texture2D>("wallV2");
            enemyTexture = Content.Load<Texture2D>("Baddie");
            heartTextureFull = Content.Load<Texture2D>("fullheartv2");
            heartTextureHalf = Content.Load<Texture2D>("halfheartv2");
            heartTextureEmpty = Content.Load<Texture2D>("emptyheartv2");
            playerTexture = Content.Load<Texture2D>("friendly");
            coinTexture = Content.Load<Texture2D>("coin");
            debugTextFont = Content.Load<SpriteFont>("spritefont1");
            missileTexture = Content.Load<Texture2D>("magicmissile2");
            doorTexture = Content.Load<Texture2D>("door2");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!started)
            {
                SpawnCharacter(new Rectangle(475, 330, 0, 0));
                ProcGen2.GenerateDungeon();
                RoomShower.StartingThing();
                RoomShower.SpawnRoom();
                AddHeart(2, 3);
                started = true;
            }

            //health
            if (maxlife % 2 == 0)
            {
                heartContainers = maxlife / 2;
            }
            else
            {
                heartContainers = (maxlife - 1) / 2;
            }
            if (hearts.Count < heartContainers)
            {
                AddHeart(0, 1);
            }

            totalFullness = 0;
            foreach (Hearts heart in hearts)
            {
                totalFullness += heart.fullness;
            }
            if (life != totalFullness)
            {
                life = totalFullness;
            }


            //bubble sorting health
            for (int i = 0; i < hearts.Count - 1; i++)
            {
                if (hearts[i].fullness > hearts[i + 1].fullness)
                {
                    sortTemp = hearts[i].fullness;
                    hearts[i].fullness = hearts[i + 1].fullness;
                    hearts[i + 1].fullness = sortTemp;
                }
                else if (hearts[i].fullness == hearts[i + 1].fullness && hearts[i].fullness == 1)
                {
                    hearts[i].fullness = 2;
                    hearts[i + 1].fullness = 0;

                }
            }

            //input
            var mouseState = Mouse.GetState();
            Key.Update(gameTime);
            mouseOneTap.Update(gameTime);
            if (mouseOneTap.IsRightPressed())
            {
                maxlife += 2;
                Console.WriteLine("life = " + life);
                Console.WriteLine("maxlife = " + maxlife);
            }
            if (mouseOneTap.IsLeftPressed())
            {
                SpawnMissile(new Vector2(characters[0].bounds.X, characters[0].bounds.Y));
            }




            if (Key.IsPressed(Keys.H))
            {
                RemoveHeart(1);
            }

            if (Key.IsPressed(Keys.K))
            {
                DamagePlayer(1);
            }

            if (Key.IsPressed(Keys.J))
            {
                HealPlayer(1);
            }

            if (Key.IsPressed(Keys.P))
            {
                SpawnGoblinAtMouse();
            }

            if (Key.IsPressed(Keys.O))
            {
                RoomShower.SpawnRoom();
            }

            //if (Key.IsPressed(Keys.I))
            //{
            //    SpawnCharacter(new Rectangle(mouseState.X, mouseState.Y, 0, 0));
            //    Console.WriteLine("Spawned!");
            //}

            if (Keyboard.GetState().IsKeyDown(Keys.U))
            {
                CreateCoin(5, new Rectangle(mouseState.Position, new Point(1)));
            }





            // TODO: Add your update logic here
            foreach (Coin coin in coins)
            {
                coin.Update(gameTime);
            }



            for (var i = 0; i < missiles.Count; i++)
            {
                missiles[i].Update(gameTime);
                if (DateTime.Now > missiles[i].spawnTime.AddSeconds(3))
                {
                    RemoveMissile(i);
                }
            }

            for (var i = 0; i < goblins.Count; i++)
            {
                goblins[i].Update(gameTime);
                for (int c = 0; c < characters.Count; c++)
                {
                    if (collision.CollisionCheck(goblins[i].bounds, characters[c].bounds, "goblin", "character"))
                    {
                        DamagePlayer(goblins[i].power);
                        break;
                    }
                }
            }


            for (var i = 0; i < missiles.Count; i++)
            {
                missiles[i].Update(gameTime);
                for (int w = 0; w < walls.Count; w++)
                {
                    if (collision.CollisionCheck(missiles[i].bounds, walls[w].bounds, "missile", "wall"))
                    {
                        RemoveMissile(i);
                        break;
                    }
                }

                for (int d = 0; d < doors.Count; d++)
                {
                    if (collision.CollisionCheck(missiles[i].bounds, doors[d].bounds, "missile", "door"))
                    {
                        RemoveMissile(i);
                        break;
                    }
                }

                for (int m = 0; m < missiles.Count; m++)
                {
                    if (collision.CollisionCheck(missiles[i].bounds, missiles[m].bounds, "missile", "missile"))
                    {
                        if (missiles[m].ID > missiles[i].ID)
                        {
                            RemoveMissile(i);
                        }
                    }
                }

                for (int g = 0; g < goblins.Count; g++)
                {
                    if (collision.CollisionCheck(missiles[i].bounds, goblins[g].bounds, "missile", "goblin"))
                    {
                        RemoveMissile(i);
                        goblins[g].health -= 1;
                        if (goblins[g].health <= 0)
                        {
                            CreateCoin(random.Next(2, 6), new Rectangle(goblins[g].bounds.Location.X, goblins[g].bounds.Location.Y, 0, 0));
                            RemoveGoblin(g);
                            
                            totalScore += 400;
                        }
                        break;
                    }
                }


            }

            for (var i = 0; i < characters.Count; i++)
            {
                characters[i].Update(gameTime);
                for (var c = 0; c < coins.Count; c++)
                {
                    if (collision.CollisionCheck(characters[i].bounds, coins[c].bounds, "character", "coin"))
                    {
                        RemoveCoin(c);
                        coinCount++;
                        totalScore += 100;
                    }
                }
                //for (int m = 0; m < missiles.Count; m++)
                //{
                //    if (collision.CollisionCheck(characters[i].bounds, missiles[m].bounds, "character", "missile"))
                //    {
                //        RemoveMissile(m);
                //        DamagePlayer(1);
                //    }
                //}
                for (int w = 0; w < walls.Count; w++)
                {
                    if (collision.CollisionCheck(characters[i].bounds, walls[w].bounds, "character", "walls"))
                    {
                        characters[i].bounds.Location -= characters[i].vector.ToPoint();
                        break;
                    }
                    foreach (Coin coin in coins)
                    {
                        if (collision.CollisionCheck(coin.bounds, walls[w].bounds, "coin", "walls"))
                        {
                            coin.bounds.Location -= coin.vector.ToPoint();
                        }

                    }
                }

               

                for (int d = 0; d < doors.Count; d++)
                {
                    foreach (Coin coin in coins)
                    {
                        if (collision.CollisionCheck(coin.bounds, doors[d].bounds, "coin", "door"))
                        {
                            coin.bounds.Location -= coin.vector.ToPoint();
                        }
                    }
                    
                    if (collision.CollisionCheck(characters[i].bounds, doors[d].bounds, "character", "doors"))
                    {
                        ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY].gobinsContained = goblins;
                        foreach (Goblin goblin in goblins)
                        {
                            Console.WriteLine("theres a goblin in goblins");
                        }
                        if (doors[d].direction == 2)
                        {
                            RoomShower.playerRoomY -= 1;
                            characters[0].bounds = new Rectangle(475, 475, characters[0].bounds.Width, characters[0].bounds.Height);

                        }
                        if (doors[d].direction == 3)
                        {
                            RoomShower.playerRoomX += 1;
                            characters[0].bounds = new Rectangle(200, 330, characters[0].bounds.Width, characters[0].bounds.Height);

                        }
                        if (doors[d].direction == 4)
                        {
                            RoomShower.playerRoomY += 1;
                            characters[0].bounds = new Rectangle(475, 200, characters[0].bounds.Width, characters[0].bounds.Height);

                        }
                        if (doors[d].direction == 5)
                        {
                            RoomShower.playerRoomX -= 1;
                            characters[0].bounds = new Rectangle(744, 330, characters[0].bounds.Width, characters[0].bounds.Height);
                        }
                        
                        RoomShower.SpawnRoom();
                        missiles.Clear();
                        coins.Clear();
                        break;
                    }
                }
            }

            foreach (Hearts heart in hearts)
            {
                switch (heart.fullness)
                {
                    case 0:
                        heart.texture = heartTextureEmpty;
                        break;
                    case 1:
                        heart.texture = heartTextureHalf;
                        break;
                    case 2:
                        heart.texture = heartTextureFull;
                        break;
                    default:
                        Console.WriteLine("fullness out of range");
                        break;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            GraphicsDevice.Clear(Color.SaddleBrown);

            // TODO: Add your drawing code here
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            string coinCounterVal = "Coins: " + coinCount.ToString();
            string nodeTrackerVal = RoomShower.playerRoomX.ToString() + ", " + RoomShower.playerRoomY.ToString();
            //string roomNodeVal = currentRoom.number.ToString();
            spriteBatch.DrawString(debugTextFont, coinCounterVal, new Vector2(50, 50), Color.White);
            //spriteBatch.DrawString(debugTextFont, "Node:" + roomNodeVal, new Vector2(250, 250), Color.White);
            spriteBatch.DrawString(debugTextFont, "x: " + mouseState.X + " y: " + mouseState.Y + "\n x:" + characters[0].bounds.X + "y:" + characters[0].bounds.Y + "\n" + nodeTrackerVal + "\n" + totalScore, new Vector2(mouseState.X + 20, mouseState.Y - 10), color: Color.White);
            foreach (Coin coin in coins)
            {
                coin.Draw(spriteBatch);
                //Console.WriteLine(coin.ToString());
            }
            foreach (Character character in characters)
            {
                character.Draw(spriteBatch);
            }
            foreach (MagicMissile missile in missiles)
            {
                missile.Draw(spriteBatch);
            }
            foreach (Hearts heart in hearts)
            {
                heart.Draw(spriteBatch);
            }
            foreach (Walls wall in walls)
            {
                wall.Draw(spriteBatch);
            }
            foreach (Doors door in doors)
            {
                door.Draw(spriteBatch);
            }
            foreach (Goblin goblin in goblins)
            {
                goblin.Draw(spriteBatch);
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
