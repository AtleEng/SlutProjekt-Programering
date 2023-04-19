using System.Numerics;
using Raylib_cs;
using Core;
using Utils;
using Scripts;
using Entities;

namespace Scripts
{
    public class PlayerScript : Component
    {
        float moveSpeed = 10000;
        Vector2 aimDir = new();
        Physics physics;
        public override void Start()
        {
            physics = entity.GetComponent<Physics>();
            physics.dragForce = 0.3f;
        }
        public override void Update()
        {
            Movement();

            aimDir = WorldSpace.GetVirtualMousePos() - entity.position;
            aimDir = Vector2.Normalize(aimDir);

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                Manager.Spawn(CreateBullet(), entity.position);
            }
        }
        void Movement()
        {
            Vector2 moveInput = new();
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                //spriteComponent.isFlipedX = false;
                moveInput.X += 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                //spriteComponent.isFlipedX = true;
                moveInput.X -= 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                moveInput.Y -= 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                moveInput.Y += 1;
            }
            if (moveInput.Length() > 0)
            {
                moveInput = Vector2.Normalize(moveInput);
            }
            physics.AddForce(moveInput * moveSpeed, Physics.ForceMode.constant);
        }
        Entity CreateBullet()
        {
            Bullet bullet = new();
            bullet.name = "PlayerBullet";

            BulletScript bulletScript = new();
            bullet.components.Add(bulletScript);
            bulletScript.direction = aimDir;

            return bullet;
        }
    }
}