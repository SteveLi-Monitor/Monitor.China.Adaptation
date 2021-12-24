using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Jwt;
using Application.MonitorApis;
using Domain.Dtos;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.SignIn
{
    public class SignInCommand : IValidateRequest<SignInCommandResp>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string CompanyNumber { get; set; }


        public bool NeedValidation { get; set; } = true;
    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInCommandResp>
    {
        private readonly ApplicationUser applicationUser;
        private readonly MonitorApiService monitorApiService;
        private readonly JwtTokenOption jwtTokenOption;
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public SignInCommandHandler(
            ApplicationUser applicationUser,
            MonitorApiService monitorApiService,
            IConfiguration configuration)
        {
            this.applicationUser = applicationUser;
            this.monitorApiService = monitorApiService;

            jwtTokenOption = configuration
                .GetSection(nameof(JwtTokenOption)).Get<JwtTokenOption>();

            jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
        }

        public async Task<SignInCommandResp> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            request.Username = request.Username.ToUpper();

            applicationUser.ApiUsername = request.Username;
            applicationUser.Password = request.Password;
            applicationUser.CompanyNumber = request.CompanyNumber;

            var loginResp = await monitorApiService.SignInAsync();

            return new SignInCommandResp
            {
                Token = new JwtTokenBuilder(jwtTokenOption)
                    .AddClaim(
                        new Claim(nameof(SignInCommandClaims.ApplicationUser),
                        JsonConvert.SerializeObject(GetApplicationUser(request, loginResp), jsonSerializerSettings)))
                    .WriteToken(),
            };
        }

        private SignInCommandClaims.ApplicationUser GetApplicationUser(SignInCommand request, LoginDto.LoginResp loginResp)
        {
            if (!Enum.TryParse(loginResp.LanguageCodeCode, out LanguageCode languageCode))
            {
                Log.Warning("Language code '{0}' is not supported. 'EN' is used instead.", loginResp.LanguageCodeCode);
                languageCode = LanguageCode.EN;
            }

            return new SignInCommandClaims.ApplicationUser
            {
                Id = loginResp.ApplicationUserId,
                Username = loginResp.ApplicationUserUsername,
                ApiUsername = loginResp.ApiUserName,
                Password = request.Password,
                LanguageCode = languageCode.ToString(),
                CompanyNumber = request.CompanyNumber,
                Person = string.IsNullOrEmpty(loginResp.PersonId) ?
                    null :
                    new SignInCommandClaims.Person
                    {
                        Id = loginResp.PersonId,
                        EmployeeNumber = loginResp.PersonEmployeeNumber,
                        FirstName = loginResp.PersonFirstName,
                        LastName = loginResp.PersonLastName,
                    },
            };
        }
    }
}
