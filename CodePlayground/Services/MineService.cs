using CodePlayground.Interfaces;
using CodePlayground.Models;

namespace CodePlayground.Services;

public class MineService : IMineService
{
    public BlastWavePressureResult CalculateEquivalentBlastWavePressureForExplosiveIsolationBridge(double pressureAmplitude, double atmosphericPressure, double dynamicCoefficient)
    {
        double overpressure = pressureAmplitude - atmosphericPressure; 
        double reflectedPressure = overpressure * (2 + 6 / (1 + 7 * atmosphericPressure / overpressure));
        double equivalentPressure = reflectedPressure * dynamicCoefficient; 

        return new BlastWavePressureResult()
        {
            Overpressure = Math.Round(overpressure, 2),
            ReflectedPressure = Math.Round(reflectedPressure, 2),
            EquivalentPressure = Math.Round(equivalentPressure, 2)
        };
    }
    
    public double CalculateMinimumThicknessForExplosiveIsolationBridge(double width, double height, double equivalentPressure, 
        double compressiveStrengthNorm, double tensileStrengthNorm, double adhesionStrengthNorm, double safetyFactor)
    {
        var shearStrengthNorm = .24d * compressiveStrengthNorm; // нормативное сопротивление на сдвиг

        double bendingStrengthThickness = 0; // толщина плиты безврубовой перемычки, обеспечивающая ее прочность на изгиб под действием эквивалентного давления, м;

        if (width < height)
        {
            bendingStrengthThickness = width * Math.Sqrt((equivalentPressure * (3 - 2 * Math.Pow(width / height, 2))) / (4 * tensileStrengthNorm * safetyFactor));
        }
        else if (width > height)
        {
            bendingStrengthThickness = height * Math.Sqrt((equivalentPressure * (3 - 2 * Math.Pow(height / width, 2))) / (4 * tensileStrengthNorm * safetyFactor));
        }
        
        double anchoringStrengthThickness = 0; // толщина плиты безврубовой перемычки, обеспечивающая прочность ее закрепления по контуру, м.

        if (adhesionStrengthNorm < shearStrengthNorm)
        {
            anchoringStrengthThickness = (equivalentPressure * height * width) / (2 * (height + width) * adhesionStrengthNorm * safetyFactor);
        }
        else if (adhesionStrengthNorm > shearStrengthNorm)
        {
            anchoringStrengthThickness = (equivalentPressure * height * width) / (2 * (height + width) * shearStrengthNorm * safetyFactor);
        }
        
        var calculatedThickness = Math.Max(bendingStrengthThickness, anchoringStrengthThickness); // расчетная толщина безврубовой перемычки, м;
        calculatedThickness = Math.Max(2, calculatedThickness);
        calculatedThickness = Math.Min(calculatedThickness, 5);
        
        return Math.Round(calculatedThickness, 2);
    }
    
    public double CalculateDryCementMixtureConsumptionForExplosiveIsolationBridge(double crossSectionArea, double minimumThickness)
    {
        double volume = crossSectionArea * minimumThickness; // объем возводимой безврубовой взрывоустойчивой изолирующей перемычки, м3.
        double dryCementMixtureConsumption = 1.05d * volume; // расход сухой цементной смеси «УГМ-П» на возведение безврубовой взрывоустойчивой перемычки
        double totalDryCementMixtureConsumption = 1.1d * dryCementMixtureConsumption; // итоговый расход сухой цементной смеси «УГМ-П» на возведение проектируемой безврубовой взрывоустойчивой изолирующей перемычки

        return Math.Round(totalDryCementMixtureConsumption, 1);
    }
}