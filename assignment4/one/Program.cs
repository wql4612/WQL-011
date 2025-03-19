namespace one;

class Program
{
    //链表节点
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }

        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }
    //泛型链表类
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public GenericList()
        {
            tail = head = null;
        }
        public Node<T> Head
        {
            get => head;
        }
        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }

        public void ForEach(Action<T> action)
        {
            for (Node<T> node = head; node != null; node = node.Next)
            {
                action(node.Data);
            }
        }
    }
    static void Main(string[] args)
    {
        GenericList<int> intlist = new GenericList<int>();
        for (int x = 0; x < 10; x++)
        {
            intlist.Add(x);
        }

        //打印链表元素
        intlist.ForEach(data => Console.WriteLine(data));

        //求最大值
        int max = int.MinValue;
        intlist.ForEach(data => { if (data > max) max = data; });
        Console.WriteLine($"最大值：{max}");

        // 求最小值
        int min = int.MaxValue;
        intlist.ForEach(data => { if (data < min) min = data; });
        Console.WriteLine($"最小值: {min}");

        // 求和
        int sum = 0;
        intlist.ForEach(data => sum += data);
        Console.WriteLine($"求和: {sum}");
    }
}
