using MChatBackend.Core;
using MChatBackend.Core.Domain.IdentityEntities;
using MChatBackend.Core.SignalR.Hubs;
using MChatBackend.Infrastrecture;
using MChatBackend.Infrastrecture.DBContext;
using MChatBackend.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// --------------------
// Register Core + Infrastructure
// --------------------
builder.Services.AddCore();
builder.Services.AddInfrastrecture(builder.Configuration);

// --------------------
// SignalR
// --------------------
builder.Services.AddSignalR();

// --------------------
// CORS
// --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


// --------------------
// Controllers
// --------------------
builder.Services.AddControllers();


// --------------------
// Swagger
// --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();


// --------------------
// Identity
// --------------------
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();


// --------------------
// JWT Authentication
// --------------------
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

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        )
    };
});


// Prevent cookie redirects (API behavior)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});


// --------------------
// Build App
// --------------------
var app = builder.Build();


// --------------------
// Global Exception Middleware
// --------------------
app.UseMiddleware<ExceptionHandlingMiddleware>();


// --------------------
// Swagger in Dev
// --------------------
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


// --------------------
// Pipeline
// --------------------
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();   // MUST come before Authorization
app.UseAuthorization();
app.MapHub<ChatHub>("/chatHub");

app.MapControllers();

app.Run();