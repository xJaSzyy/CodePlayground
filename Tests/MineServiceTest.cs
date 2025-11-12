using CodePlayground.Interfaces;
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
    public void CalculationParametersPipeFreeExplosionProofInsulatingJumpersTest(double width, double height, double pressureAmplitude, double atmosphericPressure, 
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
            Assert.That(minimumThicknessResult.AnchoringStrengthThickness, Is.EqualTo(expectedAnchoringStrengthThickness));
            Assert.That(minimumThicknessResult.MinimumThickness, Is.EqualTo(expectedMinimumThickness));
            Assert.That(dryCementMixtureConsumptionResult.Volume, Is.EqualTo(expectedVolume));
            Assert.That(dryCementMixtureConsumptionResult.DryCementMixtureConsumption, Is.EqualTo(expectedDryCementMixtureConsumption));
            Assert.That(dryCementMixtureConsumptionResult.TotalDryCementMixtureConsumption, Is.EqualTo(expectedTotalDryCementMixtureConsumption));
        });
    }

    [TestCase(298, 15, 0.045d, 0.030d,
        2100, 5.0d, 0.3d, 0.126d,
        3, 5, 10, 0.0003d, 0.00324,
        1497.60d, 5d, 7.488d,
        58, 0.22d, 1, 0.2d, 2.552d, 
        1085105d, 2368.00d, 94.72d, 
        30d, 3523.925d)]
    public void CalculationWasteGenerationVolumesTest(double electrodesUsedMass, double wasteNormCoefficient,
        double expectedElectrodeWasteMass,
        double expectedSlagWasteMass, double annualRawMaterialConsumption,
        double rawMaterialPackageWeight, double emptyPackageWeight, double expectedContaminatedMetalWasteMass,
        double acetoneContentPercent, double paintMaterialContentPercent, double toolCount, double toolWeightTons,
        double expectedContaminatedToolsWasteMass,
        double sanitaryCleaningArea, double wasteNormPerSquareMeter, double expectedAnnualWasteNorm,
        int numberOfWorkers, double solidWasteNormPerWorker,
        int conservationDurationYears, double solidWasteDensity, double expectedAnnualSolidWaste,
        double annualWasteWaterVolume, double suspendedSolidsConcentrationBeforeTreatment,
        double suspendedSolidsConcentrationAfterTreatment,
        double sludgeMoistureContent, double expectedTreatmentSedimentWaste)
    {
        // Act
        var electrodeWasteMass = _service.CalculateWeldingElectrodeWasteMass(electrodesUsedMass, wasteNormCoefficient);
        var slagWasteMass = _service.CalculateWeldingSlagWasteMass(electrodesUsedMass);
        var contaminatedMetalWasteMass = _service.CalculatePaintContaminatedMetalWasteMass(annualRawMaterialConsumption,
            rawMaterialPackageWeight, emptyPackageWeight);
        var contaminatedToolsWasteMass = _service.CalculatePaintContaminatedToolsWasteMass(acetoneContentPercent,
            paintMaterialContentPercent, toolCount, toolWeightTons);
        var annualWasteNorm =
            _service.CalculateAnnualWasteNormForSiteCleaning(sanitaryCleaningArea, wasteNormPerSquareMeter);
        var annualSolidWaste = _service.CalculateAnnualSolidWasteBasedOnWorkers(numberOfWorkers,
            solidWasteNormPerWorker, conservationDurationYears, solidWasteDensity);
        var treatmentSedimentWaste = _service.CalculateMechanicalTreatmentSedimentWaste(annualWasteWaterVolume,
            suspendedSolidsConcentrationBeforeTreatment, suspendedSolidsConcentrationAfterTreatment,
            sludgeMoistureContent);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(electrodeWasteMass, Is.EqualTo(expectedElectrodeWasteMass));
            Assert.That(slagWasteMass, Is.EqualTo(expectedSlagWasteMass));
            Assert.That(contaminatedMetalWasteMass, Is.EqualTo(expectedContaminatedMetalWasteMass));
            Assert.That(contaminatedToolsWasteMass, Is.EqualTo(expectedContaminatedToolsWasteMass));
            Assert.That(annualWasteNorm, Is.EqualTo(expectedAnnualWasteNorm));
            Assert.That(annualSolidWaste, Is.EqualTo(expectedAnnualSolidWaste));
            Assert.That(treatmentSedimentWaste, Is.EqualTo(expectedTreatmentSedimentWaste));
        });
    }
}