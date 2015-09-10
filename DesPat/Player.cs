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

        private float movementSpeed;
        private float rotateSpeed;

        private long lastShot = 0;

        public Player(int playerNumber, TextureObj textureObj, float movementSpeed, float rotateSpeed, Keys up, Keys left, Keys down, Keys right, Keys shoot)
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
        }

        public void checkKeys()
        {
            var KBS = Keyboard.GetState();

            var movementDelta = Vector2.Zero;
            float angle = textureObj.getAngle();

            if (KBS.IsKeyDown(up))
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
            if (KBS.IsKeyDown(left))
            {
                angle -= rotateSpeed;
            }
            if (KBS.IsKeyDown(down))
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
            if (KBS.IsKeyDown(right))
            {
                angle += rotateSpeed;
            }
            if (KBS.IsKeyDown(shoot))
            {
                if (lastShot == 0 || (DateTime.Now.Ticks - lastShot) / 5000000 >= 1)
                {
                    shootSeed(playerNumber, 5.0, 5f);
                    lastShot = DateTime.Now.Ticks;
                }
            }

            textureObj.changeAngle(angle);
            textureObj.addToLocation(movementDelta);
        }
        public void shootSeed(int playerNumber, double seconds, float speed)
        {
            TextureObj seedObj = createSeedObject();
            Main.activeObjects.Add(seedObj);
            Main.automaticMovement.Add(new AutoMoveSeed(playerNumber, seedObj, seconds, speed, textureObj.getAngle()));
        }

        public TextureObj createSeedObject()
        {
            Texture2D seedImage = Main.imageList.Find(name => name.Name == "Seed.png");
            return new TextureObj(seedImage, textureObj.getLocation(), new Rectangle(0, 0, seedImage.Width, seedImage.Height), Color.White, 0, new Vector2(seedImage.Width / 2, seedImage.Height / 2), 1.0f, SpriteEffects.None, 1, "Seed");
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
