// See https://aka.ms/new-console-template for more information
using Raylib_cs;

Raylib.InitWindow(800, 600, "hej");
Raylib.SetTargetFPS(60);
int blockSize = 32;
int[,] grid = {
    {1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1}
};

while (!Raylib.WindowShouldClose())
{

    
Raylib.BeginDrawing();















Raylib.EndDrawing();

}