using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FoodOrderClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodOrderClient.Controllers
{
    public class OrdersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var list = new List<Item>();
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:53827/api/Item/");
                HttpResponseMessage res = await httpclient.GetAsync("GetItems");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Item>>(result);
                }
            }
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Order order)
        {
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:55370/");
                var postData = httpclient.PostAsJsonAsync<Order>("/api/Order/AddOrder", order);
                var res = postData.Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Create");
                }
            }
            return View(order);
        }

    }
}