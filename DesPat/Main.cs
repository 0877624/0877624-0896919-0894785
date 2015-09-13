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
        public static List<Texture2D> imageList = new List<Texture2D>();
        public static List<AutoMoveSeed> automaticMovement = new List<AutoMoveSeed>();
        public static List<Player> playerList = new List<Player>();
        private static List<TextureObj> toActivateObjects = new List<TextureObj>();
        private static List<TextureObj> toDeactivateObjects = new List<TextureObj>();
        private static bool changeInActives = false;


        private int playerAmount = 0;
        private static bool forceExit = false;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

            //############################################################################################################################################
            //Load Banana Projectile image.
            Texture2D projectileBananaImage = Content.Load<Texture2D>("Projectile-banana.png");
            imageList.Add(projectileBananaImage);
            System.Diagnostics.Debug.WriteLine("Banana projectile: " + projectileBananaImage.Name);
            //Load Tomato Projectile image.
            Texture2D projectileTomatoImage = Content.Load<Texture2D>("Projectile-tomato.png");
            imageList.Add(projectileTomatoImage);
            System.Diagnostics.Debug.WriteLine("Tomato projectile: " + projectileTomatoImage.Name);
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
            createPlayer(imageList.Find(name => name.Name == "Banana.png"), 200, 250, 2.5f, 5f, Keys.W, Keys.A, Keys.S, Keys.D, Keys.Space, new Vector2(48, 16));

            System.Diagnostics.Debug.WriteLine("Making a player ");
            Texture2D player2Image = Content.Load<Texture2D>("Tomato.png");
            imageList.Add(player2Image);
            createPlayer(imageList.Find(name => name.Name == "Tomato.png"), 600, 250, 2.5f, 5f, PlayerIndex.One, new Vector2(752, 16));
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
            if (changeInActives)
            {
                addActives();
            }
            //System.Diagnostics.Debug.WriteLine("A Frame!");
            if (automaticMovement.Count != 0)
            {
                for(int i = 0; i < automaticMovement.Count; i++)
                {
                    AutoMoveSeed obj = automaticMovement[i];
                    foreach (TextureObj collisionCheck in activeObjects)
                    {
                        //System.Diagnostics.Debug.WriteLine("obj playerNumber: " + obj.getPlayerNumber() + ", checkNumber: " + collisionCheck.getPlayerNumber());
                        bool collidable = true;

                        //in this if statement, add any Type which shouldnt collide with seeds. Like background and GUI.
                        if(collisionCheck.getType().Equals("Background") || collisionCheck.getType().Equals("Healthbar"))
                        {
                            collidable = false;
                        }
                        if (obj.getPlayerNumber() != collisionCheck.getPlayerNumber() && collidable == true)
                        {
                            //System.Diagnostics.Debug.WriteLine("Seed from player not colliding with own player");
                            if (obj.getObject() != collisionCheck)
                            {
                                //System.Diagnostics.Debug.WriteLine("Seed not colliding with itself");

                                if (obj.getObject().checkCollision(collisionCheck))
                                {
                                    //if the playerNumber of the texture object is not 0, a player has been hit.
                                    if (collisionCheck.getPlayerNumber() != 0)
                                    {
                                        System.Diagnostics.Debug.WriteLine("Seed from player: " + obj.getPlayerNumber() + " has COLLISION with Player: " + collisionCheck.getPlayerNumber());
                                        //Here you can handle the collision with a player, lowering the HP of the hit target.
                                        Player hitPlayer = playerList.Find(player => player.getPlayerNumber() == collisionCheck.getPlayerNumber());
                                        hitPlayer.changeHealth(hitPlayer.getHealth() - 1);

                                    }
                                    //else the hit object is not a player.
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine("Seed from player: " + obj.getPlayerNumber() + " has COLLISION with something: " + collisionCheck.getType());
                                        //here you can handle the collision with a non-player object.
                                        if(collisionCheck.getType().Equals("Seed"))
                                        {
                                            automaticMovement.Find(findSeed => findSeed.getObject() == collisionCheck).setSecondsLeft(0);
                                        }

                                    }

                                    //Delete seed that hit something.
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
                        automaticMovement.Remove(obj);
                        removeAsActive(obj.getObject());
                    }
                }
            }

            foreach(Player player in playerList)
            {
                //System.Diagnostics.Debug.WriteLine("A Player!");
                player.checkKeys();
            }
            var KBS = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || KBS.IsKeyDown(Keys.Escape))
                Exit();
            if(forceExit)
            {
                Exit();
            }


            //System.Diagnostics.Debug.WriteLine("Angle: " + angle);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach(TextureObj activeObject in activeObjects)
            {
                activeObject.drawObj(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void addActives()
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
        }
        public static void addAsActive(TextureObj obj)
        {
            toActivateObjects.Add(obj);
            changeInActives = true;
        }
        public static void removeAsActive(TextureObj obj)
        {
            toDeactivateObjects.Add(obj);
            changeInActives = true;
        }
        public static void ExitGame()
        {
            forceExit = true;
        }
    }
}
