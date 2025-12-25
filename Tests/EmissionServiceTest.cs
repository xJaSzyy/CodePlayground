using CodePlayground;
using CodePlayground.Enums;
using CodePlayground.Interfaces;
using CodePlayground.Models;
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
        var result = _service.CalculateGasolineGeneratorEmissionsBatch(new List<Pollutant> { pollutant }, workHoursPerDay, workDaysPerYear,
            generatorCount, sameGeneratorCount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.First().PollutantInfo.Code, Is.EqualTo(info.Code));
            Assert.That(result.First().PollutantInfo.Name, Is.EqualTo(info.Name));
            Assert.That(result.First().PollutantInfo.ShortName, Is.EqualTo(info.ShortName));
            Assert.That(result.First().PollutantInfo.Pollutant, Is.EqualTo(info.Pollutant));
            Assert.That(result.First().PollutantInfo.SpecificEmission, Is.EqualTo(info.SpecificEmission));
            Assert.That((float)Math.Round(result.First().MaximumEmission, 6), Is.EqualTo(expectedMaximumEmission));
            Assert.That((float)Math.Round(result.First().GrossEmission, 6), Is.EqualTo(expectedGrossEmission));
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
        var result = _service.CalculateReservoirsEmissionsBatch(new List<Pollutant> { pollutant }, vaporConcentration, autumnWinterOilAmount,
            springSummerOilAmount, drainedVolume, averageDrainTime);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Emissions.First().PollutantInfo.Code, Is.EqualTo(info.Code));
            Assert.That(result.Emissions.First().PollutantInfo.Name, Is.EqualTo(info.Name));
            Assert.That(result.Emissions.First().PollutantInfo.ShortName, Is.EqualTo(info.ShortName));
            Assert.That(result.Emissions.First().PollutantInfo.Pollutant, Is.EqualTo(info.Pollutant));
            Assert.That(result.Emissions.First().PollutantInfo.SpecificEmission, Is.EqualTo(info.SpecificEmission));
            Assert.That((float)Math.Round(result.MaxVaporEmission, 6), Is.EqualTo(maxVaporEmission));
            Assert.That((float)Math.Round(result.AnnualInjectionEmissions, 6), Is.EqualTo(annualInjectionEmissions));
            Assert.That((float)Math.Round(result.AnnualIrrigationEmissions, 6), Is.EqualTo(annualIrrigationEmissions));
            Assert.That((float)Math.Round(result.Emissions.First().MaximumEmission, 6), Is.EqualTo(expectedMaximumEmission));
            Assert.That((float)Math.Round(result.Emissions.First().GrossEmission, 6), Is.EqualTo(expectedGrossEmission));
        });
    }

    [TestCase(MetalMachiningMachineType.Drilling, 365, 0.0014f, 0.001840f)]
    [TestCase(MetalMachiningMachineType.Milling, 365, 0.0194f, 0.025492f)]
    [TestCase(MetalMachiningMachineType.Cutting, 365, 0.0406f, 0.053348f)]
    public void CalculateDuringMetalMachiningEmissions_ShouldReturnCorrectValues(MetalMachiningMachineType type, int workDaysPerYear, float expectedMaximumEmission, float expectedGrossEmission)
    {
        // Arrange
        var info = DataStorage.PollutantInfos.First(i => i.Pollutant == Pollutant.Fe2O3);
        
        // Act
        var result = _service.CalculateDuringMetalMachiningEmissions(type, workDaysPerYear);

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
    
    [TestCase(Pollutant.Fe2O3, 241.36f, 365, 0.00061f, 0.000802f)]
    [TestCase(Pollutant.MnO2, 241.36f, 365, 0.000108f, 0.000142f)]
    [TestCase(Pollutant.FluorideGases, 241.36f, 365, 0.000025f, 0.000033f)]
    public void CalculateDuringWeldingOperationsEmissions_ShouldReturnCorrectValues(Pollutant pollutant, float electrodesPerYear, int workDaysPerYear, float expectedMaximumEmission, float expectedGrossEmission)
    {
        // Arrange
        var info = DataStorage.PollutantInfos.First(i => i.Pollutant == pollutant);
        
        // Act
        var result = _service.CalculateDuringWeldingOperationsEmissionsBatch(new List<Pollutant> { pollutant }, electrodesPerYear, workDaysPerYear);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Emissions.First().PollutantInfo.Code, Is.EqualTo(info.Code));
            Assert.That(result.Emissions.First().PollutantInfo.Name, Is.EqualTo(info.Name));
            Assert.That(result.Emissions.First().PollutantInfo.ShortName, Is.EqualTo(info.ShortName));
            Assert.That(result.Emissions.First().PollutantInfo.Pollutant, Is.EqualTo(info.Pollutant));
            Assert.That(result.Emissions.First().PollutantInfo.SpecificEmission, Is.EqualTo(info.SpecificEmission));
            Assert.That((float)Math.Round(result.Emissions.First().MaximumEmission, 6), Is.EqualTo(expectedMaximumEmission));
            Assert.That((float)Math.Round(result.Emissions.First().GrossEmission, 6), Is.EqualTo(expectedGrossEmission));
        });
    }
    
    /*[TestCase(1, 365, 1, 1, , )]
    [TestCase(Pollutant.CH, 1, 365, 1, 1, )]
    [TestCase(Pollutant.NO2, 1, 365, 1, 1, )]
    [TestCase(Pollutant.NO, 1, 365, 1, 1, )]
    [TestCase(Pollutant.SO2, 1, 365, 1, 1, )]*/
    [Test]
    public void CalculateGasolineGeneratorEmissionsBatch_ShouldReturnCorrectValues()
    {
        // Arrange
        var workHoursPerDay = 1;
        var workDaysPerYear = 365;
        var generatorCount = 1;
        var sameGeneratorCount = 1;
        var pollutants = new List<Pollutant> { Pollutant.CO, Pollutant.CH, Pollutant.NO2, Pollutant.NO, Pollutant.SO2 };
        var expectedEmissionsResult = new List<EmissionsResult>
        {
            new()
            {
                PollutantInfo = DataStorage.PollutantInfos.First(i => i.Pollutant == Pollutant.CO),
                MaximumEmission = 0.002604f,
                GrossEmission = 0.003422f
            },
            new()
            {
                PollutantInfo = DataStorage.PollutantInfos.First(i => i.Pollutant == Pollutant.CH),
                MaximumEmission = 0.000347f,
                GrossEmission = 0.000456f
            },
            new()
            {
                PollutantInfo = DataStorage.PollutantInfos.First(i => i.Pollutant == Pollutant.NO2),
                MaximumEmission = 0.000039f,
                GrossEmission = 0.000051f
            },
            new()
            {
                PollutantInfo = DataStorage.PollutantInfos.First(i => i.Pollutant == Pollutant.NO),
                MaximumEmission = 0.000006f,
                GrossEmission = 0.000008f
            },
            new()
            {
                PollutantInfo = DataStorage.PollutantInfos.First(i => i.Pollutant == Pollutant.SO2),
                MaximumEmission = 0.000012f,
                GrossEmission = 0.000016f
            }
        };
        
        // Act
        var result = _service.CalculateGasolineGeneratorEmissionsBatch(pollutants, workHoursPerDay, workDaysPerYear, generatorCount, sameGeneratorCount);

        // Assert
        Assert.That(result, Has.Count.EqualTo(expectedEmissionsResult.Count));
        foreach (var actualResult in result)
        {
            var expectedResult = expectedEmissionsResult.FirstOrDefault(e => e.PollutantInfo.Code == actualResult.PollutantInfo.Code);
            Assert.That(expectedResult, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(actualResult.PollutantInfo.Code, Is.EqualTo(expectedResult.PollutantInfo.Code));
                Assert.That(actualResult.PollutantInfo.Name, Is.EqualTo(expectedResult.PollutantInfo.Name));
                Assert.That(actualResult.PollutantInfo.ShortName, Is.EqualTo(expectedResult.PollutantInfo.ShortName));
                Assert.That(actualResult.PollutantInfo.Pollutant, Is.EqualTo(expectedResult.PollutantInfo.Pollutant));
                Assert.That(actualResult.PollutantInfo.SpecificEmission, Is.EqualTo(expectedResult.PollutantInfo.SpecificEmission));
                Assert.That((float)Math.Round(actualResult.MaximumEmission, 6), Is.EqualTo(expectedResult.MaximumEmission));
                Assert.That((float)Math.Round(actualResult.GrossEmission, 6), Is.EqualTo(expectedResult.GrossEmission));
            });
        }
    }
}