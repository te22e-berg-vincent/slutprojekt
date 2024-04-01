// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Xml.Schema;
using Raylib_cs;






Raylib.InitWindow(800, 700, "hej");
Raylib.SetTargetFPS(60);


//int key = 0;
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
    {1,0,0,0,0,0,0,0,0,0,0,1,4,1,3,0,0,0,0,0,0,0,0,0,1},
    {1,0,1,1,1,1,1,1,0,1,0,1,0,1,1,1,1,0,1,1,1,1,1,0,1},
    {1,0,0,0,0,0,0,1,0,1,0,1,0,0,0,1,0,0,1,0,0,0,1,0,1},
    {1,0,1,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,0,1,0,1,0,1},
    {1,0,1,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,1,0,1,4,1},
    {1,3,1,0,1,3,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1},
    {1,1,1,0,1,1,1,0,0,0,0,1,1,1,0,0,0,1,1,1,1,0,0,0,1},
    {1,4,1,0,0,0,0,0,1,0,0,0,0,1,1,1,0,0,0,0,1,1,0,1,1},
    {1,0,1,1,1,1,1,1,1,0,1,1,0,1,0,0,0,0,1,0,0,0,0,0,1},
    {1,0,1,0,0,0,0,1,0,0,1,0,0,1,0,1,1,1,1,1,1,1,1,1,1},
    {1,0,1,0,1,1,0,1,0,1,1,0,1,1,0,0,0,0,0,0,1,0,0,0,1},
    {1,0,1,0,1,0,0,1,0,3,1,0,1,1,1,1,1,0,1,0,0,0,1,0,1},
    {1,0,0,0,1,0,1,1,1,1,1,0,0,0,1,0,1,3,1,0,1,0,1,0,1},
    {1,1,1,1,1,0,0,1,0,0,0,0,1,0,0,0,1,1,1,1,1,1,1,0,1},
    {1,2,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,1,0,0,0,1},
    {1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,1,1,1,1,0,1,0,1,1,1},
    {1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
};
int[,] grid2 = {
    {1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,1},
    {1,0,1,1,1,1,1,1,0,1,1,1,0,1,1,1,0,1,0,0,1,1,1,0,1},
    {1,0,1,0,0,0,0,1,0,0,0,0,0,1,3,1,0,1,1,1,1,3,1,1,1},
    {1,0,1,1,1,0,0,1,1,1,1,1,1,1,0,1,0,0,0,0,0,0,0,0,1},
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
    {1,0,1,0,0,0,1,0,1,0,0,1,1,1,1,1,1,1,0,1,1,0,1,0,1},
    {1,2,0,0,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,1,3,0,1,0,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
};
int[,] grid3 = {
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
};
int[,] grid4 = {
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
};
levels.Add(grid1);
levels.Add(grid2);
levels.Add(grid3);
levels.Add(grid4);
//---------------------------------------------------------------------------------
//RECTS
//---------------------------------------------------------------------------------
Rectangle Player = new Rectangle(78, 534, 14, 14);
Rectangle hud = new Rectangle(0, 605, 800, 100);
//--------------------------------------------------------------------------------------------------------------------------
//GAME LOGIC
//--------------------------------------------------------------------------------------------------------------------------
while (!Raylib.WindowShouldClose())
{
    List<Rectangle> rects = new List<Rectangle>();
    List<Rectangle> rects2 = new List<Rectangle>();
    List<Rectangle> Portal1 = new List<Rectangle>();
    List<Rectangle> Portal2 = new List<Rectangle>();



    int[,] gridNu = levels[lvlNum]; //gridNU är baserat på lvlNum
    for (int y = 0; y < gridNu.GetLength(0); y++)
    {
        for (int x = 0; x < gridNu.GetLength(1); x++)
        {
            if (gridNu[y, x] == 1)
            {

                Rectangle block = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(block, Color.Gray);
                rects.Add(block);
            }
            else if (gridNu[y, x] == 2)
            {
                Rectangle keyBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(keyBlock, Color.Gold);
                rects2.Add(keyBlock);

            }
            else if (gridNu[y, x] == 3)
            {
                Rectangle timeBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(timeBlock, Color.SkyBlue);
                rects2.Add(timeBlock);
            }
            else if (gridNu[y, x] == 4)
            {
                Rectangle portalBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(portalBlock, Color.Maroon);
                Portal1.Add(portalBlock);
            }
            if (gridNu[y, x] == 5)
            {
                Rectangle portalBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(portalBlock, Color.Maroon);
                Portal2.Add(portalBlock);
            }
        }
    }
    foreach (Rectangle portalBlock in Portal1)
    {
        if (Raylib.CheckCollisionRecs(Player, portalBlock))
        {
            lvlNum++;
            Player.Y += 28;

        }
    }
    foreach (Rectangle portalBlock in Portal2)
    {
        if (Raylib.CheckCollisionRecs(Player, portalBlock))
        {
            lvlNum--; // gå tillbaka till grid1
            if (lvlNum >= 0) // lvlNum är within bounds aka inga errors
            {
                grid1 = gridNu; //sätter grid 1 till nuvarande lvl
                Player.Y += 28;
            }

        }
    }
    Raylib.DrawRectangleRec(Player, Color.Black);
    if (scene == "start")
    {
        Raylib.ClearBackground(Color.White);
        Raylib.DrawRectangle(0, 0, 800, 700, Color.Black);
        if (Raylib.IsKeyDown(KeyboardKey.Space))
        {
            scene = "lvl1";
        }
    }
    if (scene == "lvl1")
    {

        Raylib.ClearBackground(Color.White);
    }
    //spelar kontroller
    if (Raylib.IsKeyDown(KeyboardKey.D)) Player.X += playerSpeed;
    if (Raylib.IsKeyDown(KeyboardKey.A)) Player.X -= playerSpeed;
    if (Raylib.IsKeyDown(KeyboardKey.W)) Player.Y -= playerSpeed;
    if (Raylib.IsKeyDown(KeyboardKey.S)) Player.Y += playerSpeed;

    foreach (Rectangle block in rects)
    {
        if (Raylib.CheckCollisionRecs(Player, block))
        {
            //om spelaren går in i en vägg så vänds spelarens hastighet för att stoppa dem
            if (Raylib.IsKeyDown(KeyboardKey.D)) { Player.X -= playerSpeed; }
            if (Raylib.IsKeyDown(KeyboardKey.A)) { Player.X += playerSpeed; }
            if (Raylib.IsKeyDown(KeyboardKey.W)) { Player.Y += playerSpeed; }
            if (Raylib.IsKeyDown(KeyboardKey.S)) { Player.Y -= playerSpeed; }

        }
    }

    foreach (Rectangle keyBlock in rects2)
    {
        if (Raylib.CheckCollisionRecs(Player, keyBlock))
        {
            gridNu[(int)(keyBlock.Y / blockSize), (int)(keyBlock.X / blockSize)] = 0; //delar y,x kordinater med storleken på blocken och sätter dem till 0 aka tar bort det.

        }
    }
    Raylib.DrawFPS(20, 550);
    Raylib.BeginDrawing();

    Raylib.DrawRectangleRec(hud, Color.Gray);
    Raylib.EndDrawing();

}