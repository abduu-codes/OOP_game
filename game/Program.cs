using System;

class Program
{
    static void Main(string[] args)
    {
        string playerName = AskPlayerName();
        ShowWelcomeScreen();
        bool keepPlaying = true;

        while (keepPlaying)
        {
            GameEngine game = new GameEngine();
            game.StartGame(playerName);         

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\n");
            Console.WriteLine("\t\t   ╔═══════════════════════════════════╗");
            Console.WriteLine("\t\t   ║       G A M E   O V E R           ║");
            Console.WriteLine("\t\t   ╚═══════════════════════════════════╝");


            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n\t\t\t   Player     : {playerName}");
            Console.WriteLine($"\t\t\t   Final Score: {game.Score}");
            Console.WriteLine("\n\t\t\t   1. Play Again");
            Console.WriteLine("\t\t\t   2. Exit");
            while (true)
            {
                Console.Write("\n\t\t\t   Selection: ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    break;           
                }
                else if (input == "2")
                {
                    keepPlaying = false;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n\t\t\t   Closing Game...");
                    System.Threading.Thread.Sleep(1000);
                    break;
                }
                else
                {
               
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t   Wrong selection! Please enter 1 or 2.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        Console.ResetColor();
    }

    static string AskPlayerName()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n\n\t\t╔══════════════════════════════════════╗");
        Console.WriteLine("\t\t║         Welcome to CAR GAME          ║");
        Console.WriteLine("\t\t╚══════════════════════════════════════╝\n");

        string name = "";

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\t\t  Enter your name (letters only): ");
            name = Console.ReadLine().Trim();
            bool isValid = name.Length > 0;
            foreach (char c in name)
            {
                if (!char.IsLetter(c))
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
                break; 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t  Invalid! Name must contain letters only (no numbers or symbols).\n");
        }

        return name;
    }

    static void ShowWelcomeScreen()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n\n\t\t╔══════════════════════════════════════════╗");
        Console.WriteLine("\t\t    ║         Welcome to CAR GAME              ║");
        Console.WriteLine("\t\t    ╚══════════════════════════════════════════╝\n");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("\t\t  Controls: Use Arrow Keys [LEFT] [RIGHT] [UP] [DOWN] to move your car");
        Console.WriteLine("\n\t\t  Dodge the falling obstacles!");
        Console.WriteLine("\t\t  Score increases each time one passes you.");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n\n\t\t  Press ENTER to start...");
        Console.ResetColor();
        Console.ReadLine();
    }
}