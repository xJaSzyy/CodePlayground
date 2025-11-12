using CodePlayground.Interfaces;
using CodePlayground.Models;

namespace CodePlayground.Services;

public class MineService : IMineService
{
    public BlastWavePressureResult CalculateEquivalentBlastWavePressureForExplosiveIsolationBridge(double pressureAmplitude, double atmosphericPressure, double dynamicCoefficient)
    {
        var overpressure = pressureAmplitude - atmosphericPressure; 
        var reflectedPressure = overpressure * (2 + 6 / (1 + 7 * atmosphericPressure / overpressure));
        var equivalentPressure = reflectedPressure * dynamicCoefficient; 

        return new BlastWavePressureResult()
        {
            Overpressure = overpressure,
            ReflectedPressure = reflectedPressure,
            EquivalentPressure = equivalentPressure
        };
    }
}