using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Entities;

namespace TaskTrackerApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<TaskItem> TaskItems { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options):  base(options) {}  
} 