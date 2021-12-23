select
    top 15
    Part.Id as PartId,
    Part.PartNumber as PartPartNumber,
    Part.Type as PartType,
    ExtensionsUser.Func_GetPartTranslationByPartIdAndLanguageId(Part.Id, 7) as PartDescription
from
    monitor.Part
where
    (Part.PartNumber like '%c%' or PartDescription like '%c%')
    and (Part.PartNumber like '%mm%' or PartDescription like '%mm%')
order by
    Part.PartNumber