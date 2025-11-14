using CodePlayground.Interfaces;
using CodePlayground.Models;
using CodePlayground.Services;

namespace Tests;

public class MineServiceTest
{
    private IMineService _service;

    [SetUp]
    public void Setup()
    {
        _service = new MineService();
    }

    [TestCase(4.90d, 3.60d, 0.3d, 0.1d,
        1.5d, 20.0d, 4.5d,
        0.9d, 0.9d, 17.6d,
        0.2d, 0.67d, 1.0d,
        1.24d, 1.28d,
        2.0d, 35.2d, 37.0d,
        40.7d)]

    [TestCase(4.61d, 3.33d, 0.3d, 0.1d,
        1.5d, 20.0d, 4.5d,
        0.9d, 0.9d, 16.0d,
        0.2d, 0.67d, 1.0d,
        1.16d, 1.19d,
        2.0d, 32.0d,
        33.6d, 37.0d)]

    [TestCase(4.64d, 3.74d, 0.3d, 0.1d,
        1.5d, 20.0d, 4.5d,
        0.9d, 0.9d, 15.8d,
        0.2d, 0.67d, 1.0d,
        1.21d, 1.28d,
        2.0d, 31.6d,
        33.2d, 36.5d)]

    [TestCase(4.60d, 3.75d, 0.3d, 0.1d,
        1.5d, 20.0d, 4.5d,
        0.9d, 0.9d, 15.0d,
        0.2d, 0.67d, 1.0d,
        1.20d, 1.28d,
        2.0d, 30.0d, 31.5d,
        34.7d)]

