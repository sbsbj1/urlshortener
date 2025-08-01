using Microsoft.EntityFrameworkCore;
using UrlShortener.Interfaces;
using UrlShortener.Model;
using UrlShortener.Repository;
using UrlShortener.Services;
using UrlShortener.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("secrets.json",
    optional: true,
    reloadOnChange: true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepositoryEF>();
builder.Services.AddScoped<IUrlRepository, UrlRepositoryEF>();
builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddAuthExtension(builder.Configuration);



builder.Services.AddDbContext<UrlContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var key = builder.Configuration.GetSection("SecretKey").Value;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
