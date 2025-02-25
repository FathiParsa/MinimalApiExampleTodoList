using Microsoft.EntityFrameworkCore;

namespace MinimalApiExampleTodoList
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItem> Todos { get; set; }
    }
}
