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
    public class CategoriesController : ControllerBase
    {
        private readonly WebShopContext context;

        public CategoriesController(WebShopContext context)
        {
            this.context = context;
        }
        //Get/api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await context.Categories.OrderBy(x => x.Id).ToListAsync();
        }
    }
}
