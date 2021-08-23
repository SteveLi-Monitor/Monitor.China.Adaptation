using Domain.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Jwt
{
    public class JwtTokenBuilder
    {
        public JwtTokenBuilder()
        {
            Claims = new List<Claim>();
        }

        public string Issuer { get; private set; }

        public string Audience { get; private set; }

        public IList<Claim> Claims { get; private set; }

        public DateTime NotBefore { get; private set; } = DateTime.UtcNow;

        public DateTime Expires { get; private set; }

        public SigningCredentials SigningCredentials { get; private set; }

        public JwtTokenBuilder SetIssuer(string issuer)
        {
            issuer.Guard(nameof(issuer));
            Issuer = issuer;
            return this;
        }

        public JwtTokenBuilder SetAudience(string audience)
        {
            audience.Guard(nameof(audience));
            Audience = audience;
            return this;
        }

        public JwtTokenBuilder AddClaim(Claim claim)
        {
            claim.Guard(nameof(claim));
            Claims.Add(claim);
            return this;
        }

        public JwtTokenBuilder SetNotBefore(DateTime notBefore)
        {
            NotBefore = notBefore;
            return this;
        }

        public JwtTokenBuilder SetExpires(DateTime expires)
        {
            Expires = expires;
            return this;
        }

        public JwtTokenBuilder SetSigningCredentialKey(string key)
        {
            key.Guard(nameof(key));
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);
            return this;
        }
    }
}
