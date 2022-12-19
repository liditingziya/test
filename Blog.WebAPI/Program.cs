using Blog.Extension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugar.IOC;
using System.Text;

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
        Type = SecuritySchemeType.ApiKey,
        Description = "Bearer token",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
// IOC
builder.Services.AddCustomIOC();
builder.Services.AddSqlSugar(new IocConfig()
{
    ConnectionString = builder.Configuration["ConnectionStrings:SqlServer"],
    DbType = IocDbType.SqlServer,
    IsAutoCloseConnection = true,
});

// JWT鉴权
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")),
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:6060",
        ValidateAudience = true,
        ValidAudience = "http://localhost:6060",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(60)
    };
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(CustomAutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();// 鉴权中间件
app.UseAuthorization(); //授权

app.MapControllers();

app.Run();
