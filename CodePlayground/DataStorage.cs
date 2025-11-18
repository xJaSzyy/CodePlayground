using CodePlayground.Enums;
using CodePlayground.Models;

namespace CodePlayground;

public static class DataStorage
{
    public static readonly List<PollutantInfo> PollutantInfos = new()
    {
        new PollutantInfo
        {
            Code = 337,
            Name = "Углерода оксид (углерод окись; углерод моноокись; угарный газ)",
            Pollutant = Pollutant.CO,
            SpecificEmission = 7.5f
        },
        new PollutantInfo
        {
            Code = 2704,
            Name = "Бензин (нефтяной, малосернистый) /в пересчете на углерод/",
            Pollutant = Pollutant.CH,
            SpecificEmission = 1.0f
        },
        new PollutantInfo
        {
            Code = 301,
            Name = "Азота диоксид (двуокись азота; пероксид азота)",
            Pollutant = Pollutant.NO2,
            SpecificEmission = 0.112f
        },
        new PollutantInfo
        {
            Code = 304,
            Name = "Азота оксид (азот (II) оксид; азот монооксид)",
            Pollutant = Pollutant.NO,
            SpecificEmission = 0.0182f
        },
        new PollutantInfo
        {
            Code = 330,
            Name = "Серы диоксид",
            Pollutant = Pollutant.SO2,
            SpecificEmission = 0.036f
        }
    };
}