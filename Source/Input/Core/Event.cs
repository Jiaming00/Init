using System.Numerics;

namespace AtomFlash.Input;

public class TimeEvent
{
    public ulong TimeStamp;
}
public class KeyEvent:TimeEvent
{
    public int Index;
    public bool Echo;
    public bool Pressed;
    public int Unicode;
}

public class MouseButtonEvent:TimeEvent
{
    public int Index;
    public bool Pressed;
    public bool Canceled;
    public Vector2 Position; 
}

public class MouseMotionEvent:TimeEvent
{
    public Vector2 Position;
}

public class ControllerButtonEvent:TimeEvent
{
    public int Device;
    public int Index;
    public bool Pressed;
}

public class ControllerAxisEvent:TimeEvent
{ 
    public int Device;
    public int Index;
    public float Value;
}

public class ControllerStickEvent:ControllerAxisEvent
{   
    public float Direction;
    public float StraightX;
    public float StraightY;
    public float X;
    public float Y;
}