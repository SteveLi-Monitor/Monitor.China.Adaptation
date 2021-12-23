alter function "ExtensionsUser"."Func_GetPartTranslationByPartIdAndLanguageId"
    (in _PartId bigint default null,
     in _LanguageId bigint default null)
returns nvarchar(400)
deterministic
begin
    declare _Description nvarchar(400);
    declare _Translation nvarchar(400);

    if _PartId is null then
        return;
    end if;

    select
        Part.Description into _Description
    from
        monitor.Part
    where
        Part.Id = _PartId;

    if _LanguageId is null then
        return _Description;
    end if;

    select
        PartTranslation.Description into _Translation
    from
        monitor.PartTranslation
    where
        PartTranslation.PartId = _PartId
        and PartTranslation.LanguageId = _LanguageId;
    
    return coalesce(_Translation, _Description);
end