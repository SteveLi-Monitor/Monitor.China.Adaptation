namespace Domain.CommandInputs
{
    public struct StringInput
    {
        public StringInput(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}
