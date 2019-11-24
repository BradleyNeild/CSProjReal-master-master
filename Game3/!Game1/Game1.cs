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
        bool debugStats = false;
        public static int coinCount = 0;
        bool showMiniMap = false;
        public const int screenX = 1400, screenY = 787;
        bool gameOver = false;
        Timer hitTimer = new Timer(1f);
        public static Random random = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Texture2D wizardTexture, rainbowVuvuzelaTexture, slimeSprites, floorTexture, wandTexture, balloonTexture, whitePixelTexture, shadowTexture, mainmenuTexture, miniRoomTexture, skullTexture, questionTexture, cursedHeartTexture, sadGhostTexture, happyGhostTexture, ghostTexture, currentDoorTexture, playerTexture, heartTextureFull, heartTextureHalf, heartTextureEmpty, coinTexture, missileTexture, wallTexture, doorTexture, enemyTexture;
        public static SpriteFont debugTextFont, roomNodeFont;
        KeyboardOneTap Key;
        Collision collision = new Collision();
        MouseOneTap mouseOneTap = new MouseOneTap();
        public static ObjectHandler objectHandler = new ObjectHandler();
        public static ParticleHandler particleHandler = new ParticleHandler();
        public static HeartManager heartManager = new HeartManager();
        public static WeaponHandler weaponHandler = new WeaponHandler();
        Character character;
        public static List<MinimapRoom> minirooms = new List<MinimapRoom>();
        Array input = Keyboard.GetState().GetPressedKeys();
        public static Doors previousDoor;
        //DateTime hitTime;
        //private int coinCount = 0, missileCount = 0, life, maxlife = 6, heartContainers, sortTemp, totalFullness, maxMissiles;
        public static int cursorTileX, cursorTileY;
        bool started = false;
        bool playPressed = true;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void GiveXP(int amount)
        {
            Character character = objectHandler.SearchFirst<Character>();

            Character.totalXP += amount;
            character.CheckLevel();
        }



        private void SpawnCharacter()
        {
            var mouseState = Mouse.GetState();
            objectHandler.AddObject(new Character());
        }

        public static void ResetSlimes()
        {
            Slime.numAggroed = 0;
            List<Slime> slimes = objectHandler.SearchArray<Slime>();
            foreach (Slime slime in slimes)
            {
                slime.aggroed = false;
                slime.frozen = true;
                slime.bounds.Location = slime.spawnPoint;

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

        private void RemoveSlime(Slime slime)
        {
            Slime.numAggroed--;
            objectHandler.RemoveObject(slime);
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
            wallTexture = Content.Load<Texture2D>("prettyWall");
            enemyTexture = Content.Load<Texture2D>("Baddie");
            heartTextureFull = Content.Load<Texture2D>("fullheartv2");
            heartTextureHalf = Content.Load<Texture2D>("halfheartv2");
            heartTextureEmpty = Content.Load<Texture2D>("emptyheartv2");
            playerTexture = Content.Load<Texture2D>("friendly");
            coinTexture = Content.Load<Texture2D>("coin");
            debugTextFont = Content.Load<SpriteFont>("spritefont1");
            missileTexture = Content.Load<Texture2D>("magicmissile2");
            doorTexture = Content.Load<Texture2D>("prettyDoor");
            ghostTexture = Content.Load<Texture2D>("ghost");
            happyGhostTexture = Content.Load<Texture2D>("happyghost");
            sadGhostTexture = Content.Load<Texture2D>("ghost");
            cursedHeartTexture = Content.Load<Texture2D>("cursedheart");
            miniRoomTexture = Content.Load<Texture2D>("minimapRoom2");
            skullTexture = Content.Load<Texture2D>("skull");
            questionTexture = Content.Load<Texture2D>("questionmark");
            mainmenuTexture = Content.Load<Texture2D>("mainmenutemp");
            shadowTexture = Content.Load<Texture2D>("shadow");
            whitePixelTexture = Content.Load<Texture2D>("whitepixel");
            balloonTexture = Content.Load<Texture2D>("balloon");
            wandTexture = Content.Load<Texture2D>("wand");
            floorTexture = Content.Load<Texture2D>("floorTile");
            slimeSprites = Content.Load<Texture2D>("slimeSprites");
            rainbowVuvuzelaTexture = Content.Load<Texture2D>("rainbowVuvuzelaScaled");
            wizardTexture = Content.Load<Texture2D>("wizard");
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
                TileHandler.GenerateTiles();
                SpawnCharacter();
                character = objectHandler.SearchFirst<Character>();
                currentDoorTexture = doorTexture;
                ProcGen2.GenerateDungeon();
                Minimap.GenerateMinimap();
                Minimap.MinimapDebug();
                RoomShower.StartingThing();
                RoomShower.SpawnRoom();
                //character.GenerateBrackets();
                //AddHeart(2, 3);
                started = true;
            }
            if (Character.life <= 0)
            {
                gameOver = true;
            }
            else
            {
                gameOver = false;
            }
            MagicMissile.maxMissiles = objectHandler.SearchFirst<Character>().level + 3;
            //attackCooldown = 0.25f / (characters[0].level + 1);
            Slime slime = objectHandler.SearchFirst<Slime>();
            if (slime == null)
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
                Weapon currentWeapon = objectHandler.SearchFirst<Weapon>();
                if (currentWeapon != null)
                {
                    currentWeapon.OnClick();
                }
                
            }

            if (mouseOneTap.IsRightPressed())
            {
                Character.maxHearts++;
            }

            if (Key.IsPressed(Keys.J))
            {
                objectHandler.SearchFirst<Character>().Heal(1);
            }


            if (Key.IsPressed(Keys.P))
            {
                Balloon.CreateBalloon(mouseState.Position);
            }

            if (Key.IsPressed(Keys.O))
            {
                weaponHandler.ChangeWeapon(new RainbowVuvuzela());
            }

            if (Key.IsPressed(Keys.Tab))
            {
                showMiniMap = !showMiniMap;
            }

            if (Key.IsPressed(Keys.LeftAlt))
            {
                debugStats = !debugStats;
            }

            //if (Key.IsPressed(Keys.I))
            //{
            //    SpawnCharacter(new Rectangle(mouseState.X, mouseState.Y, 0, 0));
            //    Console.WriteLine("Spawned!");
            //}

            if (Keyboard.GetState().IsKeyDown(Keys.U) && Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                Coin.CreateCoin(objectHandler, mouseState.Position, 10);
            }





            // TODO: Add your update logic here
            objectHandler.Update(gameTime);
            heartManager.Update(gameTime);
            weaponHandler.Update(gameTime);

            foreach (MinimapRoom miniRoom in minirooms)
            {
                miniRoom.Update(gameTime);
            }


            //for (var i = 0; i < slimes.Count; i++)
            //{
            //    slimes[i].Update(gameTime);
            //    for (int w = 0; w < walls.Count; w++)
            //    {
            //        if (collision.CollisionCheck(slimes[i].bounds, walls[w].bounds, "slime", "wall"))
            //        {
            //            if (slimes[i].aggroed)
            //            {
            //                slimes[i].bounds.Location -= slimes[i].vector.ToPoint();
            //            }
            //            else
            //                slimes[i].bounds.Location -= slimes[i].roamingVector.ToPoint();

            //            break;
            //        }
            //    }
            //    for (int c = 0; c < characters.Count; c++)
            //    {
            //        if (collision.CollisionCheck(slimes[i].bounds, characters[c].bounds, "slime", "character"))
            //        {
            //            DamagePlayer(slimes[i].power);
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
            //    for (int g = 0; g < slimes.Count; g++)
            //    {
            //        if (collision.CollisionCheck(missiles[i].bounds, slimes[g].bounds, "missile", "slime"))
            //        {
            //            RemoveMissile(i);
            //            slimes[g].health -= 1;
            //            if (!slimes[g].aggroed)
            //            {
            //                slimes[g].aggroed = true;
            //            }
            //            if (slimes[g].health <= 0)
            //            {
            //                CreateCoin(random.Next(2, 6), new Rectangle(slimes[g].bounds.Location.X, slimes[g].bounds.Location.Y, 0, 0));
            //                RemoveSlime(g);

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


            //                ProcGen2.roomNodes[RoomShower.playerRoomX, RoomShower.playerRoomY].gobinsContained = slimes;
            //                foreach (Slime slime in slimes)
            //                {
            //                    Console.WriteLine("theres a slime in slimes");
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
            GraphicsDevice.Clear(new Color(0, 0, 30));

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp);
            string coinCounterVal = "Coins: " + coinCount.ToString();
            string instructions = "WASD to move\nClick to shoot\nPress Tab to toggle Minimap";
            string nodeTrackerVal = RoomShower.playerRoomX.ToString() + ", " + RoomShower.playerRoomY.ToString();


            objectHandler.Draw(spriteBatch);


            heartManager.Draw(spriteBatch);

            //GMIUOPDERJGU9IPERGU9PRWEHU9PREHGU9EWRHUGPERWHUGWREUOGWUPOGHWRUGWHUGHUOWGHUOREGUOPWEHGUPOEHGUOIPW4RGUJOPWNJOGNPOWJ[UIGFNPUOENFUGOPJNIOJ[VNWPOUJVNWIESOVN[OEWNV[
            if (showMiniMap)
            {
                //spriteBatch.Draw(whitePixelTexture, destinationRectangle: new Rectangle(0, 0, 10000, 100000), color: Color.Gray,layerDepth: 1);
                foreach (MinimapRoom miniroom in minirooms)
                {
                    miniroom.Draw(spriteBatch);
                }
            }
            spriteBatch.DrawString(debugTextFont, coinCounterVal + "\n" + instructions + "\n" + "FPS: " + Math.Round(1f / gameTime.ElapsedGameTime.TotalSeconds), new Vector2(0, 150), Color.White);
            if (debugStats)
            {
                spriteBatch.DrawString(debugTextFont, "x: " + mouseState.X + " y: " + mouseState.Y + "\n x:" + objectHandler.SearchFirst<Character>().bounds.X + "y:" + objectHandler.SearchFirst<Character>().bounds.Y + "\n" + nodeTrackerVal + "\n" + Character.totalXP, new Vector2(mouseState.X + 20, mouseState.Y - 10), color: Color.White);
            }


            if (gameOver && playPressed)
            {
                spriteBatch.Draw(whitePixelTexture, new Rectangle(0, 0, 5000, 5000), Color.Black);
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
