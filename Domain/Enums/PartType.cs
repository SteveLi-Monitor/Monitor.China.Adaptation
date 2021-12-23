using System.Collections.Generic;

namespace Domain.Enums
{
    public enum PartType
    {
        Purchased = 0,
        Manufactured = 1,
        Fictitious = 2,
        Service = 5,
        Subcontract = 6,
    }

    public static class PartTypeDescription
    {
        public static Dictionary<LanguageCode, Dictionary<PartType, string>> Descriptions
            = new Dictionary<LanguageCode, Dictionary<PartType, string>>
            {
                { LanguageCode.EN, new Dictionary<PartType, string>
                    {
                        { PartType.Purchased, "Purchased" },
                        { PartType.Manufactured, "Manufactured" },
                        { PartType.Fictitious, "Fictitious" },
                        { PartType.Service, "Service" },
                        { PartType.Subcontract, "Subcontract" },
                    }
                },
                { LanguageCode.ZH, new Dictionary<PartType, string>
                    {
                        { PartType.Purchased, "采购件" },
                        { PartType.Manufactured, "生产件" },
                        { PartType.Fictitious, "虚拟件" },
                        { PartType.Service, "服务" },
                        { PartType.Subcontract, "外协" },
                    }
                },
            };

        public static string GetDescription(LanguageCode languageCode, PartType partType)
        {
            return Descriptions[languageCode][partType];
        }
    }
}
