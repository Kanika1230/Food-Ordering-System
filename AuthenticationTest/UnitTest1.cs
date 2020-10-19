using AuthenticationApi.Controllers;
using Castle.Core.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using AuthenticationApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationTest
{
    public class Tests
    {
        Ordering_FoodContext db;
        [SetUp]
        public void Setup()
        {
            var logindetails = new List<Login>
            {
                new Login{UserId=10,Password="happy" },
                 new Login{UserId=160102174,Password="Kanu12" }
            };

            var logindata = logindetails.AsQueryable();
            var mockSetLogin = new Mock<DbSet<Login>>();
            mockSetLogin.As<IQueryable<Login>>().Setup(m => m.Provider).Returns(logindata.Provider);
            mockSetLogin.As<IQueryable<Login>>().Setup(m => m.Expression).Returns(logindata.Expression);
            mockSetLogin.As<IQueryable<Login>>().Setup(m => m.ElementType).Returns(logindata.ElementType);
            mockSetLogin.As<IQueryable<Login>>().Setup(m => m.GetEnumerator()).Returns(logindata.GetEnumerator());
            var mockContext = new Mock<Ordering_FoodContext>();
            mockContext.Setup(c => c.Login).Returns(mockSetLogin.Object);
            db = mockContext.Object;
        }
        [Test]
        public void Authorised_User()
        {
            TokenController obj = new TokenController(db);
            Login nuser = new Login()
            {
                UserId = 10,
                Password = "happy"
            };
            var data = obj.LoginDetail(nuser);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }


        [Test]
        public void Unauthorised_User()
        {
            TokenController obj = new TokenController(db);
            Login nuser = new Login()
            {
                UserId = 11,
                Password = "wrong"
               
            };
            var data = obj.LoginDetail(nuser);
            var result = data as UnauthorizedResult;
            Assert.AreEqual(401, result.StatusCode);
        }
    }
}