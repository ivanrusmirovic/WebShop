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
    public class ProductsController : ControllerBase
    {
        private readonly WebShopContext context;

        public ProductsController(WebShopContext context)
        {
            this.context = context;
        }

        //Get/api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(int p = 1)
        {
            int pageSize = 4;
            var products = context.Products.OrderBy(x => x.Id).Include(x => x.Category).Skip((p-1)* pageSize).Take(pageSize);

            return await products.ToListAsync();
        }

        //Get/api/products/category
        [HttpGet("{slug}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string slug, int p = 1) 
        {
            Category category = await context.Categories.Where(x => x.Slug == slug).FirstOrDefaultAsync();
            if (category == null) return NotFound();

            int pageSize = 4;
            var products = context.Products.OrderBy(x => x.Id).Where(x => x.CategoryId == category.Id).Skip((p - 1) * pageSize).Take(pageSize);

            return await products.ToListAsync();
        }

        //Get/api/products/count/category
        [HttpGet("count/{slug}")]
        public async Task<ActionResult<int>> GetProductCount(string slug)
        {
            if (slug == "all") 
            {
                return await context.Products.CountAsync();
            }
            Category category = await context.Categories.Where(x => x.Slug == slug).FirstOrDefaultAsync();
            if (category == null) return NotFound();

                return await context.Products.Where(x => x.CategoryId == category.Id).CountAsync();
            
        }

    }
}
