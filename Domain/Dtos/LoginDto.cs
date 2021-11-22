using Domain.Extensions;

namespace Domain.Dtos
{
    public class LoginDto
    {
        public class LoginReq
        {
            public string Identifier { get; set; }

            public void Guard()
            {
                Identifier.Guard(nameof(Identifier));
            }
        }

        public class LoginResp
        {
            public string ApplicationUserId { get; set; }

            public string ApplicationUserUsername { get; set; }

            public string LanguageCodeCode { get; set; }

            public string PersonId { get; set; }

            public string PersonEmployeeNumber { get; set; }

            public string PersonFirstName { get; set; }

            public string PersonLastName { get; set; }

            public string ApiUserName { get; set; }
        }
    }
}
