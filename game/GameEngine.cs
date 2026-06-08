using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class GameEngine
{
    private const int WIDTH = 80;  
    private const int PLAY_HEIGHT = 28;  
    private const int HEADER_ROWS = 8; 
    private int TopBoundary 
    {
        get 
        {
            return HEADER_ROWS; 
        }
    }
    private int BottomBoundary 
    {
        get 
        {
            return HEADER_ROWS + PLAY_HEIGHT; 
        }
    }
    private int _score = 0;
    private bool _isRunning = true;
    private string _playerName = "Player"; 

    private Car _player;
    private List<Obstacle> _obstacles = new List<Obstacle>();
    private Random _rnd = new Random();
    private Stopwatch _timer = new Stopwatch();
    private DatabaseHelper _db = new DatabaseHelper();
    private char[] _shapes = { '#', '*', 'O', '@', 'X', '%', '~', '!' };
    private ConsoleColor[] _colors =
    {
        ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Magenta,
        ConsoleColor.Green, ConsoleColor.White, ConsoleColor.DarkYellow
    };
    public int Score 
    {
        get 
        {
            return _score; 
        }
    }
    public void StartGame(string playerName)
    {
        _playerName = playerName;
        _score = 0;
        _isRunning = true;
        _obstacles.Clear();

        Console.CursorVisible = false;
        try 
        {
            Console.SetWindowSize(WIDTH + 6, HEADER_ROWS + PLAY_HEIGHT + 6); 
        } 
        catch 
        {
        }
        Console.Clear();
        _db.EnsureTableExists();
        DrawHeader();
        DrawBoundary();
        _player = new Car(WIDTH / 2 - 3, BottomBoundary - 4);
        _timer.Restart();
        while (_isRunning)
        {
            HandleInput();     
            SpawnObstacles(); 
            MoveObstacles();   
            Draw();            
            Thread.Sleep(35);   
        }

        EndGame();
    }
    private void DrawHeader()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.SetCursorPosition(0, 0);
        Console.Write("  " + new string('*', 76));
        string[] art = {
            @"  ██████╗ █████╗ ██████╗      ██████╗  █████╗ ███╗   ███╗███████╗",
            @"  ██╔════╝██╔══██╗██╔══██╗    ██╔════╝ ██╔══██╗████╗ ████║██╔════╝",
            @"  ██║     ███████║██████╔╝    ██║  ███╗███████║██╔████╔██║█████╗  ",
            @"  ██║     ██╔══██║██╔══██╗    ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ",
            @"  ╚██████╗██║  ██║██║  ██║    ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗",
            @"   ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝"
        };
        ConsoleColor[] artColors =
        {
            ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Green,
            ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Cyan
        };

        for (int i = 0; i < art.Length; i++)
        {
            Console.ForegroundColor = artColors[i];
            Console.SetCursorPosition(0, i + 1);
            Console.Write(art[i]);
        }
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.SetCursorPosition(0, 7);
        Console.Write("  " + new string('*', 76));

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(3, 7);
        Console.Write(" Arrow Keys: Move    Avoid obstacles!    Score increases as obstacles pass ");

        Console.ResetColor();
    }
    private void DrawBoundary()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        for (int col = 1; col < WIDTH; col++)
        {
            Console.SetCursorPosition(col, TopBoundary);
            Console.Write("═");
            Console.SetCursorPosition(col, BottomBoundary); 
            Console.Write("═");
        }

        for (int row = TopBoundary; row <= BottomBoundary; row++)
        {
            Console.SetCursorPosition(1, row); Console.Write("║");
            Console.SetCursorPosition(WIDTH, row); Console.Write("║");
        }

        Console.ResetColor();
    }
    private void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            _player.Clear(); 
            _player.Move(key.Key, WIDTH, BottomBoundary, TopBoundary);
        }
    }
    private void SpawnObstacles()
    {
        if (_rnd.Next(0, 100) < 15)
        {
            int rx = _rnd.Next(2, WIDTH - 2);
            char rs = _shapes[_rnd.Next(_shapes.Length)];
            ConsoleColor rc = _colors[_rnd.Next(_colors.Length)];
            _obstacles.Add(new Obstacle(rx, TopBoundary + 1, rs, rc));
        }
    }
    private void MoveObstacles()
    {
       
        for (int i = _obstacles.Count - 1; i >= 0; i--)
        {
            _obstacles[i].Clear();      
            _obstacles[i].MoveDown();   
            if (_player.CollidesWith(_obstacles[i]))
            {
                _isRunning = false;   
                return;
            }

            if (_obstacles[i].IsOffScreen(BottomBoundary))
            {
                int ox = _obstacles[i].X;  
                _obstacles.RemoveAt(i);
                _score++;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(ox, BottomBoundary);
                Console.Write("═");
                Console.ResetColor();
            }
        }
    }
    private void Draw()
    {
       
        string timeStr = string.Format("{0:00}:{1:00}",(int)_timer.Elapsed.TotalMinutes,_timer.Elapsed.Seconds);

        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(WIDTH / 2 - 12, TopBoundary - 1);
        Console.Write($"  SCORE: {_score,-5} | TIME: {timeStr}  ");   
        _player.Draw();
        foreach (Obstacle obs in _obstacles)
            obs.Draw();
    }
    private void EndGame()
    {
        _timer.Stop();

        string finalTime = string.Format("{0:00}:{1:00}",
            (int)_timer.Elapsed.TotalMinutes,
            _timer.Elapsed.Seconds);

        Console.Beep(400, 300);  

      
        ScoreRecord record = new ScoreRecord(_playerName, _score, finalTime);
        bool saved = _db.SaveScore(record);      

       
        int midRow = TopBoundary + PLAY_HEIGHT / 2;
        Console.ForegroundColor = saved ? ConsoleColor.Green : ConsoleColor.Red;
        Console.SetCursorPosition(WIDTH / 2 - 14, midRow + 1);
        Console.Write(saved ? " Score saved to database! " : " Could not save to database. ");

        ShowLeaderboard(midRow + 3);

        Thread.Sleep(1500);   
    }
    private void ShowLeaderboard(int startRow)
    {
        List<ScoreRecord> top = _db.GetTopScores(5); 
        if (top.Count == 0) return;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(WIDTH / 2 - 10, startRow);
        Console.Write("  ── TOP SCORES ──");

        for (int i = 0; i < top.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WIDTH / 2 - 14, startRow + i + 1);
            Console.Write($"  {i + 1}. {top[i].PlayerName,-12} Score: {top[i].Score,-5} Time: {top[i].TimeSurvived}");
        }

        Console.ResetColor();
    }
}