using Godot;
using System;

namespace AtomFlash;

[GlobalClass]
public partial class GodotLoop : SceneTree
{
    //在此主循环中实现所有游戏性相关的内容

    public Game Game=null;
  

    public override void _Initialize()
    {
        //godot源码中,再window成为根节点前就调用了这个函数,所以逻辑是不对的,godot相关内容暂时在Root中实现
        //在此处实现游戏系统初始化
        //input 初始化
        //
    }

    public override bool _Process(double delta)
    {
        if (Game != null) 
            return Game.Porcess(delta);
        return false;
    }

    public override void _Finalize()
    {
        //程序退出
    }
}