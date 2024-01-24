using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Repository
{
	public class ToDo_DB_Context : DbContext
	{
		public ToDo_DB_Context(DbContextOptions<ToDo_DB_Context> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<ToDoTask> ToDoTasks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ToDoTask>(builder =>
			{
				builder.ToTable(nameof(ToDoTasks)).HasKey(x => x.Task_ID);
				builder.Property(x => x.Task_ID).ValueGeneratedOnAdd();
			});
		}
	}
}
