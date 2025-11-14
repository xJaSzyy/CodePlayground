using CodePlayground.Interfaces;
using CodePlayground.Services;

namespace Tests;

public class EmissionServiceTest
{
    private IEmissionService _service;

    [SetUp]
    public void Setup()
    {
        _service = new EmissionService();
    }

    [TestCase(7.5f, 1, 365, 1, 1, 0.002604f, 0.003422f)]
    [TestCase(1f, 1, 365, 1, 1, 0.000347f, 0.000456f)]
    [TestCase(0.14f, 1, 365, 1, 1, 0.000049f, 0.000064f)]
    [TestCase(0.112f, 1, 365, 1, 1, 0.000039f, 0.000051f)]
    [TestCase(0.0182f, 1, 365, 1, 1, 0.000006f, 0.000008f)]
    [TestCase(0.036f, 1, 365, 1, 1, 0.000012f, 0.000016f)]
    public void CalculateGasolineGeneratorEmissions_ShouldReturnCorrectValues(float specificEmission,
        int workHoursPerDay,
        int workDaysPerYear, int generatorCount, int sameGeneratorCount, float expectedMaximumEmission,
        float expectedGrossEmission)
    {
        // Act
        var result = _service.CalculateGasolineGeneratorEmissions(specificEmission, workHoursPerDay, workDaysPerYear,
            generatorCount, sameGeneratorCount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Item1, Is.EqualTo(expectedMaximumEmission));
            Assert.That(result.Item2, Is.EqualTo(expectedGrossEmission));
        });
    }

    [TestCase(1.86f, 150f, 1200f, 100f, 50f, 
        0.96f, 1.32f, 99.72f, 0.231849f, 0.007641f)]
    public void CalculateReservoirsEmissions_ShouldReturnCorrectValues(float maximumConcentration, float drainedVolume,
        float averageDrainTime, float oilAmountInAutumnWinter, float oilAmountInSpringSummer,
        float fillingConcentrationInAutumnWinter, float fillingConcentrationInSpringSummer,
        float pollutantConcentration, float expectedMaximumEmission, float expectedGrossEmission)
    {
        // Act
        var result = _service.CalculateReservoirsEmissions(maximumConcentration, drainedVolume, averageDrainTime,
            oilAmountInAutumnWinter, oilAmountInSpringSummer, fillingConcentrationInAutumnWinter,
            fillingConcentrationInSpringSummer, pollutantConcentration);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Item1, Is.EqualTo(expectedMaximumEmission));
            Assert.That(result.Item2, Is.EqualTo(expectedGrossEmission));
        });
    }
}