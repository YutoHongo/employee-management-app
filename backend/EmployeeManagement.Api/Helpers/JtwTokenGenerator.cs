using System.IdentityModel.Tokens.Jwt;
using Backend.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenGenerator
{
    // 定数SecreKey宣言
    private const string SecretKey = "supersecretkey_supersecretkey53418762346287468725";

    // ログイン成功時に呼び出されるGenerateTokenメソッド
    public static string GenerateToken(User user)
    {
        // トークンに埋め込むユーザー情報(UserId, Role)を配列に格納
        var claims = new[]
        {
            new Claim("userId", user.Id.ToString()),
            new Claim("Roles", user.Role.ToString())
        };

        // SecretKeyをbyte配列に変換
        // HMAC - SHA256方式でトークンに署名するように設定
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));
        var creads = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // トークン本体情報を生成
        var token = new JwtSecurityToken(
            issuer: "yourapp", //発行者
            audience: "yourapp", //対象者
            claims: claims, //ユーザー情報
            expires: DateTime.UtcNow.AddHours(1), //有効期限
            signingCredentials: creads //署名情報
            );

        // token内容を基にJWT文字列を生成
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}