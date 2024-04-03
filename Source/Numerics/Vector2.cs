using System;

namespace AtomFlash.Numerics;

public struct Vector2
{
    public long X;
    public long Y;

    public Vector2(long value)
    {
        X = value;
        Y = value;
    }

    public Vector2(long x, long y)
    {
        X = x;
        Y = y;
    }

    //数组
    public long this[int i] => (i % 2) == 0 ? X : Y;

    public static implicit operator long[](Vector2 vec) => [vec.X, vec.Y];

    public static readonly Vector2 Zero = default;
    public static readonly Vector2 Unit = new(1, 1);
    public static readonly Vector2 UnitX = new(1, 0);
    public static readonly Vector2 UnitY = new(0, 1);

    public static Vector2 operator +(long left, Vector2 right) => new(left + right.X, left + right.Y);
    public static Vector2 operator +(Vector2 left, long right) => new(left.X + right, left.Y + right);
    public static Vector2 operator +(Vector2 left, Vector2 right) => new(left.X + right.X, left.Y + right.Y);

    public static Vector2 operator -(Vector2 value) => new(-value.X, -value.Y);

    public static Vector2 operator -(long left, Vector2 right) => new(left - right.X, left - right.Y);
    public static Vector2 operator -(Vector2 left, long right) => new(left.X - right, left.Y - right);
    public static Vector2 operator -(Vector2 left, Vector2 right) => new(left.X - right.X, left.Y - right.Y);

    public static Vector2 operator *(long left, Vector2 right) => new(left * right.X, left * right.Y);
    public static Vector2 operator *(Vector2 left, long right) => new(left.X * right, left.Y * right);
    public static Vector2 operator *(Vector2 left, Vector2 right) => new(left.X * right.X, left.Y * right.Y);

    public static Vector2 operator /(long left, Vector2 right) => new(left / right.X, left / right.Y);
    public static Vector2 operator /(Vector2 left, long right) => new(left.X / right, left.Y / right);
    public static Vector2 operator /(Vector2 left, Vector2 right) => new(left.X / right.X, left.Y / right.Y);

    public static Vector2 operator %(long left, Vector2 right) => new(left % right.X, left % right.Y);
    public static Vector2 operator %(Vector2 left, long right) => new(left.X % right, left.Y % right);
    public static Vector2 operator %(Vector2 left, Vector2 right) => new(left.X % right.X, left.Y % right.Y);


    public static bool operator ==(Vector2 left, Vector2 right) => left.X == right.X && left.Y == right.Y;
    public static bool operator ==(long left, Vector2 right) => left == right.X && left == right.Y;
    public static bool operator ==(Vector2 left, long right) => left.X == right && left.Y == right;

    public static bool operator !=(Vector2 left, Vector2 right) => left.X != right.X || left.Y != right.Y;
    public static bool operator !=(Vector2 left, long right) => left.X != right || left.Y != right;
    public static bool operator !=(long left, Vector2 right) => left != right.X && left != right.Y;


    public static bool operator >(Vector2 left, Vector2 right) => left.X > right.X && left.Y > right.Y;
    public static bool operator >(Vector2 left, long right) => left.X > right && left.Y > right;
    public static bool operator >(long left, Vector2 right) => left > right.X && left > right.Y;

    public static bool operator <(Vector2 left, Vector2 right) => left.X < right.X && left.Y < right.Y;
    public static bool operator <(Vector2 left, long right) => left.X < right && left.Y < right;
    public static bool operator <(long left, Vector2 right) => left < right.X && left < right.Y;

    public static bool operator >=(Vector2 left, Vector2 right) => left.X >= right.X && left.Y >= right.Y;
    public static bool operator >=(Vector2 left, long right) => left.X >= right && left.Y >= right;
    public static bool operator >=(long left, Vector2 right) => left >= right.X && left >= right.Y;

    public static bool operator <=(Vector2 left, Vector2 right) => left.X <= right.X && left.Y <= right.Y;
    public static bool operator <=(Vector2 left, long right) => left.X <= right && left.Y <= right;
    public static bool operator <=(long left, Vector2 right) => left <= right.X && left <= right.Y;


    public static Vector2 Max(Vector2 left, Vector2 right) => new(Math.Max(left.X, right.X), Math.Max(left.Y, right.Y));

    public static Vector2 Max(Vector2 left, long right) => new(Math.Max(left.X, right), Math.Max(left.Y, right));

    public static Vector2 Max(long left, Vector2 right) => new(Math.Max(left, right.X), Math.Max(left, right.Y));

    public static Vector2 Min(Vector2 left, Vector2 right) => new(Math.Min(left.X, right.X), Math.Min(left.Y, right.Y));

    public static Vector2 Min(Vector2 left, long right) => new(Math.Min(left.X, right), Math.Min(left.Y, right));

    public static Vector2 Min(long left, Vector2 right) => new(Math.Min(left, right.X), Math.Min(left, right.Y));
    
    


    public bool IsPositive => X < 0 && Y < 0;
    public bool IsNegative => X > 0 && Y > 0;
    public bool IsZero => X == 0 && Y == 0;
    public bool IsXEqualY => X == Y;

    public Vector2 Abs => new(Math.Abs(X), Math.Abs(Y));
    public Vector2 Sign => new(Math.Sign(X), Math.Sign(Y));


    public double Tan => (double)Y / X;
    public double Angle => Math.Atan2(Y, X);

    public Vector2 Sqr => new(X * X, Y * Y);
    public long LengthSqr => X * X + Y * Y;
    public double Length => Math.Sqrt(X * X + Y * Y);

    public long Sum => X + Y;
    public long Product => X * Y;
    public long Diff => X - Y;
    public long DiffReverse => X - Y;
    public double Quotient => X / Y;
    public double QuotientReverse => Y / X;
    public long Mod => X % Y;
    public long ModReverse => Y % X;


    public Vector2 Rotate90Increase => new(-Y, X);
    public Vector2 Rotate90Reduce => new(Y, -X);

    public Vector2 Swap => new(Y, X);
    public Vector2 Reverse => Swap;

    public long MaxValue => Math.Max(X, Y);
    public long MinValue => Math.Min(X, Y);
    
    public static bool SlopeEqual(Vector2 s1, Vector2 s2)
    {
        return s1.X * s2.Y == s1.Y * s2.X;
    }
    
    public static bool IsOrthogonal(Vector2 s1, Vector2 s2)
    {
        return SlopeEqual(s1.Rotate90Increase, s2);
    }
    
    public static bool DirectionEqual(Vector2 s1, Vector2 s2)
    {
        return s1.Sign == s2.Sign && SlopeEqual(s1,s2);
    }

    public static bool DirectionOpposite(Vector2 s1, Vector2 s2)
    {
        return s1.Sign == -s2.Sign && SlopeEqual(s1,s2);
    }

    // public static bool DirectionHalfSideIncrease(Vector2 s1, Vector2 s2)
    // {
    //     
    // }
    //
    // public static bool DirectionHalfSideReduce(Vector2 s1, Vector2 s2)
    // {
    // }

    

}


// 目的 ，使用整数是为了平均化坐标精度