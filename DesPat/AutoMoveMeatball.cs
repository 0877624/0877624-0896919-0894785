using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    public class AutoMoveMeatball
    {
        TextureObj obj;
        float speed;
        public float toAddX;
        public float toAddY;

        public AutoMoveMeatball(TextureObj obj, float speed, float angle)
        {
            this.obj = obj;
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
        public float getSpeed()
        {
            return speed;
        }
    }
}
