using DataManagers;
using Controller;
using Controller.IControllers;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



builder.Services.AddDbContext<SqlContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<UserRepositorySql>();
builder.Services.AddScoped<GenericController>();
builder.Services.AddScoped<IUserController, GenericController>();
builder.Services.AddScoped<ICategoryController, GenericController>();
builder.Services.AddScoped<IGoalController, GenericController>();
builder.Services.AddScoped<IExchangeHistoryController, GenericController>();
builder.Services.AddScoped<IMonetaryAccount, GenericController>();
builder.Services.AddScoped<ICreditAccount, GenericController>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SqlContext>();
    context.Database.Migrate();
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
