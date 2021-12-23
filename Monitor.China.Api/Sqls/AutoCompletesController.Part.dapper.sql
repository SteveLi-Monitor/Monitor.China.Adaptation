select
    top :PageSize
    Part.Id as PartId,
    Part.PartNumber as PartPartNumber,
    Part.Type as PartType,
    ExtensionsUser.Func_GetPartTranslationByPartIdAndLanguageId(Part.Id, :LanguageId) as PartDescription
from
    monitor.Part
where
    (Part.PartNumber like :Param1 or PartDescription like :Param1)
    and (Part.PartNumber like :Param2 or PartDescription like :Param2)
    and (Part.PartNumber like :Param3 or PartDescription like :Param3)
    and (Part.PartNumber like :Param4 or PartDescription like :Param4)
    and (Part.PartNumber like :Param5 or PartDescription like :Param5)
order by
    Part.PartNumber