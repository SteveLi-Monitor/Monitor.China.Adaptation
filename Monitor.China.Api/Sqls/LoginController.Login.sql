select
    ApplicationUser.Id as ApplicationUserId,
    ApplicationUser.Username as ApplicationUserUsername,
    LanguageCode.Code as LanguageCodeCode,
    Person.Id as PersonId,
    Person.EmployeeNumber as PersonEmployeeNumber,
    Person.FirstName as PersonFirstName,
    Person.LastName as PersonLastName,
    coalesce(PersonApiUserName.ExtraFieldString, ApplicationUser.Username) AS ApiUserName
from
    monitor.ApplicationUser
    inner join monitor.LanguageCode on LanguageCode.Id = ApplicationUser.LanguageId
    left outer join monitor.Person on Person.ApplicationUserId = ApplicationUser.Id
    left outer join ExtensionsUser.Proc_GetPersonExtraFieldStringByIdentifier(_Identifier = 'CN_PE_APIUN') 
        as PersonApiUserName on PersonApiUserName.PersonId = Person.Id
where
    ApplicationUser.Username = 'STEVE.LI'