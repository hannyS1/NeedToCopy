using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Services;
using ChatBackend.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatBackend.Infrastructure.Services;

public class JwtTokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    
    public string CreateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = GetTokenDescriptor(user);
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public int? GetUserIdByToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            return int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
        }
        catch
        {
            return null;
        }
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Key)),
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
        };
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        return new[] { new Claim("id", user.Id.ToString()) };
    }
    
    private SecurityTokenDescriptor GetTokenDescriptor(User user)
    {
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(GetClaims(user)),
            Expires = DateTime.UtcNow.AddSeconds(_jwtOptions.TokenLifeTime),
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    }
}