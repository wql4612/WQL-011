using System;
namespace one;

// 定义形状接口
public interface IShape
{
    double CalculateArea();
    bool IsValid();
}

// 抽象基类，提供公共方法
public abstract class ShapeBase : IShape
{
    public abstract double CalculateArea();
    public abstract bool IsValid();

    // 检查边长是否均为正数
    protected bool AreSidesPositive(params double[] sides)
    {
        foreach (var side in sides)
        {
            if (side <= 0)
                return false;
        }
        return true;
    }
}

// 长方形类
public class Rectangle : ShapeBase
{
    public double Length { get; }
    public double Width { get; }

    public Rectangle(double length, double width)
    {
        Length = length;
        Width = width;
    }

    public override double CalculateArea()
    {
        return IsValid() ? Length * Width : 0;
    }

    public override bool IsValid()
    {
        return AreSidesPositive(Length, Width);
    }
}

// 正方形类
public class Square : ShapeBase
{
    public double Side { get; }

    public Square(double side)
    {
        Side = side;
    }

    public override double CalculateArea()
    {
        return IsValid() ? Side * Side : 0;
    }

    public override bool IsValid()
    {
        return AreSidesPositive(Side);
    }
}

// 三角形类
public class Triangle : ShapeBase
{
    public double A { get; }
    public double B { get; }
    public double C { get; }

    public Triangle(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }

    public override double CalculateArea()
    {
        if (!IsValid()) return 0;

        // 海伦公式计算面积
        double s = (A + B + C) / 2;
        return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
    }

    public override bool IsValid()
    {
        return AreSidesPositive(A, B, C) &&
               (A + B > C) && (A + C > B) && (B + C > A);
    }
}

// 示例用法
class Program
{
    static void Main()
    {
        IShape[] shapes = {
            new Rectangle(3, 4),
            new Square(5),
            new Triangle(3, 4, 5),
            new Rectangle(-1, 5),      // 无效长方形
            new Triangle(1, 1, 3)      // 无效三角形
        };

        foreach (var shape in shapes)
        {
            Console.WriteLine($"形状类型: {shape.GetType().Name}");
            Console.WriteLine($"是否有效: {shape.IsValid()}");
            Console.WriteLine($"计算面积: {shape.CalculateArea():F2}");
            Console.WriteLine(new string('-', 30));
        }
    }
}