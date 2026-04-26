using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolStudentAPI.Helper;
using SchoolStudentAPI.Repository;
using SchoolStudentAPI.Service;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Controllers & Swagger
// --------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --------------------
// CORS (Updated for AWS)
// --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "http://localhost:5174",
                "http://52.64.115.50" // 🔥 your EC2 frontend
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// --------------------
// Swagger + JWT
// --------------------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SchoolStudentAPI",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {JWT token}"
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
            Array.Empty<string>()
        }
    });
});

// --------------------
// Database Connection
// --------------------
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// --------------------
// Dependency Injection
// --------------------
builder.Services.AddScoped<TestDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ISchoolMarksheetRepo, SchoolMarksheetRepo>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<ISchoolMarksheetService, SchoolMarksheetService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IActivityLogService, ActivityLogService>();

// --------------------
// JWT Authentication
// --------------------
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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// --------------------
// Middleware pipeline
// --------------------

// 🔥 Enable Swagger in EC2 (production also)
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseCors("AllowReactApp");

// ❌ Disabled for now (no HTTPS setup yet)
// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();