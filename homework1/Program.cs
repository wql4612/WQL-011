namespace homework1;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Console.WriteLine("The current time is" + DateTime.Now );

            Console.WriteLine("简单计算器");
            
            // 获取第一个数字
            double num1;
            Console.Write("请输入第一个数字: ");
            if (!double.TryParse(Console.ReadLine(), out num1))
            {
                Console.WriteLine("错误：无效的数字输入！");
                return;
            }

            // 获取运算符
            Console.Write("请选择运算符 (+ - * /): ");
            string op = Console.ReadLine();
            if (!IsValidOperator(op))
            {
                Console.WriteLine("错误：无效的运算符！");
                return;
            }

            // 获取第二个数字
            double num2;
            Console.Write("请输入第二个数字: ");
            if (!double.TryParse(Console.ReadLine(), out num2))
            {
                Console.WriteLine("错误：无效的数字输入！");
                return;
            }

            // 检查除数是否为零
            if (op == "/" && num2 == 0)
            {
                Console.WriteLine("错误：除数不能为零！");
                return;
            }

            // 计算并显示结果
            try
            {
                double result = Calculate(num1, num2, op);
                Console.WriteLine($"结果: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"计算错误: {ex.Message}");
            }
        }

        static bool IsValidOperator(string op)
        {
            return op == "+" || op == "-" || op == "*" || op == "/";
        }

        static double Calculate(double a, double b, string op)
        {
            switch (op)
            {
                case "+": return a + b;
                case "-": return a - b;
                case "*": return a * b;
                case "/": return a / b;
                default: throw new InvalidOperationException("未知运算符");
            }

    }
}
