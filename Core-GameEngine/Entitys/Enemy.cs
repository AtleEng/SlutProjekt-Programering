using System.Numerics;
using Raylib_cs;
using Core;
using Scripts;
using Utils;

namespace Entities
{
    public class Enemy : Entity
    {
        public Enemy()
        {
            //Name of entity
            name = "Enemy";
            //Components
            Physics physics = new();
            Collider collider = new();
            Sprite sprite = new();

            //Sprite settings
            sprite.spriteSheet = Raylib.LoadTexture(@"Images\16x16sprites.png");
            sprite.spriteCutter = new Rectangle(0, 0, 16, 16);
            sprite.spriteRectangle = new Rectangle(0, 0, 100, 100);
            //Collider Settings
            collider.SetCollider(0, 0, 25, 25);
            collider.tag = Collider.Tag.enemy;
            //Physics settings
            physics.dragForce = 0.1f;
            //Add all components
            components.Add(physics);
            components.Add(collider);
            components.Add(sprite);
            components.Add(new EnemyScript());
        }
    }
}