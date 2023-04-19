using System.Numerics;
using Raylib_cs;
using Core;
using Scripts;
using Utils;

namespace Entities
{
    public class Bullet : Entity
    {
        Physics physics = new();
        Collider collider = new();
        Sprite sprite = new();

        public override void Build()
        {
            name = "Bullet";

            sprite.spriteSheet = Raylib.LoadTexture(@"Images\8x8sprites.png");
            sprite.spriteCutter = new Rectangle(0, 8, 8, 8);
            sprite.spriteRectangle = new Rectangle(0, 0, 50, 50);
            sprite.color = new Color(255, 255, 255, 200);

            collider.SetCollider(0, 0, 25, 25);

            physics.dragForce = 0;

            components.Add(physics);
            components.Add(collider);
            components.Add(sprite);
        }
    }
}