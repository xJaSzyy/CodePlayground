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
}