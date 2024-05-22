namespace SlutProjekt;

using System.Security.Cryptography.X509Certificates;
using Raylib_cs;
public class Metoder
{

    public static void StartScreen()
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

    public static Rectangle MovePlayer(Rectangle player, int playerSpeed)
    {

        if (Raylib.IsKeyDown(KeyboardKey.D)) player.X += playerSpeed;

        if (Raylib.IsKeyDown(KeyboardKey.A)) player.X -= playerSpeed;

        if (Raylib.IsKeyDown(KeyboardKey.W)) player.Y -= playerSpeed;

        if (Raylib.IsKeyDown(KeyboardKey.S)) player.Y += playerSpeed;
        return player;
    }



}

