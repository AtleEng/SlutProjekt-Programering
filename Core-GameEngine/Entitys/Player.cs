using System.Numerics;
using Raylib_cs;
using Core;
using Scripts;

namespace Entities
{
    public class Player : Entity
    {
        public Player()
        {
            //Set name of entity
            name = "Player";
            //Components
            Sprite sprite = new();
            Collider collider = new();
            Physics physics = new();
            //Sprite Settings
            sprite.spriteSheet = Raylib.LoadTexture(@"Images\16x16sprites.png");
            sprite.spriteCutter = new Rectangle(16, 32, 16, 16);
            //Collisder Settings
            collider.SetCollider(0, 10, 40, 40);
            collider.tag = Collider.Tag.player;
            //Physics Settings
            physics.dragForce = 0.1f;
            //Add components
            components.Add(new PlayerScript());
            components.Add(physics);
            components.Add(collider);
            components.Add(sprite);
        }
    }
}