using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace Game3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static bool win = false;
        string weaponrotation = "";
        Vector2 cameraPosition = Vector2.Zero;
        Song fortnite;
        public static SoundEffect hurtSfx, reloadSfx, reload1Sfx, reload2Sfx, shootSfx, hurt2Sfx, coinPickupSfx;
        public static bool greyTiles = false;
        bool debugStats = false;
        public static int coinCount = 0;
        bool showMiniMap = false;
        public const int screenX = 15 * 64, screenY = 9 * 64;
        bool gameOver = false;
        Timer hitTimer = new Timer(1f);
        public static Random random = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Texture2D pistolTexture, trapDoorTexture,wizardTexture, rainbowVuvuzelaTexture, slimeSprites, floorTexture, wandTexture, balloonTexture, whitePixelTexture, shadowTexture, mainmenuTexture, miniRoomTexture, skullTexture, questionTexture, cursedHeartTexture, sadGhostTexture, happyGhostTexture, ghostTexture, currentDoorTexture, playerTexture, heartTextureFull, heartTextureHalf, heartTextureEmpty, coinTexture, missileTexture, wallTexture, doorTexture, enemyTexture;
        public static SpriteFont debugTextFont, roomNodeFont;
        public static KeyboardOneTap Key;
        Collision collision = new Collision();
        public static MouseOneTap mouseOneTap = new MouseOneTap();
        public static ObjectHandler objectHandler = new ObjectHandler();
        public static ParticleHandler particleHandler = new ParticleHandler();
        public static HeartManager heartManager = new HeartManager();
        public static WeaponHandler weaponHandler = new WeaponHandler();
        public static Camera camera;
        Character character;
        public static List<MinimapRoom> minirooms = new List<MinimapRoom>();
        Array input = Keyboard.GetState().GetPressedKeys();
        public static Doors previousDoor;
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

        public static void PlayDeathSound()
        {
            hurtSfx.Play();
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

            camera = new Camera(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2));
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            MediaPlayer.IsRepeating = true;
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
            trapDoorTexture = Content.Load<Texture2D>("TrapdoorSprites");
            fortnite = Content.Load<Song>("defaultdance");
            hurtSfx = Content.Load<SoundEffect>("hurt");
            reloadSfx = Content.Load<SoundEffect>("reload");
            reload2Sfx = Content.Load<SoundEffect>("reload2");
            reload1Sfx = Content.Load<SoundEffect>("reload1");
            hurt2Sfx = Content.Load<SoundEffect>("hurt2");
            pistolTexture = Content.Load<Texture2D>("pistol");
            shootSfx = Content.Load<SoundEffect>("shoot");
            coinPickupSfx = Content.Load<SoundEffect>("coinPickup");
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
                objectHandler.AddObject(new ReloadIndicator());
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



            if (mouseOneTap.IsRightPressed())
            {
                Character.maxHearts++;
            }

            if (Key.IsPressed(Keys.J))
            {
                objectHandler.SearchFirst<Character>().Heal(1);
            }

            if (Key.IsPressed(Keys.R))
            {
                if (objectHandler.SearchFirst<Pistol>() != null)
                {
                    objectHandler.SearchFirst<Pistol>().Reload();
                        }
            }

            if (Key.IsPressed(Keys.P))
            {
                Balloon.CreateBalloon(mouseState.Position);
            }

            if (Key.IsPressed(Keys.O))
            {
                weaponHandler.ChangeWeapon(new Pistol());
            }

            if (Key.IsPressed(Keys.I))
            {
                weaponHandler.ChangeWeapon(new Wand());
            }

            if (Key.IsPressed(Keys.Tab))
            {
                showMiniMap = !showMiniMap;
            }

            if (Key.IsPressed(Keys.LeftAlt))
            {
                debugStats = !debugStats;
            }

            if (Key.IsPressed(Keys.RightAlt))
            {
                greyTiles = !greyTiles;
                if (greyTiles == true)
                {
                    MediaPlayer.Play(fortnite);
                }
                else
                {
                    MediaPlayer.Stop();
                }
                
            }

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
            spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: camera.WorldMatrix);
            string coinCounterVal = "Coins: " + coinCount.ToString();
            string instructions = "WASD or Arrows to move\nHold Click or Space to shoot\nTab to toggle Minimap";
            string nodeTrackerVal = RoomShower.playerRoomX.ToString() + ", " + RoomShower.playerRoomY.ToString();


            objectHandler.Draw(spriteBatch);


            heartManager.Draw(spriteBatch);

            if (showMiniMap)
            {
                foreach (MinimapRoom miniroom in minirooms)
                {
                    miniroom.Draw(spriteBatch);
                }
            }
            spriteBatch.DrawString(debugTextFont, coinCounterVal + "\n" + instructions + "\n" + "FPS: " + Math.Round(1f / gameTime.ElapsedGameTime.TotalSeconds), new Vector2(0, 150), Color.White);
            if (objectHandler.SearchFirst<Weapon>() != null)
            {
                weaponrotation = objectHandler.SearchFirst<Weapon>().direction.ToString();
            }
            if (debugStats)
            {
                spriteBatch.DrawString(debugTextFont, "x: " + mouseState.X + " y: " + mouseState.Y + "\n x:" + objectHandler.SearchFirst<Character>().bounds.X + "y:" + objectHandler.SearchFirst<Character>().bounds.Y + "\n" + nodeTrackerVal + "\n" + Character.totalXP + "\n" + "X: " + mouseState.X / 64 + "Y: " + mouseState.Y / 64 + "\n" + weaponrotation, new Vector2(mouseState.X + 20, mouseState.Y - 10), color: Color.White);
            }
            if (win)
            {
                spriteBatch.DrawString(debugTextFont, "You Win!", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), Color.Magenta, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);
            }



            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp);
            if (gameOver && playPressed)
            {
                spriteBatch.Draw(whitePixelTexture, new Rectangle(0, 0, 5000, 5000), Color.Black);
                spriteBatch.DrawString(debugTextFont, "Game Over", new Vector2(500, 350), Color.Red, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            }
            if (!playPressed)
            {
                spriteBatch.Draw(mainmenuTexture, new Rectangle(0, 0, screenX, screenY), Color.White);
                spriteBatch.DrawString(debugTextFont, "Press Enter to Start", new Vector2(300, 350), Color.Green, 0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(debugTextFont, instructions, new Vector2(300, 200), Color.Yellow, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);

            }
            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
