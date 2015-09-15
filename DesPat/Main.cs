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

        private int playerAmount = 0;
        private static bool forceExit = false;

        //screen Parameters
        public static int screenWidth;
        public static int screenHeight;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);

            //NOTE: if fullscreen is activated, if an error occurs exiting the game will be very hard as the fullscreen will
            //cover taskmanager and you wont exit. Instead try pressing enter if this happens that fixed it for me. -Juno
            //graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load BG
            Texture2D bg = Content.Load<Texture2D>("Space.jpg");
            imageList.Add(bg);
            addAsActive(new TextureObj(bg, new Vector2(0,0), new Rectangle(0, 0, bg.Width, bg.Height), Color.White, 0, new Vector2(bg.Width / 2, bg.Height / 2), 1.0f, SpriteEffects.None, 1, "Background"));

            //detect screen parameters
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

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

            //  5k         25k 35k   20k 
            // [8*, 8, 16, 32*, 64*, 32*, 32, 64]
<<<<<<< HEAD

=======
            
>>>>>>> 8e4c4c9ef603ab1b8aef1500ce610acb7ec3ea9d
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
<<<<<<< HEAD
            createPlayer(imageList.Find(name => name.Name == "Strawberry.png"), screenWidth / 4 * 3 - player2Image.Width / 2, screenHeight / 4 - player2Image.Height / 2, 2.5f, 5f, PlayerIndex.One, new Vector2(screenWidth - 48, 16));
=======
            createPlayer(imageList.Find(name => name.Name == "Tomato.png"), screenWidth / 4 * 3 - player2Image.Width / 2, screenHeight / 4 - player2Image.Height / 2, 2.5f, 5f, PlayerIndex.One, new Vector2(screenWidth - 48, 16));
>>>>>>> 8e4c4c9ef603ab1b8aef1500ce610acb7ec3ea9d

            System.Diagnostics.Debug.WriteLine("Making a player ");
            Texture2D player3Image = Content.Load<Texture2D>("Pear.png");
            imageList.Add(player3Image);
            createPlayer(imageList.Find(name => name.Name == "Pear.png"), screenWidth / 4 * 2 - player3Image.Width / 2, screenHeight / 4 * 3 - player3Image.Height / 2, 2.5f, 5f, PlayerIndex.Two, new Vector2(48, screenHeight - 16));

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

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
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
            //System.Diagnostics.Debug.WriteLine("A Frame!");
            if (projectileObjects.Count != 0)
            {
                for(int i = 0; i < projectileObjects.Count; i++)
                {
                    AutoMoveProjectile obj = projectileObjects[i];
                    foreach (TextureObj collisionCheck in activeObjects)
                    {
                        //System.Diagnostics.Debug.WriteLine("obj playerNumber: " + obj.getPlayerNumber() + ", checkNumber: " + collisionCheck.getPlayerNumber());
                        bool collidable = true;

                        //in this if statement, add any Type which shouldnt collide with projectiles. Like background and GUI.
                        if(collisionCheck.getType().Equals("Background") || collisionCheck.getType().Equals("Healthbar"))
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

<<<<<<< HEAD
                                        if (collisionCheck.getType().Equals("Seed") || collisionCheck.getType().Equals("Strawberry slice") || collisionCheck.getType().Equals("Banana slice") || collisionCheck.getType().Equals("Pear slice")) 
=======
                                        if (collisionCheck.getType().Equals("Seed") || collisionCheck.getType().Equals("Tomato slice") || collisionCheck.getType().Equals("Banana slice"))
>>>>>>> 8e4c4c9ef603ab1b8aef1500ce610acb7ec3ea9d
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
            foreach(Player player in playerList)
            {
                player.checkKeys();
            }

            var KBS = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed || GamePad.GetState(PlayerIndex.Three).Buttons.Back == ButtonState.Pressed || KBS.IsKeyDown(Keys.Escape))
                Exit();
            if (forceExit)
            {
                Exit();
            }

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
