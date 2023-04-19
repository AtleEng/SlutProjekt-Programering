using System.Numerics;
using Raylib_cs;
using Core;
//Lite extra fuktionalitet, för att använda: using Utils
namespace Utils
{
    public static class Time
    {
        //deltaTime variabler
        static float oldTime = 0;
        static float newTime = 0;
        //detta är deltaTime variabeln man kan ändas få (get) värdet eftersom den har private set
        public static float deltaTime { get; private set; }

        public static void Update()
        {
            //räkna ut delta time
            //------------------------------------------------
            //sätter oldTime till förra framens tid
            oldTime = newTime;
            //sätter newTime till denna frames tid
            newTime = (float)Raylib.GetTime();
            //subrahera newTime med oldTime för att få skillnaden i tid mellan framen
            deltaTime = newTime - oldTime;
            //------------------------------------------------
        }
    }

    public static class WorldSpace
    {
        public static Vector2 GetVirtualMousePos() //den virtuella musen är kopplad till spelfönstret
        {
            //virtuell mus position
            Vector2 virtualMouse = new();

            // Uppdatera virtuella musen (låst till spelfönstret)
            //------------------------------------------------
            float scale = GameWindow.scale;
            //Sätter en position till musens position
            Vector2 mouse = Raylib.GetMousePosition();
            //Sätter virtuella musen anpassad till spelfönstret
            virtualMouse.X = (mouse.X - (Raylib.GetScreenWidth() - (GameWindow.gameScreenWidth * scale)) * 0.5f) / scale;
            virtualMouse.Y = (mouse.Y - (Raylib.GetScreenHeight() - (GameWindow.gameScreenHeight * scale)) * 0.5f) / scale;
            //lås (clamp) virtualMouse till spelfönstret
            virtualMouse = new Vector2(Math.Max(Math.Min(virtualMouse.X, GameWindow.gameScreenWidth), 0f), Math.Max(Math.Min(virtualMouse.Y, GameWindow.gameScreenHeight), 0f));
            //returnerar position
            return virtualMouse;
        }
    }
}