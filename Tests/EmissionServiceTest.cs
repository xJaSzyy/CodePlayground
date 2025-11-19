using CodePlayground;
using CodePlayground.Enums;
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

    [TestCase(Pollutant.CO, 1, 365, 1, 1, 0.002604f, 0.003422f)]
    [TestCase(Pollutant.CH, 1, 365, 1, 1, 0.000347f, 0.000456f)]
    [TestCase(Pollutant.NO2, 1, 365, 1, 1, 0.000039f, 0.000051f)]
    [TestCase(Pollutant.NO, 1, 365, 1, 1, 0.000006f, 0.000008f)]
    [TestCase(Pollutant.SO2, 1, 365, 1, 1, 0.000012f, 0.000016f)]
    public void CalculateGasolineGeneratorEmissions_ShouldReturnCorrectValues(Pollutant pollutant,
        int workHoursPerDay,
        int workDaysPerYear, int generatorCount, int sameGeneratorCount, float expectedMaximumEmission,
        float expectedGrossEmission)
    {
        // Arrange
        var info = DataStorage.PollutantInfos.First(i => i.Pollutant == pollutant);
        
        // Act
        var result = _service.CalculateGasolineGeneratorEmissions(pollutant, workHoursPerDay, workDaysPerYear,
            generatorCount, sameGeneratorCount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.PollutantInfo.Code, Is.EqualTo(info.Code));
            Assert.That(result.PollutantInfo.Name, Is.EqualTo(info.Name));
            Assert.That(result.PollutantInfo.ShortName, Is.EqualTo(info.ShortName));
            Assert.That(result.PollutantInfo.Pollutant, Is.EqualTo(info.Pollutant));
            Assert.That(result.PollutantInfo.SpecificEmission, Is.EqualTo(info.SpecificEmission));
            Assert.That((float)Math.Round(result.MaximumEmission, 6), Is.EqualTo(expectedMaximumEmission));
            Assert.That((float)Math.Round(result.GrossEmission, 6), Is.EqualTo(expectedGrossEmission));
        });
    }

    [TestCase(Pollutant.RPK240280, ReservoirType.Ground, OilProduct.DieselFuel, ClimateZone.Second, 100f, 50f, 150f,
        1200f, 0.2325f, 0.000162f, 0.0075f, 0.231849f, 0.007641f)]
    [TestCase(Pollutant.H2S, ReservoirType.Ground, OilProduct.DieselFuel, ClimateZone.Second, 100f, 50f, 150f,
        1200f, 0.2325f, 0.000162f, 0.0075f, 0.000651f, 0.000021f)]
    public void CalculateReservoirsEmissions_ShouldReturnCorrectValues(Pollutant pollutant, ReservoirType reservoirType,
        OilProduct oilProduct, ClimateZone climateZone, float autumnWinterOilAmount, float springSummerOilAmount,
        float drainedVolume, float averageDrainTime, float maxVaporEmission, float annualInjectionEmissions,
        float annualIrrigationEmissions, float expectedMaximumEmission, float expectedGrossEmission)
    {
        // Arrange
        var info = DataStorage.PollutantInfos.First(i => i.Pollutant == pollutant);
        var vaporConcentration = DataStorage.VaporConcentration[reservoirType][climateZone][oilProduct];

        // Act
        var result = _service.CalculateReservoirsEmissions(pollutant, vaporConcentration, autumnWinterOilAmount,
            springSummerOilAmount, drainedVolume, averageDrainTime);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.PollutantInfo.Code, Is.EqualTo(info.Code));
            Assert.That(result.PollutantInfo.Name, Is.EqualTo(info.Name));
            Assert.That(result.PollutantInfo.ShortName, Is.EqualTo(info.ShortName));
            Assert.That(result.PollutantInfo.Pollutant, Is.EqualTo(info.Pollutant));
            Assert.That(result.PollutantInfo.SpecificEmission, Is.EqualTo(info.SpecificEmission));
            Assert.That((float)Math.Round(result.MaxVaporEmission, 6), Is.EqualTo(maxVaporEmission));
            Assert.That((float)Math.Round(result.AnnualInjectionEmissions, 6), Is.EqualTo(annualInjectionEmissions));
            Assert.That((float)Math.Round(result.AnnualIrrigationEmissions, 6), Is.EqualTo(annualIrrigationEmissions));
            Assert.That((float)Math.Round(result.MaximumEmission, 6), Is.EqualTo(expectedMaximumEmission));
            Assert.That((float)Math.Round(result.GrossEmission, 6), Is.EqualTo(expectedGrossEmission));
        });
    }

    [TestCase(MetalMachiningMachineType.Drilling, 365, 0.0014f, 0.001840f)]
    public void CalculateDuringMetalMachiningEmissions_ShouldReturnCorrectValues(MetalMachiningMachineType type, float annualEquipmentOperatingTimeFund, float expectedMaximumEmission, float expectedGrossEmission)
    {
        // Act
        var result = _service.CalculateDuringMetalMachiningEmissions(type, annualEquipmentOperatingTimeFund);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Item1, Is.EqualTo(expectedMaximumEmission));
            Assert.That(result.Item2, Is.EqualTo(expectedGrossEmission));
        });
    }
}