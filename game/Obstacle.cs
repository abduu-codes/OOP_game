using System;

class Obstacle : GameObject
{
    private char _shape;
    private ConsoleColor _color;   
    public Obstacle(int startX, int startY, char symbol, ConsoleColor col) : base(startX, startY)
    {
        _shape = symbol;
        _color = col;
    }
    public void MoveDown()
    {
        _y++;
    }
    public bool IsOffScreen(int bottomBoundary)
    {
        return _y >= bottomBoundary;
    }
    public override void Draw()
    {
        Console.ForegroundColor = _color;
        Console.SetCursorPosition(_x, _y);
        Console.Write(_shape);
        Console.ResetColor();
    }
    public override void Clear()
    {
        Console.SetCursorPosition(_x, _y);
        Console.Write(" ");
    }
}
