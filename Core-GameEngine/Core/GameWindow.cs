
using System.Numerics;
using Raylib_cs;
using Core;
using Utils;

namespace Core
{
    public static class GameWindow
    {
        public static bool isEditMode = false; //editMode är för debugging, använd f3 (precis som i Minecraft) för att byta mellan på och av

        //bredd och höjd på fönstret
        static int windowWidth = 800;
        static int windowHeight = 450;

        //storleken för spelfönstret
        public static int gameScreenWidth = 1200;
        public static int gameScreenHeight = 900;

        public static float scale;

        //bool som kollar om spelet ska stängas ner
        static bool shouldClose;

        //Skapa en Render texture2D, används för att resiza spelfönstret
        static RenderTexture2D target = new RenderTexture2D();

        //början av programet
        public static void ProgramStart()
        {
            //Fönster inställningar
            //------------------------------------------------
            //gör det möjligt att ändra storleken på fönstret
            Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            //skapa fönstret
            Raylib.InitWindow(windowWidth, windowHeight, "Game Window");
            //sätt en minsta gräns för fönstret
            Raylib.SetWindowMinSize(400, 300);
            //sätt en viss FPS (Bildrutor per sekund)
            Raylib.SetTargetFPS(200);
            //tar bort alternativet att klicka esc för att gå ut ur spelet
            Raylib.SetExitKey(KeyboardKey.KEY_NULL);
            //------------------------------------------------

            //Skapar en texture som används till att rita ut spelgrafiken
            target = Raylib.LoadRenderTexture(gameScreenWidth, gameScreenHeight);

            Manager.OnStart();

            // Main game loop
            while (!Raylib.WindowShouldClose())
            {
                //Update
                //------------------------------------------------
                //Update för utils time
                Utils.Time.Update();
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F3))
                {
                    isEditMode = !isEditMode;
                }
                //Update för alla entitys
                Manager.Update();

                //räkna ut en storlek att använda sig av när man ritar ut spelet
                scale = Math.Min((float)Raylib.GetScreenWidth() / gameScreenWidth, (float)Raylib.GetScreenHeight() / gameScreenHeight);
                //------------------------------------------------

                // Draw
                //------------------------------------------------
                // Rita allt i en texture och rita sedan in den i Drawing
                Raylib.BeginTextureMode(target);
                //rensa texture bakgrund
                Raylib.ClearBackground(new Color(41, 189, 193, 255));
                //rita in resten
                Manager.Render();

                Raylib.EndTextureMode();


                Raylib.BeginDrawing();
                //Rensa skärmen bakgrund
                Raylib.ClearBackground(Color.BLACK);
                // Rita in texturen med rätt skala
                Raylib.DrawTexturePro(target.texture,
                new Rectangle(0.0f, 0.0f, (float)target.texture.width, (float)-target.texture.height),
                new Rectangle((Raylib.GetScreenWidth() - ((float)gameScreenWidth * scale)) * 0.5f,
                (Raylib.GetScreenHeight() - ((float)gameScreenHeight * scale)) * 0.5f,
                (float)gameScreenWidth * scale, (float)gameScreenHeight * scale),
                 new Vector2(0, 0), 0.0f, Color.WHITE);
                //avsluta drawmode
                Raylib.EndDrawing();
                //------------------------------------------------

                if (shouldClose) // detta görs sist eftersom de unviker att förstöra de andra proceserna
                {
                    //tar bort render texture
                    Raylib.UnloadRenderTexture(target);
                    //stänger fönstret
                    Raylib.CloseWindow();
                }
            }
        }
        // När programet stängs
        public static void ExitGame()
        {
            shouldClose = true;
        }
    }

}