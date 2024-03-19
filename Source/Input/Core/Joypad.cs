using Godot;

namespace AtomFlash.Input;

public delegate void JoyPadButtonReceiver(JoyPadButtonContext context);
public delegate void JoyPadAxisReceiver(JoyPadAxisContext context);
public delegate void JoyPadStickReceiver(JoyPadAxisContext context);


public struct JoyPadButtonEvent
{
    //顺序已逻辑严格化
    public int Code;
    public bool Pressed;
}

public struct JoyPadMotionEvent
{
    //顺序已逻辑严格化
    public int Code;
    public float Value;
}

public class JoyPadButtonContext
{
    public const ulong TimeNone = 0;
    public bool Pressed;
    public ulong TimePress;
    public ulong TimeRelease;
    public ulong Duration; 
    public JoyPadButtonReceiver ReceiverPress;
    public JoyPadButtonReceiver ReceiverRelease;
}

public class JoyPadAxisContext
{
    
}
public class JoyPadStickContext
{
    
}

public class JoyPad
{
    //index在Main中筛选
    public JoyPadButtonContext[] ButtonArr =
    [
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
        null, null, null, null
    ];
    //index在Main中筛选
    public JoyPadAxisContext[] AxisArr = [null, null, null, null, null, null, null, null, null, null, null];
    
    //index在Main中筛选
    public JoyPadStickContext[] StickArr = [null, null, null, null, null, null, null, null, null, null, null];
    
    public void ButtonInput(JoyPadButtonEvent e)
    {
        
    }
    public void MotionInput(JoyPadMotionEvent e)
    {
        
    }
 
}