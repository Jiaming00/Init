using Godot;

namespace AtomFlash.Input;

public delegate void MouseButtonReceiver(MouseButtonContext context);

public delegate void MouseMotionReceiver(MouseMotionContext context);

public struct MouseButtonEvent
{
    //顺序已逻辑严格化
    public ulong TimeStamp;
    public Vector2 Position;
    public int Code;
    public bool Pressed;
    public bool Canceled;
}

public struct MouseMotionEvent
{
    //顺序已逻辑严格化
    public ulong TimeStamp;
    public Vector2 Position;
    public Vector2 Relative;
    public Vector2 Velocity;
}

public class MouseButtonContext
{
    public const ulong TimeNone = 0;
    public bool Pressed;
    public ulong TimePress;
    public ulong TimeRelease;
    public ulong Duration;
    public Vector2 Position;
    public MouseButtonReceiver ReceiverPress;
    public MouseButtonReceiver ReceiverCancel;
    public MouseButtonReceiver ReceiverRelease;
}

public class MouseMotionContext
{
    public ulong TimeUpdateLast;
    public Vector2 Position;
    public Vector2 Relative;
    public Vector2 Velocity;
    public Vector2 PositionPrev;
    public MouseMotionReceiver ReceiverUpdate;
}

public class Mouse
{
    //index在Main中筛选
    public MouseButtonContext[] ButtonContextArr = [null, null, null, null, null, null, null, null, null, null];
    public MouseMotionContext MotionContext;

    public void ButtonInput(MouseButtonEvent e)
    {
    }

    public void MotionInput(MouseMotionEvent e)
    {
    }

 
}