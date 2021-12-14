namespace Domain.CommandInputs
{
    public struct IntegerInput
    {
        public IntegerInput(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}
