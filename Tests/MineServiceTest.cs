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
    public void EndToEnd(double width, double height, double pressureAmplitude, double atmosphericPressure, 
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
        Assert.That(blastWavePressureResult.Overpressure, Is.EqualTo(expectedOverpressure));
        Assert.That(blastWavePressureResult.ReflectedPressure, Is.EqualTo(expectedReflectedPressure));
        Assert.That(blastWavePressureResult.EquivalentPressure, Is.EqualTo(expectedEquivalentPressure));
        Assert.That(minimumThicknessResult.BendingStrengthThickness, Is.EqualTo(expectedBendingStrengthThickness));
        Assert.That(minimumThicknessResult.AnchoringStrengthThickness, Is.EqualTo(expectedAnchoringStrengthThickness));
        Assert.That(minimumThicknessResult.MinimumThickness, Is.EqualTo(expectedMinimumThickness));
        Assert.That(dryCementMixtureConsumptionResult.Volume, Is.EqualTo(expectedVolume));
        Assert.That(dryCementMixtureConsumptionResult.DryCementMixtureConsumption, Is.EqualTo(expectedDryCementMixtureConsumption));
        Assert.That(dryCementMixtureConsumptionResult.TotalDryCementMixtureConsumption, Is.EqualTo(expectedTotalDryCementMixtureConsumption));
    }
}