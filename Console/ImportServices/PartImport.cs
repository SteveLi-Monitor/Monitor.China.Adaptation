using Console.Models;
using Console.Models.ClassMaps;
using Console.Settings.CsvMappers;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Console.ImportServices
{
    internal class PartImport : ImportBase
    {
        private readonly PartCsvMapper partCsvMapper;
        private readonly PartMap partMap;

        public PartImport(IConfiguration configuration, PartMap partMap) : base(configuration)
        {
            this.partMap = partMap;

            partCsvMapper = configuration
                .GetSection(nameof(PartCsvMapper.Part)).Get<PartCsvMapper>();

            Log.Debug($"{nameof(PartImport)}: partCsvMapper is " + Environment.NewLine +
                "{@0}", partCsvMapper);
        }

        public override async Task StartAsync()
        {
            var fileInfo = new FileInfo(partCsvMapper.FilePath);
            if (!fileInfo.Exists)
            {
                Log.Error($"Missing file: {fileInfo.FullName}");
                return;
            }

            using var reader = new StreamReader(fileInfo.FullName);
            using var csv = new CsvReader(reader, BuildCsvConfiguration());
            csv.Context.RegisterClassMap(partMap);

            var lineNumber = 1;

            foreach (var part in csv.GetRecords<Part>())
            {
                lineNumber++;

                Log.Debug($"Line {lineNumber}: " + Environment.NewLine +
                    "{@0}", part);
            }
        }
    }
}
