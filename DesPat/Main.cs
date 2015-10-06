using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DesPat
{
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

        private List<AutoMoveMeatball> meatballs = new List<AutoMoveMeatball>();
        private static List<AutoMoveMeatball> toDeautomateMeatballs = new List<AutoMoveMeatball>();
        private static List<AutoMoveMeatball> toAutomateMeatballs = new List<AutoMoveMeatball>();

        public static List<Texture2D> imageList = new List<Texture2D>();
        public static List<Player> playerList = new List<Player>();

        private static bool changeInLists = false;
        private static bool changeInActives = false;
        private static bool changeInProjectiles = false;
        private static bool changeInMeatballs;
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

        //switch stuff
        private Random random = new Random();
        private int pc = 0;
        private int maximumMeatballs;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);

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
                    {
                        IsMouseVisible = true;

                        //detect screen parameters
                        graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                        graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                        

                        screenWidth = GraphicsDevice.Viewport.Width;
                        screenHeight = GraphicsDevice.Viewport.Height;


                        Texture2D mainScreen = Content.Load<Texture2D>("MainScreen.png");
                        imageList.Add(mainScreen);
                        addAsActive(new TextureObj(mainScreen, new Vector2(0, 0), new Rectangle(0, 0, mainScreen.Width, mainScreen.Height), Color.White, 0, 
                            new Vector2(0, 0), 1.0f, SpriteEffects.None, 1, "MainScreen"));
                        break;
                    }

                case 1:
                    {
                        IsMouseVisible = false;

                        //Load BG
                        Texture2D bg = Content.Load<Texture2D>("Space.jpg");
                        imageList.Add(bg);
                        addAsActive(new TextureObj(bg, new Vector2(0, 0), new Rectangle(0, 0, bg.Width, bg.Height), Color.White, 0, 
                            new Vector2(0, 0), 1.0f, SpriteEffects.None, 1, "Background"));

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
                        //Load Grape projectile image.
                        Texture2D projectileGrapeImage = Content.Load<Texture2D>("Projectile-grape.png");
                        imageList.Add(projectileGrapeImage);
                        System.Diagnostics.Debug.WriteLine("Grape projectile: " + projectileGrapeImage.Name);
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
                        createPlayer(imageList.Find(name => name.Name == "Banana.png"), 
                            screenWidth / 4 - playerImage.Width / 2, screenHeight / 4 - playerImage.Height / 2, 2.5f, 5f, 
                            new Vector2(48, 16), new InputKeyboard(Keys.Escape, Keys.W, Keys.A, Keys.S, Keys.D, Keys.Space));

                        System.Diagnostics.Debug.WriteLine("Making a player ");
                        Texture2D player2Image = Content.Load<Texture2D>("Strawberry.png");
                        imageList.Add(player2Image);

                        createPlayer(imageList.Find(name => name.Name == "Strawberry.png"), 
                            screenWidth / 4 * 3 - player2Image.Width / 2, screenHeight / 4 - player2Image.Height / 2, 2.5f, 5f, 
                            new Vector2(screenWidth - 48, 16), new InputManager(
                            new InputKeyboard(Keys.Escape, Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.RightShift), 
                            new InputController(PlayerIndex.One)));

                        System.Diagnostics.Debug.WriteLine("Making a player ");
                        Texture2D player3Image = Content.Load<Texture2D>("Pear.png");
                        imageList.Add(player3Image);
                        createPlayer(imageList.Find(name => name.Name == "Pear.png"), screenWidth / 4 * 1 - player3Image.Width / 2, screenHeight / 4 * 3 - player3Image.Height / 2, 2.5f, 5f, 
                            new Vector2(48, screenHeight - 16), new InputManager(new InputKeyboard(Keys.Escape, Keys.I, Keys.J, Keys.K, Keys.L, Keys.RightAlt), 
                            new InputController(PlayerIndex.Two)));

                        System.Diagnostics.Debug.WriteLine("Making a player ");
                        Texture2D player4Image = Content.Load<Texture2D>("Grapes.png");
                        imageList.Add(player4Image);
                        createPlayer(imageList.Find(name => name.Name == "Grapes.png"), screenWidth / 4 * 3 - player3Image.Width / 2, screenHeight / 4 * 3 - player3Image.Height / 2, 2.5f, 5f, 
                            new Vector2(screenWidth - 48, screenHeight - 16), new InputManager(new InputKeyboard(Keys.Escape, Keys.NumPad8, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.Enter), 
                            new InputController(PlayerIndex.Three)));
                        break;
                    }

                case 2:
                    {
                        //Load BG
                        Texture2D bg2 = Content.Load<Texture2D>("Space.jpg");
                        imageList.Add(bg2);
                        addAsActive(new TextureObj(bg2, new Vector2(0, 0), 
                            new Rectangle(0, 0, bg2.Width, bg2.Height), Color.White, 0, 
                            new Vector2(0, 0), 1.0f, SpriteEffects.None, 1, "Background"));

                        //Load meatball
                        Texture2D meatballImage = Content.Load<Texture2D>("Meatball.png");
                        imageList.Add(meatballImage);

                        //Load Banana Projectile image.
                        Texture2D projectileBananaImage = Content.Load<Texture2D>("Projectile-banana.png");
                        imageList.Add(projectileBananaImage);
                        System.Diagnostics.Debug.WriteLine("Banana projectile: " + projectileBananaImage.Name);

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
                        createPlayer(imageList.Find(name => name.Name == "Banana.png"), 
                            screenWidth / 4 - playerImage.Width / 2, screenHeight / 4 - playerImage.Height / 2, 2.5f, 5f, 
                            new Vector2(48, 16), new InputKeyboard(Keys.Escape, Keys.W, Keys.A, Keys.S, Keys.D, Keys.Space));

                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        private void createPlayer(Texture2D playerImage, int x, int y, float movementSpeed, 
            float rotateSpeed, Vector2 healthBarLocation, Input playerInput)
        {
            playerAmount++;
            TextureObj playerObj = new TextureObj(playerAmount, playerImage, new Vector2(x, y), 
                new Rectangle(0, 0, playerImage.Width, playerImage.Height), Color.White, 0, 
                new Vector2(playerImage.Width / 2, playerImage.Height / 2), 1.0f, SpriteEffects.None, 1, "Player");

            addAsActive(playerObj);
            playerList.Add(new Player(playerAmount, playerObj, movementSpeed, rotateSpeed, healthBarLocation, playerInput));
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            //SharpDX.XInput.Controller.SetVibration();
            //check if the screen should change to another "level".
            if (changeInCurrentScreen)
            {
                activeObjects.Clear();
                LoadContent();
                changeInCurrentScreen = false;
            }

            //check if either the Actives list, Projectile list or Meatball list has been changed.
            if (changeInLists)
            {
                if (changeInActives)
                {
                    changeActives();
                }
                if (changeInProjectiles)
                {
                    changeProjectiles();
                }
                if (changeInMeatballs)
                {
                    changeMeatballs();
                }
            }

            var KBS = Keyboard.GetState();
            var newMS = Mouse.GetState();
            var gpdOne = GamePad.GetState(PlayerIndex.One);

            switch (currentScreen)
            {
                case 0:
                    Mouse.SetPosition((int)(newMS.X + gpdOne.ThumbSticks.Left.X * 6), (int)(newMS.Y - gpdOne.ThumbSticks.Left.Y * 6));
                    if (newMS.LeftButton == ButtonState.Released 
                        && oldMS.LeftButton == ButtonState.Pressed 
                        || gpdOne.Buttons.A == ButtonState.Pressed)
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
                    moveAndCheckCollisionProjectiles();

                    //check if a player has pressed any keys.
                    foreach (Player player in playerList)
                    {
                        player.checkInput();
                    }

                    break;

                case 2:
                    moveAndCheckCollisionProjectiles();

                    //check if a player has pressed any keys.
                    foreach (Player player in playerList)
                    {
                        player.checkInput();
                    }

                    //this switch generates meatballs.
                    switch (gamePC)
                    {
                        case 0:
                            if (true)
                            {
                                gamePC = 1;
                                maximumMeatballs = (int) (18 / playerAmount);
                            }
                            break;

                        case 1:
                            if (meatballs.Count < maximumMeatballs)
                            {
                                System.Diagnostics.Debug.WriteLine("Max meat:: " + maximumMeatballs);

                                createMeatball();
                            }
                            break;

                        case 2:
                            maximumMeatballs = (int)(24 / playerAmount);
                            break;

                        default:
                            break;
                    }

                    moveMeatballs();
                    break;

                default:
                    System.Diagnostics.Debug.WriteLine("Could not find screen");
                    Exit();
                    break;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed 
                || GamePad.GetState(PlayerIndex.Three).Buttons.Back == ButtonState.Pressed 
                || KBS.IsKeyDown(Keys.Escape))
                Exit();

            if (KBS.IsKeyDown(Keys.PageUp))
            {
                //make fullscreen
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
                screenWidth = GraphicsDevice.Viewport.Width;
                screenHeight = GraphicsDevice.Viewport.Height;
            }
            if (KBS.IsKeyDown(Keys.PageDown))
            {
                //make fullscreen
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
                screenWidth = GraphicsDevice.Viewport.Width;
                screenHeight = GraphicsDevice.Viewport.Height;
            }
            if (forceExit)
            {
                Exit();
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

        public void moveAndCheckCollisionProjectiles()
        {
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
                                        System.Diagnostics.Debug.WriteLine("Projectile from player: " + obj.getPlayerNumber() 
                                            + " has COLLISION with Player: " + collisionCheck.getPlayerNumber());
                                        //Here you can handle the collision with a player, like lowering the HP of the hit target.

                                        Player hitPlayer = playerList.Find(player => player.getPlayerNumber() == collisionCheck.getPlayerNumber());
                                        hitPlayer.changeHealth(hitPlayer.getHealth() - 1);

                                    }
                                    //else the hit object is not a player.
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine("Projectile from player: " + obj.getPlayerNumber() 
                                            + " has COLLISION with something: " + collisionCheck.getType());
                                        //here you can handle the collision with a non-player object.

                                        if (collisionCheck.getType().Equals("Seed") 
                                            || collisionCheck.getType().Equals("Strawberry slice") 
                                            || collisionCheck.getType().Equals("Banana slice") 
                                            || collisionCheck.getType().Equals("Pear slice") 
                                            || collisionCheck.getType().Equals("Grape slice"))
                                        {
                                            projectileObjects.Find(findProjectile => findProjectile.getObject() == collisionCheck).setSecondsLeft(0);
                                        }
                                        else if(collisionCheck.getType().Equals("Meatball"))
                                        {
                                            removeAsMeatball(meatballs.Find(meatball => meatball.getObject() == collisionCheck));
                                            removeAsActive(collisionCheck);
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
        }
        
        public void moveMeatballs()
        {
            if (meatballs.Count != 0)
            {
                System.Diagnostics.Debug.WriteLine("Meatballs: " + meatballs.Count);
                for (int i = 0; i < meatballs.Count; i++)
                {
                    AutoMoveMeatball obj = meatballs[i];
                    float objWidth = obj.getObject().getTexture().Width * obj.getObject().getScale();
                    float objHeight = obj.getObject().getTexture().Height * obj.getObject().getScale();
                    float objX = obj.getObject().getLocation().X;
                    float objY = obj.getObject().getLocation().Y;
                    System.Diagnostics.Debug.WriteLine("This meatball: " + objX + ", " + objY);
                    if (objX < 0 - objWidth / 2 || objX > screenWidth + objWidth / 2)
                    {
                        removeAsMeatball(obj);
                        removeAsActive(obj.getObject());
                    }
                    else if (objY < 0 - objHeight / 2 || objY > screenHeight + objHeight / 2)
                    {
                        removeAsMeatball(obj);
                        removeAsActive(obj.getObject());
                    }
                    else
                    {
                        obj.getObject().addToLocation(obj.toAddX, obj.toAddY);
                    }
                }
            }
        }

        public void createMeatball()
        {
            Texture2D meatballImage = Main.imageList.Find(name => name.Name == "Meatball.png");

            Vector2 location = new Vector2(0, 0);
            float angle = 0;
            float speed = (float)(random.NextDouble() * 20) + 1.0f;
            float scale = (float)(random.NextDouble() * 4);

            int n = random.Next(4);
            if(n == 0)
            {
                location = new Vector2((float)(random.NextDouble() * screenWidth), (float)(0 - ((meatballImage.Height / 2) * scale)));
                angle = (float)(random.NextDouble() * 140 + 110);
            }
            else if(n == 1)
            {
                location = new Vector2((float)(screenWidth + ((meatballImage.Width / 2) * scale)), (float)(random.NextDouble() * screenHeight));
                angle = (float)(random.NextDouble() * 140 + 200);
            }
            else if (n == 2)
            {
                location = new Vector2((float)(random.NextDouble() * screenWidth), (float)(screenHeight + ((meatballImage.Height / 2) * scale)));
                angle = (float)(random.NextDouble() * 140 + - 70);
            }
            else if (n == 3)
            {
                location = new Vector2((float)(0 - ((meatballImage.Width / 2) * scale)), (float)(random.NextDouble() * screenHeight));
                angle = (float)(random.NextDouble() * 140 + 20);
            }

            TextureObj projectileObj = new TextureObj(meatballImage, location, new Rectangle(0, 0, meatballImage.Width, meatballImage.Height), Color.White, angle, new Vector2(meatballImage.Width / 2, meatballImage.Height / 2), scale, SpriteEffects.None, 1.0f, "Meatball");
            Main.addAsActive(projectileObj);
            Main.addAsMeatball(new AutoMoveMeatball(projectileObj, speed, angle));
        }

        //the following 9 methods are to make sure a list will not be altered while a loop is checking them.
        //Instead if changes are made they will be done at the start of the update() method.

        private void changeMeatballs()
        {
            foreach (AutoMoveMeatball waitingMeatball in toAutomateMeatballs)
            {
                meatballs.Add(waitingMeatball);
            }
            toAutomateMeatballs.Clear();
            foreach (AutoMoveMeatball waitingMeatball in toDeautomateMeatballs)
            {
                meatballs.Remove(waitingMeatball);
            }
            toDeautomateMeatballs.Clear();
            changeInMeatballs = false;
            changeInLists = false;
        }
        public static void addAsMeatball(AutoMoveMeatball obj)
        {
            toAutomateMeatballs.Add(obj);
            changeInMeatballs = true;
            changeInLists = true;
        }
        public static void removeAsMeatball(AutoMoveMeatball obj)
        {
            toDeautomateMeatballs.Add(obj);
            changeInMeatballs = true;
            changeInLists = true;
        }

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
