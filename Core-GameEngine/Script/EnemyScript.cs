using System.Numerics;
using Raylib_cs;
using Core;
using Utils;
using Scripts;
using Entities;

namespace Scripts
{
    public class EnemyScript : Component
    {
        int health = 10; //hälsa av enemy
        float moveSpeed = 500; //hur snabb enemy är
        Vector2 walkDir = new(); //vilken riktning
        //Components
        Physics physics;
        Sprite sprite;

        public Entity player; // en referens till player som används för att jaga den
        public override void Start()
        {
            //Hitta components
            physics = entity.GetComponent<Physics>();
            sprite = entity.GetComponent<Sprite>();
        }
        public override void Update()
        {
            walkDir = player.position - entity.position; //hittar den riktning player är
            walkDir = Vector2.Normalize(walkDir); //sätter längden till 1

            Movement(); //hanterar rörelse
        }
        void Movement()
        {
            physics.AddForce(walkDir * moveSpeed, Physics.ForceMode.constant); //ger en kraft till enemy riktad mot player

            if (physics.velocity.X < 0)//om hastigheten är mindre en noll flippa spriten
            {
                sprite.isFlipedX = true;
            }
            else if (physics.velocity.X > 0)//om hastigheten är större en noll flippa spriten
            {
                sprite.isFlipedX = false;
            }
        }

        public void Hurt(int dmg) //denna metod skadar enemy och tar bort den helt om health <= 0
        {
            health -= dmg;
            if (health <= 0)
            {
                Manager.Destroy(entity);
            }
        }
    }
}