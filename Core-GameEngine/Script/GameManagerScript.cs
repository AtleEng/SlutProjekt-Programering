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
        float spawnTime = 5f;
        float _spawnTime;

        Vector2[] spawnPositions = new Vector2[8]
        {
            new Vector2(-100, -100), new Vector2(-100, 1000), new Vector2(1300, -100), new Vector2(1300, 1000),
            new Vector2(-100, 450), new Vector2(600, -100), new Vector2(1300, 450), new Vector2(600, 1000)
        };

        // Skapar en slumpgenerator
        Random generator = new Random();
        public override void Start()
        {
            entity.name = "GameManager";

            Manager.Spawn(player, Vector2.Zero);

            _spawnTime = spawnTime;
        }
        public override void Update()
        {
            _spawnTime -= Time.deltaTime;
            if (_spawnTime <= 0)
            {
                SpawnEnemy();
            }
        }
        void SpawnEnemy()
        {
            Enemy enemy = new Enemy();
            EnemyScript eS = enemy.GetComponent<EnemyScript>();
            eS.player = player;

            Manager.Spawn(enemy, spawnPositions[generator.Next(0, spawnPositions.Count())]);
            _spawnTime = spawnTime;
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