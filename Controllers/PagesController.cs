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
    public class PagesController : ControllerBase
    {
        private readonly WebShopContext context;

        public PagesController(WebShopContext context)
        {
            this.context = context;
        }
        //Get/api/pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> Get()
        {
            return await context.Pages.OrderBy(x => x.Id).ToListAsync();
        }
    }
}
