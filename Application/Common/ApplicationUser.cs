using Application.Entities;
using Domain.Common;
using Domain.Extensions;
using System.Collections.Generic;

namespace Application.Common
{
    public class ApplicationUser : MonitorApiUser
    {
        public ApplicationUser()
        {
            UiComponents = new List<UiComponent>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public IList<UiComponent> UiComponents { get; set; }


        public string ParentUserId { get; set; }

        public ApplicationUser ParentUser { get; set; }

        public IList<ApplicationUser> ChildUsers { get; set; }


        public int? UserRoleId { get; set; }

        public UserRole UserRole { get; set; }

        public void CopyFrom(MonitorApiUser applicationUser)
        {
            applicationUser.Guard(nameof(applicationUser));
            applicationUser.Guard();

            ApiUsername = applicationUser.ApiUsername;
            Password = applicationUser.Password;
            LanguageCode = applicationUser.LanguageCode;
            CompanyNumber = applicationUser.CompanyNumber;
        }
    }
}
