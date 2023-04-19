using System.Numerics;
using Raylib_cs;
using Core;
using Utils;
using Scripts;
using Entities;

namespace Scripts
{
    public class BulletScript : Component
    {
        public Vector2 direction = new();
        float moveSpeed = 500;
        float timeToDestroy = 300;

        Physics physics;
        public override void Start()
        {
            physics = entity.GetComponent<Physics>();
            physics.AddForce(direction * moveSpeed, Physics.ForceMode.impulse);
        }
        public override void Update()
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0)
            {
                Manager.Destroy(entity);
            }
        }
    }
}