using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodOrderClient.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FoodOrderClient.Controllers
{
    public class ItemsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var list = new List<Item>();
            using(var httpclient=new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:53827/api/Item/");
                HttpResponseMessage res = await httpclient.GetAsync("GetItems");
                if(res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Item>>(result);
                }
            }
            return View(list);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            using (var httpclient=new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:53827/");
                var delete = httpclient.DeleteAsync("/api/Item/Delete?id=" + id);
                var res = delete.Result;
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            Item item = new Item();
            using(var httpclient=new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:53827/");
                HttpResponseMessage res = await httpclient.GetAsync("/api/Item/GetItem?id=" + id);
                if(res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject<Item>(result);
                }

            }
            return View(item);
        }
        public IActionResult Create()
        {
            return View();
        }
       [HttpPost]
        public IActionResult Create(Item item)
        {
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:53827/");
                var postData = httpclient.PostAsJsonAsync<Item>("/api/Item/AddItem", item);
                var res = postData.Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(item);
        } 
        public async Task<IActionResult> Edit(int id)
        {
            Item item = new Item();
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:53827/");
                HttpResponseMessage res = await httpclient.GetAsync("/api/Item/GetItem?id=" + id);
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject<Item>(result);
                }

            }
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(Item item)
        {
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:53827/");
                var postData = httpclient.PutAsJsonAsync<Item>("/api/Item/UpdateItem?id="+item.Iid,item);
                var res = postData.Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(item);
        }
    }
}