using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DesPat
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static List<TextureObj> activeObjects = new List<TextureObj>();
        private static List<TextureObj> toActivateObjects = new List<TextureObj>();
        private static List<TextureObj> toDeactivateObjects = new List<TextureObj>();

        public static List<AutoMoveProjectile> projectileObjects = new List<AutoMoveProjectile>();
        private static List<AutoMoveProjectile> toAutomateProjectiles = new List<AutoMoveProjectile>();
        private static List<AutoMoveProjectile> toDeautomateProjectiles = new List<AutoMoveProjectile>();

        public static List<Texture2D> imageList = new List<Texture2D>();
        public static List<Player> playerList = new List<Player>();

        private static bool changeInLists = false;
        private static bool changeInActives = false;
        private static bool changeInProjectiles = false;
        private bool changeInCurrentScreen = false;

        private int playerAmount = 0;
        private static bool forceExit = false;

        //case 2
        public int gamePC = 0, iLine1, iLine5, rndLine1, rndLine5;
        public Random randomGen = new Random();
        public float timeToWaitLine4, timeToWaitLine3;

        //screen Parameters
        public static int screenWidth;
        public static int screenHeight;

        private int currentScreen = 0;

        private MouseState oldMS;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);

            //NOTE: if fullscreen is activated, if an error occurs exiting the game will be very hard as the fullscreen will
            //cover taskmanager and you wont exit. Instead try pressing enter if this happens that fixed it for me. -Juno
            //Kevin: Try hitting Shift-F5 first in visual  studio, as this stops the current process.
            //NOTE2: NOT RECOMMENDED AS IT WILL NOT WORK WITH THE MAIN SCREEN.
            //graphics.ToggleFullScreen();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("Starting up the game");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            switch (currentScreen)
            {
                case 0:
                    IsMouseVisible = true;

                    //detect screen parameters
                    graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                    graphics.IsFullScreen = true;
                    graphics.ApplyChanges();

                    screenWidth = GraphicsDevice.Viewport.Width;
                    screenHeight = GraphicsDevice.Viewport.Height;


                    Texture2D mainScreen = Content.Load<Texture2D>("MainScreen.png");
                    imageList.Add(mainScreen);
                    addAsActive(new TextureObj(mainScreen, new Vector2(0, 0), new Rectangle(0, 0, mainScreen.Width, mainScreen.Height), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 1, "MainScreen"));
                    break;

                case 1:
                    IsMouseVisible = false;

                    //Load BG
                    Texture2D bg = Content.Load<Texture2D>("Space.jpg");
                    imageList.Add(bg);
                    addAsActive(new TextureObj(bg, new Vector2(0, 0), new Rectangle(0, 0, bg.Width, bg.Height), Color.White, 0, new Vector2(0,0), 1.0f, SpriteEffects.None, 1, "Background"));

                    //############################################################################################################################################
                    //Load Banana Projectile image.
                    Texture2D projectileBananaImage = Content.Load<Texture2D>("Projectile-banana.png");
                    imageList.Add(projectileBananaImage);
                    System.Diagnostics.Debug.WriteLine("Banana projectile: " + projectileBananaImage.Name);
                    //Load Strawberry Projectile image.
                    Texture2D projectileStrawberryImage = Content.Load<Texture2D>("Projectile-strawberry.png");
                    imageList.Add(projectileStrawberryImage);
                    System.Diagnostics.Debug.WriteLine("Strawberry projectile: " + projectileStrawberryImage.Name);
                    //Load Pear projectile image.
                    Texture2D projectilePearImage = Content.Load<Texture2D>("Projectile-pear.png");
                    imageList.Add(projectilePearImage);
                    System.Diagnostics.Debug.WriteLine("Pear projectile: " + projectilePearImage.Name);
                    //############################################################################################################################################

                    //load the seed image.
                    Texture2D seedImage = Content.Load<Texture2D>("Seed.png");
                    imageList.Add(seedImage);
                    System.Diagnostics.Debug.WriteLine("SeedName: " + seedImage.Name);

                    //Load health images.
                    Texture2D health3Image = Content.Load<Texture2D>("Life-3.png");
                    imageList.Add(health3Image);
                    Texture2D health2Image = Content.Load<Texture2D>("Life-2.png");
                    imageList.Add(health2Image);
                    Texture2D health1Image = Content.Load<Texture2D>("Life-1.png");
                    imageList.Add(health1Image);
                    Texture2D health0Image = Content.Load<Texture2D>("Life-0.png");
                    imageList.Add(health0Image);

                    //Load player images and create player Objects.
                    System.Diagnostics.Debug.WriteLine("Making a player ");
                    Texture2D playerImage = Content.Load<Texture2D>("Banana.png");
                    imageList.Add(playerImage);
                    createPlayer(imageList.Find(name => name.Name == "Banana.png"), screenWidth / 4 - playerImage.Width / 2, screenHeight / 4 - playerImage.Height / 2, 2.5f, 5f, Keys.W, Keys.A, Keys.S, Keys.D, Keys.Space, new Vector2(48, 16));

                    System.Diagnostics.Debug.WriteLine("Making a player ");
                    Texture2D player2Image = Content.Load<Texture2D>("Strawberry.png");
                    imageList.Add(player2Image);

                    createPlayer(imageList.Find(name => name.Name == "Strawberry.png"), screenWidth / 4 * 3 - player2Image.Width / 2, screenHeight / 4 - player2Image.Height / 2, 2.5f, 5f, PlayerIndex.One, new Vector2(screenWidth - 48, 16));

                    System.Diagnostics.Debug.WriteLine("Making a player ");
                    Texture2D player3Image = Content.Load<Texture2D>("Pear.png");
                    imageList.Add(player3Image);
                    createPlayer(imageList.Find(name => name.Name == "Pear.png"), screenWidth / 4 * 2 - player3Image.Width / 2, screenHeight / 4 * 3 - player3Image.Height / 2, 2.5f, 5f, PlayerIndex.Two, new Vector2(48, screenHeight - 16));
                    break;
                case 2:
                    //Load BG
                    Texture2D bg2 = Content.Load<Texture2D>("Space.jpg");
                    imageList.Add(bg2);
                    addAsActive(new TextureObj(bg2, new Vector2(0, 0), new Rectangle(0, 0, bg2.Width, bg2.Height), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 1, "Background"));

                    break;

                default:
                    break;
            }
        }

        private void createPlayer(Texture2D playerImage, int x, int y, float movementSpeed, float rotateSpeed, Keys up, Keys left, Keys down, Keys right, Keys shoot, Vector2 healthBarLocation)
        {
            playerAmount++;
            TextureObj playerObj = new TextureObj(playerAmount, playerImage, new Vector2(x, y), new Rectangle(0, 0, playerImage.Width, playerImage.Height), Color.White, 0, new Vector2(playerImage.Width / 2, playerImage.Height / 2), 1.0f, SpriteEffects.None, 1, "Player");

            addAsActive(playerObj);
            playerList.Add(new Player(playerAmount, playerObj, movementSpeed, rotateSpeed, up, left, down, right, shoot, healthBarLocation));
        }
        private void createPlayer(Texture2D playerImage, int x, int y, float movementSpeed, float rotateSpeed, PlayerIndex playerIndex, Vector2 healthBarLocation)
        {
            playerAmount++;
            TextureObj playerObj = new TextureObj(playerAmount, playerImage, new Vector2(x, y), new Rectangle(0, 0, playerImage.Width, playerImage.Height), Color.White, 0, new Vector2(playerImage.Width / 2, playerImage.Height / 2), 1.0f, SpriteEffects.None, 1, "Player");

            addAsActive(playerObj);
            playerList.Add(new Player(playerAmount, playerObj, movementSpeed, rotateSpeed, playerIndex, healthBarLocation));
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            //check if the screen should change to another "level".
            if (changeInCurrentScreen)
            {
                activeObjects.Clear();
                LoadContent();
                changeInCurrentScreen = false;
            }

            //check if either the Actives list or the Projectile list has been changed.
            if (changeInLists)
            {
                if (changeInActives)
                {
                    changeActives();
                }
                if(changeInProjectiles)
                {
                    changeProjectiles();
                }
            }

            var KBS = Keyboard.GetState();
            var newMS = Mouse.GetState();
            var gpdOne = GamePad.GetState(PlayerIndex.One);

            switch (currentScreen)
            {
                case 0:
                    Mouse.SetPosition((int)(newMS.X + gpdOne.ThumbSticks.Left.X * 6), (int)(newMS.Y - gpdOne.ThumbSticks.Left.Y * 6));
                    if (newMS.LeftButton == ButtonState.Released && oldMS.LeftButton == ButtonState.Pressed || gpdOne.Buttons.A == ButtonState.Pressed)
                    {
                        if (newMS.X >= 100 && newMS.X < 300)
                        {
                            if (newMS.Y >= 100 && newMS.Y < 300)
                            {
                                currentScreen = 1;
                                changeInCurrentScreen = true;
                            }
                            else if (newMS.Y >= 400 && newMS.Y < 600)
                            {
                                currentScreen = 2;
                                changeInCurrentScreen = true;
                            }
                        }
                    }
                    break;
                case 1:
                    if (projectileObjects.Count != 0)
                    {
                        for (int i = 0; i < projectileObjects.Count; i++)
                        {
                            AutoMoveProjectile obj = projectileObjects[i];
                            foreach (TextureObj collisionCheck in activeObjects)
                            {
                                //System.Diagnostics.Debug.WriteLine("obj playerNumber: " + obj.getPlayerNumber() + ", checkNumber: " + collisionCheck.getPlayerNumber());
                                bool collidable = true;

                                //in this if statement, add any Type which shouldnt collide with projectiles. Like background and GUI.
                                if (collisionCheck.getType().Equals("Background") || collisionCheck.getType().Equals("Healthbar"))
                                {
                                    collidable = false;
                                }
                                //this if checks if the TextureObj is not the player that shot the projectile.
                                if (obj.getPlayerNumber() != collisionCheck.getPlayerNumber() && collidable == true)
                                {
                                    //this if checks if the TextureObj is not the same as the Projectile.
                                    if (obj.getObject() != collisionCheck)
                                    {
                                        //this if checks if the Projectile hit something.
                                        if (obj.getObject().checkCollision(collisionCheck))
                                        {
                                            //if the playerNumber of the texture object is not 0, a player has been hit.
                                            if (collisionCheck.getPlayerNumber() != 0)
                                            {
                                                System.Diagnostics.Debug.WriteLine("Projectile from player: " + obj.getPlayerNumber() + " has COLLISION with Player: " + collisionCheck.getPlayerNumber());
                                                //Here you can handle the collision with a player, like lowering the HP of the hit target.

                                                Player hitPlayer = playerList.Find(player => player.getPlayerNumber() == collisionCheck.getPlayerNumber());
                                                hitPlayer.changeHealth(hitPlayer.getHealth() - 1);

                                            }
                                            //else the hit object is not a player.
                                            else
                                            {
                                                System.Diagnostics.Debug.WriteLine("Projectile from player: " + obj.getPlayerNumber() + " has COLLISION with something: " + collisionCheck.getType());
                                                //here you can handle the collision with a non-player object.

                                                if (collisionCheck.getType().Equals("Seed") || collisionCheck.getType().Equals("Strawberry slice") || collisionCheck.getType().Equals("Banana slice") || collisionCheck.getType().Equals("Pear slice"))
                                                {
                                                    projectileObjects.Find(findProjectile => findProjectile.getObject() == collisionCheck).setSecondsLeft(0);
                                                }

                                            }

                                            //Delete the projectile that hit something.
                                            obj.setSecondsLeft(0);
                                        }
                                    }
                                }
                            }
                            if (obj.getSecondsLeft() != 0)
                            {
                                obj.getObject().addToLocation(obj.toAddX, obj.toAddY);

                                long currentTime = DateTime.Now.Ticks;
                                long seedTimeDifference = (currentTime - obj.getStartTime()) / 10000000;
                                //System.Diagnostics.Debug.WriteLine("Difference: " + seedTimeDifference + ", secondsleft: " + obj.getSecondsLeft());
                                if (seedTimeDifference >= 1)
                                {
                                    obj.setStartTime(currentTime);
                                    obj.setSecondsLeft(obj.getSecondsLeft() - 1);
                                }
                            }
                            else
                            {
                                removeAsProjectile(obj);
                                removeAsActive(obj.getObject());
                            }
                        }
                    }

                    //check if a player has pressed any keys.
                    foreach (Player player in playerList)
                    {
                        player.checkKeys();
                    }
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed || GamePad.GetState(PlayerIndex.Three).Buttons.Back == ButtonState.Pressed || KBS.IsKeyDown(Keys.Escape))
                        Exit();
                    if (forceExit)
                    {
                        Exit();
                    }
                    break;

                case 2:
                    switch (gamePC)
                    {
                        case 0:
                            if(true)
                            {
                                gamePC = 1;
                                iLine1 = 1;
                                rndLine1 = randomGen.Next(20, 60);
                            } 
                            else
                            {
                                gamePC = 9;
                            }
                            break;
                        case 1:
                            if(iLine1 <= rndLine1)
                            {
                                gamePC = 2;
                            }
                            else
                            {
                                gamePC = 4;
                                timeToWaitLine4 = (float)(randomGen.NextDouble() * 2.0 + 5.0);
                            }
                            break;
                        case 2:
                            newMeatballPositions.Add(new Vector2((float)(randomGen.NextDouble() * 500.0 + 51.0), 51.0f));
                            gamePC = 3;
                            timeToWaitLine3 = (float)(randomGen.NextDouble() * 0.2 + 0.1);
                            break;
                        case 3:
                            timeToWaitLine3 -= deltaTime;
                            if(timeToWaitLine3 > 0.0f)
                            {
                                gamePC = 3;
                            }
                            else
                            {
                                gamePC = 1;
                                iLine1++;
                            }
                            break;
                        case 4:
                            timeToWaitLine4 -= deltaTime;
                            if(timeToWaitLine4 > 0.0f)
                            {
                                gamePC = 4;
                            }
                            else
                            {
                                gamePC = 5;
                                iLine5 = 1;
                                rndLine5 = randomGen.Next(10, 20);
                            }
                            break;
                        case 5:
                            Main.ExitGame();
                            break;
                        default:
                            System.Diagnostics.Debug.WriteLine("Error!");
                            break;
                    }

                    break;

                default:
                    System.Diagnostics.Debug.WriteLine("Could not find screen");
                    Exit();
                    break;
            }

            oldMS = newMS;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            //draw every Active object.
            foreach(TextureObj activeObject in activeObjects)
            {
                activeObject.drawObj(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }


        //the following 6 methods are to make sure a list will not be altered while a loop is checking them.
        //Instead if changes are made they will be done at the start of the update() method.
        private void changeProjectiles()
        {
            foreach (AutoMoveProjectile waitingProjectile in toAutomateProjectiles)
            {
                projectileObjects.Add(waitingProjectile);
            }
            toAutomateProjectiles.Clear();
            foreach (AutoMoveProjectile waitingProjectile in toDeautomateProjectiles)
            {
                projectileObjects.Remove(waitingProjectile);
            }
            toDeautomateProjectiles.Clear();
            changeInProjectiles = false;
            changeInLists = false;
        }
        public static void addAsAutomatic(AutoMoveProjectile obj)
        {
            toAutomateProjectiles.Add(obj);
            changeInProjectiles = true;
            changeInLists = true;
        }
        public static void removeAsProjectile(AutoMoveProjectile obj)
        {
            toDeautomateProjectiles.Add(obj);
            changeInProjectiles = true;
            changeInLists = true;
        }

        private void changeActives()
        {
            foreach (TextureObj waitingObj in toActivateObjects)
            {
                activeObjects.Add(waitingObj);
            }
            toActivateObjects.Clear();
            foreach (TextureObj waitingObj in toDeactivateObjects)
            {
                activeObjects.Remove(waitingObj);
            }
            toDeactivateObjects.Clear();
            changeInActives = false;
            changeInLists = false;
        }
        public static void addAsActive(TextureObj obj)
        {
            toActivateObjects.Add(obj);
            changeInActives = true;
            changeInLists = true;
        }
        public static void removeAsActive(TextureObj obj)
        {
            toDeactivateObjects.Add(obj);
            changeInActives = true;
            changeInLists = true;
        }

        public static void ExitGame()
        {
            forceExit = true;
        }
    }
}
