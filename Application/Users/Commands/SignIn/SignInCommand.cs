using Application.Common;
using Application.Common.Jwt;
using Application.Common.MonitorApi;
using Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.SignIn
{
    public class SignInCommand : IRequest<string>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string LanguageCode { get; set; } = "ZH";

        public string CompanyNumber { get; set; }

        public void Guard()
        {
            Username.Guard(nameof(Username));
            Password.Guard(nameof(Password));
            CompanyNumber.Guard(nameof(CompanyNumber));
        }
    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, string>
    {
        private ApplicationUser applicationUser;
        private readonly MonitorApiService monitorApiService;
        private readonly JwtTokenOption jwtTokenOption;

        public SignInCommandHandler(
            ApplicationUser applicationUser,
            MonitorApiService monitorApiService,
            IConfiguration configuration)
        {
            this.applicationUser = applicationUser;
            this.monitorApiService = monitorApiService;
            jwtTokenOption = configuration
                .GetSection(nameof(JwtTokenOption)).Get<JwtTokenOption>();
        }

        public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            request.Guard(nameof(SignInCommand));
            request.Guard();

            applicationUser.Username = request.Username;
            applicationUser.Password = request.Password;
            applicationUser.LanguageCode = request.LanguageCode;
            applicationUser.CompanyNumber = request.CompanyNumber;

            await monitorApiService.SignInAsync();

            return new JwtTokenBuilder(jwtTokenOption)
                .AddClaim(
                    new Claim(nameof(ApplicationUser),
                    JsonConvert.SerializeObject(applicationUser)))
                .WriteToken();
        }
    }
}
