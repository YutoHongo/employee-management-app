using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using Backend.Models;
using Backend.Enums;

namespace Backend.Services    
{
    public class AuthService
    {
        private readonly List<User> _users; //本番環境ではDB接続

        public AuthService()
        {
            _users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "test@examle.com",
                    PasswordHash = Hash("password123"),
                    Role = UserRole.Admin
                },

                new User
                {
                    Id = 2,
                    Email = "test_general@examle.com",
                    PasswordHash = Hash("password456"),
                    Role = UserRole.General
                }
             };
        }

        // 引数emil, passwordが_users要素のId, PasswordHashと一致する場合、そのUserを返す(すべて不一致の場合nullを返す)
        public User? ValidateUser(string email, string password)
        {
            string hash = Hash(password);
            return _users.FirstOrDefault(u => u.Email == email && u.PasswordHash == hash);
        }

        // パスワードハッシュ化
        private string Hash(string input)
        {
            using var sha256 = SHA256.Create(); //SHA-256（暗号学的ハッシュ関数）のインスタンスを作成
            var bytes = Encoding.UTF8.GetBytes(input); //引数inputをUTF-8のバイト列に変換
            var hashBytes = sha256.ComputeHash(bytes); //引数bytesに対してSHA-256を適用する
            return Convert.ToBase64String(hashBytes); //引数hashBytesをBase64形式の文字列へ変換して返す
        }
    }
}
