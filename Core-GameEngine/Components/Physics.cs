using System.Numerics;
using Raylib_cs;
using Core;
using Utils;

namespace Core
{
    //this class handles the Physics in the game
    public class Physics : Component
    {
        public Vector2 velocity = Vector2.Zero; //Hastigheten på entity
        public Vector2 acceleration = Vector2.Zero; //accelerationen på entity
        public float mass = 1; //massan - Påverkar hur mycket kraft det behövs för att flytta entity
        public float dragForce = 0f; // hur snabbt entity tappar velocity - Värden mellan 0 och 1 för realistisk fysik
        public override void Update()
        {
            velocity += acceleration * Time.deltaTime;    //ändrar velocity beroende på accelerationen

            velocity *= 1 - dragForce;     //saktar ner objektet beroende på drag: 0 = ingenting 1 = stannar direkt

            entity.position += velocity * Time.deltaTime; //uppdaterar positionen beroende på velocity

            acceleration = Vector2.Zero; //resetar accelerationen
        }

        public void AddForce(Vector2 force, ForceMode fM)
        {
            if (fM == ForceMode.constant)
            {
                acceleration += force / mass; //följer fysikens F = a * m
            }
            else if (fM == ForceMode.impulse)
            {
                velocity += force / mass; //följer fysikens F = a * m
            }
        }

        public override void EditMode()
        {
            if (GameWindow.isEditMode) //ritar ut en linje som visar hastigheten
            {
                Color color = new Color(255, 55, 55, 255);
                Raylib.DrawLine((int)entity.position.X, (int)entity.position.Y, (int)(entity.position.X + velocity.X), (int)(entity.position.Y + velocity.Y), color);
            }
        }

        public enum ForceMode
        {
            constant, impulse
        }
    }
}