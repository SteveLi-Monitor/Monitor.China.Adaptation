using System;

namespace Domain.CommandInputs
{
    public struct DateTimeOffsetInput
    {
        public DateTimeOffsetInput(DateTimeOffset value)
        {
            Value = value;
        }

        public DateTimeOffset Value { get; set; }
    }
}
