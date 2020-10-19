using NUnit.Framework;
using Moq;
using Food_Ordering_System.Models;
using Food_Ordering_System.Repository;
using Food_Ordering_System.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingTest
{
    public class Tests
    {
        Ordering_FoodContext db;
        [SetUp]
        public void Setup()
        {
            var details = new List<Item>
            {
                new Item{Iid = 1, Iname="Dummy1",Iprice="Dummy Address 1",Iavailability="Yes"},
                new Item{Iid = 1, Iname="Dummy1",Iprice="Dummy Address 1",Iavailability="Yes"},
                new Item{Iid = 1, Iname="Dummy1",Iprice="Dummy Address 1",Iavailability="No"},
                new Item{Iid = 1, Iname="Dummy1",Iprice="Dummy Address 1",Iavailability="Yes"},
            };
            var detaildata = details.AsQueryable();
            var mockSet = new Mock<DbSet<Item>>();
            mockSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(detaildata.Provider);
            mockSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(detaildata.Expression);
            mockSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(detaildata.ElementType);
            mockSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(detaildata.GetEnumerator());
            var mockContext = new Mock<Ordering_FoodContext>();
            mockContext.Setup(c => c.Item).Returns(mockSet.Object);
            db = mockContext.Object;
        }

        [Test]
        public void get_Item()
        {
            Admin itemdata = new Admin(db);
            ItemController obj = new ItemController(itemdata);
            var data = obj.GetItem(2);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void get_all_Items()
        {
            Admin admindata = new Admin(db);
            ItemController obj = new ItemController(admindata);
            var data = obj.GetItems();
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}