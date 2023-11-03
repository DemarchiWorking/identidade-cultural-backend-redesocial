using IdentidadeCultural.Entity.Aplicacao.Service;
using IdentidadeCultural.Entity.Infraestrutura;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var stringConexao = builder.Configuration.GetConnectionString("StringConexao");


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://example.com",
                                              "http://www.contoso.com");
                      });
});


//builder.Services.AddScoped<IServicoTrabalhoService, ServicoTrabalhoService>();
builder.Services.AddScoped<IServicoTrabalhoService, ServicoTrabalhoService>();
builder.Services.AddScoped<IAmizadeService, AmizadeService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<IdentityContext>()
        .AddDefaultTokenProviders();


    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
            ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        });



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("user", policy => policy.RequireClaim("roles", "usuario"));
    options.AddPolicy("admin", policy => policy.RequireClaim("roles", "admin"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "apiagenda", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
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
                         new string[] {}
                    }
                });
});
//builder.Services.AddDbContext<DataContext>(options =>
//{
//    options.UseSqlServer(stringConexao);
//});

builder.Services.AddDbContext<IdentityContext>(options =>
{
    options.UseSqlServer(stringConexao);
});
// Configure the HTTP request pipeline.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin 
    .AllowCredentials());

app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
