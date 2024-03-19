using Godot;

namespace AtomFlash;

public class Game
{
    private static GodotLoop GodotLoop;
    private static Game _Singleton;
    private ulong _FrameStartUSec;

    public Game(GodotLoop GodotLoop)
    {
        Game.GodotLoop = GodotLoop;
        _Singleton = this;
    }

    private static Game Singleton => _Singleton;
    public static Window Window => GodotLoop.Root;
    public static long FrameCurrent => GodotLoop.GetFrame();
    public static ulong USecFrameStart => Singleton._FrameStartUSec;


    public void Initialize()
    {
        
    }
    public bool Porcess(double delta)
    {
        _FrameStartUSec = Time.GetTicksUsec();

        return false;
    }

    public void Finalize()
    {
        
    }
    
}