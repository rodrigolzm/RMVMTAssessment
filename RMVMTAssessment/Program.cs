using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RMVMTAssessment.Data;
using RMVMTAssessment.Interfaces;
using RMVMTAssessment.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// set sqlite
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("Default"))
);

// repository dependency injection
builder.Services.AddScoped<IDriverLicenceRepository, DriverLicenceRepository>();
builder.Services.AddScoped<ISuspensionTransactionRepository, SuspensionTransactionRepository>();

var app = builder.Build();

// set up initial database data 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
