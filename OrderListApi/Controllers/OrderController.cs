using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderListApi.Models;
using OrderListApi.Repository;

namespace OrderListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderRepository acontext;
        public OrderController(IOrderRepository _acontext)
        {
            acontext = _acontext;
        }
        [HttpGet]
        [Route("GetOrders")]
        public IActionResult GetOrders()
        {
            try
            {
                var data = acontext.GetOrders();
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetOrder")]
        public IActionResult GetOrder(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var data = acontext.GetOrder(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder(Order newOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = acontext.AddOrder(newOrder);
                    if (id > 0)
                    {
                        return Ok(id);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = acontext.DeleteOrder(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}