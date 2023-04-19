using System.Numerics;
using Raylib_cs;
using Core;

namespace Core
{
    //this class handles the sprites in the game
    public class Sprite : Component
    {
        //flippa sprite via X eller Y axeln
        public bool isFlipedY;
        public bool isFlipedX;

        public bool isSpriteInView = false;
        //färgton på spriten (Vitfärg gör att sprite ser ut som innan)
        public Color color = Color.WHITE;
        //var spriten ritas ut i
        public Rectangle spriteRectangle = new Rectangle(0, 0, 100, 100);
        //position utöver entity position, används för att byta plats på spriten relativt till entity
        public Vector2 localPosition;
        //Vilken texture som används
        public Texture2D spriteSheet;
        //Vilken del av texturen används
        public Rectangle spriteCutter = new();
        public override void Start()
        {
            Manager.spritesInScene.Add(this);
            //om spriteCutter har area 0 då använd hela spriteSheet
            if (spriteCutter.width <= 0 || spriteCutter.height <= 0)
            {
                spriteCutter = new Rectangle(0, 0, spriteSheet.width, spriteSheet.height);
            }
        }
        public override void Update()
        {
            //flytta med entity position
            spriteRectangle.x = entity.position.X - spriteRectangle.width / 2 + localPosition.X;
            spriteRectangle.y = entity.position.Y - spriteRectangle.height / 2 + localPosition.Y;

            if (spriteRectangle.x > GameWindow.gameScreenWidth || spriteRectangle.y > GameWindow.gameScreenHeight
            || spriteRectangle.x + spriteRectangle.width < 0 || spriteRectangle.y + spriteRectangle.height < 0)
            {
                isSpriteInView = false;
            }
            else
            {
                isSpriteInView = true;
            }
        }
        public override void EditMode()
        {
            Color debugColor = new Color(55, 55, 255, 50); //en lätt genomskinlig blå
            Raylib.DrawRectangleRec(spriteRectangle, debugColor); //ritar ut en rektangel där spriten ska vara
        }
        public void Render()
        {
            if (spriteSheet.id != 0) //kollar om spriteSheet har någon texture2D
            {
                //här flippas spriten om det är de valda inställningarna
                int flipX = isFlipedX ? -1 : 1;
                int flipY = isFlipedY ? -1 : 1;

                //här ritas texturen ut
                Raylib.DrawTexturePro(spriteSheet, new Rectangle(spriteCutter.x, spriteCutter.y, spriteCutter.width * flipX, spriteCutter.height * flipY), spriteRectangle, Vector2.Zero, 0, color);
            }
            else
            {
                System.Console.WriteLine($"{entity.name}s spriteRender doesnt have a texture");
            }
        }
        public override void OnDestroy()
        {
            Manager.spritesInScene.Remove(this);
        }
    }
}