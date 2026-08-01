using LearningDotNetCoreAPI.Data;
using LearningDotNetCoreAPI.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using static LearningDotNetCoreAPI.Controllers.GreeterController;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy => policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddScoped<IGreeter, Greeter>();

var app = builder.Build();

//app.MapGet("/api/hello", (IGreeter greeter) => "Hello from DI! (Program.cs)");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");
app.UseAuthorization();

app.MapControllers();

Console.WriteLine(builder.Configuration["AppName"]);
app.Run();
