using System.Numerics;
using Raylib_cs;
using Core;
using Scripts;
using Utils;

namespace Entities
{
    public class Bullet : Entity
    {
        public Bullet()
        {
            //Set name
            name = "Bullet";
            //Components
            Physics physics = new();
            Collider collider = new();
            Sprite sprite = new();
            //Sprite Settings
            sprite.spriteSheet = Raylib.LoadTexture(@"Images\8x8sprites.png");
            sprite.spriteCutter = new Rectangle(24, 0, 8, 8);
            sprite.spriteRectangle = new Rectangle(0, 0, 50, 50);
            sprite.color = new Color(255, 255, 255, 200);
            //Collider Settings
            collider.SetCollider(0, 0, 25, 25);
            collider.tag = Collider.Tag.bullet;
            //Physics Settings
            physics.dragForce = 0;
            //Add all components
            components.Add(physics);
            components.Add(collider);
            components.Add(sprite);
        }
    }
}