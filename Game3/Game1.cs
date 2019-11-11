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
        bool showMiniMap = false;
        int screenX = 1400, screenY = 787;
        bool canDie = true;
        bool gameOver = false;
        Timer hitTimer = new Timer(1f);
        public static Random random = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Texture2D shadowTexture, mainmenuTexture, miniRoomTexture, skullTexture, questionTexture, cursedHeartTexture, sadGhostTexture, happyGhostTexture, ghostTexture, currentDoorTexture, playerTexture, heartTextureFull, heartTextureHalf, heartTextureEmpty, coinTexture, missileTexture, wallTexture, doorTexture, enemyTexture;
        public static SpriteFont debugTextFont, roomNodeFont;
        KeyboardOneTap Key;
        Collision collision = new Collision();
        MouseOneTap mouseOneTap = new MouseOneTap();
        public static ObjectHandler objectHandler = new ObjectHandler();
        public static List<MinimapRoom> minirooms = new List<MinimapRoom>();
        Array input = Keyboard.GetState().GetPressedKeys();
        public static Doors previousDoor;
        //DateTime hitTime;
        int[,] wall2DArray = new int[1, 1]
        {
            {1},
        };
        //private int coinCount = 0, missileCount = 0, life, maxlife = 6, heartContainers, sortTemp, totalFullness, maxMissiles;
        public static int cursorTileX, cursorTileY;
        bool started = false;
        bool playPressed = false;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void GiveXP(int amount)
        {
            Character character = (Character)objectHandler.SearchFirst<Character>();

            character.totalXP += amount;
            character.CheckLevel();
        }


        private void CreateCoin(int amount, Rectangle location)
        {
            for (int i = 0; i < amount; i++)
            {
                objectHandler.AddObject(new Coin(location, coinTexture));
            }
        }

        private void SpawnCharacter(Rectangle location)
        {
            var mouseState = Mouse.GetState();
            objectHandler.AddObject(new Character(location, playerTexture, 5));
        }

        public static void SpawnCursedHeart(Rectangle position)
        {
            objectHandler.AddObject(new Pickup("Cursed Heart", 0, cursedHeartTexture, position));
        }

        public static void SpawnGoblin(Rectangle position)
        {
            var mouseState = Mouse.GetState();
            objectHandler.AddObject(new Goblin(3, 3, 1, enemyTexture, position, position));
            //Console.WriteLine("GOBLIN SPAWNED");
        }

        public static void SpawnGoblinAtMouse()
        {
            var mouseState = Mouse.GetState();
            objectHandler.AddObject(new Goblin(3, 3, 1, enemyTexture, new Rectangle(mouseState.Position, new Point(30)), new Rectangle(mouseState.Position, new Point(30))));
            //Console.WriteLine("GOBLIN SPAWNED");
        }

        public static void ResetGoblins()
        {
            Goblin.noAggroed = 0;
            List<Goblin> goblins = objectHandler.SearchArray<Goblin>();
            foreach (Goblin goblin in goblins)
            {
                goblin.aggroed = false;
                goblin.spawnTime = DateTime.Now;
                goblin.frozen = true;
                goblin.bounds.Location = goblin.spawnPoint.Location;

            }
        }

        private void RemoveCoin(Coin coin)
        {
            objectHandler.RemoveObject(coin);
        }

        private void RemoveMissile(MagicMissile missile)
        {
            objectHandler.RemoveObject(missile);
        }

        private void RemoveGoblin(Goblin goblin)
        {
            Goblin.noAggroed--;
            objectHandler.RemoveObject(goblin);
        }

        private void SpawnMissile(Vector2 position)
        {
            if (MagicMissile.noMissiles < MagicMissile.maxMissiles)
            {
                objectHandler.AddObject(new MagicMissile(new Rectangle((int)position.X, (int)position.Y, 1, 1), missileTexture, MagicMissile.noMissiles, 1, DateTime.Now));
                MagicMissile.noMissiles++;
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
            graphics.PreferredBackBufferWidth = screenX;
            graphics.PreferredBackBufferHeight = screenY;
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
            ghostTexture = Content.Load<Texture2D>("ghost");
            happyGhostTexture = Content.Load<Texture2D>("happyghost");
            sadGhostTexture = Content.Load<Texture2D>("ghost");
            cursedHeartTexture = Content.Load<Texture2D>("cursedheart");
            miniRoomTexture = Content.Load<Texture2D>("minimapRoom2");
            skullTexture = Content.Load<Texture2D>("skull");
            questionTexture = Content.Load<Texture2D>("questionmark");
            mainmenuTexture = Content.Load<Texture2D>("mainmenutemp");
            shadowTexture = Content.Load<Texture2D>("shadow");
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
            if (Key.IsPressed(Keys.Enter))
            {
                playPressed = true;
            }

            if (!started)
            {
                SpawnCharacter(new Rectangle(475, 330, 0, 0));
                currentDoorTexture = doorTexture;
                ProcGen2.GenerateDungeon();

                Minimap.GenerateMinimap();
                Minimap.MinimapDebug();
                RoomShower.StartingThing();
                RoomShower.SpawnRoom();
                objectHandler.SearchFirst<Character>().GenerateBrackets();
                //AddHeart(2, 3);
                canDie = true;
                started = true;
            }
            if (life <= 0)
            {
                gameOver = true;
            }
            else
            {
                gameOver = false;
            }
            MagicMissile.maxMissiles = objectHandler.SearchFirst<Character>().level + 3;
            //attackCooldown = 0.25f / (characters[0].level + 1);
            if (ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY].gobinsContained.Count == 0)
            {
                currentDoorTexture = doorTexture;
            }
            else
                currentDoorTexture = wallTexture;



            //input
            var mouseState = Mouse.GetState();
            Key.Update(gameTime);
            mouseOneTap.Update(gameTime);
            if (mouseOneTap.IsLeftPressed())
            {
                SpawnMissile(objectHandler.SearchFirst<Character>().bounds.Center.ToVector2());
            }



            if (Key.IsPressed(Keys.P))
            {
                Ghost.SpawnSelf();
            }

            if (Key.IsPressed(Keys.O))
            {
                SpawnCursedHeart(new Rectangle(500, 350, 40, 40));
            }

            if (Key.IsPressed(Keys.Tab))
            {
                showMiniMap = !showMiniMap;
            }

            //if (Key.IsPressed(Keys.I))
            //{
            //    SpawnCharacter(new Rectangle(mouseState.X, mouseState.Y, 0, 0));
            //    Console.WriteLine("Spawned!");
            //}

            if (Keyboard.GetState().IsKeyDown(Keys.U) && Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                CreateCoin(25, new Rectangle(mouseState.Position, new Point(1)));
            }





            // TODO: Add your update logic here


            foreach (MinimapRoom miniRoom in minirooms)
            {
                miniRoom.Update(gameTime);
            }


            //for (var i = 0; i < goblins.Count; i++)
            //{
            //    goblins[i].Update(gameTime);
            //    for (int w = 0; w < walls.Count; w++)
            //    {
            //        if (collision.CollisionCheck(goblins[i].bounds, walls[w].bounds, "goblin", "wall"))
            //        {
            //            if (goblins[i].aggroed)
            //            {
            //                goblins[i].bounds.Location -= goblins[i].vector.ToPoint();
            //            }
            //            else
            //                goblins[i].bounds.Location -= goblins[i].roamingVector.ToPoint();

            //            break;
            //        }
            //    }
            //    for (int c = 0; c < characters.Count; c++)
            //    {
            //        if (collision.CollisionCheck(goblins[i].bounds, characters[c].bounds, "goblin", "character"))
            //        {
            //            DamagePlayer(goblins[i].power);
            //            break;
            //        }
            //    }

            //}



            //for (var i = 0; i < missiles.Count; i++)
            //{
            //    missiles[i].Update(gameTime);
            //    if (DateTime.Now > missiles[i].spawnTime.AddSeconds(3))
            //    {
            //        RemoveMissile(i);
            //    }
            //}
            //for (var i = 0; i < missiles.Count; i++)
            //{
            //    for (int w = 0; w < walls.Count; w++)
            //    {
            //        if (collision.CollisionCheck(missiles[i].bounds, walls[w].bounds, "missile", "wall"))
            //        {
            //            RemoveMissile(i);
            //            break;
            //        }
            //    }
            //}
            //for (var i = 0; i < missiles.Count; i++)
            //{

            //    for (int d = 0; d < doors.Count; d++)
            //    {
            //        if (collision.CollisionCheck(missiles[i].bounds, doors[d].bounds, "missile", "door"))
            //        {
            //            RemoveMissile(i);
            //            break;
            //        }
            //    }
            //}
            //for (var i = 0; i < missiles.Count; i++)
            //{

            //    for (int m = 0; m < missiles.Count; m++)
            //    {
            //        if (collision.CollisionCheck(missiles[i].bounds, missiles[m].bounds, "missile", "missile"))
            //        {
            //            if (missiles[m].ID > missiles[i].ID)
            //            {
            //                RemoveMissile(i);
            //                break;
            //            }
            //        }
            //    }
            //}

            //for (var i = 0; i < missiles.Count; i++)
            //{
            //    for (int g = 0; g < goblins.Count; g++)
            //    {
            //        if (collision.CollisionCheck(missiles[i].bounds, goblins[g].bounds, "missile", "goblin"))
            //        {
            //            RemoveMissile(i);
            //            goblins[g].health -= 1;
            //            if (!goblins[g].aggroed)
            //            {
            //                goblins[g].aggroed = true;
            //            }
            //            if (goblins[g].health <= 0)
            //            {
            //                CreateCoin(random.Next(2, 6), new Rectangle(goblins[g].bounds.Location.X, goblins[g].bounds.Location.Y, 0, 0));
            //                RemoveGoblin(g);

            //                GiveXP(400);
            //            }
            //            break;
            //        }
            //    }



            //}

            //for (var i = 0; i < characters.Count; i++)
            //{
            //    characters[i].Update(gameTime);

            //    foreach (Pickup pickup in pickups)
            //    {
            //        if (collision.CollisionCheck(characters[i].bounds, pickup.bounds, "character", "pickup"))
            //        {
            //            Pickup.Effect(pickup.effID);
            //            pickups.Remove(pickup);
            //            break;
            //        }
            //    }

            //    for (var c = 0; c < coins.Count; c++)
            //    {
            //        if (collision.CollisionCheck(characters[i].bounds, coins[c].bounds, "character", "coin"))
            //        {
            //            RemoveCoin(c);
            //            coinCount++;
            //            GiveXP(100);
            //        }
            //    }
            //    //for (int m = 0; m < missiles.Count; m++)
            //    //{
            //    //    if (collision.CollisionCheck(characters[i].bounds, missiles[m].bounds, "character", "missile"))
            //    //    {
            //    //        RemoveMissile(m);
            //    //        DamagePlayer(1);
            //    //    }
            //    //}
            //    for (int w = 0; w < walls.Count; w++)
            //    {
            //        if (collision.CollisionCheck(characters[i].bounds, walls[w].bounds, "character", "walls"))
            //        {
            //            characters[i].bounds.Location -= characters[i].vector.ToPoint();
            //            break;
            //        }
            //        foreach (Coin coin in coins)
            //        {
            //            if (collision.CollisionCheck(coin.bounds, walls[w].bounds, "coin", "walls"))
            //            {
            //                coin.bounds.Location -= coin.vector.ToPoint();
            //            }

            //        }
            //    }



            //    for (int d = 0; d < doors.Count; d++)
            //    {
            //        foreach (Coin coin in coins)
            //        {
            //            if (collision.CollisionCheck(coin.bounds, doors[d].bounds, "coin", "door"))
            //            {
            //                coin.bounds.Location -= coin.vector.ToPoint();
            //            }
            //        }

            //        //if (ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY].gobinsContained.Count == 0)
            //        if (collision.CollisionCheck(characters[i].bounds, doors[d].bounds, "character", "doors"))
            //        {
            //            if (ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY].gobinsContained.Count == 0)
            //            {


            //                ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY].gobinsContained = goblins;
            //                foreach (Goblin goblin in goblins)
            //                {
            //                    Console.WriteLine("theres a goblin in goblins");
            //                }
            //                if (doors[d].direction == 2)
            //                {
            //                    RoomShower.playerRoomY -= 1;
            //                    characters[0].bounds = new Rectangle(475, 475, characters[0].bounds.Width, characters[0].bounds.Height);

            //                }
            //                if (doors[d].direction == 3)
            //                {
            //                    RoomShower.playerRoomX += 1;
            //                    characters[0].bounds = new Rectangle(200, 330, characters[0].bounds.Width, characters[0].bounds.Height);

            //                }
            //                if (doors[d].direction == 4)
            //                {
            //                    RoomShower.playerRoomY += 1;
            //                    characters[0].bounds = new Rectangle(475, 200, characters[0].bounds.Width, characters[0].bounds.Height);

            //                }
            //                if (doors[d].direction == 5)
            //                {
            //                    RoomShower.playerRoomX -= 1;
            //                    characters[0].bounds = new Rectangle(744, 330, characters[0].bounds.Width, characters[0].bounds.Height);
            //                }

            //                RoomShower.SpawnRoom();
            //                missiles.Clear();
            //                coins.Clear();
            //                break;
            //            }
            //            else
            //            {
            //                characters[i].bounds.Location -= characters[i].vector.ToPoint();
            //                break;
            //            }
            //        }
            //    }
            //}
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
            string instructions = "WASD to move\nClick to shoot\nPress Tab to toggle Minimap";
            string nodeTrackerVal = RoomShower.playerRoomX.ToString() + ", " + RoomShower.playerRoomY.ToString();
            //string roomNodeVal = currentRoom.number.ToString();

            objectHandler.Draw(spriteBatch);

            //GMIUOPDERJGU9IPERGU9PRWEHU9PREHGU9EWRHUGPERWHUGWREUOGWUPOGHWRUGWHUGHUOWGHUOREGUOPWEHGUPOEHGUOIPW4RGUJOPWNJOGNPOWJ[UIGFNPUOENFUGOPJNIOJ[VNWPOUJVNWIESOVN[OEWNV[
            if (showMiniMap)
            {
                foreach (MinimapRoom miniroom in minirooms)
                {
                    miniroom.Draw(spriteBatch);
                }
            }
            spriteBatch.DrawString(debugTextFont, coinCounterVal, new Vector2(50, 50), Color.White);
            spriteBatch.DrawString(debugTextFont, instructions, new Vector2(0, 100), Color.White);
            spriteBatch.DrawString(debugTextFont, "x: " + mouseState.X + " y: " + mouseState.Y + "\n x:" + objectHandler.SearchFirst<Character>().bounds.X + "y:" + objectHandler.SearchFirst<Character>().bounds.Y + "\n" + nodeTrackerVal + "\n" + objectHandler.SearchFirst<Character>().totalXP, new Vector2(mouseState.X + 20, mouseState.Y - 10), color: Color.White);



            if (gameOver && playPressed)
            {
                spriteBatch.Draw(wallTexture, new Rectangle(0, 0, 5000, 5000), Color.Black);
                spriteBatch.DrawString(debugTextFont, "Game Over", new Vector2(500, 350), Color.Red, 0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0f);
            }
            if (!playPressed)
            {
                spriteBatch.Draw(mainmenuTexture, new Rectangle(0, 0, screenX, screenY), Color.White);
                spriteBatch.DrawString(debugTextFont, "Press Enter to Start", new Vector2(500, 350), Color.Green, 0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();




            base.Draw(gameTime);
        }
    }
}
