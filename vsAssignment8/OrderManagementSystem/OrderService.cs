﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrderManagementSystem
{
    public class OrderService
    {
        public OrderService()
        {
            using (var ctx = new Context())
            {
                if (ctx.Goods.Count() == 0)
                {
                    ctx.Goods.Add(new Product("apple", 100.0));
                    ctx.Goods.Add(new Product("egg", 200.0));
                    ctx.SaveChanges();
                }
                if (ctx.Customers.Count() == 0)
                {
                    ctx.Customers.Add(new Customer("li"));
                    ctx.Customers.Add(new Customer("zhang"));
                    ctx.SaveChanges();
                }
            }
        }

        public List<Order> Orders
        {
            get
            {
                using (var ctx = new Context())
                {
                    return ctx.Orders
                      .Include(o => o.Details.Select(d => d.Product))
                      .Include(o => o.Customer)
                      .ToList<Order>();
                }
            }
        }

        public Order GetOrder(string id)
        {
            using (var ctx = new Context())
            {
                return ctx.Orders
                  .Include(o => o.Details.Select(d => d.Product))
                  .Include(o => o.Customer)
                  .SingleOrDefault(o => o.OrderId == id);
            }
        }

        public void AddOrder(Order order)
        {
            FixOrder(order);
            using (var ctx = new Context())
            {
                ctx.Entry(order).State = EntityState.Added;
                ctx.SaveChanges();
            }
        }

        public void RemoveOrder(string orderId)
        {
            using (var ctx = new Context())
            {
                var order = ctx.Orders.Include("Details")
                  .SingleOrDefault(o => o.OrderId == orderId);
                if (order == null) return;
                ctx.OrderDetails.RemoveRange(order.Details);
                ctx.Orders.Remove(order);
                ctx.SaveChanges();
            }
        }

        public List<Order> QueryOrdersByProductName(string productsName)
        {
            using (var ctx = new Context())
            {
                return ctx.Orders
                  .Include(o => o.Details.Select(d => d.Product))
                  .Include(o => o.Customer)
                  .Where(order => order.Details.Any(item => item.Product.Name == productsName))
                  .ToList();
            }
        }

        public List<Order> QueryOrdersByCustomerName(string customerName)
        {
            using (var ctx = new Context())
            {
                return ctx.Orders
                  .Include(o => o.Details.Select(d => d.Product))
                  .Include(o => o.Customer)
                  .Where(order => order.Customer.Name == customerName)
                  .ToList();
            }
        }


        public List<Order> QueryByTotalPrice(float price)
        {
            using (var ctx = new Context())
            {
                return ctx.Orders
                  .Include(o => o.Details.Select(d => d.Product)) //EF core中使用ThenInclude
                  .Include("Customer")
                .Where(order => order.Details.Sum(d => d.Quantity * d.Product.Price) > price)
                .ToList();
            }
        }

        public void UpdateOrder(Order newOrder)
        {
            RemoveOrder(newOrder.OrderId);
            AddOrder(newOrder);
        }

        //避免级联添加或修改Customer和Goods
        private static void FixOrder(Order newOrder)
        {
            newOrder.CustomerId = newOrder.Customer.Id;
            newOrder.Customer = null;
            newOrder.Details.ForEach(d => {
                d.ProductId = d.Product.Id;
                d.Product = null;
            });
        }

        public void Export(String fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                xs.Serialize(fs, Orders);
            }
        }

        public void Import(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (var ctx = new Context())
                {
                    List<Order> temp = (List<Order>)xs.Deserialize(fs);
                    temp.ForEach(order => {
                        if (ctx.Orders.SingleOrDefault(o => o.OrderId == order.OrderId) == null)
                        {
                            FixOrder(order);
                            ctx.Orders.Add(order);
                        }
                    });
                    ctx.SaveChanges();
                }
            }
        }
    }
}
