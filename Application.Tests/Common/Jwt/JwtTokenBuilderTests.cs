using Application.Common.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace Application.Tests.Common.Jwt
{
    public class JwtTokenBuilderTests
    {
        private const string issuer = "MONITOR";
        private const string audience = "TestAudience";
        private const string claimType = "ApiUsername";
        private const string claimValue = "API01";
        private const string key = "monitor_china_jwt_token_key";

        [Fact]
        public void ShouldBuildJwtToken()
        {
            var now = DateTime.UtcNow;

            var builder = new JwtTokenBuilder()
                .SetIssuer(issuer)
                .SetAudience(audience)
                .AddClaim(new Claim(claimType, claimValue))
                .SetNotBefore(now)
                .SetExpires(now.AddMinutes(1))
                .SetSigningCredentialKey(key);

            var tokenString = builder.WriteToken();

            Assert.False(string.IsNullOrEmpty(tokenString));
            Assert.Equal(issuer, builder.Token.Issuer);
            Assert.Equal(audience, builder.Token.Audiences.First());
            Assert.Equal(
                claimValue,
                builder.Token.Claims.First(x => x.Type == claimType).Value);
            Assert.Equal(now, builder.Token.ValidFrom, TimeSpan.FromSeconds(1));
            Assert.Equal(now.AddMinutes(1), builder.Token.ValidTo, TimeSpan.FromSeconds(1));
            Assert.Equal(SecurityAlgorithms.HmacSha256, builder.Token.SignatureAlgorithm);
        }

        [Fact]
        public void ShouldBuildJwtTokenFromOption()
        {
            var now = DateTime.UtcNow;

            var builder = new JwtTokenBuilder(
                new JwtTokenOption
                {
                    Issuer = issuer,
                    Audience = audience,
                    ExpiresInMinutes = 1,
                    Key = key
                });

            var tokenString = builder.WriteToken();

            Assert.False(string.IsNullOrEmpty(tokenString));
            Assert.Equal(issuer, builder.Token.Issuer);
            Assert.Equal(audience, builder.Token.Audiences.First());
            Assert.Equal(now, builder.Token.ValidFrom, TimeSpan.FromSeconds(1));
            Assert.Equal(now.AddMinutes(1), builder.Token.ValidTo, TimeSpan.FromSeconds(1));
            Assert.Equal(SecurityAlgorithms.HmacSha256, builder.Token.SignatureAlgorithm);
        }

        [Fact]
        public void ShouldThrowExceptionWhenExpiresIsLessThanOrEqualToNotBefore()
        {
            var now = DateTime.UtcNow;

            Assert.Throws<InvalidOperationException>(() =>
            {
                new JwtTokenBuilder()
                    .SetIssuer(issuer)
                    .SetAudience(audience)
                    .SetNotBefore(now)
                    .SetExpires(now)
                    .WriteToken();
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                new JwtTokenBuilder()
                    .SetIssuer(issuer)
                    .SetAudience(audience)
                    .SetNotBefore(now)
                    .SetExpires(now.AddMinutes(-1))
                    .WriteToken();
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenKeyLengthIsNotEnough()
        {
            var now = DateTime.UtcNow;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new JwtTokenBuilder()
                    .SetIssuer(issuer)
                    .SetAudience(audience)
                    .SetNotBefore(now)
                    .SetExpires(now.AddMinutes(1))
                    .SetSigningCredentialKey("monitor")
                    .WriteToken();
            });
        }
    }
}
