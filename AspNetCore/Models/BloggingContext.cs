using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AspNetCore.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
           : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* Not implimented yet due to framework bug
            modelBuilder.Entity<Blog>().HasData(
                new Blog { BlogId = 1, Url = "http://sample1.com" },
                new Blog { BlogId = 2, Url = "http://sample2.com" });

            modelBuilder.Entity<Post>().HasData(
                new { BlogId = 1, PostId = 1, Title = "First post", Content = "Test 1" },
                new { BlogId = 1, PostId = 2, Title = "Second post", Content = "Test 2" },
                new { BlogId = 2, PostId = 1, Title = "2nd Blog - First post", Content = "Test 1" },
                new { BlogId = 2, PostId = 2, Title = "2nd Blog - Second post", Content = "Test 2" });
             */
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
