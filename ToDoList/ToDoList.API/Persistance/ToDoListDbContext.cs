using Microsoft.EntityFrameworkCore;
using ToDoList.API.Models;

namespace ToDoList.API.Persistance
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {

        }

        public DbSet<ToDoListModel> ToDoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ToDoListModel>(t =>
            {
                t.HasKey(t => t.Id);
                t.Property(t => t.TaskTitle).IsRequired();
                t.Property(t => t.TaskCategory).IsRequired();
                t.Property(t => t.TaskDescription).IsRequired(false);
                t.Property(t => t.TaskDeadline).IsRequired();
                t.Property(t => t.Status).IsRequired();
            });
        }
    }
}
