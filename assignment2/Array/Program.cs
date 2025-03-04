namespace Array;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = { 3, 7, 2, 9, 5, 1, 8 };

        if (numbers.Length == 0)
        {
            Console.WriteLine("错误：数组为空！");
            return;
        }

        // 初始化统计值
        int max = numbers[0];
        int min = numbers[0];
        int sum = 0;

        // 遍历数组计算统计值
        foreach (int num in numbers)
        {
            if (num > max) max = num;
            if (num < min) min = num;
            sum += num;
        }

        double average = (double)sum / numbers.Length;

        // 输出结果
        Console.WriteLine($"数组内容: [{string.Join(", ", numbers)}]");
        Console.WriteLine($"最大值\t: {max}");
        Console.WriteLine($"最小值\t: {min}");
        Console.WriteLine($"总和\t: {sum}");
        Console.WriteLine($"平均值\t: {average:F2}"); // 保留两位小数

    }
}
