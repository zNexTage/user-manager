using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManager.Authorization;
using UserManager.Data;
using UserManager.Models;
using UserManager.Service;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManager.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration["ConnectionStrings:UserMysqlConnection"];

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

// dependency injection of age authorization;
builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorization>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var userSettings = builder.Configuration.GetSection("User").Get<UserSettings>();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userSettings.TokenKey)),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy("MinAge", policy => {
        policy.AddRequirements(new MinAge(18));
    });
}); // Config a authorization policy

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
