using Common.Constants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPI.Common.Interfaces;
using WebAPI.Common.Models;

namespace WebAPI.Common.Implementations
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SecurityKey _issuerSigningKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;


        public JwtHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = _options.Issuer,
                IssuerSigningKey = _issuerSigningKey
            };
        }

        public JsonWebToken Create()
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_options.ExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1);
            var exp = (long)new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds;
            var now = (long)new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds;
            var payload = new JwtPayload
            {
                {"sub", SystemConst.SYSTEM_USER_NAME}, // subject
                {"iss", _options.Issuer},
                {"iat", now}, // issued at
                {"exp", exp}, // experiation date
                {"unique_name", SystemConst.SYSTEM_USER_GUID} // user within API
            };
            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };
        }
    }
}
