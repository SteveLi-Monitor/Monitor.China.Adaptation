namespace Application.Users.Commands.SignIn
{
    public class SignInCommandClaims
    {
        public class ApplicationUser
        {
            public string Id { get; set; }

            public string Username { get; set; }

            public string ApiUsername { get; set; }

            public string Password { get; set; }

            public string LanguageCode { get; set; }

            public string CompanyNumber { get; set; }

            public Person Person { get; set; }
        }

        public class Person
        {
            public string Id { get; set; }

            public string EmployeeNumber { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
    }
}
