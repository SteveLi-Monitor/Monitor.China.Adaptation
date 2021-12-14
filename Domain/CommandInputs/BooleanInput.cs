namespace Domain.CommandInputs
{
    public struct BooleanInput
    {
        public BooleanInput(bool value)
        {
            Value = value;
        }

        public bool Value { get; set; }
    }
}
