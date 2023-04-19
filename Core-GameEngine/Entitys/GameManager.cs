using System.Numerics;
using Raylib_cs;
using Core;
using Scripts;

namespace Entities
{
    public class GameManager : Entity
    {
        GameManagerScript gM = new();
        public override void Build()
        {
            components.Add(gM);
        }
    }
}