using AuditApp.Server.Services;
using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Business;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuditApp.Server.Objects
{
    public class JwtTokenHandler
    {
        private const int JwtTokenValidMinutes = 240;

        public UserSession? GenerateJwtToken(UserModel userModel)
        {
            var dateTimeNow = DateTime.Now;

            var expierationDate = dateTimeNow.AddMinutes(JwtTokenValidMinutes);

            var jwtSecurityKey = Guid.NewGuid().ToString();

            var tokenKey = Encoding.ASCII.GetBytes(jwtSecurityKey);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userModel.Name),
                new Claim(ClaimTypes.Role, userModel.Role)
            });

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = expierationDate,
                SigningCredentials = signingCredentials,
                NotBefore = dateTimeNow
            };

            var jwtSecurityHandler = new JwtSecurityTokenHandler();

            var securityToken = jwtSecurityHandler.CreateToken(securityTokenDescriptor);

            jwtSecurityHandler.WriteToken(securityToken);

            var expirationDate =  expierationDate.Subtract(dateTimeNow);

            var userSession = new UserSession(userModel.Email, userModel.Token, userModel.Role, (int)expirationDate.TotalSeconds, expierationDate, userModel.Name);

            return userSession;
        }
    }
}
