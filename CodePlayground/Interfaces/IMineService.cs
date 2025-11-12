using CodePlayground.Models;

namespace CodePlayground.Interfaces;

public interface IMineService
{
    /// <summary>
    /// Расчет давления, эквивалентного воздействию на безврубовую взрывоустойчивую изолирующую перемычку ударно-воздушной волны
    /// </summary>
    /// <param name="pressureAmplitude">Амплитуда давления, МПа</param>
    /// <param name="atmosphericPressure">Атмосферное давление, МПа</param>
    /// <param name="dynamicCoefficient">Коэффициент динамичности</param>
    public BlastWavePressureResult CalculateEquivalentBlastWavePressureForExplosiveIsolationBridge(
        double pressureAmplitude, double atmosphericPressure, double dynamicCoefficient);

    /// <summary>
    /// Расчет минимальной толщины безврубовой взрывоустойчивой изолирующей перемычки
    /// </summary>
    /// <param name="width">высота плиты</param>
    /// <param name="height">ширина плиты</param>
    /// <param name="equivalentPressure">Эквивалентное давление на перемычку, МПа</param>
    /// <param name="compressiveStrengthNorm">нормативное сопротивление на сжатие</param>
    /// <param name="tensileStrengthNorm">нормативное сопротивление на растяжение при изгибе</param>
    /// <param name="adhesionStrengthNorm">нормативная адгезионная прочность</param>
    /// <param name="safetyFactor">коэффициент запаса прочности для материала перемычки (изменяется в пределах 0,8-1,0)</param>
    /// <returns></returns>
    public MinimumThicknessResult CalculateMinimumThicknessForExplosiveIsolationBridge(double width, double height, double equivalentPressure, 
        double compressiveStrengthNorm, double tensileStrengthNorm, double adhesionStrengthNorm, double safetyFactor);

    /// <summary>
    /// Расход сухой цементной смеси «УГМ-П» для возведения взрывоустойчивой изолирующей перемычки
    /// </summary>
    /// <param name="crossSectionArea">Площадь сечения выработки вчерне, в которой устанавливается изолирующая перемычка, м2</param>
    /// <param name="minimumThickness">Минимальная расчетная толщина изолирующей перемычки, м</param>
    /// <returns></returns>
    public DryCementMixtureConsumptionResult CalculateDryCementMixtureConsumptionForExplosiveIsolationBridge(double crossSectionArea,
        double minimumThickness);

    /// <summary>
    /// Расчет массы отходов стальных сварочных электродов в метрах в год
    /// </summary>
    /// <param name="electrodesUsedMass">Количество использованных электродов, кг/год</param>
    /// <param name="wasteNormCoefficient">Норматив образования огарков электродов от их расхода, 15%</param>
    /// <returns></returns>
    public double CalculateWeldingElectrodeWasteMass(double electrodesUsedMass, double wasteNormCoefficient);

    /// <summary>
    /// Расчет массы сварочного шлака
    /// </summary>
    /// <param name="electrodesUsedMass">Количество использованных электродов, кг/ год</param>
    /// <returns></returns>
    public double CalculateWeldingSlagWasteMass(double electrodesUsedMass);

    /// <summary>
    /// Расчет массы тары из черных металлов, загрязненной лакокрасочными материалами (менее 5%)
    /// </summary>
    /// <param name="annualRawMaterialConsumption">Годовой расход сырья i-ого вида, кг</param>
    /// <param name="rawMaterialPackageWeight">Вес сырья i-ого вида в упаковке, кг</param>
    /// <param name="emptyPackageWeight">Вес пустой упаковки из-под сырья i-ого вида, кг</param>
    /// <returns></returns>
    public double CalculatePaintContaminatedMetalWasteMass(double annualRawMaterialConsumption,
        double rawMaterialPackageWeight, double emptyPackageWeight);

    /// <summary>
    /// Расчет массы лакокрасочных инструментов (кисти, валики), загрязненных лакокрасочными материалами (менее 5%)
    /// </summary>
    /// <param name="acetoneContentPercent">Содержание ацетона, %</param>
    /// <param name="paintMaterialContentPercent">Содержание ЛКМ, %</param>
    /// <param name="toolCount">Количество инструментов одного вида, шт</param>
    /// <param name="toolWeightTons">Вес инструмента одного вида, т</param>
    /// <returns></returns>
    public double CalculatePaintContaminatedToolsWasteMass(double acetoneContentPercent,
        double paintMaterialContentPercent, double toolCount, double toolWeightTons);

    /// <summary>
    /// Расчет годового норматива образования отходов от уборки территории промплощадки предприятия 
    /// </summary>
    /// <param name="sanitaryCleaningArea">Площадь территории, подвергаемой санитарной уборке, м2</param>
    /// <param name="wasteNormPerSquareMeter">Удельная норма образования смета с 1 м2 территории, кг/м2</param>
    /// <returns></returns>
    public double CalculateAnnualWasteNormForSiteCleaning(double sanitaryCleaningArea, double wasteNormPerSquareMeter);

    /// <summary>
    /// Расчет мусора от офисных и бытовых помещений организаций (исключая крупногабаритный)
    /// </summary>
    /// <param name="numberOfWorkers">Максимальное единовременное число трудящихся, чел</param>
    /// <param name="solidWasteNormPerWorker">удельная норма образования твердых коммунальных/бытовых отходов на 1 работающего, м3/год</param>
    /// <param name="conservationDurationYears">продолжительность консервации, лет</param>
    /// <param name="solidWasteDensity">бъемный вес ТКО, т/м3</param>
    /// <returns></returns>
    public double CalculateAnnualSolidWasteBasedOnWorkers(int numberOfWorkers, double solidWasteNormPerWorker,
        int conservationDurationYears, double solidWasteDensity);
}