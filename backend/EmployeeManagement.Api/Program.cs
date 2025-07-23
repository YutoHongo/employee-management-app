using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// サービス登録
builder.Services.AddControllers();

// AuthServiceのDI設定
builder.Services.AddScoped<AuthService>();

// JWT認証設定
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "Roles",

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "yourapp",
        ValidAudience = "yourapp",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("supersecretkey_supersecretkey53418762346287468725"))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// ミドルウェア設定
app.UseHttpsRedirection();

// 認証・承認有効化
app.UseAuthentication(); // トークン認証
app.UseAuthorization(); // [Authorize]の適用

app.MapControllers(); // [ApiController]属性使用設定

app.Run();