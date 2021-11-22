alter procedure "ExtensionsUser"."Proc_GetPersonExtraFieldStringByIdentifier"
    (in _Identifier nvarchar(12) default null)
begin
	if _Identifier is null then
        return;
    else
        select
            Person.Id as PersonId,
            ExtraField.Id as ExtraFieldId,
            ExtraField.String as ExtraFieldString,
            ExtraFieldTemplate.Id as ExtraFieldTemplateId,
            ExtraFieldTemplate.Identifier as ExtraFieldTemplateIdentifier
        from
            monitor.Person
            inner join monitor.PersonExtraField on PersonExtraField.PersonId = Person.Id
            inner join monitor.ExtraField on ExtraField.Id = PersonExtraField.ExtraFieldId
            inner join monitor.ExtraFieldTemplate on ExtraFieldTemplate.Id = ExtraField.TemplateId
        where
            ExtraFieldTemplate.Identifier = _Identifier
    end if;
end