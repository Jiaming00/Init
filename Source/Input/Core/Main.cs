using System;
using System.Collections.Generic;
using Godot;


namespace AtomFlash.Input;

public class Main
{
    public Keyboard Keyboard = new();
    public Mouse Mouse = new();
    public Dictionary<int, JoyPad> JoyPadMap = new();
    
      

    public void Input(InputEvent e) //输入回调， 进行事件分类分流
    {
        if (e is InputEventFromWindow)
        {
            if (e is InputEventWithModifiers)
            {
                if (e is InputEventKey)
                {
                    InputKey(e as InputEventKey);
                }
                else if (e is InputEventMouse)
                {
                    if (e is InputEventMouseButton)
                    {
                        InputMouseButton(e as InputEventMouseButton);
                    }

                    if (e is InputEventMouseMotion)
                    {
                        InputMouseMotion(e as InputEventMouseMotion);
                    }
                }
                else if (e is InputEventGesture)
                {
                    //这两个事件的数据只精确到两次更新之间,存在误差积累,所有如果有需求就自己实现
                    // if (e is InputEventPanGesture)
                    // {
                    //     //平移
                    // }
                    // else if (e is InputEventMagnifyGesture)
                    // {
                    //     //缩放
                    // }
                }
            }
            else if (e is InputEventScreenTouch)
            {
                InputScreenTouch(e as InputEventScreenTouch);
            }
            else if (e is InputEventScreenDrag)
            {
                InputScreenDrag(e as InputEventScreenDrag);
            }
        }
        else
        {
            if (e is InputEventJoypadButton)
            {
                InputJoyPadButton(e as InputEventJoypadButton);
            }
            else if (e is InputEventJoypadMotion)
            {
                InputJoyPadMotion(e as InputEventJoypadMotion);
            }
        }
    }

    public void Process(double delta)
    {
        
    }

    public void JoypadStickProcess()
    {
        
    }


    private static readonly string TextNone = new([(char)0]);

    private void InputKey(InputEventKey e)
    {
        var keyEventIsValid = false;
        if (!e.Echo)
        {
            KeyEvent eKey;
            eKey.TimeStamp = Game.USecFrameStart;
            eKey.Code = (int)e.PhysicalKeycode;
            eKey.Pressed = e.Pressed;
            keyEventIsValid=Keyboard.KeyInput(eKey);
        }

        if (keyEventIsValid) return;
        var charString = char.ConvertFromUtf32((int)e.Unicode);
        if (charString == TextNone) return;
        CharEvent eChar;
        eChar.Char = charString;
        Keyboard.CharInput(eChar);
    }

    private void InputMouseButton(InputEventMouseButton e)
    {
        var index = (int)e.ButtonIndex;
        if (index is < 1 or > 9) return;
        MouseButtonEvent mbe;
        mbe.TimeStamp = Game.USecFrameStart;
        mbe.Position = e.Position;
        mbe.Code = index;
        mbe.Pressed = e.Pressed;
        mbe.Canceled = e.Canceled;
        Mouse.ButtonInput(mbe);
    }

    private void InputMouseMotion(InputEventMouseMotion e)
    {
        MouseMotionEvent mme;
        mme.TimeStamp = Game.USecFrameStart;
        mme.Position = e.Position;
        mme.Relative = e.Relative;
        mme.Velocity = e.Velocity;
        Mouse.MotionInput(mme);
    }

    private void InputJoyPadButton(InputEventJoypadButton e)
    {
        if (!JoyPadMap.TryGetValue(e.Device, out var joyPad)) return;
        var index = (int)e.ButtonIndex;
        if (index is < 00 or > 20) return;
        JoyPadButtonEvent jbe;
        jbe.Code = index;
        jbe.Pressed = e.Pressed;
        joyPad.ButtonInput(jbe);
    }

    private void InputJoyPadMotion(InputEventJoypadMotion e)
    {
        if (!JoyPadMap.TryGetValue(e.Device, out var joyPad)) return;
        var axis = (int)e.Axis;
        if (axis is >= 0 and <= 3)
        { 
        }else if (axis is 4 or 5 )
        {
            
        }
        else
        {
            return;
        } 
    }

    private void InputScreenTouch(InputEventScreenTouch e)
    {
    }

    private void InputScreenDrag(InputEventScreenDrag e)
    {
    }
}