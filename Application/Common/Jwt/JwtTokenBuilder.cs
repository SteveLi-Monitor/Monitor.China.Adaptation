using Domain.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public JwtTokenBuilder(JwtTokenOption option) : this()
        {
            option.Guard(nameof(option));
            option.Guard();

            var now = DateTime.UtcNow;

            this.SetIssuer(option.Issuer)
                .SetAudience(option.Audience)
                .SetNotBefore(now)
                .SetExpires(now.AddMinutes(option.ExpiresInMinutes))
                .SetSigningCredentialKey(option.Key);
        }

        public string Issuer { get; private set; }

        public string Audience { get; private set; }

        public IList<Claim> Claims { get; private set; }

        public DateTime NotBefore { get; private set; }

        public DateTime Expires { get; private set; }

        public SigningCredentials SigningCredentials { get; private set; }

        public JwtSecurityToken Token { get; private set; }

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

        public string WriteToken()
        {
            Guard();

            Token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: Claims,
                notBefore: NotBefore,
                expires: Expires,
                signingCredentials: SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        private void Guard()
        {
            Issuer.Guard(nameof(Issuer));
            Audience.Guard(nameof(Audience));

            if (Expires <= NotBefore)
            {
                throw new InvalidOperationException
                    ($"{nameof(Expires)} '{Expires}' should be later than {nameof(NotBefore)} '{NotBefore}'.");
            }

            SigningCredentials.Guard(nameof(SigningCredentials));
        }
    }
}
