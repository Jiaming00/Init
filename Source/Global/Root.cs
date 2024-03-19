using System;
using System.Globalization;
using Godot;
using System.Text;


namespace AtomFlash;

public partial class Root : Node
{
    //在节点树中实现与游戏性无关的功能
    public override void _Ready()
    {
        var GodotLoop = GetTree() as GodotLoop;
        var Game = new Game(GodotLoop);
        GodotLoop.Game = Game;
        Game.Initialize();
        //在此处对godot相关内容进行初始化
    }

    public override void _Input(InputEvent e)
    {
        if (e is InputEventJoypadMotion)
        {
            var jm = e as InputEventJoypadMotion;
            GD.Print(jm.Axis, " , ", jm.AxisValue, " , ", jm.AsText());
        }
        else
            GD.Print(e);
    }


    // public override void _Process(double delta)
    // {
    // 	
    // }
}