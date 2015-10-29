using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DesPat
{
    public class TextureObj
    {
        private int playerNumber;
        Texture2D texture;
        Vector2 location;
        Rectangle sourceRectangle;
        Color color;
        float angle;
        Vector2 origin;
        float scale;
        SpriteEffects effects;
        float layerDepth;
        string type;

        float angleRad = (2 * (float)System.Math.PI) / 360;

        Vector2 hitboxTL;
        Vector2 hitboxTR;
        Vector2 hitboxBL;
        Vector2 hitboxBR;

        public TextureObj(Texture2D texture, Vector2 location, Rectangle sourceRectangle, Color color, float angle, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, String type)
        {
            playerNumber = 0;
            this.texture = texture;
            this.location = location;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.angle = correctAngle(angle);
            this.origin = origin;
            this.scale = scale;
            this.effects = effects;
            this.layerDepth = layerDepth;
            this.type = type;
            hitboxTL = new Vector2(location.X - (texture.Width / 2) * scale, location.Y - (texture.Height / 2) * scale);
            hitboxTR = new Vector2(location.X + (texture.Width / 2) * scale, location.Y - (texture.Height / 2) * scale);
            hitboxBL = new Vector2(location.X - (texture.Width / 2) * scale, location.Y + (texture.Height / 2) * scale);
            hitboxBR = new Vector2(location.X + (texture.Width / 2) * scale, location.Y + (texture.Height / 2) * scale);

        }

        public TextureObj(int playerNumber, Texture2D texture, Vector2 location, Rectangle sourceRectangle, Color color, float angle, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, String type)
        {
            this.playerNumber = playerNumber;
            this.texture = texture;
            this.location = location;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.angle = correctAngle(angle);
            this.origin = origin;
            this.scale = scale;
            this.effects = effects;
            this.layerDepth = layerDepth;
            this.type = type;
            hitboxTL = new Vector2(location.X - (texture.Width / 2) * scale, location.Y - (texture.Height / 2) * scale);
            hitboxTR = new Vector2(location.X + (texture.Width / 2) * scale, location.Y - (texture.Height / 2) * scale);
            hitboxBL = new Vector2(location.X - (texture.Width / 2) * scale, location.Y + (texture.Height / 2) * scale);
            hitboxBR = new Vector2(location.X + (texture.Width / 2) * scale, location.Y + (texture.Height / 2) * scale);

        }

        public void drawObj(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, angle * angleRad, origin, scale, effects, layerDepth);
        }

        private float correctAngle(float angle)
        {
            float newAngle;
            if(angle < 0)
            {
                int angleTimes = (int)(angle / 360);
                newAngle = 360 - Math.Abs((angle + 360 * angleTimes));
            }
            else if(angle > 360)
            {
                int angleTimes = (int)(angle / 360);
                newAngle = angle - 360 * angleTimes;
            }
            else
            {
                return angle;
            }

            return newAngle;
        }

        public void addToLocation(Vector2 moveAmount)
        {
            location.X += moveAmount.X;
            location.Y += moveAmount.Y;
            hitboxTL.X += moveAmount.X;
            hitboxTL.Y += moveAmount.Y;
            hitboxTR.X += moveAmount.X;
            hitboxTR.Y += moveAmount.Y;
            hitboxBL.X += moveAmount.X;
            hitboxBL.Y += moveAmount.Y;
            hitboxBR.X += moveAmount.X;
            hitboxBR.Y += moveAmount.Y;

        }
        public void addToLocation(float x, float y)
        {
            location.X += x;
            location.Y += y;
            hitboxTL.X += x;
            hitboxTL.Y += y;
            hitboxTR.X += x;
            hitboxTR.Y += y;
            hitboxBL.X += x;
            hitboxBL.Y += y;
            hitboxBR.X += x;
            hitboxBR.Y += y;
        }
        public void changeLocation(float x, float y)
        {
            location.X = x;
            location.Y = y;
            hitboxTL.X = x - (texture.Width / 2) * scale;
            hitboxTL.Y = y - (texture.Height / 2) * scale;
            hitboxTR.X = x + (texture.Width / 2) * scale;
            hitboxTR.Y = y - (texture.Height / 2) * scale;
            hitboxBL.X = x - (texture.Width / 2) * scale;
            hitboxBL.Y = y + (texture.Height / 2) * scale;
            hitboxBR.X = x + (texture.Width / 2) * scale;
            hitboxBR.Y = y + (texture.Height / 2) * scale;
        }
        public Vector2 getLocation()
        {
            return location;
        }
        public void addToAngle(float newAngle)
        {
            angle += newAngle;
            angle = correctAngle(angle);
        }
        public void changeAngle(float newAngle)
        {
            angle = correctAngle(newAngle);
        }
        public float getAngle()
        {
            return angle;
        }
        public bool checkCollision(TextureObj obj)
        {
            if (this.hitboxBL.X >= obj.hitboxBL.X && this.hitboxBL.X <= obj.hitboxBR.X)
            {
                if (this.hitboxBL.Y <= obj.hitboxBL.Y && this.hitboxBL.Y >= obj.hitboxTL.Y)
                {
                    return true;
                }
                else if (this.hitboxTL.Y <= obj.hitboxBL.Y && this.hitboxTL.Y >= obj.hitboxTL.Y)
                {
                    return true;
                }
            }
            else if (this.hitboxBR.X >= obj.hitboxBL.X && this.hitboxBR.X <= obj.hitboxBR.X)
            {
                if (this.hitboxBL.Y <= obj.hitboxBL.Y && this.hitboxBL.Y >= obj.hitboxTL.Y)
                {
                    return true;
                }
                else if (this.hitboxTL.Y <= obj.hitboxBL.Y && this.hitboxTL.Y >= obj.hitboxTL.Y)
                {
                    return true;
                }
            }
            return false;
        }
        public int getPlayerNumber()
        {
            return playerNumber;
        }
        public string getType()
        {
            return type;
        }
        public Texture2D getTexture()
        {
            return texture;
        }
        public float getScale()
        {
            return scale;
        }
             

    }
}
