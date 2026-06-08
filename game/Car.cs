using System;
class Car : GameObject
{
    private string _carShape = " [=O=] ";      
    private ConsoleColor _color = ConsoleColor.Cyan;

    public Car(int startX, int startY) : base(startX, startY)
    {
    }
    public void Move(ConsoleKey key, int screenWidth, int screenBottom, int topBoundary)
    {
        if (key == ConsoleKey.LeftArrow && _x > 2) _x--;
        if (key == ConsoleKey.RightArrow && _x < screenWidth - 8) _x++;
        if (key == ConsoleKey.UpArrow && _y > topBoundary + 2) _y--;
        if (key == ConsoleKey.DownArrow && _y < screenBottom - 2) _y++;
    }
    public bool CollidesWith(Obstacle obs)
    {
        return obs.Y == _y && obs.X >= _x && obs.X <= _x + 6;
    }
    public override void Draw()
    {
        Console.ForegroundColor = _color;
        Console.SetCursorPosition(_x, _y);
        Console.Write(_carShape);
        Console.ResetColor();
    }

    public override void Clear()
    {
        Console.SetCursorPosition(_x, _y);
        Console.Write("       ");
    }
}
