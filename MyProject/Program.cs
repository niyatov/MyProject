using Microsoft.EntityFrameworkCore; // Using Entity Framework Core for database operations
using MyProject.Data; // Using the data context for database operations
using MyProject.Repositories; // Using repositories for data access
using MyProject.Services; // Using services for business logic

var builder = WebApplication.CreateBuilder(args);

// Add MVC services with Razor runtime compilation for development
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Add database context to services with connection string from configuration
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));

// Register the UnitOfWork and EmployeeService in the dependency injection container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Set up error handling and HTTPS redirection
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Employee/CustomError");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Configure default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
