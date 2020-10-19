using NUnit.Framework;
using OrderListApi.Models;
using OrderListApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.EntityFrameworkCore;
using OrderListApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace NUnitTestProjectForOrders
{
    public class Tests
    {
        Ordering_FoodContext db;
        [SetUp]
        public void Setup()
        {
            var details = new List<Order>
            {
                new Order{OrderId = 1, UserId=2,ItemId=4,Quantity=4,Price=45},
                new Order{OrderId = 1, UserId=2,ItemId=4,Quantity=4,Price=45},
                new Order{OrderId = 1, UserId=2,ItemId=4,Quantity=4,Price=45},
                new Order{OrderId = 1, UserId=2,ItemId=4,Quantity=4,Price=45}
            };
            var detaildata = details.AsQueryable();
            var mockSet = new Mock<DbSet<Order>>();
            mockSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(detaildata.Provider);
            mockSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(detaildata.Expression);
            mockSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(detaildata.ElementType);
            mockSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(detaildata.GetEnumerator());
            var mockContext = new Mock<Ordering_FoodContext>();
            mockContext.Setup(c => c.Order).Returns(mockSet.Object);
            db = mockContext.Object;
        }

        [Test]
       public void GetOrder()
        {
            OrderRepository itemdata = new OrderRepository(db);
            OrderController obj = new OrderController(itemdata);
            var data = obj.GetOrder(1);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void GetOrders()
        {
            OrderRepository itemdata = new OrderRepository(db);
            OrderController obj = new OrderController(itemdata);
            var data = obj.GetOrders();
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
} 