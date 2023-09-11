using ChatBackend.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatBackend.Infrastructure;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<UserRoom> UsersRooms { get; set; }
}