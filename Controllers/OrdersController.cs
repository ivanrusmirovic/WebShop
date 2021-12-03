using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly WebShopContext context;

        public OrdersController(WebShopContext context)
        {
            this.context = context;
        }
        //Get/api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await context.Orders.OrderBy(x => x.Id).ToListAsync();
        }

        //POST / api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromForm] Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), order);
        }
    }
}
