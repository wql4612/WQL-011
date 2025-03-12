using System;
namespace two;

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

// 新增形状类型枚举
public enum ShapeType
{
    Rectangle,
    Square,
    Triangle
}

// 形状工厂类
public class ShapeFactory
{
    private readonly Random _random = new Random();

    public IShape CreateRandomShape()
    {
        // 随机选择形状类型
        var type = (ShapeType)_random.Next(0, 3);

        // 生成1-10之间的随机参数（保留两位小数）
        double a = GetRandomSize();
        double b = GetRandomSize();
        double c = GetRandomSize();

        return type switch
        {
            ShapeType.Rectangle => new Rectangle(a, b),
            ShapeType.Square => new Square(a),
            ShapeType.Triangle => new Triangle(a, b, c),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetRandomSize()
    {
        return Math.Round(_random.NextDouble() * 9 + 1, 2); // 1.00-10.00
    }
}

class Program
{
    static void Main()
    {
        ShapeFactory factory = new ShapeFactory();
        double totalArea = 0;

        for (int i = 0; i < 10; i++)
        {
            IShape shape = factory.CreateRandomShape();
            double area = shape.CalculateArea();

            Console.WriteLine($"[{i + 1}] {shape.GetType().Name.PadRight(9)} | " +
                              $"Valid: {shape.IsValid().ToString().PadRight(5)} | " +
                              $"Area: {area:F2}");

            totalArea += area;
        }

        Console.WriteLine($"\nTotal area of 10 shapes: {totalArea:F2}");
    }
}
