using Console.Settings.CsvMappers;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace Console.Models.ClassMaps
{
    internal class PartMap : ClassMapBase<Part>
    {
        public PartMap(IConfiguration configuration)
        {
            var csvMapper = configuration
                .GetSection(nameof(PartCsvMapper.Part)).Get<PartCsvMapper>();

            Log.Debug($"{nameof(PartMap)}: csvMapper is " + Environment.NewLine +
                "{@0}", csvMapper);

            MapIndex(x => x.PartNumber, csvMapper.PartNumber - 1);
            MapIndex(x => x.PartName, csvMapper.PartName - 1);
            MapIndex(x => x.PartType, csvMapper.PartType - 1);
            MapIndex(x => x.AdditionalName, csvMapper.AdditionalName - 1);
        }
    }
}
