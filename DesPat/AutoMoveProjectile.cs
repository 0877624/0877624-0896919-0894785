using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    public class AutoMoveProjectile
    {
        int playerNumber;
        TextureObj obj;
        float timeLeft;
        float speed;
        public float toAddX;
        public float toAddY;

        public AutoMoveProjectile(int playerNumber, TextureObj obj, float seconds, float speed, float angle)
        {
            this.playerNumber = playerNumber;
            this.obj = obj;
            timeLeft = seconds;
            this.speed = speed;
            calculateToAdd(angle);
        }
        public void calculateToAdd(float angle)
        {
            if (angle >= 0 && angle < 90)
            {
                toAddX += speed * ((angle) / 45);
                toAddY -= speed * ((2f - (angle) / 45));
            }
            else if (angle >= 90 && angle < 180)
            {
                toAddX += speed * ((2f - ((90 - (180 - angle)) / 45)));
                toAddY += speed * ((90 - (180 - angle)) / 45);
            }
            else if (angle >= 180 && angle < 270)
            {
                toAddX -= speed * ((90 - (270 - angle)) / 45);
                toAddY += speed * (2f - ((90 - (270 - angle)) / 45));
            }
            else if (angle >= 270 && angle < 360)
            {
                toAddX -= speed * ((360 - angle) / 45);
                toAddY -= speed * (2f - ((360 - angle) / 45));
            }
        }
        public TextureObj getObject()
        {
            return obj;
        }
        public float getTimeLeft()
        {
            return timeLeft;
        }
        public float getSpeed()
        {
            return speed;
        }
        public void setTimeLeft(float seconds)
        {
            timeLeft = seconds;
        }
        public int getPlayerNumber()
        {
            return playerNumber;
        }
    }
}
