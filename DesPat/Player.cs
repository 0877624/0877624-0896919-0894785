using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DesPat
{
    public class Player : Entity
    {
        private int playerNumber;

        //lastShot holds the time since last shot fired.
        private long lastShot = 0;

        //maxHealth is the max health, health is the currentHealth and is the field that changes when the player is hit.
        private double maxHealth = 6;
        private double health = 6;

        private Vector2 healthBarLocation;
        private Weapon playerWeapon;

        public Player(int playerNumber, TextureObj textureObj, float movementSpeed, float rotateSpeed, Vector2 healthBarLocation, Input entityInput)
        {
            this.playerNumber = playerNumber;
            base.textureObj = textureObj;
            base.movementSpeed = movementSpeed;
            base.rotateSpeed = rotateSpeed;
            this.healthBarLocation = healthBarLocation;
            base.entityInput = entityInput;

            //create and add the healthbar to active textures.
            String objName = "Life-" + (int)(((health + (maxHealth / 3.0 - 1)) / maxHealth) * 3) + ".png";
            Texture2D healthBar = Main.imageList.Find(obj => obj.Name == objName);
            Main.addAsActive(new TextureObj(playerNumber, healthBar, healthBarLocation, new Rectangle(0, 0, healthBar.Width, healthBar.Height), Color.White, 0, new Vector2(healthBar.Width / 2, healthBar.Height / 2), 1.0f, SpriteEffects.None, 1, "Healthbar"));
        }

        //CheckInput is called every update for every player. It will check if input is happening and respond with
        //whatever has to happen.
        public new void checkInput()
        {
            base.checkInput();
            if (playerWeapon != null)
            {
                if (entityInput.shooting)
                {
                    System.Diagnostics.Debug.Print("SHOOOOOOOOOTINGGGGGGG");
                    if (lastShot == 0 || (DateTime.Now.Ticks - lastShot) / 5000000 >= 1)
                    {
                        playerWeapon.shoot();
                        lastShot = DateTime.Now.Ticks;
                        entityInput.vibrate();
                    }
                }
            }
        }
        //The changeHealth method changes the picture of the health aswell as the stored health value into the given value.
        public void changeHealth(double newHealth)
        {
            health = newHealth;
            //First remove old healthbar from active textures. Do this by finding a texture which's type is "Healthbar" and has a corresponding playerNumber.
            Main.removeAsActive(Main.activeObjects.Find(obj => obj.getPlayerNumber() == playerNumber && obj.getType() == "Healthbar"));
            //Then create the new healthbar with its corresponding picture.
            Texture2D healthBar = Main.imageList.Find(obj => obj.Name == "Life-" + (int)(((health + (maxHealth / 3.0 - 1)) / maxHealth) * 3) + ".png");
            //Check if the picture actually exist and is not null. If not, crash the game.
            if (healthBar != null)
            {
                Main.addAsActive(new TextureObj(playerNumber, healthBar, healthBarLocation, new Rectangle(0, 0, healthBar.Width, healthBar.Height), Color.White, 0, new Vector2(healthBar.Width / 2, healthBar.Height / 2), 1.0f, SpriteEffects.None, 1, "Healthbar"));
            }
            else
            {
                Main.ExitGame();
            }
            //If health is less than 0, remove the player from active textures and from the playerList.
            if(health <= 0)
            { 
                Main.playerList.Remove(this);
                Main.removeAsActive(textureObj);
            }
        }

        //Getters that return certain values.
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
        public void addWeapon(Weapon playerWeapon)
        {
            this.playerWeapon = playerWeapon;
        }
    }
}
