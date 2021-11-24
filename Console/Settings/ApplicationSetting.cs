namespace Console.Settings
{
    internal class ApplicationSetting
    {
        public CsvParser CsvParser { get; set; }
    }

    internal class CsvParser
    {
        public string Delimiter { get; set; }
    }
}
