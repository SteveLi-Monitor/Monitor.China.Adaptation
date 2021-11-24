using Console.Settings;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Console.ImportServices
{
    internal abstract class ImportBase : IImportService
    {
        public ImportBase(IConfiguration configuration)
        {
            ApplicationSetting = configuration
                .GetSection(nameof(ApplicationSetting)).Get<ApplicationSetting>();

            Log.Debug($"{nameof(ImportBase)}: ApplicationSetting is " + Environment.NewLine +
                "{@0}", ApplicationSetting);
        }

        protected ApplicationSetting ApplicationSetting { get; set; }

        public abstract Task StartAsync();

        protected CsvConfiguration BuildCsvConfiguration()
        {
            return new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = delegate { },
                Delimiter = ApplicationSetting.CsvParser.Delimiter,
            };
        }
    }
}
