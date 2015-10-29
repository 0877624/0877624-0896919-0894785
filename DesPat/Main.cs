using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DesPat
{
    enum IResult
    {
        Done,
        DoneAndCreate,
        Running,
        RunningAndCreate
    }

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

        public static List<AutoMoveMeatball> meatballs = new List<AutoMoveMeatball>();
        private static List<AutoMoveMeatball> toDeautomateMeatballs = new List<AutoMoveMeatball>();
        private static List<AutoMoveMeatball> toAutomateMeatballs = new List<AutoMoveMeatball>();

        public static List<Player> playerList = new List<Player>();
        public static List<Player> toAddPlayers = new List<Player>();
        public static List<Player> toRemovePlayers = new List<Player>();

        public static List<Texture2D> imageList = new List<Texture2D>();

        private static bool changeInLists = false;
        private static bool changeInActives = false;
        private static bool changeInProjectiles = false;
        private static bool changeInMeatballs = false;
        private static bool changeInPlayers = false;

        private bool changeInCurrentScreen = false;

        private int playerAmount = 0;
        private static bool forceExit = false;
        public static Random randomGen = new Random();
        Instruction gameLogic =
        new Repeat(
         new For(0, 100, i =>
               new Wait(() => i * 0.1f) +
               new CreateMeatball()) +
         new Wait(() => randomGen.Next(1, 5)) +
         new For(0, 100, i =>
               new Wait(() => (float)randomGen.NextDouble() * 1.0f + 0.2f) +
               new CreateMeatball()) +
         new Wait(() => randomGen.Next(2, 3)));

        //screen Parameters
        public static int screenWidth;
        public static int screenHeight;

        private int currentScreen = 0;

        private MouseState oldMS;

        //switch stuff
        private Random random = new Random();

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
                        Texture2D playerImage = Content.Load<Texture2D>("Banana.png");
                        imageList.Add(playerImage);
                        Player playerOne = createPlayer(imageList.Find(name => name.Name == "Banana.png"),
                            screenWidth / 4 - playerImage.Width / 2, screenHeight / 4 - playerImage.Height / 2, 2.5f, 5f,
                            new Vector2(48, 16), new InputKeyboard(Keys.Escape, Keys.W, Keys.A, Keys.S, Keys.D, Keys.Space));
                        playerOne.addWeapon(new bananaShot(playerOne));

                        Texture2D player2Image = Content.Load<Texture2D>("Strawberry.png");
                        imageList.Add(player2Image);
                        Player playerTwo = createPlayer(imageList.Find(name => name.Name == "Strawberry.png"), 
                            screenWidth / 4 * 3 - player2Image.Width / 2, screenHeight / 4 - player2Image.Height / 2, 2.5f, 5f, 
                            new Vector2(screenWidth - 48, 16), new InputManager(
                            new InputKeyboard(Keys.Escape, Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.RightShift), 
                            new InputController(PlayerIndex.One)));
                        playerTwo.addWeapon(new strawberryShot(playerTwo));

                        Texture2D player3Image = Content.Load<Texture2D>("Pear.png");
                        imageList.Add(player3Image);
                        Player playerThree = createPlayer(imageList.Find(name => name.Name == "Pear.png"), screenWidth / 4 * 1 - player3Image.Width / 2, screenHeight / 4 * 3 - player3Image.Height / 2, 2.5f, 5f, 
                            new Vector2(48, screenHeight - 16), new InputManager(
                            new InputKeyboard(Keys.Escape, Keys.I, Keys.J, Keys.K, Keys.L, Keys.RightAlt), 
                            new InputController(PlayerIndex.Two)));
                        playerThree.addWeapon(new pearShot(playerThree));

                        Texture2D player4Image = Content.Load<Texture2D>("Grapes.png");
                        imageList.Add(player4Image);
                        Player playerFour = createPlayer(imageList.Find(name => name.Name == "Grapes.png"), screenWidth / 4 * 3 - player3Image.Width / 2, screenHeight / 4 * 3 - player3Image.Height / 2, 2.5f, 5f, 
                            new Vector2(screenWidth - 48, screenHeight - 16), new InputManager(
                            new InputKeyboard(Keys.Escape, Keys.NumPad8, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.Enter), 
                            new InputController(PlayerIndex.Three)));
                        playerFour.addWeapon(new grapeShot(playerFour));
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
                        Texture2D playerImage = Content.Load<Texture2D>("Banana.png");
                        imageList.Add(playerImage);
                        Player playerOne = createPlayer(imageList.Find(name => name.Name == "Banana.png"), 
                            screenWidth / 4 - playerImage.Width / 2, screenHeight / 4 - playerImage.Height / 2, 2.5f, 5f, 
                            new Vector2(48, 16), new InputKeyboard(Keys.Escape, Keys.W, Keys.A, Keys.S, Keys.D, Keys.Space));
                        playerOne.addWeapon(new bananaShot(playerOne));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private Player createPlayer(Texture2D playerImage, int x, int y, float movementSpeed, 
            float rotateSpeed, Vector2 healthBarLocation, Input playerInput)
        {
            playerAmount++;
            System.Diagnostics.Debug.WriteLine("Making a player: " + playerAmount);
            TextureObj playerObj = new TextureObj(playerAmount, playerImage, new Vector2(x, y), 
                new Rectangle(0, 0, playerImage.Width, playerImage.Height), Color.White, 0, 
                new Vector2(playerImage.Width / 2, playerImage.Height / 2), 1.0f, SpriteEffects.None, 1, "Player");

            addAsActive(playerObj);
            Player newPlayer = new Player(playerAmount, playerObj, movementSpeed, rotateSpeed, healthBarLocation, playerInput);
            playerList.Add(newPlayer);
            return newPlayer;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //check if the screen should change to another "level".
            if (changeInCurrentScreen)
            {
                activeObjects.Clear();
                LoadContent();
                changeInCurrentScreen = false;
            }

            //check if either the Actives list, Projectile list, Meatball list or Player list has been changed.
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
                if (changeInPlayers)
                {
                    changePlayers();
                }
            }

            var KBS = Keyboard.GetState();
            var newMS = Mouse.GetState();
            var gpdOne = GamePad.GetState(PlayerIndex.One);

            switch (currentScreen)
            {
                //case 0 means the main screen.
                case 0:
                    //Make sure controller One can move the mouse in the main screen.
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
                //case 1 means the PvP with 4 players.
                case 1:
                    //Moves the projectile and checks if it collised with any of the actives.
                    moveAndCheckCollisionProjectiles(deltaTime);

                    //check if a player has pressed any keys, and react.
                    foreach (Player player in playerList)
                    {
                        player.checkInput(deltaTime);
                    }
                    break;
                //case 2 means the 1-player fight where you can shoot meatballs. Not much else.
                case 2:
                    //Moves the projectile and checks if it collised with any of the actives.
                    moveAndCheckCollisionProjectiles(deltaTime);

                    //check if a player has pressed any keys.
                    foreach (Player player in playerList)
                    {
                        player.checkInput(deltaTime);
                    }

                    //this switch generates the meatballs.
                    switch (gameLogic.Execute(deltaTime))
                    {
                        case IResult.DoneAndCreate:
                            createMeatball();
                            break;
                        case IResult.RunningAndCreate:
                            createMeatball();
                            break;
                    }

                    //Moves the meatballs so they move across the screen.
                    moveMeatballs();
                    break;

                //If for some reason the "level" or screen could not be found exit the game.
                default:
                    System.Diagnostics.Debug.WriteLine("Could not find screen");
                    Exit();
                    break;
            }

            //Exit the game if controllers 1-3 press back, or keyboard presses escape.
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
                //make non-fullscreen
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

        public void moveAndCheckCollisionProjectiles(float deltaTime)
        {
            //Check if there are any projectiles.
            if (projectileObjects.Count != 0)
            {
                //Loop through the existing projectiles
                for (int i = 0; i < projectileObjects.Count; i++)
                {
                    AutoMoveProjectile obj = projectileObjects[i];
                    //Check the current projectile (obj) against all active objects through a loop (collisionCheck);
                    foreach (TextureObj collisionCheck in activeObjects)
                    {
                        bool collidable = true;
                        //in this if statement, add any Type which shouldnt collide with projectiles. Like background and GUI.
                        if (collisionCheck.getType().Equals("Background") || collisionCheck.getType().Equals("Healthbar"))
                        {
                            collidable = false;
                        }
                        //this if checks if the TextureObj is not the player that shot the projectile, or if its not checking against itself
                        if (obj.getPlayerNumber() != collisionCheck.getPlayerNumber() && obj.getObject() != collisionCheck && collidable == true)
                        {
                            //this if checks if the Projectile (obj) hit a texture (checkCollision).
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
                                    if (collisionCheck.getType().Equals("Seed")
                                        || collisionCheck.getType().Equals("Strawberry slice")
                                        || collisionCheck.getType().Equals("Banana slice")
                                        || collisionCheck.getType().Equals("Pear slice")
                                        || collisionCheck.getType().Equals("Grape slice"))
                                    {
                                        projectileObjects.Find(findProjectile => findProjectile.getObject() == collisionCheck).setTimeLeft(0);
                                    }
                                    else if (collisionCheck.getType().Equals("Meatball"))
                                    {
                                        removeAsMeatball(meatballs.Find(meatball => meatball.getObject() == collisionCheck));
                                       removeAsActive(collisionCheck);
                                    }
                                }
                                //Delete the projectile that hit something.
                                obj.setTimeLeft(0);
                            }
                        }
                    }
                    if (obj.getTimeLeft() > 0)
                    {
                        obj.getObject().addToLocation(obj.toAddX, obj.toAddY);
                        obj.setTimeLeft(obj.getTimeLeft() - deltaTime);
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
                //System.Diagnostics.Debug.WriteLine("Meatballs: " + meatballs.Count);
                for (int i = 0; i < meatballs.Count; i++)
                {
                    AutoMoveMeatball obj = meatballs[i];
                    float objWidth = obj.getObject().getTexture().Width * obj.getObject().getScale();
                    float objHeight = obj.getObject().getTexture().Height * obj.getObject().getScale();
                    float objX = obj.getObject().getLocation().X;
                    float objY = obj.getObject().getLocation().Y;
                    //System.Diagnostics.Debug.WriteLine("This meatball: " + objX + ", OK,  " + objY + ", AND: " + (screenHeight + objHeight / 2));
                    if (objX <= (0 - objWidth / 2) || objX >= (screenWidth + objWidth / 2))
                    {
                        removeAsMeatball(obj);
                        removeAsActive(obj.getObject());
                    }
                    else if (objY <= (0 - objHeight / 2) || objY >= (screenHeight + objHeight / 2))
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
            Texture2D meatballImage = imageList.Find(name => name.Name == "Meatball.png");

            Vector2 location = new Vector2(0, 0);
            float angle = 0;
            float speed = (float)(random.NextDouble() * 20) + 1.0f;
            float scale = (float)(random.NextDouble() * 4) + 1.0f;

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
                //To make sure you dont pass on negative angles.
                if (angle < 0)
                {
                    angle += 359;
                }
            }
            else if (n == 3)
            {
                location = new Vector2((float)(0 - ((meatballImage.Width / 2) * scale)), (float)(random.NextDouble() * screenHeight));
                angle = (float)(random.NextDouble() * 140 + 20);
            }

            TextureObj projectileObj = new TextureObj(meatballImage, location, new Rectangle(0, 0, meatballImage.Width, meatballImage.Height), Color.White, angle, new Vector2(meatballImage.Width / 2, meatballImage.Height / 2), scale, SpriteEffects.None, 1.0f, "Meatball");
            addAsActive(projectileObj);
            addAsMeatball(new AutoMoveMeatball(projectileObj, speed, angle));
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
        }
        public static void addAsAutomatic(AutoMoveProjectile obj)
        {
            toAutomateProjectiles.Add(obj);
            changeInProjectiles = true;
        }
        public static void removeAsProjectile(AutoMoveProjectile obj)
        {
            toDeautomateProjectiles.Add(obj);
            changeInProjectiles = true;
        }

        private void changePlayers()
        {
            foreach (Player player in toAddPlayers)
            {
                playerList.Add(player);
            }
            toAddPlayers.Clear();
            foreach (Player player in toRemovePlayers)
            {
                playerList.Remove(player);
            }
            toRemovePlayers.Clear();
            changeInPlayers = false;
            changeInLists = false;
        }
        public static void addAsPlayer(Player player)
        {
            toAddPlayers.Add(player);
            changeInPlayers = true;
            changeInLists = true;
        }
        public static void removeAsPlayer(Player player)
        {
            toRemovePlayers.Add(player);
            changeInPlayers = true;
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
