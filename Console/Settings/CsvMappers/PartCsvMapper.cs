namespace Console.Settings.CsvMappers
{
    internal class PartCsvMapper : CsvMapperBase
    {
        public const string Part = "Part";

        public int PartNumber { get; set; }

        public int PartName { get; set; }

        public int PartType { get; set; }

        public int AdditionalName { get; set; }
    }
}
