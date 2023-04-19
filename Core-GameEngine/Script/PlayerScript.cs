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
        float moveSpeed = 1000; //hastigheten på player
        //components
        Physics physics;
        Sprite sprite;
        public override void Start()
        {
            //Hitta components
            physics = entity.GetComponent<Physics>();
            sprite = entity.GetComponent<Sprite>();
        }
        public override void Update()
        {
            Movement(); //hanterar rörelse

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))//om klicka vänster klick skjut en bullet i riktning mot musen
            {
                Vector2 aimDir = WorldSpace.GetVirtualMousePos() - entity.position; //få riktningen från player till musen
                aimDir = Vector2.Normalize(aimDir);//gör länden till 1

                Manager.Spawn(CreateBullet(aimDir), entity.position);//skapa en bullet
            }
        }
        void Movement()
        {
            Vector2 moveInput = new();
            //kollar inputen och ändrar då riktningen på kraften
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                moveInput.X += 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
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
            if (moveInput.Length() > 0)//unvika en error (dela med 0)
            {
                moveInput = Vector2.Normalize(moveInput);//sätt längden till 1
            }
            physics.AddForce(moveInput * moveSpeed, Physics.ForceMode.constant);//ge kraft till player

            //flippa spriten beroende på riktning
            if (physics.velocity.X < 0)
            {
                sprite.isFlipedX = true;
            }
            else if (physics.velocity.X > 0)
            {
                sprite.isFlipedX = false;
            }
        }
        //skapa en bullet
        Entity CreateBullet(Vector2 aimDir)
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