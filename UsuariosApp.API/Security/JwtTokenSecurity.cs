using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UsuariosApp.API.Security
{
    public class JwtTokenSecurity
    {
        #region Propriedades

        public static string SecurityKey => "665A6ADE-75FD-45F3-B71A-8DE91B85F2D9";

        public static int ExpirationInHours => 1;

        #endregion

        public static string GenerateToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecurityKey);

           
            var tokenDescriptor = new SecurityTokenDescriptor
            {
              
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, userId.ToString()) }),             
                Expires = DateTime.UtcNow.AddHours(ExpirationInHours),
                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };         
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}

