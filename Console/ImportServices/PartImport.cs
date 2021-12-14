using Console.Exceptions;
using Console.Models;
using Console.Models.ClassMaps;
using Console.MonitorApis;
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
        private readonly MonitorApiService monitorApiService;

        public PartImport(
            IConfiguration configuration,
            PartMap partMap,
            MonitorApiService monitorApiService) : base(configuration)
        {
            this.partMap = partMap;
            this.monitorApiService = monitorApiService;

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

                try
                {
                    Log.Debug($"Line {lineNumber}: " + Environment.NewLine +
                    "{@0}", part);

                    var partFound = await monitorApiService.GetPartByPartNumber(part.PartNumber);
                    if (partFound != null)
                    {
                        throw new EntityAlreadyExistsException("Part", "PartNumber", part.PartNumber);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed to import line {0}: {@1}", lineNumber, part);
                }
            }
        }
    }
}
