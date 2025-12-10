using APIService.Models;
using APIService.UserLogin;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace APIService.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly JwtSettings _jwtSettings;

        public UserService(IMongoCollection<User> usersCollection, JwtSettings jwtSettings)
        {
            _usersCollection = usersCollection;
            _jwtSettings = jwtSettings;
        }

        public async Task<User?> GetUserByName(string username) => 
            await _usersCollection
                .Find(u => u.UserName == username)
                .FirstOrDefaultAsync();
        

        public List<User> GetUsers() => _usersCollection.Find(User => true).ToList();
        public User GetUser(string id) => _usersCollection.Find(user => user.Id == id).FirstOrDefault();

        public async Task<User>CreateUser(User user)
        {
            await _usersCollection.InsertOneAsync(user);
            return user;
        }

        public string Authenticate(string name, string password)
        {
            var user = this._usersCollection.Find(x => x.UserName == name && x.Password == password).FirstOrDefault();

            if(user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var tokenDescripter = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, name),
                }),
                Expires = DateTime.UtcNow.AddHours(1), 

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), 
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescripter);

            return tokenHandler.WriteToken(token);
        }
    }
}
