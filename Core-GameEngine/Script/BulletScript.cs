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
        public Vector2 direction = new(); //direction som bullet rör sig
        float moveSpeed = 1000; //Hastigheten av bullet
        float timeToDestroy = 3; //tiden innan bullet förstörs

        Physics physics; //en referens till entityns physics
        public override void Start()
        {
            physics = entity.GetComponent<Physics>(); //leta upp physics
            physics.AddForce(direction * moveSpeed, Physics.ForceMode.impulse); //ge bullet en kraft
        }
        public override void Update()
        {
            timeToDestroy -= Time.deltaTime; //räkna ner varje frame
            if (timeToDestroy <= 0) //om  noll förstör
            {
                Manager.Destroy(entity);
            }
        }
        public override void OnTrigger(Collider other)
        {
            if (other.tag == Collider.Tag.enemy) //kollar om det den kollidera med var en enemy
            {

                if (other.entity.HasComponent<EnemyScript>())//kollar om enemy först har enemyscript
                {
                    EnemyScript enemy = other.entity.GetComponent<EnemyScript>(); //letar upp enemys enemyscript
                    enemy.Hurt(1); //kalla metoden Hurt på enemy alltså skada enemy med 1
                }
                Manager.Destroy(entity); //förstör entity
            }
        }
    }
}