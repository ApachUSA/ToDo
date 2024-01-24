using Microsoft.EntityFrameworkCore;
using ToDo.Middlewares;
using ToDo.Repository;
using ToDo.Repository.Implementations;
using ToDo.Repository.Interfaces;
using ToDo.Services.Implementations;
using ToDo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configures the application to use the ToDo_DB_Context with the specified SQL Server connection string.
builder.Services.AddDbContext<ToDo_DB_Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registers a scoped service for the generic repository.
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(SqlRepository<>));

// Registers a scoped service for the ITaskService interface, with its implementation being TaskService.
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	//app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

// Configures the application to use the ExceptionMiddleware to handle exceptions during the request processing.
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=ToDoTask}/{action=Index}/{id?}");

app.Run();
