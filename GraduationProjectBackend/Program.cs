using GraduationProjectBackend.DataAccess.Context;
using GraduationProjectBackend.DataAccess.Repositories.Favorite;
using GraduationProjectBackend.DataAccess.Repositories.Member;
using GraduationProjectBackend.Filter.Swagger;
using GraduationProjectBackend.Helper.Member;
using GraduationProjectBackend.Services.Favorite;
using GraduationProjectBackend.Services.Member;
using GraduationProjectBackend.Services.PopularityAnalysis;
using GraduationProjectBackend.Services.WordCloud;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWTÈÑÊöï`èq"
    });

    options.OperationFilter<AuthorizeCheckOperationFilter>();
    options.UseDateOnlyTimeOnlyStringConverters();
});

builder.Services.AddDbContext<MssqlDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
});

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IFavoriteFolderService, FavoriteFolderService>();



builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserRoleRepository>();
builder.Services.AddScoped<FavoriteFolderRepository>();


builder.Services.AddScoped<FavoriteFolderItemRepository>();
builder.Services.AddScoped<FavoriteItemRepository>();
builder.Services.AddSingleton<EncryptHelper>();
builder.Services.AddSingleton<JwtHelper>();

builder.Services.AddScoped<IWordCloudService, FakeWordCloudService>();
builder.Services.AddScoped<IPopularityAnalysisService, FakePopularityAnalysisService>();


JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = "role",
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SignKey"))),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader())
);

builder.Services.AddDateOnlyTimeOnlyStringConverters();


var app = builder.Build();

app.UseCors();

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

app.MigrateDatabase();
app.Run();
