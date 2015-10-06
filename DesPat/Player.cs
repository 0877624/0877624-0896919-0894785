using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DesPat
{
    public class Player : Entity
    {
        private int playerNumber;

        private long lastShot = 0;

        private double maxHealth = 3;
        private double health = 3;

        private Vector2 healthBarLocation;

        //controller vibration
        public static ushort leftRumbleMotor = 65000;
        public static ushort rightRumbleMotor = 65000;
        public static SharpDX.XInput.Controller controller = new SharpDX.XInput.Controller();
        public static SharpDX.XInput.Vibration vibration = new SharpDX.XInput.Vibration();

        public Player(int playerNumber, TextureObj textureObj, float movementSpeed, float rotateSpeed, Vector2 healthBarLocation, Input entityInput)
        {
            this.playerNumber = playerNumber;
            base.textureObj = textureObj;
            base.movementSpeed = movementSpeed;
            base.rotateSpeed = rotateSpeed;
            this.healthBarLocation = healthBarLocation;
            base.entityInput = entityInput;
            
            String objName = "Life-" + (int)((health / maxHealth) * 3) + ".png";
            Texture2D healthBar = Main.imageList.Find(obj => obj.Name == objName);
            Main.addAsActive(new TextureObj(playerNumber, healthBar, healthBarLocation, new Rectangle(0, 0, healthBar.Width, healthBar.Height), Color.White, 0, new Vector2(healthBar.Width / 2, healthBar.Height / 2), 1.0f, SpriteEffects.None, 1, "Healthbar"));
        }
        public new void checkInput()
        {
            base.checkInput();
            if (entityInput.shooting)
            {
                if (lastShot == 0 || (DateTime.Now.Ticks - lastShot) / 5000000 >= 1)
                {
                    shootProjectile(playerNumber, 5.0, 5f);
                    lastShot = DateTime.Now.Ticks;
                }
            }
        }
        public void shootProjectile(int playerNumber, double seconds, float speed)
        {
            //vibration.LeftMotorSpeed = leftRumbleMotor;
            //vibration.RightMotorSpeed = rightRumbleMotor;
            //controller.SetVibration(vibration);
               
            TextureObj projectileObj = createProjectileObject(playerNumber);
            Main.addAsActive(projectileObj);
            Main.addAsAutomatic(new AutoMoveProjectile(playerNumber, projectileObj, seconds, speed, textureObj.getAngle()));
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
                projectileImage = Main.imageList.Find(name => name.Name == "Projectile-strawberry.png");
                projectileType = "Strawberry slice";
            }
            else if(playerNumber == 3)
            {
                projectileImage = Main.imageList.Find(name => name.Name == "Projectile-pear.png");
                projectileType = "Pear slice";
            }
            else if (playerNumber == 4)
            {
                projectileImage = Main.imageList.Find(name => name.Name == "Projectile-grape.png");
                projectileType = "Grape slice";
            }
            else
            {
                projectileImage = Main.imageList.Find(name => name.Name == "Seed.png");
                projectileType = "Seed";
            }
            return new TextureObj(projectileImage, textureObj.getLocation(), new Rectangle(0, 0, projectileImage.Width, projectileImage.Height), Color.White, 0, new Vector2(projectileImage.Width / 2, projectileImage.Height / 2), 1.0f, SpriteEffects.None, 1, projectileType);
        }
        public void changeHealth(double newHealth)
        {
            health = newHealth;
            if (health > 0)
            {
                Main.removeAsActive(Main.activeObjects.Find(obj => obj.getPlayerNumber() == playerNumber && obj.getType() == "Healthbar"));
                Texture2D healthBar = Main.imageList.Find(obj => obj.Name == "Life-" + (int)((health / maxHealth) * 3) + ".png");
                if (healthBar != null)
                {
                    Main.addAsActive(new TextureObj(playerNumber, healthBar, healthBarLocation, new Rectangle(0, 0, healthBar.Width, healthBar.Height), Color.White, 0, new Vector2(healthBar.Width / 2, healthBar.Height / 2), 1.0f, SpriteEffects.None, 1, "Healthbar"));
                }
                else
                {
                    Main.ExitGame();
                }
            }
            else
            {
                Main.removeAsActive(Main.activeObjects.Find(obj => obj.getPlayerNumber() == playerNumber && obj.getType() == "Healthbar"));
                Texture2D healthBar = Main.imageList.Find(obj => obj.Name == "Life-" + (int)((health / maxHealth) * 3) + ".png");
                if (healthBar != null)
                {
                    Main.addAsActive(new TextureObj(playerNumber, healthBar, healthBarLocation, new Rectangle(0, 0, healthBar.Width, healthBar.Height), Color.White, 0, new Vector2(healthBar.Width / 2, healthBar.Height / 2), 1.0f, SpriteEffects.None, 1, "Healthbar"));
                }
                else
                {
                    Main.ExitGame();
                }

                Main.playerList.Remove(this);
                Main.removeAsActive(textureObj);
            }
            
        }
        public double getHealth()
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
