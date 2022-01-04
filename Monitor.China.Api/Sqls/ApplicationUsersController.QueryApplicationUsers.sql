select
    ApplicationUser.Id as ApplicationUserId,
    ApplicationUser.Username as ApplicationUserUsername
from
    monitor.ApplicationUser
    inner join monitor.Person on Person.ApplicationUserId = ApplicationUser.Id
    inner join ExtensionsUser.Proc_GetPersonExtraFieldStringByIdentifier(_Identifier = 'CN_PE_APIUN') 
        as PersonApiUserName on PersonApiUserName.PersonId = Person.Id