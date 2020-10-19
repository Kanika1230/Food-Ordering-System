using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ItemListApi.Models;
using ItemListApi.Repository;
using Microsoft.AspNetCore.Cors;

namespace ItemListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ItemController : ControllerBase
    {
        IItemList acontext;
        public ItemController(IItemList _acontext)
        {
            acontext = _acontext;
        }
        [HttpGet]
        [Route("GetItems")]
        public IActionResult GetItems()
        {
            try
            {
                var data = acontext.GetItems();
                if(data==null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch(Exception)
            {
                return BadRequest();
            }
            
        }
        [HttpGet]
        [Route("GetItem")]
        public IActionResult GetItem(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var data = acontext.GetItem(id);
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
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = acontext.Delete(id);
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
        [HttpPost]
        [Route("AddItem")]
        public IActionResult AddItem([FromBody]Item newItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = acontext.AddItem(newItem);
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
        [HttpPut]
        [Route("UpdateItem")]
        public IActionResult UpdateItem(int? id, [FromBody]Item newItem)
        {
            if (id != null)
            {
                acontext.UpdateItem(id, newItem);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}