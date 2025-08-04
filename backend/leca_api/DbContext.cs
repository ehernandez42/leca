using leca_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace leca_api;

public class LecaDbContext : DbContext
{
    public LecaDbContext(DbContextOptions<LecaDbContext> options): base(options) {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<CalendarEvent>  CalendarEvents { get; set; }
    
}