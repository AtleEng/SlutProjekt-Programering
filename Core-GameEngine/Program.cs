using Core;//för att använda min "game engine" behöver man använda Core;

//Hej Mike detta här är mitt slutprojekt. De delar som jag vill visa är i mapparna Core och Components.
//De andra koden i mapparna Entitys och Script är inte färdig och så bra gjorda utan används mer som en "demo" för att kunna visa hur mitt system funkar

//Detta program bygger på ECS --> Entity component system

//Förbättringar som kan göras av mitt ECS är:
//Collisionsystemet, just nu testas alla entitys om de kolliderar med varandra om man delar upp collistionerna i olika zoner kan man minska prestationen
//använda structs till simplare komponenter, har dock inte hunnit implemetera det än
//använda arrays istället för listor
//ersätta virtual void med delegates

//startar gameWindow
GameWindow.ProgramStart();