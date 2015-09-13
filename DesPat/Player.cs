 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DesPat
{
    public class Player
    {
        private int playerNumber;
        private TextureObj textureObj;
        private Keys left;
        private Keys right;
        private Keys down;
        private Keys shoot;
        private Keys up;

        private bool controller;
        private PlayerIndex playerIndex;

        private float movementSpeed;
        private float rotateSpeed;

        private long lastShot = 0;

        private int health = 3;

        private Vector2 healthBarLocation;

        public Player(int playerNumber, TextureObj textureObj, float movementSpeed, float rotateSpeed, Keys up, Keys left, Keys down, Keys right, Keys shoot, Vector2 healthBarLocation)
        {
            this.playerNumber = playerNumber;
            this.textureObj = textureObj;
            this.movementSpeed = movementSpeed;
            this.rotateSpeed = rotateSpeed;
            this.up = up;
            this.left = left;
            this.down = down;
            this.right = right;
            this.shoot = shoot;
            this.healthBarLocation = healthBarLocation;
            controller = false;

            String objName = "Life-" + health + ".png";
            Texture2D healthBar = Main.imageList.Find(obj => obj.Name == objName);
            Main.addAsActive(new TextureObj(playerNumber, healthBar, healthBarLocation, new Rectangle(0, 0, healthBar.Width, healthBar.Height), Color.White, 0, new Vector2(healthBar.Width / 2, healthBar.Height / 2), 1.0f, SpriteEffects.None, 1, "Healthbar"));
        }
        public Player(int playerNumber, TextureObj textureObj, float movementSpeed, float rotateSpeed, PlayerIndex playerIndex, Vector2 healthBarLocation)
        {
            this.playerNumber = playerNumber;
            this.textureObj = textureObj;
            this.movementSpeed = movementSpeed;
            this.rotateSpeed = rotateSpeed;
            this.playerIndex = playerIndex;
            this.healthBarLocation = healthBarLocation;
            controller = true;

            String objName = "Life-" + health + ".png";
            Texture2D healthBar = Main.imageList.Find(obj => obj.Name == objName);
            Main.addAsActive(new TextureObj(playerNumber, healthBar, healthBarLocation, new Rectangle(0, 0, healthBar.Width, healthBar.Height), Color.White, 0, new Vector2(healthBar.Width / 2, healthBar.Height / 2), 1.0f, SpriteEffects.None, 1, "Healthbar"));
        }

        public void checkKeys()
        {
            if(controller == true)
            {
                var gpd = GamePad.GetState(playerIndex);
                executeKeys(gpd.ThumbSticks.Left.Y > 0f, gpd.ThumbSticks.Right.X < 0f, gpd.ThumbSticks.Left.Y < 0f, gpd.ThumbSticks.Right.X > 0f, gpd.Triggers.Right == 1.0f);
            }
            else
            {
                var KBS = Keyboard.GetState();
                executeKeys(KBS.IsKeyDown(up), KBS.IsKeyDown(left), KBS.IsKeyDown(down), KBS.IsKeyDown(right), KBS.IsKeyDown(shoot));
            }

          
        }
        private void executeKeys(bool up, bool left, bool down, bool right, bool shoot)
        {
            var movementDelta = Vector2.Zero;
            float angle = textureObj.getAngle();
            if (up)
            {
                if (angle >= 0 && angle < 90)
                {
                    movementDelta.X += movementSpeed * ((angle) / 45);
                    movementDelta.Y -= movementSpeed * ((2f - (angle) / 45));
                }
                else if (angle >= 90 && angle < 180)
                {
                    movementDelta.X += movementSpeed * ((2f - ((90 - (180 - angle)) / 45)));
                    movementDelta.Y += movementSpeed * ((90 - (180 - angle)) / 45);
                }
                else if (angle >= 180 && angle < 270)
                {
                    movementDelta.X -= movementSpeed * ((90 - (270 - angle)) / 45);
                    movementDelta.Y += movementSpeed * (2f - ((90 - (270 - angle)) / 45));
                }
                else if (angle >= 270 && angle < 360)
                {
                    movementDelta.X -= movementSpeed * ((360 - angle) / 45);
                    movementDelta.Y -= movementSpeed * (2f - ((360 - angle) / 45));
                }
            }
            if (left)
            {
                angle -= rotateSpeed;
            }
            if (down)
            {
                if (angle >= 0 && angle < 90)
                {
                    movementDelta.X -= movementSpeed * ((angle) / 45);
                    movementDelta.Y += movementSpeed * ((2f - (angle) / 45));
                }
                else if (angle >= 90 && angle < 180)
                {
                    movementDelta.X -= movementSpeed * ((2f - ((90 - (180 - angle)) / 45)));
                    movementDelta.Y -= movementSpeed * ((90 - (180 - angle)) / 45);
                }
                else if (angle >= 180 && angle < 270)
                {
                    movementDelta.X += movementSpeed * ((90 - (270 - angle)) / 45);
                    movementDelta.Y -= movementSpeed * (2f - ((90 - (270 - angle)) / 45));
                }
                else if (angle >= 270 && angle < 360)
                {
                    movementDelta.X += movementSpeed * ((360 - angle) / 45);
                    movementDelta.Y += movementSpeed * (2f - ((360 - angle) / 45));
                }
            }
            if (right)
            {
                angle += rotateSpeed;
            }
            if (shoot)
            {
                if (lastShot == 0 || (DateTime.Now.Ticks - lastShot) / 5000000 >= 1)
                {
                    shootSeed(playerNumber, 5.0, 5f);
                    lastShot = DateTime.Now.Ticks;
                }
            }

            textureObj.changeAngle(angle);
            textureObj.addToLocation(movementDelta);

            if (textureObj.getLocation().X <= textureObj.getTexture().Width / 2)
                textureObj.changeLocation(textureObj.getTexture().Width / 2 ,textureObj.getLocation().Y);
            if (textureObj.getLocation().X + textureObj.getTexture().Width / 2 >= Main.screenWidth)
                textureObj.changeLocation(Main.screenWidth - textureObj.getTexture().Width / 2, textureObj.getLocation().Y);

            if (textureObj.getLocation().Y <= textureObj.getTexture().Height / 2)
                textureObj.changeLocation(textureObj.getLocation().X, textureObj.getTexture().Height / 2);
            if (textureObj.getLocation().Y + textureObj.getTexture().Height / 2 >= Main.screenHeight)
                textureObj.changeLocation(textureObj.getLocation().X, Main.screenHeight - textureObj.getTexture().Height / 2);


        }
        public void shootSeed(int playerNumber, double seconds, float speed)
        {
            TextureObj projectileObj = createProjectileObject(playerNumber);
            Main.activeObjects.Add(projectileObj);
            Main.automaticMovement.Add(new AutoMoveSeed(playerNumber, projectileObj, seconds, speed, textureObj.getAngle()));
        }
        public TextureObj createProjectileObject(int playerNumber)
        {
            Texture2D projectileImage;
            String projectileType;

            if (playerNumber == 1)
            {
                projectileImage = Main.imageList.Find(name => name.Name == "Projectile-banana.png");
                projectileType = "Banana slice";
            }
            else if (playerNumber == 2)
            {
                projectileImage = Main.imageList.Find(name => name.Name == "Projectile-tomato.png");
                projectileType = "Tomato Slice";
            }
            else
            {
                projectileImage = Main.imageList.Find(name => name.Name == "Seed.png");
                projectileType = "Seed";
            }
            return new TextureObj(projectileImage, textureObj.getLocation(), new Rectangle(0, 0, projectileImage.Width, projectileImage.Height), Color.White, 0, new Vector2(projectileImage.Width / 2, projectileImage.Height / 2), 1.0f, SpriteEffects.None, 1, projectileType);
        }
        public void changeHealth(int newHealth)
        {
            health = newHealth;
            Main.removeAsActive(Main.activeObjects.Find(obj => obj.getPlayerNumber() == playerNumber && obj.getType() == "Healthbar"));
            Texture2D healthBar = Main.imageList.Find(obj => obj.Name == "Life-" + health + ".png");
            if(healthBar != null)
            {
                Main.addAsActive(new TextureObj(playerNumber, healthBar, healthBarLocation, new Rectangle(0, 0, healthBar.Width, healthBar.Height), Color.White, 0, new Vector2(healthBar.Width / 2, healthBar.Height / 2), 1.0f, SpriteEffects.None, 1, "Healthbar"));
            }
            else
            {
                Main.ExitGame();
            }
            
        }
        public int getHealth()
        {
            return health;
        }
        public TextureObj getTextureObj()
        {
            return textureObj;
        }
        public int getPlayerNumber()
        {
            return playerNumber;
        }
    }
}
