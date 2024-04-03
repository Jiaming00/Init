using Godot;

namespace AtomFlash;

[GlobalClass]
public partial class Loop : SceneTree
{
    //在此主循环中实现所有游戏性相关的内容

    public Game Game { get; private set; } = null; 

    public override void _Initialize()
    {
        //godot源码中,再window成为根节点前就调用了这个函数,如果某些操作无效,就在root 节点中实现
        Game.Loop = this;
        Game = new Game();
    }

    public override bool _Process(double delta)
    { 
        if (Game == null) return true;
        if (Game.VisionFrameBegin(delta)) return true;
        while (Time.GetTicksUsec() - Game.USecVisionFrameBegin < (delta * 0.9375)) //暂时定的阈值,之后要改为动态调整的预测值?
        {
            if (Game.Process(delta)) return true;
        }
        if (Game.VisionFrameEnd(delta)) return true;
        return false;
    }

    public override void _Finalize()
    {
        //程序退出
    }
}