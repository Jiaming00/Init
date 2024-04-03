
using Godot;

namespace AtomFlash.Input;

public static class Index
{
     //还需要把索引映射为字符串
     //除了默认设置(游戏开发中)需要的索引,其他都是系统中自动传递的整数值,不需要在此特殊的转换
     //键盘
     public const int Space = (int)Key.Space;
     
     //鼠标
     public const int MouseButtonNone = (int)MouseButton.None;
     public const int MouseButtonLeft = (int)MouseButton.Left;
     public const int MouseButtonRight = (int)MouseButton.Right;
     public const int MouseButtonMiddle = (int)MouseButton.Middle;
     public const int MouseButtonWheelUp= (int)MouseButton.WheelUp;
     public const int MouseButtonWheelDown = (int)MouseButton.WheelDown;
     public const int MouseButtonWheelLeft = (int)MouseButton.WheelLeft;
     public const int MouseButtonWheelRight= (int)MouseButton.WheelRight;
     public const int MouseButtonExtra1 = (int)MouseButton.Xbutton1;
     public const int MouseButtonExtra2 = (int)MouseButton.Xbutton2;
     public const int MouseButtonMax = MouseButtonExtra2;
     public const int MouseButtonCount= 9; 
     
     //控制器
     public const int ControllerButtonInvalid = (int)JoyButton.Invalid;
     public const int ControllerButtonA = (int)JoyButton.A;
     public const int ControllerButtonB = (int)JoyButton.B;
     public const int ControllerButtonX = (int)JoyButton.X;
     public const int ControllerButtonY = (int)JoyButton.Y;
     public const int ControllerButtonBack = (int)JoyButton.Back;
     public const int ControllerButtonHome = (int)JoyButton.Guide;
     public const int ControllerButtonMenu = (int)JoyButton.Start;
     public const int ControllerButtonLeftStick = (int)JoyButton.LeftStick;
     public const int ControllerButtonRightStick= (int)JoyButton.RightStick;
     public const int ControllerButtonLeftShoulder = (int)JoyButton.LeftShoulder;
     public const int ControllerButtonRightShoulder= (int)JoyButton.RightShoulder;
     public const int ControllerButtonUp = (int)JoyButton.DpadUp;
     public const int ControllerButtonDown = (int)JoyButton.DpadDown;
     public const int ControllerButtonLeft = (int)JoyButton.DpadLeft;
     public const int ControllerButtonRight = (int)JoyButton.DpadRight;
     public const int ControllerButtonMisc1 = (int)JoyButton.Misc1;
     public const int ControllerButtonPaddle1 = (int)JoyButton.Paddle1;
     public const int ControllerButtonPaddle2= (int)JoyButton.Paddle2;
     public const int ControllerButtonPaddle3= (int)JoyButton.Paddle3;
     public const int ControllerButtonPaddle4= (int)JoyButton.Paddle4;
     public const int ControllerButtonTouchPad= (int)JoyButton.Touchpad;
     public const int ControllerButtonMax= ControllerButtonTouchPad;
     public const int ControllerButtonCount= ControllerButtonMax;
    
     
     public const int ControllerAxisInvalid = (int)JoyAxis.Invalid;
     public const int ControllerStickLeft = (int)JoyAxis.LeftX;
     public const int ControllerStickLeftDirection = (int)JoyAxis.LeftX;
     public const int ControllerStickLeftValue = (int)JoyAxis.LeftY;
     public const int ControllerAxisLeftX = (int)JoyAxis.LeftX;
     public const int ControllerAxisLeftY = (int)JoyAxis.LeftY;
     public const int ControllerStickRight = (int)JoyAxis.RightX;
     public const int ControllerStickRightDirection = (int)JoyAxis.RightX;
     public const int ControllerStickRightValue = (int)JoyAxis.RightY;
     public const int ControllerAxisRightX = (int)JoyAxis.RightX;
     public const int ControllerAxisRightY = (int)JoyAxis.RightY;
     public const int ControllerAxisTriggerLeft = (int)JoyAxis.TriggerLeft;
     public const int ControllerAxisTriggerRight= (int)JoyAxis.TriggerRight;
     public const int ControllerAxisMax = ControllerAxisTriggerRight;
     public const int ControllerAxisCount =ControllerAxisMax;

}