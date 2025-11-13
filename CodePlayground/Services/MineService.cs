using CodePlayground.Interfaces;
using CodePlayground.Models;

namespace CodePlayground.Services;

public class MineService : IMineService
{
    #region Расчет параметров безврубовых взрывоустойчивых изолирующих перемычек
    
    public BlastWavePressureResult CalculateEquivalentBlastWavePressureForExplosiveIsolationBridge(
        double pressureAmplitude, double atmosphericPressure, double dynamicCoefficient)
    {
        var overpressure = pressureAmplitude - atmosphericPressure;
        var reflectedPressure = overpressure * (2 + 6 / (1 + 7 * atmosphericPressure / overpressure));
        var equivalentPressure = reflectedPressure * dynamicCoefficient;

        return new BlastWavePressureResult()
        {
            Overpressure = Math.Round(overpressure, 2),
            ReflectedPressure = Math.Round(reflectedPressure, 2),
            EquivalentPressure = Math.Round(equivalentPressure, 2)
        };
    }

    public MinimumThicknessResult CalculateMinimumThicknessForExplosiveIsolationBridge(double width, double height,
        double equivalentPressure,
        double compressiveStrengthNorm, double tensileStrengthNorm, double adhesionStrengthNorm, double safetyFactor)
    {
        var shearStrengthNorm = .24d * compressiveStrengthNorm; // нормативное сопротивление на сдвиг

        double bendingStrengthThickness = 0;

        if (width < height)
        {
            bendingStrengthThickness = width * Math.Sqrt((equivalentPressure * (3 - 2 * Math.Pow(width / height, 2))) /
                                                         (4 * tensileStrengthNorm * safetyFactor));
        }
        else if (width > height)
        {
            bendingStrengthThickness = height * Math.Sqrt((equivalentPressure * (3 - 2 * Math.Pow(height / width, 2))) /
                                                          (4 * tensileStrengthNorm * safetyFactor));
        }

        double anchoringStrengthThickness = 0;

        if (adhesionStrengthNorm < shearStrengthNorm)
        {
            anchoringStrengthThickness = (equivalentPressure * height * width) /
                                         (2 * (height + width) * adhesionStrengthNorm * safetyFactor);
        }
        else if (adhesionStrengthNorm > shearStrengthNorm)
        {
            anchoringStrengthThickness = (equivalentPressure * height * width) /
                                         (2 * (height + width) * shearStrengthNorm * safetyFactor);
        }

        var calculatedThickness = Math.Max(bendingStrengthThickness, anchoringStrengthThickness);
        calculatedThickness = Math.Max(2d, calculatedThickness);
        calculatedThickness = Math.Min(calculatedThickness, 5d);

        return new MinimumThicknessResult()
        {
            BendingStrengthThickness = Math.Round(bendingStrengthThickness, 2),
            AnchoringStrengthThickness = Math.Round(anchoringStrengthThickness, 2),
            MinimumThickness = Math.Round(calculatedThickness, 2)
        };
    }

    public DryCementMixtureConsumptionResult CalculateDryCementMixtureConsumptionForExplosiveIsolationBridge(
        double crossSectionArea, double minimumThickness)
    {
        var volume = crossSectionArea * minimumThickness;
        var dryCementMixtureConsumption = 1.05d * volume;
        var totalDryCementMixtureConsumption = 1.1d * dryCementMixtureConsumption;

        return new DryCementMixtureConsumptionResult()
        {
            Volume = Math.Round(volume, 2),
            DryCementMixtureConsumption = Math.Round(dryCementMixtureConsumption, 1),
            TotalDryCementMixtureConsumption = Math.Round(totalDryCementMixtureConsumption, 1)
        };
    }
    
    #endregion

    #region Расчет объемов образования отходов

    public double CalculateWeldingElectrodeWasteMass(double electrodesUsedMass, double wasteNormCoefficient)
    {
        var electrodeWasteMass = electrodesUsedMass * wasteNormCoefficient * 1e-5;

        return Math.Round(electrodeWasteMass, 3);
    }

    public double CalculateWeldingSlagWasteMass(double electrodesUsedMass)
    {
        var slagWasteMass = electrodesUsedMass / 1000 * 0.1d;

        return Math.Round(slagWasteMass, 3);
    }

    public double CalculatePaintContaminatedMetalWasteMass(double annualRawMaterialConsumption,
        double rawMaterialPackageWeight, double emptyPackageWeight)
    {
        var contaminatedMetalWasteMass =
            annualRawMaterialConsumption / rawMaterialPackageWeight * emptyPackageWeight * 1e-3;

        return Math.Round(contaminatedMetalWasteMass, 3);
    }

    public double CalculatePaintContaminatedToolsWasteMass(double acetoneContentPercent,
        double paintMaterialContentPercent, double toolCount, double toolWeightTons)
    {
        var contaminatedToolsWasteMass = toolCount * toolWeightTons / 100 *
                                         (100 + acetoneContentPercent + paintMaterialContentPercent);

        return Math.Round(contaminatedToolsWasteMass, 5);
    }

    public double CalculateAnnualWasteNormForSiteCleaning(double sanitaryCleaningArea, double wasteNormPerSquareMeter)
    {
        var annualWasteNorm = sanitaryCleaningArea * wasteNormPerSquareMeter * 1e-3;

        return Math.Round(annualWasteNorm, 3);
    }

    public double CalculateAnnualSolidWasteBasedOnWorkers(int numberOfWorkers, double solidWasteNormPerWorker,
        int conservationDurationYears, double solidWasteDensity)
    {
        var annualSolidWaste =
            numberOfWorkers * solidWasteNormPerWorker * conservationDurationYears * solidWasteDensity;

        return Math.Round(annualSolidWaste, 3);
        ;
    }
    
    public double CalculateMechanicalTreatmentSedimentWaste(double annualWasteWaterVolume,
        double suspendedSolidsConcentrationBeforeTreatment, double suspendedSolidsConcentrationAfterTreatment,
        double sludgeMoistureContent)
    {
        var treatmentSedimentWaste =
            annualWasteWaterVolume *
            (suspendedSolidsConcentrationBeforeTreatment - suspendedSolidsConcentrationAfterTreatment) * 1e-6 /
            (1 - sludgeMoistureContent / 100);

        return Math.Round(treatmentSedimentWaste, 3);
    }
    
    public double CalculateUsedBatteryMass(int numberOfVehicles, int batteriesPerVehicle, double batteryMassStandard, int batteryLifespanYears)
    {
        var massUsedBatteriesWithElectrolyte = numberOfVehicles * batteriesPerVehicle * batteryMassStandard / batteryLifespanYears * 1e-3;

        return Math.Round(massUsedBatteriesWithElectrolyte, 3);
    }

    #endregion
}