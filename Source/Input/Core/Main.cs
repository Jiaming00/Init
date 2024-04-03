using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Intrinsics;
using AtomFlash.Numerics;
using Godot;
using Range = System.Range;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace AtomFlash.Input;

public class Main
{
    //键盘
    public static readonly HashSet<int> KeyPressSet = new();

    //鼠标
    public static readonly bool[] MouseButtonPressArr =
        [false, false, false, false, false, false, false, false, false, false];

    public static Vector2 MousePosition { get; private set; }

    //控制器
    public class ControllerData
    {
        public readonly bool[] ButtonPressArr =
        [
            false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false
        ];

        public class TriggerData
        {
            public float RealValue = 0;
            public float Value = 0;
            public float Origin = 0;
            public float Limit = 1;
        }

        public class StickData
        {
            //真实数据
            public float RealX = 0;
            public float RealY = 0;

            //原点
            public float OriginX = 0;
            public float OriginY = 0;
            public float OriginXMin = -1.0f / 8;
            public float OriginXMax = 1.0f / 8;
            public float OriginYMin = -1.0f / 8;
            public float OriginYMax = 1.0f / 8;

            //边界
            public float LimitXMin = -1.0f;
            public float LimitXMax = 1.0f;
            public float LimitYMin = -1.0f;
            public float LimitYMax = 1.0f;

            //基于以上原点和边界的分别映射的结果
            public float X = 0;
            public float Y = 0;

            //基于real x y 数据 计算得出的 
            public float Direction = float.NaN;
            public float Value = 0;

            //基于 realx y 数据 计算得出的 
            public float StraightX;
            public float StraightY;

            public float StickAngleRange = 30; //摇杆数据角度范围 度数
            public bool PhysicalDeadZone = true; //正交方向是否有物理死区
            public float LimiterRadius = 1.0f; //据我推测将此值应该是纵切映射的最大值,当然也可能不是
            public int Map = MapRawToBall; //表示 某个原始数据类型 映射到 某个目标数据类型
            public bool IgnoreZ = false; //是否忽略纵坐标,仅适用于球面映射和平直映射,
            
            
            public void InputRealXY(float _x, float _y)
            {
                double x = _x;
                double y = _y;
            
            }
        }

        public const float StickDetectAngleAccuracy = 0.5f;

        public const int MapNone = 0;
        public const int MapRawToBall = 1;
        public const int MapRawToStraight = 2;
        public const int MapBallToStraight = 3;
        public const int MapStraightToBall = 4;
        public const int MapBallToRaw = 5;
        public const int MapStraightToRaw = 6;

        public readonly float[] AxisValueArr = [0, 0, 0, 0, 0, 0];
        public readonly TriggerData[] TriggerStateArr = [null, null, null, null, new(), new()];

        public readonly StickData[] StickerStateArr = [new(), null, new(), null];


    }

    private static readonly Dictionary<int, ControllerData> ControllerDataDict = [];

    public static ControllerData GetControllerData(int device)
    {
        if (ControllerDataDict.TryGetValue(device, out var controller))
        {
            return controller;
        }
        else
        {
            var deviceIdArr = Godot.Input.GetConnectedJoypads();
            if (deviceIdArr.Contains(device))
            {
                controller = new();
                ControllerDataDict.Add(device, controller);
                return controller;
            }
            else
            {
                return new ControllerData();
            }
        }
    }


    public static void VibrationStart(int device, float duration, float weak, float strong) =>
        Godot.Input.StartJoyVibration(device, weak, strong, duration);

    public static void VibrationStop(int device) => Godot.Input.StopJoyVibration(device);
    //未采用的功能:获取当前振动的时长和强度,而且获取振动的开始时间的函数在godot中没有注册到脚本程序中.

    public static readonly ConcurrentQueue<TimeEvent> EventQueue = new();
    public static void ConnectEventQueue() => Game.Window.WindowInput += EventQueueEnter;
    public static void DisconnectEventQueue() => Game.Window.WindowInput -= EventQueueEnter;

