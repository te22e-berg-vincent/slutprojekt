// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Security.Cryptography;
using System.Timers;
using System.Xml.Schema;
using Raylib_cs;



static void startScreen()
{
        Raylib.ClearBackground(Color.White);
        Raylib.DrawRectangle(0, 0, 800, 700, Color.Gray);
        Raylib.DrawRectangle(100, 300, 50, 50, Color.Gold);
        Raylib.DrawRectangle(220, 300, 50, 50, Color.SkyBlue);
        Raylib.DrawRectangle(375, 300, 50, 50, Color.Maroon);
        Raylib.DrawRectangle(530, 300, 50, 50, Color.DarkPurple);
        Raylib.DrawRectangle(650, 300, 50, 50, Color.Purple);
        Raylib.DrawText("Key", 103, 355, 25, Color.Black);
        Raylib.DrawText("+5 sek", 205, 355, 25, Color.Black);
        Raylib.DrawText("Door", 370, 355, 25, Color.Black);
        Raylib.DrawText("Gate", 528, 355, 25, Color.Black);
        Raylib.DrawText("Switch", 640, 355, 25, Color.Black);
         Raylib.DrawText("PRESS SPACE TO START", 200, 500, 30, Color.Black);
}

Raylib.InitWindow(800, 700, "hej");
Raylib.SetTargetFPS(60);
float countDownSpeed = 1 / 54f;


int key = 0;
int lvlNum = 0;
int playerSpeed = 2;
int blockSize = 32;
string scene;
scene = "start";
//lista för dem olika gridsen som använd för rummen
List<int[,]> levels = new();
//--------------------------------------------------------------------------------------------------------------------------
//Rendering
//--------------------------------------------------------------------------------------------------------------------------




