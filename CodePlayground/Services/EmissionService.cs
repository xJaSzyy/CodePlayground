using CodePlayground.Enums;
using CodePlayground.Interfaces;
using CodePlayground.Models;

namespace CodePlayground.Services;

public class EmissionService : IEmissionService
{
    #region Public

    public List<GasolineGeneratorEmissionsResult> CalculateGasolineGeneratorEmissionsBatch(List<Pollutant> pollutants,
        int workHoursPerDay, int workDaysPerYear, int generatorCount, int sameGeneratorCount)
    {
        var result = new List<GasolineGeneratorEmissionsResult>();

        foreach (var pollutant in pollutants)
        {
            result.Add(CalculateGasolineGeneratorEmissions(pollutant, workHoursPerDay, workDaysPerYear, generatorCount, sameGeneratorCount));
        }

        return result.OrderBy(e => e.PollutantInfo.Code).ToList();
    }
    
    public ReservoirsEmissionsResult CalculateReservoirsEmissions(Pollutant pollutant,
        VaporConcentrationRecord vaporConcentration, float autumnWinterOilAmount, float springSummerOilAmount,
        float drainedVolume, float averageDrainTime = 1200f)
    {
        var pollutantInfo = DataStorage.PollutantInfos.First(i => i.Pollutant == pollutant);

        var annualInjectionEmissions = (vaporConcentration.AutumnWinterVaporConcentration * autumnWinterOilAmount +
                                        vaporConcentration.SpringSummerVaporConcentration * springSummerOilAmount) *
                                       1e-6f;
        var annualIrrigationEmissions = 50f * (autumnWinterOilAmount + springSummerOilAmount) * 1e-6f;

        var maxVaporEmission = (vaporConcentration.MaxVaporConcentration * drainedVolume) / averageDrainTime;
        var grossEmission = annualInjectionEmissions + annualIrrigationEmissions;

        var result = new ReservoirsEmissionsResult
        {
            PollutantInfo = pollutantInfo,
            MaxVaporEmission = maxVaporEmission,
            AnnualInjectionEmissions = annualInjectionEmissions,
            AnnualIrrigationEmissions = annualIrrigationEmissions,
            MaximumEmission = maxVaporEmission * pollutantInfo.SpecificEmission * 1e-2f,
            GrossEmission = grossEmission * pollutantInfo.SpecificEmission * 1e-2f,
        };

        return result;
    }

    public (float, float) CalculateDuringMetalMachiningEmissions(MetalMachiningMachineType type,
        float annualEquipmentOperatingTimeFund, int precision = 6)
    {
        var specificDustEmissions = DataStorage.SpecificDustEmissionsByType.GetValueOrDefault(type, 0f);

        var maximumEmission = 0.2f * specificDustEmissions;
        var grossEmission = 0.2f * 3.6f * specificDustEmissions * annualEquipmentOperatingTimeFund * 1e-3f;

        return ((float)Math.Round(maximumEmission, precision), (float)Math.Round(grossEmission, precision));
    }

    #endregion

    #region Private

    private GasolineGeneratorEmissionsResult CalculateGasolineGeneratorEmissions(Pollutant pollutant,
        int workHoursPerDay, int workDaysPerYear, int generatorCount, int sameGeneratorCount)
    {
        var pollutantInfo = DataStorage.PollutantInfos.First(i => i.Pollutant == pollutant);

        var maximumEmission = 0.25f * pollutantInfo.SpecificEmission * 5f * sameGeneratorCount / 3600f;
        var grossEmission = 0.25f * pollutantInfo.SpecificEmission * 5f * workHoursPerDay * workDaysPerYear *
                            generatorCount * 1e-6f;

        var result = new GasolineGeneratorEmissionsResult
        {
            PollutantInfo = pollutantInfo,
            MaximumEmission = maximumEmission,
            GrossEmission = grossEmission
        };

        return result;
    }

    #endregion
}