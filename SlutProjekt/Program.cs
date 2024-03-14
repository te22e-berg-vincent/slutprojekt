// See https://aka.ms/new-console-template for more information
using Raylib_cs;

Raylib.InitWindow(800, 600, "hej");
Raylib.SetTargetFPS(60);
int blockSize = 32;
int[,] grid = {
    {1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,0,0,0,0,0,0,0,1,0,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1}
};

while (!Raylib.WindowShouldClose())
{

        for (int y = 0; y < grid.GetLength(0); y++)
    {
        for (int x = 0; x < grid.GetLength(1); x++)
        {
            if (grid[y, x] == 1)
            {
                Rectangle rect = new Rectangle(x * blockSize, y * blockSize, 32, 32);
                Raylib.DrawRectangleRec(rect, Color.Red);
            }
        }
    }




Raylib.BeginDrawing();















Raylib.EndDrawing();

}