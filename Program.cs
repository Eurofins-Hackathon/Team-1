using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the CLI Game!");
        Console.WriteLine("Press any key to start...");
        Console.ReadKey();

        GameLogic game = new GameLogic();
        game.StartGame();

        Console.WriteLine("Game Over. Thanks for playing!");
    }
}
