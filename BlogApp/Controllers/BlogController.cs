using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogApp.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ApiDbContext apiDbContext;
       public BlogController(ApiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }
       
        [HttpGet]
        public object Get()
        {


            return this.apiDbContext.blog.Where(b => b.Title.Contains("Title")).Select((c) => new
            {

                Id = c.Id,
                Title = c.Title,
                Description = c.Description
            }).ToList();
        }
    }
}
