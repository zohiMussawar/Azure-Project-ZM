using Blog1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Blog1.Data;

public class ApplicationDbContext :IdentityDbContext<CustomUser, CustomRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
     public DbSet<Article>? Articles { get; set; }
   
    protected override void OnModelCreating(ModelBuilder builder) {
  base.OnModelCreating(builder);
  // Use seed method here
  SeedUsersRoles seedUsersRoles = new();
  builder.Entity<CustomRole>().HasData(seedUsersRoles.Roles);
  builder.Entity<CustomUser>().HasData(seedUsersRoles.Users);
  builder.Entity<IdentityUserRole<string>>().HasData(seedUsersRoles.UserRoles);
} 
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
}

public DbSet<Blog1.Models.Article> Article { get; set; } = default!;
}
