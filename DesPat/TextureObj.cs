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
        private Texture2D projectileImage;
        private Func<Vector2> getLocation1;
        private Rectangle rectangle;
        private Color white;
        private int v1;
        private Vector2 vector2;
        private float v2;
        private SpriteEffects none;
        private int v3;
        private string v4;

        public TextureObj(Texture2D texture, Vector2 location, Rectangle sourceRectangle, Color color, float angle, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, String type)
        {
            playerNumber = 0;
            this.texture = texture;
            this.location = location;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.angle = angle;
            this.origin = origin;
            this.scale = scale;
            this.effects = effects;
            this.layerDepth = layerDepth;
            this.type = type;
            hitboxTL = new Vector2(location.X - texture.Width / 2, location.Y - texture.Height / 2);
            hitboxTR = new Vector2(location.X + texture.Width / 2, location.Y - texture.Height / 2);
            hitboxBL = new Vector2(location.X - texture.Width / 2, location.Y + texture.Height / 2);
            hitboxBR = new Vector2(location.X + texture.Width / 2, location.Y + texture.Height / 2);

            //System.Diagnostics.Debug.WriteLine("X: " + hitboxTL.X);
            //System.Diagnostics.Debug.WriteLine("Y: " + hitboxTL.Y);
        }
        public TextureObj(int playerNumber, Texture2D texture, Vector2 location, Rectangle sourceRectangle, Color color, float angle, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, String type)
        {
            this.playerNumber = playerNumber;
            this.texture = texture;
            this.location = location;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.angle = angle;
            this.origin = origin;
            this.scale = scale;
            this.effects = effects;
            this.layerDepth = layerDepth;
            this.type = type;
            hitboxTL = new Vector2(location.X - texture.Width / 2, location.Y - texture.Height / 2);
            hitboxTR = new Vector2(location.X + texture.Width / 2, location.Y - texture.Height / 2);
            hitboxBL = new Vector2(location.X - texture.Width / 2, location.Y + texture.Height / 2);
            hitboxBR = new Vector2(location.X + texture.Width / 2, location.Y + texture.Height / 2);

            //System.Diagnostics.Debug.WriteLine("X: " + hitboxTL.X);
            //System.Diagnostics.Debug.WriteLine("Y: " + hitboxTL.Y);
        }

        public TextureObj(Texture2D projectileImage, Func<Vector2> getLocation1, Rectangle rectangle, Color white, int v1, Vector2 vector2, float v2, SpriteEffects none, int v3, string v4)
        {
            this.projectileImage = projectileImage;
            this.getLocation1 = getLocation1;
            this.rectangle = rectangle;
            this.white = white;
            this.v1 = v1;
            this.vector2 = vector2;
            this.v2 = v2;
            this.none = none;
            this.v3 = v3;
            this.v4 = v4;
        }

        public void drawObj(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, angle * angleRad, origin, scale, effects, layerDepth);
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
        public bool checkCollision(TextureObj obj)
        {
            //System.Diagnostics.Debug.WriteLine("Checking for Collision");
            //System.Diagnostics.Debug.WriteLine("This BL X: " + this.hitboxBL.X + ", collision BL X: " + obj.hitboxBL.X + ", collision BR X: " + obj.hitboxBR.X);
            //System.Diagnostics.Debug.WriteLine("This BL X: " + this.hitboxBL.X + ", collision BR X: " + obj.hitboxBR.X);
            if (this.hitboxBL.X >= obj.hitboxBL.X && this.hitboxBL.X <= obj.hitboxBR.X)
            {
                //System.Diagnostics.Debug.WriteLine("BL left in image1");
                //System.Diagnostics.Debug.WriteLine("This BL Y: " + this.hitboxBL.Y + ", collision BL Y: " + obj.hitboxBL.Y + ", collision TL Y: " + obj.hitboxTL.Y);
                //System.Diagnostics.Debug.WriteLine("This BL Y: " + this.hitboxBL.Y + ", collision TL Y: " + obj.hitboxTL.Y);
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
    }
}
