using System.Numerics;
using Raylib_cs;
using Core;
using Utils;

namespace Core
{
    //this class handles the buttons in the game
    public class Button : Component
    {
        //storlek och position av knappen
        public Rectangle buttonRect = new Rectangle(100, 100, 100, 100);
        //är true om virtuella musen är över knappen
        public bool isHovering = false;
        //är true om knappen blir klickad på
        public bool isKlicked = false;
        public override void Update()
        {
            //kollar om virtuella musen och knappen överlappar
            if (Raylib.CheckCollisionPointRec(WorldSpace.GetVirtualMousePos(), buttonRect))
            {
                //kollar om knappen blir klickad på (om stämmer isKlicked,om inte isHover)
                if (!isKlicked)
                {
                    isHovering = true;
                }
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                {
                    isKlicked = true;
                    isHovering = false;
                }
            }
            else
            {
                isHovering = false;
            }
        }
    }
}