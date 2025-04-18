﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace OrderApp
{

    public class Customer
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Customer() { }

        public Customer(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return $"customerId:{Id}, CustomerName:{Name}";
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null && Id == customer.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }

    class MainClass
    {
        public static void Main()
        {
            try
            {
                Customer customer1 = new Customer(1, "Customer1");
                Customer customer2 = new Customer(2, "Customer2");

                Product milk = new Product(1, "Milk", 69.9f);
                Product eggs = new Product(2, "eggs", 4.99f);
                Product apple = new Product(3, "apple", 5.59f);

                Order order1 = new Order(1, customer1, new DateTime(2021, 3, 21));
                order1.AddDetails(new OrderDetail(apple, 8));
                order1.AddDetails(new OrderDetail(eggs, 10));
                //order1.AddDetails(new OrderDetail(eggs, 8));
                //order1.AddDetails(new OrderDetail(milk, 10));

                Order order2 = new Order()
                {
                    Id = 2,
                    Customer = customer2,
                    CreateTime = new DateTime(2021, 3, 21),
                    Details = { new OrderDetail(eggs, 10), new OrderDetail(milk, 10) }
                };

                Order order3 = new Order(3, customer2, new DateTime(2021, 3, 21));
                order3.AddDetails(new OrderDetail(milk, 100));

                OrderService orderService = new OrderService();
                orderService.AddOrder(order1);
                orderService.AddOrder(order2);
                orderService.AddOrder(order3);

                Console.WriteLine("\n GetOrder");
                Console.WriteLine(orderService.GetOrder(1));
                Console.WriteLine(orderService.GetOrder(5) == null);

                Console.WriteLine("\nGetAllOrders");
                List<Order> orders = orderService.QueryAll();
                orders.ForEach(o => Console.WriteLine(o));

                Console.WriteLine("\nGetOrdersByCustomerName:'Customer2'");
                orders = orderService.QueryByCustomerName("Customer2");
                orders.ForEach(o => Console.WriteLine(o));

                Console.WriteLine("\nGetOrdersByProductName:'eggs'");
                orders = orderService.QueryByProductName("eggs");
                orders.ForEach(o => Console.WriteLine(o));

                Console.WriteLine("\nGetOrdersTotalAmount:1000");
                orders = orderService.QueryByTotalPrice(1000);
                orders.ForEach(o => Console.WriteLine(o));

                Console.WriteLine("\nQueryByCondition");
                var query = orderService.Query(o => o.TotalPrice > 1000);
                foreach (Order order in query)
                {
                    Console.WriteLine(order);
                }

                Console.WriteLine("\nRemove order(id=2) and qurey all");
                orderService.RemoveOrder(2);
                orderService.QueryAll().ForEach(
                    o => Console.WriteLine(o));

                Console.WriteLine("\n order by Amount");
                orderService.Sort(
                  (o1, o2) => o2.TotalPrice.CompareTo(o1.TotalPrice));
                orderService.QueryAll().ForEach(
                    o => Console.WriteLine(o));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }

    public class Order : IComparable<Order>
    {

        private readonly List<OrderDetail> details = new List<OrderDetail>();

        public int Id { get; set; }

        public Customer Customer { get; set; }

        public DateTime CreateTime { get; set; }

        public float TotalPrice
        {
            get => Details.Sum(d => d.TotalPrice);
        }

        public List<OrderDetail> Details => details;

        public Order()
        {
            CreateTime = DateTime.Now;
        }

        public Order(int orderId, Customer customer, DateTime creatTime)
        {
            Id = orderId;
            Customer = customer;
            CreateTime = creatTime;
        }

        public void AddDetails(OrderDetail orderDetail)
        {
            if (Details.Contains(orderDetail))
            {
                throw new ApplicationException($"The product ({orderDetail.Product.Name}) already exists in order {Id}");
            }
            Details.Add(orderDetail);
        }

        public int CompareTo(Order other)
        {
            return (other == null) ? 1 : Id - other.Id;
        }

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order != null && Id == order.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void RemoveDetails(int num)
        {
            Details.RemoveAt(num);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($"orderId:{Id}, customer:({Customer})");
            Details.ForEach(detail => result.Append("\n\t" + detail));
            return result.ToString();
        }

    }

    public class OrderDetail
    {

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public float TotalPrice
        {
            get => Product.Price * Quantity;
        }

        public OrderDetail() { }

        public OrderDetail(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        public override bool Equals(object obj)
        {
            var detail = obj as OrderDetail;
            return detail != null &&
                   EqualityComparer<Product>.Default.Equals(Product, detail.Product);
        }

        public override int GetHashCode()
        {
            return 785010553 + EqualityComparer<Product>.Default.GetHashCode(Product);
        }

        public override string ToString()
        {
            return $"OrderDetail:{Product},{Quantity}";
        }
    }
    public class OrderService
    {


        private readonly List<Order> orders = new List<Order>();

        public OrderService()
        {
        }

        //添加订单
        public void AddOrder(Order order)
        {
            if (orders.Contains(order))
            {
                throw new ApplicationException($"the order {order.Id} already exists!");
            }
            orders.Add(order);
        }

        //更新订单
        public void UpdateOrder(Order order)
        {
            int idx = orders.FindIndex(o => o.Id == order.Id);
            if (idx < 0)
            {
                throw new ApplicationException($"the order {order.Id} doesn't exist!");
            }
            orders.RemoveAt(idx);
            orders.Add(order);
        }

        //根据Id查询订单
        public Order GetOrder(int orderId)
        {
            return orders.FirstOrDefault(o => o.Id == orderId);
        }

        //根据Id删除订单
        public void RemoveOrder(int orderId)
        {
            int idx = orders.FindIndex(o => o.Id == orderId);
            if (idx >= 0)
            {
                orders.RemoveAt(idx);
            }
            /**
            orders.RemoveAll(o => o.Id == orderId);
            */
        }

        //查询所有订单
        public List<Order> QueryAll()
        {
            return orders;
        }

        //根据客户名查询
        public List<Order> QueryByCustomerName(string customerName)
        {
            var query = orders
                .Where(o => o.Customer.Name == customerName)
                .OrderBy(o => o.TotalPrice);
            return query.ToList();
        }

        //根据货物名查询
        public List<Order> QueryByProductName(string productName)
        {
            var query = orders.Where(
              o => o.Details.Any(d => d.Product.Name == productName))
                .OrderBy(o => o.TotalPrice);
            return query.ToList();

            /** 其他方法
            var query2 = orders.Where(
              o => o.Details.Exists(d => d.Product.Name == productName))
                .OrderBy(o => o.TotalPrice);

            var query3 = orders.Where(
             o => o.Details.Where(d => d.Product.Name == productName).Count()>0)
               .OrderBy(o => o.TotalPrice);

            var query4 = orders.Where(
             o => HasProduct(o,productName)) //写一个HasProduct方法，来检查o中是否包含名为productName的货物
               .OrderBy(o => o.TotalPrice); 
            **/
        }

        //根据总价查询
        public List<Order> QueryByTotalPrice(float totalPrice)
        {
            var query = orders.Where(o => o.TotalPrice >= totalPrice)
                .OrderBy(o => o.TotalPrice);
            return query.ToList();
        }


        //对orders中的数据进行排序
        public void Sort(Comparison<Order> comparison)
        {
            orders.Sort(comparison);
        }

        //根据传入的条件进行查询
        public IEnumerable<Order> Query(Predicate<Order> condition)
        {
            return orders.Where(o => condition(o)).OrderBy(o => o.TotalPrice);
        }

    }

    public class Product
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public Product() { }

        public Product(int id, string name, float price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product != null && Id == product.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Id:{Id}, Name:{Name}, Value:{Price}";
        }

    }
}
