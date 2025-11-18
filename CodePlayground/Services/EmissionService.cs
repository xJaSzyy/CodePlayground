using CodePlayground.Enums;
using CodePlayground.Interfaces;
using CodePlayground.Models;

namespace CodePlayground.Services;

public class EmissionService : IEmissionService
{
    public GasolineGeneratorEmissions CalculateGasolineGeneratorEmissions(Pollutant pollutant, int workHoursPerDay,
        int workDaysPerYear, int generatorCount, int sameGeneratorCount)
    {
        var pollutantInfo = DataStorage.PollutantInfos.First(i => i.Pollutant == pollutant);
        
        var maximumEmission = 0.25f * pollutantInfo.SpecificEmission * 5f * sameGeneratorCount / 3600f;
        var grossEmission = 0.25f * pollutantInfo.SpecificEmission * 5f * workHoursPerDay * workDaysPerYear * generatorCount * 1e-6f;

        var result = new GasolineGeneratorEmissions
        {
            PollutantInfo = pollutantInfo,
            MaximumEmission = maximumEmission,
            GrossEmission = grossEmission
        };
        
        return result;
    }

    public (float, float) CalculateReservoirsEmissions(float maximumConcentration, float drainedVolume,
        float averageDrainTime, float oilAmountInAutumnWinter, float oilAmountInSpringSummer,
        float fillingConcentrationInAutumnWinter, float fillingConcentrationInSpringSummer,
        float pollutantConcentration, int precision = 6)
    {
        var annualInjectionEmissions = (fillingConcentrationInAutumnWinter * oilAmountInAutumnWinter +
                                        fillingConcentrationInSpringSummer * oilAmountInSpringSummer) * 1e-6f;
        var annualIrrigationEmissions = 50f * (oilAmountInAutumnWinter + oilAmountInSpringSummer) * 1e-6f;

        var maximumEmission = (maximumConcentration * drainedVolume) / averageDrainTime;
        var grossEmission = annualInjectionEmissions + annualIrrigationEmissions;

        var finalMaximumEmission = maximumEmission * pollutantConcentration * 1e-2f;
        var finalGrossEmission = grossEmission * pollutantConcentration * 1e-2f;

        return ((float)Math.Round(finalMaximumEmission, precision), (float)Math.Round(finalGrossEmission, precision));
    }
    
    public (float, float) CalculateDuringMetalMachiningEmissions(MetalMachiningMachineType type, float annualEquipmentOperatingTimeFund, int precision = 6)
    {
        var specificDustEmissions = _specificDustEmissionsByType.GetValueOrDefault(type, 0f);
        
        var maximumEmission = 0.2f * specificDustEmissions;
        var grossEmission = 0.2f * 3.6f * specificDustEmissions * annualEquipmentOperatingTimeFund * 1e-3f;

        return ((float)Math.Round(maximumEmission, precision), (float)Math.Round(grossEmission, precision));
    }

    private readonly Dictionary<MetalMachiningMachineType, float> _specificDustEmissionsByType = new()
    {
        { MetalMachiningMachineType.Drilling, 0.007f },
        { MetalMachiningMachineType.Milling, 0.097f },
        { MetalMachiningMachineType.Cutting, 0.203f }
    };
}