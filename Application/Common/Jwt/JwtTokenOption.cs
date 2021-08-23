using Domain.Extensions;
using System;

namespace Application.Common.Jwt
{
    public class JwtTokenOption
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpiresInMinutes { get; set; }

        public string Key { get; set; }

        public void Guard()
        {
            Issuer.Guard(nameof(Issuer));
            Audience.Guard(nameof(Audience));
            Key.Guard(nameof(Key));

            if (ExpiresInMinutes <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(ExpiresInMinutes)} should be positive.");
            }
        }
    }
}
