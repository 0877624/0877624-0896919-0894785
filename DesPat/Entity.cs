using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    public class Entity
    {
        protected TextureObj textureObj;
        protected Input entityInput;

        protected float movementSpeed;
        protected float rotateSpeed;

        protected void checkInput()
        {
            entityInput.update();
            executeMovement(entityInput.up, entityInput.left, entityInput.down, entityInput.right);
        }
        private void executeMovement(bool up, bool left, bool down, bool right)
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

            textureObj.changeAngle(angle);
            textureObj.addToLocation(movementDelta);

            if (textureObj.getLocation().X <= textureObj.getTexture().Width / 2)
                textureObj.changeLocation(textureObj.getTexture().Width / 2, textureObj.getLocation().Y);
            if (textureObj.getLocation().X + textureObj.getTexture().Width / 2 >= Main.screenWidth)
                textureObj.changeLocation(Main.screenWidth - textureObj.getTexture().Width / 2, textureObj.getLocation().Y);

            if (textureObj.getLocation().Y <= textureObj.getTexture().Height / 2)
                textureObj.changeLocation(textureObj.getLocation().X, textureObj.getTexture().Height / 2);
            if (textureObj.getLocation().Y + textureObj.getTexture().Height / 2 >= Main.screenHeight)
                textureObj.changeLocation(textureObj.getLocation().X, Main.screenHeight - textureObj.getTexture().Height / 2);
        }
    }
}
