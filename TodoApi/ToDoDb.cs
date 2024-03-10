using Microsoft.EntityFrameworkCore;
using TodoApi;

class TodoDb : ToDoDbContext
{
    public TodoDb(DbContextOptions<ToDoDbContext> options)
        : base(options) { }

    public DbSet<Item> Todos => Set<Item>();
}