int[,] grid1 = {
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,0,0,0,0,0,0,0,0,0,0,1,14,1,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,1,1,1,1,1,1,0,1,0,1,15,1,1,1,1,0,1,1,1,1,1,0,1},
    {1,0,0,0,0,0,0,1,0,1,0,1,0,0,0,1,0,0,1,0,0,0,1,0,1},
    {1,0,1,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,0,1,0,1,0,1},
    {1,0,1,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,1,0,1,8,1},
    {1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1},
    {1,1,1,0,1,1,1,0,0,0,0,1,1,1,0,0,0,1,1,1,1,0,0,0,1},
    {1,4,1,0,0,0,0,0,1,0,0,0,0,1,1,1,0,0,0,0,1,1,0,1,1},
    {1,0,1,1,1,1,1,1,1,0,1,1,0,1,0,0,0,0,1,0,0,0,0,0,1},
    {1,0,1,0,0,0,0,1,0,0,1,0,0,1,0,1,1,1,1,1,1,1,1,1,1},
    {1,0,1,0,1,1,0,1,0,1,1,0,1,1,0,0,0,0,0,0,1,0,0,0,1},
    {1,0,1,0,1,0,0,1,0,0,1,0,1,1,1,1,1,0,1,0,0,0,1,0,1},
    {1,0,0,0,1,0,1,1,1,1,1,0,0,0,1,0,1,1,1,0,1,0,1,0,1},
    {1,1,1,1,1,0,0,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,0,1},
    {1,0,0,0,0,0,0,1,0,0,0,1,1,1,1,1,0,0,0,0,1,0,0,0,1},
    {1,0,0,0,1,0,0,1,0,1,0,1,0,0,0,1,1,1,1,0,1,0,1,1,1},
    {1,0,0,0,1,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,6,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
};
int[,] grid2 = {
    {1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,1},
    {1,0,1,1,1,1,1,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1,1,0,1},
    {1,0,1,0,0,0,0,1,0,0,0,0,0,1,3,1,0,1,0,1,0,1,1,0,1},
    {1,0,1,1,1,0,0,1,1,1,1,1,1,1,0,1,0,0,0,1,0,0,0,0,1},
    {1,0,0,0,1,0,0,0,0,0,0,1,2,1,0,1,0,1,1,1,1,1,1,0,1},
    {1,1,1,0,1,1,1,1,1,1,0,1,0,0,0,0,0,1,0,0,0,0,1,0,1},
    {1,1,1,0,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,0,1},
    {1,5,1,1,1,1,1,1,0,1,1,1,1,1,3,1,0,0,0,1,0,0,0,0,1},
    {1,0,1,3,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,2,0,1,1,1},
    {1,0,1,1,0,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,1,1,1,2,1},
    {1,0,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,0,1},
    {1,1,1,1,0,1,2,0,0,1,0,0,0,0,0,1,0,1,0,1,1,0,1,1,1},
    {1,0,2,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,0,1,1,0,1,0,1},
    {1,0,1,1,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,1,1,0,1,0,1},
    {1,0,1,1,1,1,1,0,1,0,1,1,0,0,0,0,0,1,0,0,0,0,1,0,1},
    {1,0,1,0,0,0,1,0,1,0,0,1,1,1,1,1,1,1,0,0,1,0,1,0,1},
    {1,0,0,0,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,1,1,0,1,0,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
};
int[,] grid3 = {
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,1,1,0,1,1,1,0,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1},
    {1,0,0,0,1,10,1,0,0,0,0,0,0,2,1,0,0,0,0,0,0,0,1,1,1},
    {1,0,1,0,0,0,1,1,1,1,1,1,0,1,1,0,1,0,1,1,1,0,1,1,1},
    {1,3,1,1,1,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,1,0,1,1,1},
    {1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,0,1,0,1,0,1,0,1,1,1},
    {1,1,1,0,1,1,0,0,0,1,0,0,0,0,1,0,1,0,1,0,0,0,1,1,1},
    {1,0,0,0,0,1,0,1,0,1,0,0,1,0,0,0,1,0,1,1,1,0,1,1,1},
    {1,0,1,1,0,0,0,1,0,1,0,1,1,1,1,1,1,0,0,0,0,0,0,0,1},
    {1,2,1,1,0,1,1,1,0,1,0,1,0,0,0,0,1,1,1,1,1,0,1,0,1},
    {1,11,1,1,0,0,0,0,0,1,0,0,0,1,1,0,1,2,0,0,0,0,1,0,1},
    {1,0,0,1,1,1,2,1,0,1,1,1,0,0,1,0,1,1,0,1,1,1,1,0,1},
    {1,0,0,1,3,1,1,1,0,1,2,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
    {1,2,1,1,0,0,0,1,0,1,1,1,1,1,1,1,1,1,1,1,0,1,1,0,1},
    {1,0,1,1,0,1,0,1,0,3,1,0,0,0,1,0,0,0,1,1,0,1,0,0,1},
    {1,0,0,0,0,1,0,1,1,1,1,0,1,0,1,0,1,11,1,1,0,1,0,1,1},
    {1,1,1,1,0,1,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,7,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
};
int[,] grid4 = {
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,1,1,3,1,1,2,1,1,1,1,1,1,1,1,2,0,1,0,0,0,1,0,3,1},
    {1,1,1,0,1,1,0,1,1,2,1,1,1,1,1,1,0,1,0,1,13,1,0,1,1},
    {1,1,1,0,1,1,0,1,1,0,1,1,0,1,1,1,0,0,0,1,0,0,0,0,1},
    {1,1,1,0,0,0,0,1,0,0,1,1,0,0,1,1,1,1,1,1,0,1,1,1,1},
    {1,1,1,0,1,1,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,9,1},
    {1,1,1,0,0,0,0,1,0,0,0,1,0,1,0,1,1,0,1,1,0,1,1,1,1},
    {1,1,1,0,1,1,0,0,0,1,0,0,0,0,0,1,1,0,1,1,0,1,1,1,1},
    {1,1,1,0,0,1,1,1,1,1,1,1,0,1,1,1,1,2,1,1,0,1,3,0,1},
    {1,2,1,1,0,0,0,1,3,0,0,1,0,0,0,0,1,1,1,1,0,1,1,0,1},
    {1,13,1,1,1,1,0,1,1,1,0,1,1,1,1,0,1,1,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,1,1,0,0,1,0,0,0,0,1,1,3,1,1,1,1,0,1},
    {1,0,1,1,1,1,1,1,0,0,0,1,0,1,1,1,1,1,1,1,1,1,1,0,1},
    {1,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,2,1,0,0,0,0,1},
    {1,0,1,0,1,0,1,1,0,1,0,1,1,1,1,0,1,1,1,1,2,1,1,1,1},
    {1,0,0,0,0,0,1,0,0,1,0,0,0,1,1,0,1,1,1,1,13,1,1,1,1},
    {1,1,1,1,1,1,1,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,1},
    {1,1,1,1,1,1,1,0,0,0,0,0,2,1,1,1,1,1,1,1,1,1,1,12,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
};
int[,] grid5 = {

    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,0,0,1,1,0,1,0,1,0,0,0,0,0,0,0,1,0,1,0,1,1,0,0,1},
    {1,1,1,1,1,0,1,0,1,0,1,1,0,1,1,0,1,0,1,0,1,1,1,1,1},
    {1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1},
    {1,0,0,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,1},
    {1,1,1,1,1,1,1,0,1,0,1,1,0,1,1,0,1,0,1,1,1,1,1,1,1},
    {1,0,0,0,0,0,0,0,1,0,0,1,1,1,0,0,1,0,0,0,0,0,0,0,1},
    {1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1},
    {1,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,1},
    {1,0,1,0,1,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,1,0,1,0,1},
    {1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,1},
    {1,0,0,1,0,0,0,1,1,1,0,0,0,0,0,1,1,1,0,0,0,1,0,0,1},
    {1,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,1},
    {1,1,1,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,1,1,1,1},
    {1,0,0,0,0,1,1,1,0,1,0,0,0,0,0,1,0,1,1,1,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},

};
levels.Add(grid1);
levels.Add(grid2);
levels.Add(grid3);
levels.Add(grid4);
levels.Add(grid5);


//---------------------------------------------------------------------------------
//RECTS
//---------------------------------------------------------------------------------
Rectangle Player = new Rectangle(78, 534, 14, 14);
Rectangle hud = new Rectangle(0, 605, 800, 100);
Rectangle timer = new Rectangle(375, 645, 240, 25);
Rectangle timerCountDown = new Rectangle(575, 645, 240, 25);
//---------------------------------------------------------------------------------


while (!Raylib.WindowShouldClose())
{

    //---------------------------------------------------------------------------------
    //RECTS
    //---------------------------------------------------------------------------------
    List<Rectangle> rects = new List<Rectangle>();
    List<Rectangle> rectsKey = new List<Rectangle>();
    List<Rectangle> rectsTime = new List<Rectangle>();
    List<Rectangle> Switch = new List<Rectangle>();
    List<Rectangle> gate = new List<Rectangle>();
    List<Rectangle> Switch2 = new List<Rectangle>();
    List<Rectangle> gate2 = new List<Rectangle>();

    Rectangle portalBlock1 = new();
    Rectangle portalBlock2 = new();
    Rectangle portalBlock3 = new();
    Rectangle portalBlock4 = new();
    Rectangle portalBlock5 = new();
    Rectangle portalBlock6 = new();
    Rectangle victoryTP = new();
    Rectangle victoryGate = new();


    //--------------------------------------------------------------------------------------------------------------------------
    //GAME LOGIC
    //--------------------------------------------------------------------------------------------------------------------------



    // Vilka block som placeras ut beroende på vilket nummer som angen i griedn
    for (int y = 0; y < levels[lvlNum].GetLength(0); y++)
    {
        for (int x = 0; x < levels[lvlNum].GetLength(1); x++)
        {
            if (levels[lvlNum][y, x] == 1) // alla gråa väggar
            {

                Rectangle block = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(block, Color.Gray);
                rects.Add(block);
            }
            else if (levels[lvlNum][y, x] == 2) // Nycklar
            {
                Rectangle keyBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(keyBlock, Color.Gold);
                rectsKey.Add(keyBlock);

            }
            else if (levels[lvlNum][y, x] == 3) // + 5 sek block
            {
                Rectangle timeBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(timeBlock, Color.Green);
                rectsTime.Add(timeBlock);
            }
            else if (levels[lvlNum][y, x] == 4) //portal/dörr till rum 2
            {
                portalBlock1 = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(portalBlock1, Color.Maroon);
                //Portal1.Add(portalBlock);
            }
            if (levels[lvlNum][y, x] == 5) // portal/dörr från rum 2 till 1
            {
                portalBlock2 = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(portalBlock2, Color.Maroon);
                //Portal2.Add(portalBlock);
            }
            if (levels[lvlNum][y, x] == 6) //portal/dörr till rum 3
            {
                portalBlock3 = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(portalBlock3, Color.Maroon);
                //Portal3.Add(portalBlock);
            }
            if (levels[lvlNum][y, x] == 7) // portal/dörr från rum 3 till 1
            {
                portalBlock4 = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(portalBlock4, Color.Maroon);
                //Portal4.Add(portalBlock);
            }
            if (levels[lvlNum][y, x] == 8) //portal/dörr till rum 4
            {
                portalBlock5 = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(portalBlock5, Color.Maroon);
                //Portal5.Add(portalBlock);
            }
            if (levels[lvlNum][y, x] == 9) // portal/dörr från rum 4 till 1
            {
                portalBlock6 = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(portalBlock6, Color.Maroon);
                //Portal6.Add(portalBlock);
            }
            if (levels[lvlNum][y, x] == 10) // switch i rum 3
            {
                Rectangle switchBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(switchBlock, Color.Purple);
                Switch.Add(switchBlock);
            }
            if (levels[lvlNum][y, x] == 11) // hinder i rum 3
            {
                Rectangle gateBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(gateBlock, Color.DarkBlue);
                gate.Add(gateBlock);
            }
            if (levels[lvlNum][y, x] == 12) // switch i rum 4
            {
                Rectangle switchBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(switchBlock, Color.DarkPurple);
                Switch2.Add(switchBlock);
            }
            if (levels[lvlNum][y, x] == 13) // hinder i rum 4
            {
                Rectangle gateBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(gateBlock, Color.DarkBlue);
                gate2.Add(gateBlock);
            }
            if (levels[lvlNum][y, x] == 14) // portal till Victoryskärmen
            {
                victoryTP = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(victoryTP, Color.DarkBlue);
                //victoryPortal.Add(victoryTP);
            }
            if (levels[lvlNum][y, x] == 15) //blocket som blokerar portalen till sissta rummet/Victoryskärmen
            {
                victoryGate = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(victoryGate, Color.Gray);
                rects.Add(victoryGate);
            }
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------
    //BYTE MELLAN RUM
    //------------------------------------------------------------------------------------------------------------------------------------------------

    if (Raylib.CheckCollisionRecs(Player, portalBlock1))
    {//gå till grid 2 och justera position
        lvlNum++;
        Player.Y += 28;

    }


    //PORTAL I RUM 2 TILL RUM 1
    if (Raylib.CheckCollisionRecs(Player, portalBlock2))
    {
        lvlNum--; // gå tillbaka till grid1 och justera position
        Player.Y += 28;
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------------------------------------------

    //PORTAL I RUM 1 TILL RUM 3
    if (Raylib.CheckCollisionRecs(Player, portalBlock3))
    {
        lvlNum += 2; // gå tillbaka till grid1 och justera position
        Player.X -= 28;
    }

    //PORTAL I RUM 1 TILL RUM 3
    if (Raylib.CheckCollisionRecs(Player, portalBlock4))
    {
        lvlNum -= 2; // gå tillbaka till grid1 och justera position
        Player.X -= 28;
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------------------------------------------

    //PORTAL I RUM 1 TILL RUM 3
    if (Raylib.CheckCollisionRecs(Player, portalBlock5))
    {
        lvlNum += 3; // gå tillbaka till grid1 och justera position
        Player.X -= 34;
        Player.Y += 22;
    }


    //PORTAL I RUM 1 TILL RUM 3
    if (Raylib.CheckCollisionRecs(Player, portalBlock6))
    {
        lvlNum -= 3; // gå tillbaka till grid1 och justera position
        Player.X += 26;
        Player.Y -= 34;
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------


    //PORTAL I RUM 1 TILL RUM 3
    if (Raylib.CheckCollisionRecs(Player, victoryTP))
    {
        lvlNum += 4;
        Player.Y = 550;
    }



    //-----------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------



    Raylib.DrawRectangleRec(Player, Color.DarkPurple);
    if (scene == "start")
    {
      startScreen();

        if (Raylib.IsKeyDown(KeyboardKey.Space))
        {
            scene = "lvl1";
        }
    }
    if (scene == "lvl1")
    { //starta timern
        Raylib.ClearBackground(Color.White);
        timerCountDown.X -= countDownSpeed;
    }

    if (scene == "lvl1")
    {
        //spelar kontroller
        if (Raylib.IsKeyDown(KeyboardKey.D)) Player.X += playerSpeed;

        if (Raylib.IsKeyDown(KeyboardKey.A)) Player.X -= playerSpeed;

        if (Raylib.IsKeyDown(KeyboardKey.W)) Player.Y -= playerSpeed;

        if (Raylib.IsKeyDown(KeyboardKey.S)) Player.Y += playerSpeed;

    }

    foreach (Rectangle block in rects)
    {//om spelaren går in i en vägg så vänds spelarens hastighet för att stoppa dem
        if (Raylib.IsKeyDown(KeyboardKey.D) && Raylib.CheckCollisionRecs(Player, block))
        { Player.X -= playerSpeed; }
        if (Raylib.IsKeyDown(KeyboardKey.A) && Raylib.CheckCollisionRecs(Player, block))
        { Player.X += playerSpeed; }
        if (Raylib.IsKeyDown(KeyboardKey.W) && Raylib.CheckCollisionRecs(Player, block))
        { Player.Y += playerSpeed; }
        if (Raylib.IsKeyDown(KeyboardKey.S) && Raylib.CheckCollisionRecs(Player, block))
        { Player.Y -= playerSpeed; }
    }

    foreach (Rectangle keyBlock in rectsKey)
    {
        if (Raylib.CheckCollisionRecs(Player, keyBlock))
        {//ta bort blocket och lägg till en key

            levels[lvlNum][(int)(keyBlock.Y / blockSize), (int)(keyBlock.X / blockSize)] = 0; //delar y,x kordinater med storleken på blocken och sätter dem till 0 aka tar bort det.
            key++;
        }


    }

    foreach (Rectangle timeBlock in rectsTime)
    {
        if (Raylib.CheckCollisionRecs(Player, timeBlock))
        {
            levels[lvlNum][(int)(timeBlock.Y / blockSize), (int)(timeBlock.X / blockSize)] = 0; // När man nuddar timeBlock så försvinner blocket och man får +5 sekunder på timern.
            timerCountDown.X += 5.655f; // lägg till 5 sek
        }
    }





    //hastighetsförändring beroende på antalet keys som spelaren har plockat upp
    if (key == 10)
    {
        playerSpeed = 5 / 2;
    }
    else if (key == 15)
    {
        playerSpeed = 3;
    }
    else if (key == 20)
    {
        // vid 20 keys så låser spelaren upp gaten till sissta rummet/victoryskärmen
        levels[lvlNum][(int)(victoryGate.Y / blockSize), (int)(victoryGate.X / blockSize)] = 0;
        playerSpeed = 7 / 2;
    }


    foreach (Rectangle switchBlock in Switch)///switch & gate interaktion i rum 3
    {
        foreach (Rectangle gateBlock in gate)
        {
            if (Raylib.CheckCollisionRecs(Player, switchBlock))

            {//om spelaren aktiverar switch blocket så försvinner switchen och gatesen
                levels[lvlNum][(int)(gateBlock.Y / blockSize), (int)(gateBlock.X / blockSize)] = 0;
                levels[lvlNum][(int)(switchBlock.Y / blockSize), (int)(switchBlock.X / blockSize)] = 0;
            }
            //spelaren kan inte gå igenom gateblocken
            if (Raylib.IsKeyDown(KeyboardKey.D) && Raylib.CheckCollisionRecs(Player, gateBlock))
            { Player.X -= playerSpeed; }
            if (Raylib.IsKeyDown(KeyboardKey.A) && Raylib.CheckCollisionRecs(Player, gateBlock))
            { Player.X += playerSpeed; }
            if (Raylib.IsKeyDown(KeyboardKey.W) && Raylib.CheckCollisionRecs(Player, gateBlock))
            { Player.Y += playerSpeed; }
            if (Raylib.IsKeyDown(KeyboardKey.S) && Raylib.CheckCollisionRecs(Player, gateBlock))
            { Player.Y -= playerSpeed; }
        }
    }





    foreach (Rectangle switchBlock in Switch2)//switch & gate interaktion i rum 4
    {
        foreach (Rectangle gateBlock in gate2)
        {
            if (Raylib.CheckCollisionRecs(Player, switchBlock))

            {//om spelaren aktiverar switch blocket så försvinner switchen och gatesen
                levels[lvlNum][(int)(gateBlock.Y / blockSize), (int)(gateBlock.X / blockSize)] = 0;
                levels[lvlNum][(int)(switchBlock.Y / blockSize), (int)(switchBlock.X / blockSize)] = 0;
            }
            //spelaren kan inte gå igenom gateblocken
            if (Raylib.IsKeyDown(KeyboardKey.D) && Raylib.CheckCollisionRecs(Player, gateBlock))
            { Player.X -= playerSpeed; }
            if (Raylib.IsKeyDown(KeyboardKey.A) && Raylib.CheckCollisionRecs(Player, gateBlock))
            { Player.X += playerSpeed; }
            if (Raylib.IsKeyDown(KeyboardKey.W) && Raylib.CheckCollisionRecs(Player, gateBlock))
            { Player.Y += playerSpeed; }
            if (Raylib.IsKeyDown(KeyboardKey.S) && Raylib.CheckCollisionRecs(Player, gateBlock))
            { Player.Y -= playerSpeed; }
        }
    }





    Raylib.DrawFPS(35, 550);

    Raylib.BeginDrawing();



    Raylib.DrawRectangleRec(hud, Color.Gray);
    Raylib.DrawRectangleRec(timer, Color.Green);
    Raylib.DrawRectangleRec(timerCountDown, Color.Gray);
    Raylib.DrawText($"Keys {key}/20", 80, 640, 40, Color.Gold);
    //---------------------------------------------------------------------
    //GAME OVER
    if (timerCountDown.X < 375)
    {
        scene = "gameOver";
        Raylib.DrawRectangle(0, 0, 800, 700, Color.Black);
        countDownSpeed = 0;
        Raylib.DrawText("GAME OVER", 185, 340, 75, Color.Red);
    }
    //---------------------------------------------------------------------
    //VICTORY
    if (lvlNum == 4)
    {
        Raylib.DrawText("Victory", 250, 330, 75, Color.Green);
        Raylib.ClearBackground(Color.Black);
        countDownSpeed = 0;
        Raylib.DrawRectangle(375, 645, 240, 25, Color.Gray);
    }



    Raylib.EndDrawing();

}