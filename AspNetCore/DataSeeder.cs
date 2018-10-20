using AspNetCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AspNetCore
{
    public class DataSeeder
    {
        private BloggingContext _context;
        private IHostingEnvironment _hosting;

        public DataSeeder(BloggingContext context, IHostingEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }

        public static void InitializeData(IServiceProvider services)
        {
            using (var serviceScope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var env = serviceScope.ServiceProvider.GetService<IHostingEnvironment>();
                if (!env.IsDevelopment()) { return; }

                var seederManager = serviceScope.ServiceProvider.GetRequiredService<DataSeeder>();
                seederManager.Seed();
            }
        }

        public void Seed()
        {
            if (!_context.Blogs.Any())
            {
                var filepath = Path.Combine(_hosting.ContentRootPath, "Resources/blogs.json");
                var json = File.ReadAllText(filepath);

                /* Mapping to Dtos TBD
                var dtoProductImages = JsonConvert.DeserializeObject<IEnumerable<Dtos.product_image>>(json);
                var productImages = ConvertToProductImages(dtoProductImages);
                */

                var blogs = JsonConvert.DeserializeObject<IEnumerable<Blog>>(json);
                _context.Blogs.AddRange(blogs);
                _context.SaveChanges();
            }
        }
    }
}