using System;
using System.Collections.Generic;

namespace Domain.Dtos
{
    public class AutoCompletesDto
    {
        public class PartReq : ReqBase
        {
            public PartReq(string filter) : base(filter)
            {
            }

            public PartReq() : base()
            {
            }

            public int LanguageId { get; set; }
        }

        public class PartResp
        {
            public IEnumerable<Part> Parts { get; set; }

            public class Part
            {
                public string PartId { get; set; }

                public string PartPartNumber { get; set; }

                public int PartType { get; set; }

                public string PartDescription { get; set; }
            }
        }

        public abstract class ReqBase
        {
            public ReqBase(string filter)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    var parameters = filter.Split(' ');
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                Param1 = $"%{parameters[i]}%";
                                break;

                            case 1:
                                Param2 = $"%{parameters[i]}%";
                                break;

                            case 2:
                                Param3 = $"%{parameters[i]}%";
                                break;

                            case 3:
                                Param4 = $"%{parameters[i]}%";
                                break;

                            case 4:
                                Param5 = $"%{parameters[i]}%";
                                break;
                        }
                    }
                }
            }

            public ReqBase()
            {
            }

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
