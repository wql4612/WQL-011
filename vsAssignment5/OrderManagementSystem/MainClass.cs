using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagementSystem
{
    public class MainClass
    {
        public static void Main()
        {
            try
            {
                // 初始化客户
                var customers = new List<Customer>
                {
                    new Customer(1, "Alice"),
                    new Customer(2, "Bob")
                };

                // 初始化产品
                var products = new List<Product>
                {
                    new Product(1, "Milk", 69.9f),
                    new Product(2, "Eggs", 4.99f),
                    new Product(3, "Apple", 5.59f)
                };

                // 创建订单
                var order1 = new Order(1, customers[0], new DateTime(2021, 3, 21));
                order1.AddDetails(new OrderDetail(products[2], 8)); // Apple
                order1.AddDetails(new OrderDetail(products[1], 10)); // Eggs

                var order2 = new Order()
                {
                    Id = 2,
                    Customer = customers[1],
                    CreateTime = new DateTime(2021, 3, 21),
                    Details = { new OrderDetail(products[1], 10), new OrderDetail(products[0], 10) } // Eggs, Milk
                };

                var order3 = new Order(3, customers[1], new DateTime(2021, 3, 21));
                order3.AddDetails(new OrderDetail(products[0], 100)); // Milk

                // 初始化订单服务
                var orderService = new OrderService();
                orderService.AddOrder(order1);
                orderService.AddOrder(order2);
                orderService.AddOrder(order3);

                // 查询和操作订单
                Console.WriteLine("\n查询订单 (ID=1):");
                var order = orderService.GetOrder(1);
                Console.WriteLine(order != null ? order.ToString() : "订单不存在");

                Console.WriteLine("\n查询所有订单:");
                orderService.QueryAll().ForEach(Console.WriteLine);

                Console.WriteLine("\n按客户名称查询订单 (客户: Bob):");
                orderService.QueryByCustomerName("Bob").ForEach(Console.WriteLine);

                Console.WriteLine("\n按产品名称查询订单 (产品: Eggs):");
                orderService.QueryByProductName("Eggs").ForEach(Console.WriteLine);

                Console.WriteLine("\n按总金额查询订单 (金额 > 1000):");
                orderService.Query(o => o.TotalPrice > 1000).ToList().ForEach(Console.WriteLine);

                Console.WriteLine("\n删除订单 (ID=2) 后查询所有订单:");
                orderService.RemoveOrder(2);
                orderService.QueryAll().ForEach(Console.WriteLine);

                Console.WriteLine("\n按订单总金额降序排序:");
                orderService.Sort((o1, o2) => o2.TotalPrice.CompareTo(o1.TotalPrice));
                orderService.QueryAll().ForEach(Console.WriteLine);
            }
            catch (Exception e)
            {
                Console.WriteLine($"发生错误: {e.Message}");
            }
        }
    }
}
