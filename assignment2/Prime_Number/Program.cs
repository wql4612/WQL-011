namespace Prime_Number;

class Program
{
    static void Main(string[] args)
    {
        bool[] isPrime = new bool[99];
        for (int i = 2; i < 99; i++)
        {
            isPrime[i] = true;
        }
        for (int num = 2; (num * num) < 99; num++)
        {
            if (isPrime[num])
            {
                for (int i = num * num; i < 99; i += num)
                {
                    isPrime[i] = false;
                }
            }
        }
        for (int i = 2; i < 99; i++)
        {
            if (isPrime[i])
            {
                Console.Write(i + " ");
            }
        }
    }
}
