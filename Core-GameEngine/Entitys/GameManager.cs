using System.Numerics;
using Raylib_cs;
using Core;
using Scripts;

namespace Entities
{
    public class GameManager : Entity
    {
        public GameManager()
        {
            //set name
            name = "GameManager";
            //Add components
            components.Add(new GameManagerScript());
        }
    }
}