using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using OrderItem.Models;
using Newtonsoft.Json;

namespace OrderItem.Controllers
{
    //public class OrderController : Controller
    //{
    //    [Route("api/[controller]")]
    //    [ApiController]
        public class OrderController : ControllerBase
        {
            List<Cart> orderItem = new List<Cart>();
            int idValue = 0;
            // POST api/<OrderController>
            [HttpPost]
            public async Task<ActionResult<Cart>> Post([FromBody] ModelId model)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri("http://20.88.173.18/api/");
                    var urlAppend = "menuitem/" + Convert.ToString(model.Id);
                    var response = await client.GetAsync(urlAppend);
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        MenuItem res = JsonConvert.DeserializeObject<MenuItem>(apiResponse);


                        Cart cart = new Cart();
                        cart.Id = idValue + 1;
                        cart.UserId = cart.Id;
                        cart.MenuItemId = model.Id;
                        cart.MenuItemName = res.Name;
                        return Ok(cart);
                    }
                }
                return BadRequest("Test");
            }


        }
    
}
