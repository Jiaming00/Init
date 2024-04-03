using System.Numerics;

namespace AtomFlash.Input;


public interface IReceiverPress
{
    public void Press();
}

public interface IReceiverButton : IReceiverPress
{
    public void Release();
}

public interface IReceiverMouseButton
{
    public void Press(Vector2 position);
    public void Release(Vector2 position);
    public void Cancel(Vector2 position);
}

public interface IReceiverMouseMotion
{
   public void Update(Vector2 position);
}

public interface IReceiverControllerAxis
{
    public void Update(float value);
}

public interface IReceiverControllerStick 
{
    public void Update(float direction,float value);
}


public interface IReceiverUnHandledIndex
{
    //只接受update 和 press
    public void Handle(int index);
}

public interface IReceiverText
{
    public void Handle(string text);
}

public interface IReceiverControllerConnection
{
    public void Handle(int device, bool connected);
}

 