    private static void EventQueueEnter(InputEvent e)
    {
        if (e is InputEventFromWindow window)
        {
            if (window is InputEventWithModifiers modifiers)
            {
                if (modifiers is InputEventKey key)
                {
                    //此处还未筛选键盘key值
                    var physicalKey = (int)key.PhysicalKeycode;
                    var unicode = (int)key.Unicode;
                    KeyEvent ke = new()
                    {
                        TimeStamp = Time.GetTicksUsec(),
                        Index = physicalKey,
                        Pressed = key.Pressed,
                        Unicode = unicode
                    };
                    if (KeyPressSet.TryGetValue(ke.Index, out var index) != ke.Pressed)
                    {
                        if (ke.Pressed)
                        {
                            KeyPressSet.Remove(ke.Index);
                        }
                        else
                        {
                            KeyPressSet.Add(ke.Index);
                        }
                    }
                    else if (AutoSkipRepeatEvent)
                    {
                        return;
                    }

                    EventQueue.Enqueue(ke);
                }
                else if (modifiers is InputEventMouse mouse)
                {
                    if (mouse is InputEventMouseButton button)
                    {
                        //只支持常见的鼠标功能
                        var index = (int)button.ButtonIndex;
                        if ((index is <= Index.MouseButtonNone or >= Index.MouseButtonCount
                                or Index.MouseButtonWheelLeft
                                or Index.MouseButtonWheelRight)) return;
                        MouseButtonEvent mbe = new()
                        {
                            TimeStamp = Time.GetTicksUsec(),
                            Index = index,
                            Pressed = button.Pressed,
                            Canceled = button.Canceled,
                            Position = new Vector2(button.Position.X, button.Position.Y)
                        };

                        if (MouseButtonPressArr[index] != mbe.Pressed)
                        {
                            MouseButtonPressArr[index] = mbe.Pressed;
                        }
                        else if (AutoSkipRepeatEvent)
                        {
                            return;
                        }

                        MousePosition = mbe.Position;
                        EventQueue.Enqueue(mbe);
                    }
                    else if (mouse is InputEventMouseMotion motion)
                    {
                        MouseMotionEvent mme = new()
                        {
                            TimeStamp = Time.GetTicksUsec(),
                            Position = new Vector2(motion.Position.X, motion.Position.Y)
                        };
                        MousePosition = mme.Position;
                        EventQueue.Enqueue(mme);
                    }
                }
                //gesture的两个事件的数据只精确到两次更新之间,存在误差积累,所有如果有需求就自己实现
            }
            //没有相关设备,所以无法测试
            // else if (window is InputEventScreenTouch touch)
            // { InputScreenTouch(touch);  }
            // else if (window is InputEventScreenDrag drag)
            // { InputScreenDrag(drag); }
        }
        else
        {
            if (e is InputEventJoypadButton button)
            {
                var index = (int)button.ButtonIndex;
                if (index is <= Index.ControllerButtonInvalid or >= Index.ControllerAxisCount) return;
                ControllerButtonEvent cbe = new()
                {
                    TimeStamp = Time.GetTicksUsec(),
                    Device = button.Device,
                    Index = index,
                    Pressed = button.Pressed
                };
                if (!ControllerDataDict.TryGetValue(cbe.Device, out var controller))
                {
                    controller = new ControllerData();
                    ControllerDataDict.Add(cbe.Device, controller);
                }

                if (controller.ButtonPressArr[cbe.Index] != cbe.Pressed)
                {
                    controller.ButtonPressArr[cbe.Index] = cbe.Pressed;
                }
                else if (AutoSkipRepeatEvent)
                {
                    return;
                }

                EventQueue.Enqueue(cbe);
            }
            else if (e is InputEventJoypadMotion motion)
            {
                var index = (int)motion.Axis;
                if (index is <= Index.ControllerAxisInvalid or >= Index.ControllerAxisCount) return;
                ControllerAxisEvent cae = new()
                {
                    TimeStamp = Time.GetTicksUsec(),
                    Device = motion.Device,
                    Index = index,
                    Value = motion.AxisValue
                };
                if (!ControllerDataDict.TryGetValue(cae.Device, out var controller))
                {
                    controller = new ControllerData();
                    ControllerDataDict.Add(cae.Device, controller);
                }

                if (Math.Abs(controller.AxisValueArr[index] - cae.Value) > float.Epsilon)
                {
                    controller.AxisValueArr[index] = cae.Value;
                }
                else if (AutoSkipRepeatEvent)
                {
                    return;
                }

                EventQueue.Enqueue(cae);
            }
        }
    }

    public static bool AutoSkipRepeatEvent = true;

    public Keyboard Keyboard = new();
    public Mouse Mouse = new();
    public Dictionary<int, Controller> ControllerDict = [];
    public IReceiverControllerConnection ControllerConnectionReceiver;


    public Main()
    {
    }

    ~Main()
    {
    }


