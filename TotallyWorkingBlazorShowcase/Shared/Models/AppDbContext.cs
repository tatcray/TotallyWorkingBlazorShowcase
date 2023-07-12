
using Microsoft.EntityFrameworkCore;
using TotallyWorkingBlazorShowcase.Shared.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserDb> Users { get; set; }
    public DbSet<DistribInputModel> Distrib { get; set; }
}