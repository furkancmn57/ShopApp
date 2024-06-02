using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Features.Order.Commands;
using Application.Services;
using Application.Services.AuthService;
using Application.Services.PasswordService;
using Infrastructure.Contexts;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.MailProviders;
using Infrastructure.RabbitMq.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IShopAppDbContext, ShopAppDbContext>();
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPasswordService, PasswordManager>();


builder.Services.AddSingleton<IRedisDbContext, RedisDbContext>();
builder.Services.AddSingleton<IMailProviderFactory, MailProviderFactory>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.NoResult();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "text/plain";

                return context.Response.WriteAsync("Unauthorized");
            }
        };
    });


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddMassTransit(bus =>
{
    bus.AddConsumer<UpdateOrderConsumer>();

    bus.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("shopapp.updateorder.sendmail", e =>
        {
            e.Consumer<UpdateOrderConsumer>(context);

            e.Bind("updateorder", x =>
            {
                x.ExchangeType = "fanout";
            });
        });

    });
});


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

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
