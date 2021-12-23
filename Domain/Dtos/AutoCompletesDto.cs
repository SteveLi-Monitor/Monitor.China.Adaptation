using System;

namespace Domain.Dtos
{
    public class AutoCompletesDto
    {
        public class PartReq : ReqBase
        {
            public int LanguageId { get; set; }
        }

        public class PartResp
        {
            public string PartId { get; set; }

            public string PartPartNumber { get; set; }

            public int PartType { get; set; }

            public string PartDescription { get; set; }
        }

        public class ReqBase
        {
            public const string EmptyParam = "%%";

            public int PageSize { get; set; } = 25;

            public string Param1 { get; set; } = EmptyParam;

            public string Param2 { get; set; } = EmptyParam;

            public string Param3 { get; set; } = EmptyParam;

            public string Param4 { get; set; } = EmptyParam;

            public string Param5 { get; set; } = EmptyParam;

            public virtual void Guard()
            {
                if (PageSize <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(PageSize),
                        PageSize,
                        $"{nameof(PageSize)} must be greater than 0");
                }

                GuardParam(nameof(Param1), Param1);
                GuardParam(nameof(Param2), Param2);
                GuardParam(nameof(Param3), Param3);
                GuardParam(nameof(Param4), Param4);
                GuardParam(nameof(Param5), Param5);
            }

            private void GuardParam(string propertyName, string propertyValue)
            {
                if (!propertyValue.StartsWith("%") || !propertyValue.EndsWith("%"))
                {
                    throw new ArgumentException($"{propertyName} must be like {EmptyParam}. {propertyName}: {propertyValue}.");
                }
            }
        }
    }
}
