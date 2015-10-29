using DesPat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    public interface Weapon
    {
        void shoot();
        void shootProjectile();
        TextureObj createProjectile();
    }

    public abstract class fruitShot : Weapon
    {
        Player player;
        protected string projectileImageString;
        protected string projectileType;
        protected float seconds;
        protected float speed;

        public fruitShot(Player player)
        {
            this.player = player;
        }
        public void shoot()
        {
            shootProjectile();
        }
        public void shootProjectile()
        {
            TextureObj projectileObj = createProjectile();
            Main.addAsActive(projectileObj);
            Main.addAsAutomatic(new AutoMoveProjectile(player.getPlayerNumber(), projectileObj, seconds, speed, player.getTextureObj().getAngle()));
        }
        public TextureObj createProjectile()
        {
            Texture2D projectileImage = Main.imageList.Find(name => name.Name == projectileImageString);
            return new TextureObj(projectileImage, player.getTextureObj().getLocation(), new Rectangle(0, 0, projectileImage.Width, projectileImage.Height), Color.White, 0, new Vector2(projectileImage.Width / 2, projectileImage.Height / 2), 1.0f, SpriteEffects.None, 1, projectileType);
        }

    }

    //The four different kind of projectiles.
    public class bananaShot : fruitShot
    {
        public bananaShot(Player player) : base(player)
        { 
            projectileImageString = "Projectile-banana.png";
            projectileType = "Banana slice";
            seconds = 5.0f;
            speed = 5f;
        }
    }
    public class strawberryShot : fruitShot
    {
        public strawberryShot(Player player) : base(player)
        {
            projectileImageString = "Projectile-strawberry.png";
            projectileType = "Strawberry slice";
            seconds = 5.0f;
            speed = 5f;
        }
    }
    public class pearShot : fruitShot
    {
        public pearShot(Player player) : base(player)
        {
            projectileImageString = "Projectile-pear.png";
            projectileType = "Pear slice";
            seconds = 5.0f;
            speed = 5f;
        }
    }
    public class grapeShot : fruitShot
    {
        public grapeShot(Player player) : base(player)
        {
            projectileImageString = "Projectile-grape.png";
            projectileType = "Grape slice";
            seconds = 5.0f;
            speed = 5f;
        }
    }
}
