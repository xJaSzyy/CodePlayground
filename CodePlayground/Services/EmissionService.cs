using CodePlayground.Interfaces;

namespace CodePlayground.Services;

public class EmissionService : IEmissionService
{
    public (float, float) CalculateGasolineGeneratorEmissions(float specificEmission, int workHoursPerDay,
        int workDaysPerYear, int generatorCount, int sameGeneratorCount, float moveSpeed = 5f, int precision = 6)
    {
        var maximumSingle = 0.25f * specificEmission * moveSpeed * sameGeneratorCount / 3600f;
        var grossEmission = 0.25f * specificEmission * moveSpeed * workHoursPerDay * workDaysPerYear * generatorCount *
                            1e-6f;

        return ((float)Math.Round(maximumSingle, precision), (float)Math.Round(grossEmission, precision));
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
}