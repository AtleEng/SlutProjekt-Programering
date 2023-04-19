using System.Numerics;
using Raylib_cs;
using Core;
using Utils;
using Scripts;
using Entities;

namespace Scripts
{
    public class GameManagerScript : Component
    {
        Player player = new Player();
        public override void Start()
        {
            entity.name = "GameManager";

            Manager.Spawn(player, Vector2.Zero);
        }
        public override void EditMode()
        {
            int fps = Raylib.GetFPS();
            Raylib.DrawText($"FPS: {fps}", 10, 25, 20, Color.BLACK);
            Raylib.DrawText($"Amount of Entities: {Manager.entitiesInScene.Count}", 10, 45, 20, Color.BLACK);
            Raylib.DrawText($"Amount of Colliders: {Manager.collidersInScene.Count}", 10, 65, 20, Color.BLACK);
            Raylib.DrawText($"Amount of Sprites: {Manager.spritesInScene.Count}", 10, 85, 20, Color.BLACK);
        }
    }
}