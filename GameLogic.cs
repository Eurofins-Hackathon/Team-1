using System;
using System.Collections.Generic;

class GameLogic
{
    private const int BoardSize = 5;
    private char[,] playerBoard = new char[BoardSize, BoardSize];
    private char[,] computerBoard = new char[BoardSize, BoardSize];
    private char[,] computerGuesses = new char[BoardSize, BoardSize];
    private Random random = new Random();

    public void StartGame()
    {
        Console.WriteLine("Welcome to Battleship!");
        InitializeBoards();
        PlaceShips(playerBoard, "Player");
        PlaceShips(computerBoard, "Computer");

        bool gameOver = false;
        while (!gameOver)
        {
            Console.Clear();
            DisplayBoard(playerBoard, "Your Board");
            DisplayBoard(computerGuesses, "Computer's Guesses");

            Console.WriteLine("Your turn to attack!");
            PlayerAttack();

            if (CheckVictory(computerBoard))
            {
                Console.WriteLine("Congratulations! You sank all the computer's ships!");
                gameOver = true;
                break;
            }

            Console.WriteLine("Computer's turn to attack!");
            ComputerAttack();

            if (CheckVictory(playerBoard))
            {
                Console.WriteLine("Game Over! The computer sank all your ships.");
                gameOver = true;
            }
        }
    }

    private void InitializeBoards()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                playerBoard[i, j] = '-';
                computerBoard[i, j] = '-';
                computerGuesses[i, j] = '-';
            }
        }
    }

    private void PlaceShips(char[,] board, string owner)
    {
        int shipsToPlace = 2;
        Console.WriteLine($"{owner}, place your {shipsToPlace} ships on the board.");

        for (int i = 0; i < shipsToPlace; i++)
        {
            int x, y;
            do
            {
                if (owner == "Player")
                {
                    x = GetValidCoordinate($"Enter row for ship {i + 1}:");
                    y = GetValidCoordinate($"Enter column for ship {i + 1}:");
                }
                else
                {
                    x = random.Next(BoardSize);
                    y = random.Next(BoardSize);
                }
            } while (board[x, y] == 'S');

            board[x, y] = 'S';
        }
    }

    private void DisplayBoard(char[,] board, string title)
    {
        Console.WriteLine(title);
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    private void PlayerAttack()
    {
        int x, y;
        do
        {
            x = GetValidCoordinate("Enter row for attack:");
            y = GetValidCoordinate("Enter column for attack:");

            if (computerBoard[x, y] == 'X' || computerBoard[x, y] == 'O')
            {
                Console.WriteLine("You already attacked this position. Try again.");
            }
            else
            {
                break;
            }
        } while (true);

        if (computerBoard[x, y] == 'S')
        {
            Console.WriteLine("Hit!");
            computerBoard[x, y] = 'X';
        }
        else
        {
            Console.WriteLine("Miss.");
            computerBoard[x, y] = 'O';
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void ComputerAttack()
    {
        int x, y;
        do
        {
            x = random.Next(BoardSize);
            y = random.Next(BoardSize);
        } while (playerBoard[x, y] == 'X' || playerBoard[x, y] == 'O');

        if (playerBoard[x, y] == 'S')
        {
            Console.WriteLine("Computer hit your ship!");
            playerBoard[x, y] = 'X';
        }
        else
        {
            Console.WriteLine("Computer missed.");
            playerBoard[x, y] = 'O';
        }
    }

    private bool CheckVictory(char[,] board)
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                if (board[i, j] == 'S')
                {
                    return false;
                }
            }
        }
        return true;
    }

    private int GetValidCoordinate(string prompt)
    {
        int coordinate;
        while (true)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out coordinate) && coordinate >= 0 && coordinate < BoardSize)
            {
                return coordinate;
            }
            Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");
        }
    }
}