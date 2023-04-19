using System.Numerics;
using Raylib_cs;
using Core;
using Scripts;

namespace Entities
{
    public class Player : Entity
    {
        PlayerScript playerScript = new();
        Sprite sprite = new();
        Collider collider = new();
        Physics physics = new();
        public override void Build()
        {
            name = "Player";

            sprite.spriteSheet = Raylib.LoadTexture(@"Images\16x16sprites.png");
            sprite.spriteCutter = new Rectangle(16, 32, 16, 16);

            collider.SetCollider(0, 10, 40, 40);

            components.Add(physics);
            components.Add(playerScript);
            components.Add(collider);
            components.Add(sprite);
        }
    }
}