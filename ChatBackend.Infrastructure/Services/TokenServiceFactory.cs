using ChatBackend.Core.Interfaces.Services;
using ChatBackend.Infrastructure.Configs;
using Microsoft.Extensions.Options;

namespace ChatBackend.Infrastructure.Services;

public enum TokenType
{
    Jwt
}

public class TokenServiceFactory
{
    private readonly IOptions<JwtOptions> _jwtOptions;

    public TokenServiceFactory(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }
    
    public ITokenService Create(TokenType tokenType)
    {
        switch (tokenType)
        {
            case TokenType.Jwt:
                return CreateJwtService();
                
        }
        throw new ArgumentException("invalid token type");
    }

    private JwtTokenService CreateJwtService() => new JwtTokenService(_jwtOptions);
}