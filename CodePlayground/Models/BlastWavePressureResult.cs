namespace CodePlayground.Models;

public class BlastWavePressureResult
{
    /// <summary>
    /// Избыточное давление во фронте УВВ, МПа
    /// </summary>
    public double Overpressure { get; set; }
    
    /// <summary>
    /// Давление отражения, МПа
    /// </summary>
    public double ReflectedPressure { get; set; }
    
    /// <summary>
    /// Эквивалентное давление на перемычку, МПа
    /// </summary>
    public double EquivalentPressure { get; set; }
}