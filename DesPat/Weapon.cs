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
            this.projectileImageString = "Projectile-banana.png";
            this.projectileType = "Banana slice";
            this.seconds = 5.0f;
            this.speed = 5f;
        }
    }
    public class strawberryShot : fruitShot
    {
        public strawberryShot(Player player) : base(player)
        {
            this.projectileImageString = "Projectile-strawberry.png";
            this.projectileType = "Strawberry slice";
            this.seconds = 5.0f;
            this.speed = 5f;
        }
    }
    public class pearShot : fruitShot
    {
        public pearShot(Player player) : base(player)
        {
            this.projectileImageString = "Projectile-pear.png";
            this.projectileType = "Pear slice";
            this.seconds = 5.0f;
            this.speed = 5f;
        }
    }
    public class grapeShot : fruitShot
    {
        public grapeShot(Player player) : base(player)
        {
            this.projectileImageString = "Projectile-grape.png";
            this.projectileType = "Grape slice";
            this.seconds = 5.0f;
            this.speed = 5f;
        }
    }
}
