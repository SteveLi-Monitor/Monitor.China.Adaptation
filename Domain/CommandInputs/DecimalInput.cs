namespace Domain.CommandInputs
{
    public struct DecimalInput
    {
        public DecimalInput(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; set; }
    }
}
