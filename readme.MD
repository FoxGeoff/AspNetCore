# Project: AspNetCore
## Asp.net Core 2.1 and EF Migration
## Online Ref: 
* https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db?tabs=visual-studio
* https://blogs.msdn.microsoft.com/dotnet/2018/05/30/announcing-entity-framework-core-2-1/
* https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding

## Check: Create the database
```
Add-Migration InitialCreate
Update-Database 
```
**If you get an error stating The term 'add-migration'** 
** is not recognized as the name of a cmdlet, close and reopen Visual Studio.**

## Check: Create a Controller

## Check: Data Seeding
* Note: **This feature is new in EF Core 2.1.**
* Note: *does not appear to be work yet*
```
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

			modelBuilder.Entity<Blog>().HasData(new Blog {BlogId = 1, Url = "http://sample.com"});

			modelBuilder.Entity<Post>().HasData(
				new {BlogId = 1, PostId = 1, Title = "First post", Content = "Test 1"},
				new {BlogId = 1, PostId = 2, Title = "Second post", Content = "Test 2"});
        }
    }
```
* Once entities have been added, it is recommended to use migrations to apply changes.
* ``` Update-Database ```
## Work around - using the current method
```

```
## ERROR: Microsoft.EntityFrameworkCore.DbUpdateException
* SqlException: Cannot insert explicit value for identity column in table 'Blogs' when IDENTITY_INSERT is set to OFF.
## FIX:
* ```
I have run into this before. This error means you are trying to assign a value explicitly to a column where the database automatically assigns it.
Suggestion: Update your edmx file to reflect any changes you may have made in the database. If the database automatically assigns the value, you should see the "IsDbGenerated=true" attribute in your designer file under that property. If it's not there, you can add it manually.
```