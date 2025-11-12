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
        2.0d, 40.7d)]
    
    [TestCase(4.61d, 3.33d, 0.3d, 0.1d, 
        1.5d, 20.0d, 4.5d, 
        0.9d, 0.9d, 16.0d,
        0.2d, 0.67d, 1.0d,
        2.0d, 37.0d)]
    public void EndToEnd(double width, double height, double pressureAmplitude, double atmosphericPressure, 
        double dynamicCoefficient, double compressiveStrengthNorm, double tensileStrengthNorm,
        double adhesionStrengthNorm, double safetyFactor, double crossSectionArea,
        double expectedOverpressure, double expectedReflectedPressure, double expectedEquivalentPressure,
        double expectedMinimumThickness, double expectedTotalDryCementMixtureConsumption)
    {
        // Act
        var blastWavePressureResult =
            _service.CalculateEquivalentBlastWavePressureForExplosiveIsolationBridge(pressureAmplitude,
                atmosphericPressure, dynamicCoefficient);
        
        var minimumThickness = _service.CalculateMinimumThicknessForExplosiveIsolationBridge(width, height,
            blastWavePressureResult.EquivalentPressure,
            compressiveStrengthNorm, tensileStrengthNorm, adhesionStrengthNorm, safetyFactor);
        
        var totalDryCementMixtureConsumption =
            _service.CalculateDryCementMixtureConsumptionForExplosiveIsolationBridge(crossSectionArea,
                minimumThickness);
        
        // Assert
        Assert.That(blastWavePressureResult.Overpressure, Is.EqualTo(expectedOverpressure));
        Assert.That(blastWavePressureResult.ReflectedPressure, Is.EqualTo(expectedReflectedPressure));
        Assert.That(blastWavePressureResult.EquivalentPressure, Is.EqualTo(expectedEquivalentPressure));
        Assert.That(minimumThickness, Is.EqualTo(expectedMinimumThickness));
        Assert.That(totalDryCementMixtureConsumption, Is.EqualTo(expectedTotalDryCementMixtureConsumption));
    }
}