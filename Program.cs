using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PersonContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddDbContext<CardContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Cors", policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Cors");
app.MapControllers();
app.Run();