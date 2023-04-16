using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GrpcServer.Managers
{
    public static class JwtAuthenticationManager
    {
        public const string JWT_TOKEN_KEY = "Abcd.1234";
        public const int JWT_TOKEN_VALIDITY = 60;

        public static AuthenticationResponse Authenticate(AuthenticationRequest authenticationRequest) 
        {
            bool nameIsAdmin = authenticationRequest.UserName == "admin";
            bool passwordIsAdmin = authenticationRequest.Password == "admin";
            if (!nameIsAdmin || !passwordIsAdmin) return null;

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(JWT_TOKEN_KEY);
            var tokenExpiryDateTime = DateTime.Now.AddMinutes(60);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim("username", authenticationRequest.UserName),
                        new Claim(ClaimTypes.Role, "administrator"),
                    }),
                Expires = tokenExpiryDateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                AccessToken = token,
                ExpiresIn = (int)tokenExpiryDateTime.Subtract(DateTime.Now).TotalSeconds
            };
        }
    }
}
