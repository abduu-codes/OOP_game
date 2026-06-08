using System;


abstract class GameObject
{
    protected int _x;
    protected int _y;

   
    public int X
    { get { return _x; } }
    public int Y 
    { get { return _y; } }

    public GameObject(int startX, int startY)
    {
        _x = startX;
        _y = startY;
    }
    public abstract void Draw();
    public abstract void Clear();
}
