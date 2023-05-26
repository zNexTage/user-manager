using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManager.Data;
using UserManager.Models;
using UserManager.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("UserMysqlConnection")!;

builder.Services.AddDbContext<UserDbContext>(
    (opts =>{
        opts.UseMySql(
            connectionString, 
            ServerVersion.AutoDetect(connectionString));
    })
);

builder.Services
.AddIdentity<User, IdentityRole>()
.AddEntityFrameworkStores<UserDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Configuration of dependecy injection:

//builder.Services.AddScoped -> Create an instance when a request ocurrs.
//builder.Services.AddTransient -> ...
//builder.Services.AddSingleton -> Create an unique instance for each request

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