    public void FlushEventQueue()
    {
        while (EventQueue.TryDequeue(out var e))
        {
            switch (e)
            {
                case KeyEvent ke:
                    InputKey(ke);
                    break;
                case MouseButtonEvent mbe:
                    InputMouseButton(mbe);
                    break;
                case MouseMotionEvent mme:
                    InputMouseMotion(mme);
                    break;
                case ControllerButtonEvent cbe:
                    InputControllerButton(cbe);
                    break;
                case ControllerAxisEvent cae:
                    InputControllerAxis(cae);

                    if (cae.Index <= Index.ControllerAxisRightY)
                    {
                        ControllerData controller = GetControllerData(cae.Device);
                        ControllerStickEvent cse = new();
                        cse.TimeStamp = cae.TimeStamp;
                        cse.Device = cae.Device;
                        float realX, realY ;
                        if (EventQueue.TryPeek(out var e2)
                            && e2 is ControllerAxisEvent cae2
                            && cae.Device == cae2.Device
                            && (
                                (cae.Index == Index.ControllerAxisLeftX &&
                                 cae2.Index == Index.ControllerAxisLeftY)
                                ||
                                (cae.Index == Index.ControllerAxisRightX &&
                                 cae2.Index == Index.ControllerAxisRightY)
                            )
                           )
                        {
                            EventQueue.TryDequeue(out e2);
                            realX = cae.Value;
                            realY = cae2.Value;
                            cse.Index = cae.Index == Index.ControllerAxisLeftX
                                ? Index.ControllerStickLeft
                                : Index.ControllerStickRight;
                        }
                        else if (cae.Index is Index.ControllerAxisLeftX or Index.ControllerAxisRightX)
                        {
                            realX = cae.Value;
                            realY = controller.AxisValueArr[cae.Index + 1];
                            cse.Index = cae.Index == Index.ControllerAxisLeftX
                                ? Index.ControllerStickLeft
                                : Index.ControllerStickRight;
                        }
                        else
                        {
                            realX = controller.AxisValueArr[cae.Index - 1];
                            realY = cae.Value;
                            cse.Index = cae.Index == Index.ControllerAxisLeftY
                                ? Index.ControllerStickLeft
                                : Index.ControllerStickRight;
                        } 
                        var sd = controller.StickerStateArr[cse.Index];
                        sd.InputRealXY(realX,realY);
                        cse.X = sd.X;
                        cse.Y = sd.Y;
                        cse.Value = sd.Value;
                        cse.Direction = sd.Direction;
                        cse.StraightX = sd.StraightX;
                        cse.StraightY = sd.StraightY;
                        InputControllerStick(cse);
                    } 
                    break;
            }
        }
    }


    private static readonly string TextNone = new([(char)0]);

    private void InputKey(KeyEvent e)
    {
        if (Keyboard == null) return;
        if (!e.Echo)
        {
            var keyReceiver = Keyboard.KeyReceiverDict?[e.Index];
            if (keyReceiver != null)
            {
                if (e.Pressed)
                {
                    keyReceiver.Press();
                }
                else
                {
                    keyReceiver.Release();
                }

                return;
            }
        }

        if (Keyboard.TextReceiver != null)
        {
            var charString = char.ConvertFromUtf32((int)e.Unicode);
            if (charString != TextNone)
            {
                Keyboard.TextReceiver.Handle(charString);
                return;
            }
        }

        if (!e.Echo && e.Pressed)
        {
            Keyboard.UnHandledKeyReceiver?.Handle(e.Index);
        }
    }

    private void InputMouseButton(MouseButtonEvent e)
    {
        if (Mouse == null) return;
        if (e.Index is < 1 or > 9) return;
        var receiver = Mouse.ButtonReceiverArr?[e.Index];
        if (receiver != null)
        {
            if (e.Pressed)
            {
                if (!e.Canceled)
                {
                    receiver.Press(e.Position);
                }
                else
                {
                    receiver.Cancel(e.Position);
                }
            }
            else if (!e.Canceled)
            {
                receiver.Release(e.Position);
            }

            return;
        }

        if (e.Pressed && !e.Canceled)
        {
            Mouse.UnHandledButtonReceiver.Handle(e.Index);
        }
    }

    private void InputMouseMotion(MouseMotionEvent e)
    {
        Mouse?.MotionReceiver?.Update(e.Position);
    }


    private void InputControllerButton(ControllerButtonEvent e)
    {
        var controller = ControllerDict?[e.Device];
        if (controller == null) return;
        var receiver = controller.ButtonReceiverArr[e.Index];
        if (receiver != null)
        {
            if (e.Pressed)
            {
                receiver.Press();
            }
            else
            {
                receiver.Release();
            }

            return;
        }

        if (e.Pressed)
        {
            controller.UnHandledButtonReceiver?.Handle(e.Index);
        }
    }

    private void InputControllerAxis(ControllerAxisEvent e)
    {
        if (!ControllerDict.TryGetValue(e.Device, out var controller)) return;
        var controller = ControllerDict?[e.Device];
        if (controller == null) return;
        if (e.Index is Index.ControllerAxisTriggerLeft)
        {
            var receiver = controller.TriggerLeftReceiver;
            if (receiver != null)
            {
            }
        }
        else
        {
        }
    }

    private void InputControllerStick(ControllerStickEvent e)
    {
    }
}