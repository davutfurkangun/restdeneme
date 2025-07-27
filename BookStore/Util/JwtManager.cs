using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Util
{
    public class JwtManager
    {
        private readonly string hashKey = "85d9966651cf78a11dcaee540cbc3c3385e5f18d5a85ab725f2e0af696ec613b270732d756970c8776dd61f6c5ed4094b14252004e60c0d62d962238635dd013";
        private readonly TimeSpan expirationTime = TimeSpan.FromMinutes(90);


        public string CreateToken(long id)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(hashKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("ids",id.ToString()),
                        new Claim("kendimeNot","Fena bir sey oldu bu be"),
                        new Claim("üreten" , "furkanturkmen")
                    }),
                    Expires = DateTime.UtcNow.Add(expirationTime),
                    Issuer = "BookStore",
                    IssuedAt = DateTime.UtcNow,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception exception)
            {
                throw new Exception("Token olusturulurken HATA !!!!");
            }
        }

        public string ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(hashKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "BookStore",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //return jwtToken.Subject;

                return jwtToken.Claims.FirstOrDefault(c => c.Type == "ids")?.Value;
            }
            catch (Exception e)
            {
                throw new Exception("Token acilirken hata...");
            }
        }
    }
}
