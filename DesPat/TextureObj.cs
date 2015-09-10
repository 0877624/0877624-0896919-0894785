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
        Texture2D texture;
        Vector2 location;
        Rectangle sourceRectangle;
        Color color;
        float angle;
        Vector2 origin;
        float scale;
        SpriteEffects effects;
        float layerDepth;

        float angleRad = (2 * (float)System.Math.PI) / 360;

        public TextureObj(Texture2D texture, Vector2 location, Rectangle sourceRectangle, Color color, float angle, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            this.texture = texture;
            this.location = location;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.angle = angle;
            this.origin = origin;
            this.scale = scale;
            this.effects = effects;
            this.layerDepth = layerDepth;
        }

        public void drawObj(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, angle * angleRad, origin, scale, effects, layerDepth);
        }


        public void addToLocation(Vector2 moveAmount)
        {
            location.X += moveAmount.X;
            location.Y += moveAmount.Y;
        }
        public void addToLocation(float x, float y)
        {
            location.X += x;
            location.Y += y;
        }
        public void changeLocation(Vector2 location)
        {
            this.location = location;
        }
        public void changeLocation(float x, float y)
        {
            location.X = x;
            location.Y = y;
        }
        public Vector2 getLocation()
        {
            return location;
        }
        public void addToAngle(float newAngle)
        {
            angle += newAngle;
            if (angle > 359)
            {
                angle = 0;
            }
            else if (angle < 0)
            {
                angle = 359;
            }
        }
        public void changeAngle(float newAngle)
        {
            angle = newAngle;
            if (angle > 359)
            {
                angle = 0;
            }
            else if (angle < 0)
            {
                angle = 359;
            }
        }
        public float getAngle()
        {
            return angle;
        }
    }
}
