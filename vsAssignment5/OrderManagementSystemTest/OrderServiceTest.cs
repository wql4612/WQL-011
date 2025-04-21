using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using OrderManagementSystem;

namespace OrderManagementSystemTest
{
    /// <summary>
    /// OrderServiceTest 的摘要说明
    /// </summary>
    [TestClass]
    public class OrderServiceTest
    {
        OrderService orderService = new OrderService();

        Product apple = new Product(1, "apple", 10.0f);
        Product egg = new Product(2, "egg", 1.2f);
        Product milk = new Product(3, "milk", 50f);
        Customer customer1 = new Customer(1, "Customer1");
        Customer customer2 = new Customer(2, "Customer2");

        [TestInitialize]
        public void Init()
        {
            Order order1 = new Order(1, customer1, new DateTime(2021, 3, 21));
            order1.AddDetails(new OrderDetail(apple, 80));
            order1.AddDetails(new OrderDetail(egg, 200));
            order1.AddDetails(new OrderDetail(milk, 10));

            Order order2 = new Order(2, customer2, new DateTime(2021, 3, 21));
            order2.AddDetails(new OrderDetail(egg, 200));
            order2.AddDetails(new OrderDetail(milk, 10));

            Order order3 = new Order(3, customer2, new DateTime(2021, 3, 21));
            order3.AddDetails(new OrderDetail(apple, 80));
            order3.AddDetails(new OrderDetail(milk, 10));

            orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);
            orderService.AddOrder(order3);
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AddOrderTest()
        {
            Order order4 = new Order(4, customer2, new DateTime(2021, 3, 21));
            order4.AddDetails(new OrderDetail(milk, 10));
            orderService.AddOrder(order4);
            List<Order> orders = orderService.QueryAll();
            Assert.IsNotNull(orders);
            Assert.AreEqual(4, orders.Count);
            CollectionAssert.Contains(orders, order4);
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void AddOrderTest2()
        {
            Order order3 = new Order(3, customer2, new DateTime(2021, 3, 21));
            orderService.AddOrder(order3);
        }


        [TestMethod]
        public void RemoveOrderTest()
        {
            orderService.RemoveOrder(3);
            List<Order> orders = orderService.QueryAll();
            Assert.AreEqual(2, orders.Count);
            orderService.RemoveOrder(100);
            Assert.AreEqual(2, orderService.QueryAll().Count);
        }

        [TestMethod]
        public void UpdateOrderTest()
        {
            Order order3 = new Order(3, customer1, new DateTime(2021, 3, 21));
            order3.AddDetails(new OrderDetail(milk, 200));
            orderService.UpdateOrder(order3);

            List<Order> orders = orderService.QueryAll();
            Assert.IsNotNull(orders);
            Assert.AreEqual(3, orders.Count);
            Order o = orderService.GetOrder(3);
            Assert.AreEqual(customer1, o.Customer);
        }


        [TestMethod]
        public void QueryOrderByIdTest()
        {
            Order order2 = orderService.GetOrder(2);
            Assert.IsNotNull(order2);
            Assert.AreEqual(2, order2.Id);
            Assert.AreEqual(customer2, order2.Customer);
            List<OrderDetail> details = new List<OrderDetail>()
        { new OrderDetail(egg, 200), new OrderDetail(milk, 10) };
            CollectionAssert.AreEqual(details, order2.Details);

            Order order4 = orderService.GetOrder(4);
            Assert.IsNull(order4);
        }


        [TestMethod]
        public void QueryOrdersByProductNameTest()
        {
            Assert.AreEqual(2, orderService.QueryByProductName("apple").Count);
            Assert.AreEqual(2, orderService.QueryByProductName("egg").Count);
            Assert.AreEqual(3, orderService.QueryByProductName("milk").Count);
            Assert.AreEqual(0, orderService.QueryByProductName("orange").Count);
        }

        [TestMethod]
        public void QueryOrdersByCustomerNameTest()
        {
            Assert.AreEqual(1, orderService.QueryByCustomerName("Customer1").Count);
            Assert.AreEqual(2, orderService.QueryByCustomerName("Customer2").Count);
            Assert.AreEqual(0, orderService.QueryByCustomerName("Customer3").Count);
        }
    }
}
