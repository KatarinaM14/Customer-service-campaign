using Application.Services;
using Domain.Interfaces;
using Domain.Interfaces.ExternalServices;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Infrastructure;
using Infrastructure.ExternalServices;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Application;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});

builder.Services.AddCors(options => {
    options.AddPolicy("Cors", builder => {

        builder.WithOrigins(new string[]{
                    "http://localhost:5500",
                    "https://localhost:3000",
                    "https://localhost:5500",
                    "https://127.0.0.1:5500",
                    "http://localhost:3000",
                    "http://localhost:5100",
                    "https://localhost:5100",
                    "https://127.0.0.1:5100",

                    "http://localhost:5173",
                    "http://127.0.0.1:5173",

                }).AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();

    });
});

//Dependency Injection
builder.Services.AddInfrastructure(builder.Configuration); // From Infrastructure layer
builder.Services.AddApplication(); // From Application layer

builder.Services.Configure<CustomerServiceCampaignUrlModel>(builder.Configuration.GetSection("CustomerServiceCampaignAPIUrl"));

//External service
builder.Services.AddHttpClient<ICustomerCampaignExternalService, CustomerCampaignExternalService>(client =>
{
    client.DefaultRequestHeaders.Add("X-Integration-Api-Key", builder.Configuration["ApiKeys:CustomerServiceCampaignApiKey"]);
});
builder.Services.AddHttpContextAccessor();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
});

// Authorization 
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Cors");

app.UseAuthorization();

app.MapControllers();

app.Run();
