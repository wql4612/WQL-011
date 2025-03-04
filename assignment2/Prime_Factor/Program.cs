namespace Prime_Factor;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("输入一个大于1的正整数：");
        if (!int.TryParse(Console.ReadLine(), out int num1) || num1 <= 1)
        {
            Console.WriteLine("输入数据有误！");
            return;
        }
        int[] Prime_Factors;
        Prime_Factors = new int[1000];
        int n = num1;
        int cnt = 0;
        for (int i = 2; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                Prime_Factors[cnt++] = i;
                while (n % i == 0)
                {
                    n /= i;
                }
            }
        }
        if (n > 1)
        {
            Prime_Factors[cnt++] = n;
        }
        // foreach(int num in Prime_Factors)
        // {
        //     Console.WriteLine(num);
        // }
        for (int i = 0; i < cnt; i++)
        {
            Console.Write(Prime_Factors[i]);
            Console.Write(" ");
        }
    }
}
