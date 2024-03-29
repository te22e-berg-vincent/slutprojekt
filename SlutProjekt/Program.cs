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
    {1,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
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
    for (int y = 0; y < grid1.GetLength(0); y++)
    {
        for (int x = 0; x < grid1.GetLength(1); x++)
        {
            if (grid1[y, x] == 1)
            {

                Rectangle block = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(block, Color.Gray);
                rects.Add(block);
            }
            else if (grid1[y, x] == 2)
            {
                Rectangle keyBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(keyBlock, Color.Gold);
                rects2.Add(keyBlock);

            }
            else if (grid1[y, x] == 3)
            {
                Rectangle timeBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(timeBlock, Color.SkyBlue);
                rects2.Add(timeBlock);
            }
            else if (grid1[y, x] == 4)
            {
                Rectangle portalBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                Raylib.DrawRectangleRec(portalBlock, Color.Maroon);
                Portal1.Add(portalBlock);
            }

        }

    }
foreach (Rectangle portalBlock in Portal1)
{
 if (Raylib.CheckCollisionRecs(Player, portalBlock))
    {
        lvlNum++;
        if (lvlNum >= levels.Count) // går till nästa level
        {
            for (int y = 0; y < grid2.GetLength(0); y++)
            {

                for (int x = 0; x < grid2.GetLength(1); x++)
                {
                    // Ladda rum 2
                    if (grid1[y, x] != grid2[y, x]) //om grid1 y,x celler inte stämmer med grid2 så bile dom 0 aka tas bort
                    {
                        grid1[y, x] = 0;
                    }
                    if (grid2[y, x] == 1)
                    {
                        Rectangle block = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                        Raylib.DrawRectangleRec(block, Color.Gray);

                    }
                    if (grid2[y, x] == 2)
                    {
                        Rectangle keyBlock = new Rectangle(x * blockSize, y * blockSize, 26, 26);
                        Raylib.DrawRectangleRec(keyBlock, Color.Gold);
                    }
                 
                }
                
            }
            Player.Y += 34;
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
        levels[0] = grid1;

        Raylib.ClearBackground(Color.White);
    }

    if (scene == "lvl2")
    {
        levels[1] = grid2;

    }
    //--------------------------------------------------------------------------------------------------------------------------
    //Player
    //--------------------------------------------------------------------------------------------------------------------------

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
            levels[lvlNum][(int)(keyBlock.Y / blockSize), (int)(keyBlock.X / blockSize)] = 0; //delar y,x kordinater med storleken på blocken och sätter dem till 0 aka tar bort det.

        }
    }
    /*
    foreach (Rectangle timeBlock in rects2)
    {
        if (Raylib.CheckCollisionRecs(Player,timeBlock))
        {

        }
    }
    */

    Raylib.DrawFPS(20, 550);
    Raylib.BeginDrawing();

    Raylib.DrawRectangleRec(hud, Color.Gray);
    Raylib.EndDrawing();

}



/*
Texture2D PlayerImg = Raylib.LoadTexture("player.png");
 Player.Width = PlayerImg.Width;
 Player.Height = PlayerImg.Height;
 */