    [TestCase(3.96d, 5.10d, 0.3d, 0.1d,
        1.5d, 20.0d, 4.5d,
        0.9d, 0.9d, 17.8d,
        0.2d, 0.67d, 1.0d,
        1.32d, 1.38d,
        2.0d, 35.6d, 37.4d,
        41.1d)]
    public void CalculationParametersPipeFreeExplosionProofInsulatingJumpersTest(double width, double height,
        double pressureAmplitude, double atmosphericPressure,
        double dynamicCoefficient, double compressiveStrengthNorm, double tensileStrengthNorm,
        double adhesionStrengthNorm, double safetyFactor, double crossSectionArea,
        double expectedOverpressure, double expectedReflectedPressure, double expectedEquivalentPressure,
        double expectedBendingStrengthThickness, double expectedAnchoringStrengthThickness,
        double expectedMinimumThickness, double expectedVolume,
        double expectedDryCementMixtureConsumption, double expectedTotalDryCementMixtureConsumption)
    {
        // Act
        var blastWavePressureResult =
            _service.CalculateEquivalentBlastWavePressureForExplosiveIsolationBridge(pressureAmplitude,
                atmosphericPressure, dynamicCoefficient);

        var minimumThicknessResult = _service.CalculateMinimumThicknessForExplosiveIsolationBridge(width, height,
            blastWavePressureResult.EquivalentPressure,
            compressiveStrengthNorm, tensileStrengthNorm, adhesionStrengthNorm, safetyFactor);

        var dryCementMixtureConsumptionResult =
            _service.CalculateDryCementMixtureConsumptionForExplosiveIsolationBridge(crossSectionArea,
                minimumThicknessResult.MinimumThickness);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(blastWavePressureResult.Overpressure, Is.EqualTo(expectedOverpressure));
            Assert.That(blastWavePressureResult.ReflectedPressure, Is.EqualTo(expectedReflectedPressure));
            Assert.That(blastWavePressureResult.EquivalentPressure, Is.EqualTo(expectedEquivalentPressure));
            Assert.That(minimumThicknessResult.BendingStrengthThickness, Is.EqualTo(expectedBendingStrengthThickness));
            Assert.That(minimumThicknessResult.AnchoringStrengthThickness,
                Is.EqualTo(expectedAnchoringStrengthThickness));
            Assert.That(minimumThicknessResult.MinimumThickness, Is.EqualTo(expectedMinimumThickness));
            Assert.That(dryCementMixtureConsumptionResult.Volume, Is.EqualTo(expectedVolume));
            Assert.That(dryCementMixtureConsumptionResult.DryCementMixtureConsumption,
                Is.EqualTo(expectedDryCementMixtureConsumption));
            Assert.That(dryCementMixtureConsumptionResult.TotalDryCementMixtureConsumption,
                Is.EqualTo(expectedTotalDryCementMixtureConsumption));
        });
    }

    [TestCase(298, 15, 0.045d, 0.030d)]
    public void CalculateWasteMasses_ShouldReturnCorrectValues(double electrodesUsedMass, double wasteNormCoefficient,
        double expectedElectrodeWasteMass, double expectedSlagWasteMass)
    {
        // Act
        var electrodeWasteMass = _service.CalculateWeldingElectrodeWasteMass(electrodesUsedMass, wasteNormCoefficient);
        var slagWasteMass = _service.CalculateWeldingSlagWasteMass(electrodesUsedMass);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(electrodeWasteMass, Is.EqualTo(expectedElectrodeWasteMass));
            Assert.That(slagWasteMass, Is.EqualTo(expectedSlagWasteMass));
        });
    }

    [TestCase(2100, 5.0d, 0.3d, 0.126d)]
    public void CalculatePaintContaminatedMetalWasteMass_ShouldReturnCorrectValue(double annualRawMaterialConsumption,
        double rawMaterialPackageWeight, double emptyPackageWeight, double expectedContaminatedMetalWasteMass)
    {
        // Act
        var contaminatedMetalWasteMass = _service.CalculatePaintContaminatedMetalWasteMass(annualRawMaterialConsumption,
            rawMaterialPackageWeight, emptyPackageWeight);

        // Assert
        Assert.That(contaminatedMetalWasteMass, Is.EqualTo(expectedContaminatedMetalWasteMass));
    }

    [TestCase(3, 5, 10, 0.0003d, 0.00324)]
    public void CalculatePaintContaminatedToolsWasteMass_ShouldReturnCorrectValue(double acetoneContentPercent,
        double paintMaterialContentPercent, double toolCount, double toolWeightTons,
        double expectedContaminatedToolsWasteMass)
    {
        // Act
        var contaminatedToolsWasteMass = _service.CalculatePaintContaminatedToolsWasteMass(acetoneContentPercent,
            paintMaterialContentPercent, toolCount, toolWeightTons);

        // Assert
        Assert.That(contaminatedToolsWasteMass, Is.EqualTo(expectedContaminatedToolsWasteMass));
    }

    [TestCase(1497.60d, 5d, 7.488d)]
    public void CalculateAnnualWasteNormForSiteCleaning_ShouldReturnCorrectValue(double sanitaryCleaningArea,
        double wasteNormPerSquareMeter, double expectedAnnualWasteNorm)
    {
        // Act
        var annualWasteNorm =
            _service.CalculateAnnualWasteNormForSiteCleaning(sanitaryCleaningArea, wasteNormPerSquareMeter);

        // Assert
        Assert.That(annualWasteNorm, Is.EqualTo(expectedAnnualWasteNorm));
    }

    [TestCase(58, 0.22d, 1, 0.2d, 2.552d)]
    public void CalculateAnnualSolidWasteBasedOnWorkers_ShouldReturnCorrectValue(int numberOfWorkers,
        double solidWasteNormPerWorker,
        int conservationDurationYears, double solidWasteDensity, double expectedAnnualSolidWaste)
    {
        // Act
        var annualSolidWaste = _service.CalculateAnnualSolidWasteBasedOnWorkers(numberOfWorkers,
            solidWasteNormPerWorker, conservationDurationYears, solidWasteDensity);

        // Assert
        Assert.That(annualSolidWaste, Is.EqualTo(expectedAnnualSolidWaste));
    }

    [TestCase(1085105d, 2368.00d, 94.72d, 30d, 3523.925d)]
    public void CalculateMechanicalTreatmentSedimentWaste_ShouldReturnCorrectValue(double annualWasteWaterVolume,
        double suspendedSolidsConcentrationBeforeTreatment,
        double suspendedSolidsConcentrationAfterTreatment,
        double sludgeMoistureContent, double expectedTreatmentSedimentWaste)
    {
        // Act
        var treatmentSedimentWaste = _service.CalculateMechanicalTreatmentSedimentWaste(annualWasteWaterVolume,
            suspendedSolidsConcentrationBeforeTreatment, suspendedSolidsConcentrationAfterTreatment,
            sludgeMoistureContent);

        // Assert
        Assert.That(treatmentSedimentWaste, Is.EqualTo(expectedTreatmentSedimentWaste));
    }

    [TestCase(5, 1, 60d, 2, 0.150d)]
    [TestCase(4, 1, 51d, 2, 0.102d)]
    [TestCase(9, 1, 32.5d, 2, 0.146d)]
    [TestCase(9, 1, 60d, 2, 0.270d)]
    [TestCase(9, 1, 76.4d, 2, 0.344d)]
    [TestCase(9, 1, 60d, 3, 0.180d)]
    [TestCase(2, 2, 20.1d, 2, 0.040d)]
    [TestCase(2, 1, 60d, 2, 0.060d)]
    [TestCase(1, 1, 60d, 2, 0.030d)]
    public void CalculateUsedBatteryMass_ShouldReturnCorrectValue(int numberOfVehicles, int batteriesPerVehicle, double batteryMassStandard, int batteryLifespanYears, double expectedMassUsedBatteries)
    {
        // Act
        var massUsedBatteries = _service.CalculateUsedBatteryMass(numberOfVehicles, batteriesPerVehicle, batteryMassStandard, batteryLifespanYears);

        // Assert
        Assert.That(massUsedBatteries, Is.EqualTo(expectedMassUsedBatteries));
    }

    /*[TestCase(5, 29d, 550d, 1000d, 0.064598d)]
    [TestCase(4, 22d, 550d, 1000d, 0.039204d)]
    [TestCase(9, 19d, 550d, 1000d, 0.076181d)]
    [TestCase(9, 25d, 550d, 1000d, 0.100238d)]
    [TestCase(9, 33.2d, 550d, 1000d, 0.133115d)]
    [TestCase(9, 31d, 550d, 1000d, 0.124295d)]
    [TestCase(2, 30.1d, 550d, 1000d, 0.026819d)]
    [TestCase(2, 19.5d, 550d, 1000d, 0.017375d)]
    [TestCase(1, 30d, 550d, 1000d, 0.013365d)]
    public void CalculateMineralMotorOilsMass_ShouldReturnCorrectValue(int vehicleCount, double oilVolume, double averageAnnualMileage, double mileageNorm, double expectedMineralMotorOilsMass)
    {
        // Act
        var mineralMotorOilsMass = _service.CalculateMineralMotorOilsMass(vehicleCount, oilVolume, averageAnnualMileage, mileageNorm);

        // Assert
        Assert.That(mineralMotorOilsMass, Is.EqualTo(expectedMineralMotorOilsMass));
    }*/
}