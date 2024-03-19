using System;
using System.Collections.Generic;
 


namespace AtomFlash.Input;

public delegate void KeyReceiver(KeyContext context);
public delegate void TextReceiver(TextContext context);

public struct KeyEvent
{ 
    //顺序已逻辑严格化
    public ulong TimeStamp;
    public int Code; 
    public bool Pressed; 
}

public struct CharEvent
{
    //顺序已逻辑严格化
    public string Char;
}


public class KeyContext
{
    public const ulong TimeNone = 0;
    public bool Pressed;
    public ulong TimePress;
    public ulong TimeRelease;
    public ulong Duration; 
    public KeyReceiver ReceiverPress;
    public KeyReceiver ReceiverRelease;
}
public class TextContext
{
    public string Row = String.Empty;
    public LinkedList<string> CharList = new();
    public TextReceiver Receiver;
}

public class Keyboard
{
    public Dictionary<int, KeyContext> KeyContextMap = new();
    public TextContext TextContext = new();
    public bool AutoSkipRepeating = true;
    
    public bool KeyInput(KeyEvent e)
    {
        if (!KeyContextMap.TryGetValue(e.Code, out KeyContext Context)) return false;
        if (AutoSkipRepeating && e.Pressed == Context.Pressed) return false;
        Context.Pressed = !Context.Pressed;
        if (Context.Pressed)
        {
            Context.TimePress = e.TimeStamp;
            Context.TimeRelease = KeyContext.TimeNone;
            Context.Duration = KeyContext.TimeNone;
            Context.ReceiverPress(Context);
        }
        else
        { 
            Context.TimeRelease =  e.TimeStamp;
            Context.Duration = Context.TimeRelease-Context.TimePress;
            Context.ReceiverRelease(Context);
        }
        return true;
    }
    
    public void CharInput(CharEvent e)
    {
        TextContext.CharList.AddLast(e.Char);
        TextContext.Row = string.Concat(TextContext.Row, TextContext.CharList.Last);
        TextContext.Receiver(TextContext);
    }
}