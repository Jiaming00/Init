using Godot;

namespace AtomFlash;

public partial class Root : Node
{
    //在节点树中实现与游戏性无关的功能
    public override void _Ready()
    {
        //在此处对godot相关内容进行初始化
        Godot.Input.UseAccumulatedInput = false;
        var loop = GetTree() as Loop;
        loop?.Game.Ready();
        
    }

    // public override void _Input(InputEvent e)
    // {
    //     if (e is InputEventJoypadMotion)
    //     {
    //         var jm = e as InputEventJoypadMotion;
    //         GD.Print(jm.Axis, " , ", jm.AxisValue, " , ", jm.AsText());
    //     }
    //    
    // }


    public override void _Process(double delta)
    {
        var controllerArr = Godot.Input.GetConnectedJoypads();
        foreach (var controllerID in controllerArr)
        {
            GD.Print(Godot.Input.GetJoyAxis(controllerID,JoyAxis.LeftX)," , ",Godot.Input.GetJoyAxis(controllerID,JoyAxis.LeftY));
        }
    }
}