using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodOrderClient.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace FoodOrderClient.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login user)
        {
            string token = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:54181/");
                var jsonString = JsonConvert.SerializeObject(user);
                var data = new StringContent(jsonString,System.Text.Encoding.UTF8,"application/json");
                var response =await httpclient.PostAsync("api/Token/LoginDetail", data);
                if (response.StatusCode== System.Net.HttpStatusCode.OK)
                {
                    token = await response.Content.ReadAsStringAsync();
                    TempData["token"] = token;
                    return RedirectToAction("Create", "Orders");
                }
            }
            return View("Login");
        }
    }
}