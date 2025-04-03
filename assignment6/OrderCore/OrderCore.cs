
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderCore
{
    //订单明细
    public class OrderDetails
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;

        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   ProductName == details.ProductName &&
                   Price == details.Price &&
                   Quantity == details.Quantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductName, Price, Quantity);
        }

        public override string ToString()
        {
            return $"Product: {ProductName}, Price: {Price:C}, Quantity: {Quantity}, Total: {Total:C}";
        }
    }
    //订单
    public class Order
    {
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetails> Details { get; } = new List<OrderDetails>();
        public decimal TotalAmount => Details.Sum(d => d.Total);

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   OrderId == order.OrderId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Order ID: {OrderId}, Customer: {Customer}, Total Amount: {TotalAmount:C}");
            sb.AppendLine("Details:");
            foreach (var detail in Details)
            {
                sb.AppendLine(detail.ToString());
            }
            return sb.ToString();
        }
    }

    public class Customer
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
    //订单服务
    public class OrderService
    {
        //private List<Order> orders = new List<Order>();
        public List<Order> orders { get; } = new List<Order>();

        public void AddOrder(Order order)
        {
            if (orders.Contains(order))
                throw new ArgumentException("Order already exists.");

            foreach (var d in order.Details)
                if (order.Details.Count(x => x.Equals(d)) > 1)
                    throw new ArgumentException("Duplicate order details detected");

            orders.Add(order);
        }

        public void RemoveOrder(int orderId)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new InvalidOperationException($"Order {orderId} not found");

            orders.Remove(order);
        }

        public void ModifyOrder(int orderId, Action<Order> modification)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new InvalidOperationException($"Order {orderId} not found");

            int originalId = order.OrderId;
            modification(order);

            if (order.OrderId != originalId && orders.Any(o => o.OrderId == order.OrderId))
            {
                order.OrderId = originalId;
                throw new ArgumentException("New Order ID conflicts with existing order");
            }

            foreach (var d in order.Details)
                if (order.Details.Count(x => x.Equals(d)) > 1)
                    throw new ArgumentException("Duplicate details after modification");
        }

        public IEnumerable<Order> Query(Func<Order, bool> predicate)
        {
            return orders.Where(predicate).OrderBy(o => o.TotalAmount);
        }

        public IEnumerable<Order> QueryByProduct(string productName)
        {
            return orders.Where(o => o.Details.Any(d => d.ProductName == productName))
                        .OrderBy(o => o.TotalAmount);
        }

        public IEnumerable<Order> QueryByCustomer(string customerName)
        {
            return orders.Where(o => o.Customer.Name == customerName)
                        .OrderBy(o => o.TotalAmount);
        }

        public void Sort(Comparison<Order> comparison = null)
        {
            orders.Sort(comparison ?? ((x, y) => x.OrderId.CompareTo(y.OrderId)));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OrderService service = new OrderService();
            while (true)
            {
                Console.WriteLine("\n1. Add Order\n2. Remove Order\n3. Query Orders\n4. Modify Orders\n5. Exit");
                Console.Write("Choice: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddOrder(service);
                        break;
                    case "2":
                        RemoveOrder(service);
                        break;
                    case "3":
                        QueryOrders(service);
                        break;
                    case "4":
                        ModifyOrder(service);
                        break;
                    case "5":
                        return;
                }
            }
        }

        static void AddOrder(OrderService service)
        {
            try
            {
                var order = new Order();
                Console.Write("Order ID: ");
                order.OrderId = int.Parse(Console.ReadLine());

                Console.Write("Customer Name: ");
                order.Customer = new Customer { Name = Console.ReadLine() };

                while (true)
                {
                    var detail = new OrderDetails();
                    Console.Write("Product Name (empty to finish): ");
                    string product = Console.ReadLine();
                    if (string.IsNullOrEmpty(product)) break;

                    detail.ProductName = product;
                    Console.Write("Price: ");
                    detail.Price = decimal.Parse(Console.ReadLine());
                    Console.Write("Quantity: ");
                    detail.Quantity = int.Parse(Console.ReadLine());

                    order.Details.Add(detail);
                }

                service.AddOrder(order);
                Console.WriteLine("Order added!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        static void RemoveOrder(OrderService service)
        {
            try
            {
                Console.Write("Order ID to remove: ");
                int id = int.Parse(Console.ReadLine());
                service.RemoveOrder(id);
                Console.WriteLine("Order removed!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        static void QueryOrders(OrderService service)
        {
            Console.Write("Search by (1) Product  (2) Customer  (3) All: ");
            var results = Console.ReadLine() switch
            {
                "1" => service.QueryByProduct(GetInput("Product Name")),
                "2" => service.QueryByCustomer(GetInput("Customer Name")),
                _ => service.Query(o => true)
            };

            Console.WriteLine("\nResults:");
            foreach (var order in results)
                Console.WriteLine(order);
        }

        static string GetInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine();
        }

        static void ModifyOrder(OrderService service)
        {
            try
            {
                Console.Write("输入要修改的订单号: ");
                int orderId = int.Parse(Console.ReadLine());

                Console.WriteLine("请选择修改内容:");
                Console.WriteLine("1. 修改客户名称");
                Console.WriteLine("2. 修改订单号");
                Console.WriteLine("3. 添加商品明细");
                Console.WriteLine("4. 删除商品明细");
                Console.Write("选择: ");

                var choice = Console.ReadLine();
                Action<Order> modification = choice switch
                {
                    "1" => ModifyCustomerName(),
                    "2" => ModifyOrderId(service),
                    "3" => AddOrderDetail(),
                    "4" => RemoveOrderDetail(),
                    _ => throw new ArgumentException("无效选择")
                };

                service.ModifyOrder(orderId, modification);
                Console.WriteLine("修改成功!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"错误: {e.Message}");
            }
        }

        static Action<Order> ModifyCustomerName()
        {
            Console.Write("新客户名称: ");
            string name = Console.ReadLine();
            return order => order.Customer.Name = name;
        }

        static Action<Order> ModifyOrderId(OrderService service)
        {
            Console.Write("新订单号: ");
            int newId = int.Parse(Console.ReadLine());
            return order =>
            {
                if (service.Query(o => o.OrderId == newId).Any())
                    throw new ArgumentException("订单号已存在");
                order.OrderId = newId;
            };
        }

        static Action<Order> AddOrderDetail()
        {
            Console.Write("商品名称: ");
            string product = Console.ReadLine();
            Console.Write("单价: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("数量: ");
            int quantity = int.Parse(Console.ReadLine());

            return order => order.Details.Add(new OrderDetails
            {
                ProductName = product,
                Price = price,
                Quantity = quantity
            });
        }

        static Action<Order> RemoveOrderDetail()
        {
            return order =>
            {
                Console.WriteLine("当前商品明细:");
                for (int i = 0; i < order.Details.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {order.Details[i]}");
                }
                Console.Write("选择要删除的明细编号: ");
                int index = int.Parse(Console.ReadLine()) - 1;
                order.Details.RemoveAt(index);
            };
        }
    }
}