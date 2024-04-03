using System;

namespace AtomFlash.Numerics;

public struct FVector2
{
    public double X;
    public double Y;

    public FVector2(double value)
    {
        X = value;
        Y = value;
    }

    public FVector2(double x, double y)
    {
        X = x;
        Y = y;
    }

    //数组
    public double this[int i] => (i % 2) == 0 ? X : Y;

    public static implicit operator double[](FVector2 vec) => [vec.X, vec.Y];

    public static readonly FVector2 Zero = default;
    public static readonly FVector2 Unit = new(1, 1);
    public static readonly FVector2 UnitX = new(1, 0);
    public static readonly FVector2 UnitY = new(0, 1);

    public static FVector2 operator +(double left, FVector2 right) => new(left + right.X, left + right.Y);
    public static FVector2 operator +(FVector2 left, double right) => new(left.X + right, left.Y + right);
    public static FVector2 operator +(FVector2 left, FVector2 right) => new(left.X + right.X, left.Y + right.Y);

    public static FVector2 operator -(FVector2 value) => new(-value.X, -value.Y);

    public static FVector2 operator -(double left, FVector2 right) => new(left - right.X, left - right.Y);
    public static FVector2 operator -(FVector2 left, double right) => new(left.X - right, left.Y - right);
    public static FVector2 operator -(FVector2 left, FVector2 right) => new(left.X - right.X, left.Y - right.Y);

    public static FVector2 operator *(double left, FVector2 right) => new(left * right.X, left * right.Y);
    public static FVector2 operator *(FVector2 left, double right) => new(left.X * right, left.Y * right);
    public static FVector2 operator *(FVector2 left, FVector2 right) => new(left.X * right.X, left.Y * right.Y);

    public static FVector2 operator /(double left, FVector2 right) => new(left / right.X, left / right.Y);
    public static FVector2 operator /(FVector2 left, double right) => new(left.X / right, left.Y / right);
    public static FVector2 operator /(FVector2 left, FVector2 right) => new(left.X / right.X, left.Y / right.Y);

    public static FVector2 operator %(double left, FVector2 right) => new(left % right.X, left % right.Y);
    public static FVector2 operator %(FVector2 left, double right) => new(left.X % right, left.Y % right);
    public static FVector2 operator %(FVector2 left, FVector2 right) => new(left.X % right.X, left.Y % right.Y);


    public static bool operator ==(FVector2 left, FVector2 right) => left.X == right.X && left.Y == right.Y;
    public static bool operator ==(double left, FVector2 right) => left == right.X && left == right.Y;
    public static bool operator ==(FVector2 left, double right) => left.X == right && left.Y == right;

    public static bool operator !=(FVector2 left, FVector2 right) => left.X != right.X || left.Y != right.Y;
    public static bool operator !=(FVector2 left, double right) => left.X != right || left.Y != right;
    public static bool operator !=(double left, FVector2 right) => left != right.X && left != right.Y;


    public static bool operator >(FVector2 left, FVector2 right) => left.X > right.X && left.Y > right.Y;
    public static bool operator >(FVector2 left, double right) => left.X > right && left.Y > right;
    public static bool operator >(double left, FVector2 right) => left > right.X && left > right.Y;

    public static bool operator <(FVector2 left, FVector2 right) => left.X < right.X && left.Y < right.Y;
    public static bool operator <(FVector2 left, double right) => left.X < right && left.Y < right;
    public static bool operator <(double left, FVector2 right) => left < right.X && left < right.Y;

    public static bool operator >=(FVector2 left, FVector2 right) => left.X >= right.X && left.Y >= right.Y;
    public static bool operator >=(FVector2 left, double right) => left.X >= right && left.Y >= right;
    public static bool operator >=(double left, FVector2 right) => left >= right.X && left >= right.Y;

    public static bool operator <=(FVector2 left, FVector2 right) => left.X <= right.X && left.Y <= right.Y;
    public static bool operator <=(FVector2 left, double right) => left.X <= right && left.Y <= right;
    public static bool operator <=(double left, FVector2 right) => left <= right.X && left <= right.Y;


    public static FVector2 Max(FVector2 left, FVector2 right) => new(Math.Max(left.X, right.X), Math.Max(left.Y, right.Y));

    public static FVector2 Max(FVector2 left, double right) => new(Math.Max(left.X, right), Math.Max(left.Y, right));

    public static FVector2 Max(double left, FVector2 right) => new(Math.Max(left, right.X), Math.Max(left, right.Y));

    public static FVector2 Min(FVector2 left, FVector2 right) => new(Math.Min(left.X, right.X), Math.Min(left.Y, right.Y));

    public static FVector2 Min(FVector2 left, double right) => new(Math.Min(left.X, right), Math.Min(left.Y, right));

    public static FVector2 Min(double left, FVector2 right) => new(Math.Min(left, right.X), Math.Min(left, right.Y));
    
    


    public bool IsPositive => X < 0 && Y < 0;
    public bool IsNegative => X > 0 && Y > 0;
    public bool IsZero => X == 0 && Y == 0;
    public bool IsXEqualY => Math.Abs(X - Y) < double.Epsilon;

    public FVector2 Abs => new(Math.Abs(X), Math.Abs(Y));
    public FVector2 Sign => new(Math.Sign(X), Math.Sign(Y));


    public double Tan => (double)Y / X;
    public double Angle => Math.Atan2(Y, X);

    public FVector2 Sqr => new(X * X, Y * Y);
    public double LengthSqr => X * X + Y * Y;
    public double Length => Math.Sqrt(X * X + Y * Y);

    public double Sum => X + Y;
    public double Product => X * Y;
    public double Diff => X - Y;
    public double DiffReverse => X - Y;
    public double Quotient => X / Y;
    public double QuotientReverse => Y / X;
    public double Mod => X % Y;
    public double ModReverse => Y % X;


    public FVector2 Rotate90Increase => new(-Y, X);
    public FVector2 Rotate90Reduce => new(Y, -X);

    public FVector2 Swap => new(Y, X);
    public FVector2 Reverse => Swap;

    public double MaxValue => Math.Max(X, Y);
    public double MinValue => Math.Min(X, Y);
    
    //和Vector2一样的卸载这行上面
 


    

}



