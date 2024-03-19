 

using System.Numerics;

namespace AtomFlash.Input;




public struct ScreenTouchEvent
{ 
    //顺序已逻辑严格化
    public ulong TimeStamp;
    public int Index;
    public Vector2 Position;
    public bool Pressed;
    public bool Cancel; 
}

public struct ScreenDragEvent
{ 
    //顺序已逻辑严格化
    public ulong TimeStamp;
    public int Index;
    public Vector2 Position;
    public Vector2 Relative ;
    public Vector2 Velocity ;
}

public class Screen
{
    
}