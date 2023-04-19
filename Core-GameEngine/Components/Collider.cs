using System.Numerics;
using Raylib_cs; // Raylib
using Core; // This namespace contains classes that are essential to my game engine

namespace Core
{
    // This class handles the Colliders in the game (It is more like trigger in unity)
    public class Collider : Component
    {
        // The rectangle used for collision detection
        Rectangle hitBox = new Rectangle(0, 0, 100, 100);

        // The local position of the collider relative to its parent entity
        Vector2 localPosition = new Vector2(0, 0);

        public override void Start()
        {
            // Add this collider to the list of colliders in the scene
            Manager.collidersInScene.Add(this);
        }

        public override void Update()
        {
            // Update the collision rectangle's position based on the parent entity's position and the local position
            hitBox.x = entity.position.X - hitBox.width / 2 + localPosition.X;
            hitBox.y = entity.position.Y - hitBox.height / 2 + localPosition.Y;

            foreach (Collider other in Manager.collidersInScene)
            {
                if (other != this)
                {
                    ResolveCollision(other);
                }
            }
        }
        public override void EditMode()
        {
            if (GameWindow.isEditMode)//draws the position of entity and hitbox
            {
                Raylib.DrawRectangleRec(hitBox, new Color(0, 255, 0, 100));

                Raylib.DrawCircle((int)entity.position.X, (int)entity.position.Y, 3, Color.RED);
            }
        }

        public override void OnDestroy()
        {
            Manager.collidersInScene.Remove(this);
        }

        void ResolveCollision(Collider other)
        {
            if (Raylib.CheckCollisionRecs(hitBox, other.hitBox))
            {
                entity.OnTrigger(other.entity);
                System.Console.WriteLine(entity.name + " collided with: " + other.entity.name);
            }
        }

        public void SetCollider(float x, float y, float width, float height)
        {
            localPosition = new Vector2(x, y);

            hitBox.width = width;
            hitBox.height = height;
        }
    }
}