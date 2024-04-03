using System.Collections.Generic;
using Godot;

namespace AtomFlash.Input;




public class Keyboard
{
    public Dictionary<int, IReceiverButton> KeyReceiverDict = [];
    public IReceiverUnHandledIndex UnHandledKeyReceiver;
    
    public IReceiverText TextReceiver; 
}

public class Mouse
{
    public IReceiverMouseButton[] ButtonReceiverArr =
        [null, null, null, null, null, null, null, null, null, null];
    public IReceiverUnHandledIndex UnHandledButtonReceiver;
    
    public IReceiverMouseMotion MotionReceiver;
}


public class Controller
{
    public IReceiverButton[] ButtonReceiverArr =
    [
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
        null, null, null, null
    ];
    public IReceiverUnHandledIndex UnHandledButtonReceiver;
    
    public IReceiverControllerAxis[] AxisReceiverArr = [null, null, null, null, null, null];
    public IReceiverControllerStick[] StickReceiverArr = [null, null, null, null]; 
}

