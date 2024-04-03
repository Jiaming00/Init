 
using Godot;

namespace AtomFlash;

public class Game
{
    private static Loop _loop;
    public static Loop Loop
    {
        get => _loop;
        set => _loop ??= value;
    }

    
    public static Window Window => Loop.Root;
    public long VisionFrame { get; private set; }
    public ulong USecVisionFrameBegin { get; private set; }
    public ulong USecGameFrameBegin { get; private set; }

    public Input.Main Input;
     


    public Game( )
    { 
    }

    public void Ready()
    {
        
    }

    public bool VisionFrameBegin(double delta)
    {
        VisionFrame = Loop.GetFrame();
        USecVisionFrameBegin = Time.GetTicksUsec();
        return false;
    }

    public bool Process(double delta)
    {
        USecGameFrameBegin = Time.GetTicksUsec();
        DisplayServer.ProcessEvents();
        return false;
    }
    
    public bool VisionFrameEnd(double delta)
    { 
        return false;
    }


    ~Game()
    {
        
    }
}