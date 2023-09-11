using System.Text;
using ChatBackend.Application.WebApi.Middlewares;
using ChatBackend.Core.Interfaces.Repositories;
using ChatBackend.Core.Interfaces.Services;
using ChatBackend.Infrastructure;
using ChatBackend.Infrastructure.Configs;
using ChatBackend.Infrastructure.Repositories;
using ChatBackend.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;


var connectionString = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(
    connectionString, 
    b => b.MigrationsAssembly("API"))
);

services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
services.AddTransient<IUserRepository, UserRepository>();
services.AddTransient<IUserService, UserService>();
services.AddTransient<ITokenService, JwtTokenService>();
services.AddTransient<TokenServiceFactory>();
services.AddTransient<IMessageRepository, MessageRepository>();
services.AddTransient<IMessageService, MessageService>();
services.AddTransient<IRoomRepository, RoomRepository>();
services.AddTransient<IUserRoomRepository, UserRoomRepository>();
services.AddTransient<IRoomService, RoomService>();
services.AddTransient<IUserRoomRepository, UserRoomRepository>();

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
});

services.AddHttpContextAccessor();
services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.Run();
