using DotNetCoreSqlDb.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreSqlDb.Models;

public class LoginDBContext : IdentityDbContext<ApplicationUser>
{
    public LoginDBContext(DbContextOptions<LoginDBContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
     public DbSet<Models.Movie> Movie { get; set; } = default!;
      public DbSet<Models.Todo> Todo { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

           // modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");
            builder.Entity<Movie>().ToTable("Movie");
            builder.Entity<Todo>().ToTable("Todo");


        
        
    }
}
