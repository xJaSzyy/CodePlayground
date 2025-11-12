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
    public double CalculateMinimumThicknessForExplosiveIsolationBridge(double width, double height, double equivalentPressure, 
        double compressiveStrengthNorm, double tensileStrengthNorm, double adhesionStrengthNorm, double safetyFactor);

    /// <summary>
    /// Расход сухой цементной смеси «УГМ-П» для возведения взрывоустойчивой изолирующей перемычки
    /// </summary>
    /// <param name="crossSectionArea">Площадь сечения выработки вчерне, в которой устанавливается изолирующая перемычка, м2</param>
    /// <param name="minimumThickness">Минимальная расчетная толщина изолирующей перемычки, м</param>
    /// <returns></returns>
    public double CalculateDryCementMixtureConsumptionForExplosiveIsolationBridge(double crossSectionArea,
        double minimumThickness);
}