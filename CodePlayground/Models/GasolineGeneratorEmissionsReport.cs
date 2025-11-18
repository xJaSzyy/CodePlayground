using CodePlayground.Enums;

namespace CodePlayground.Models;

public class GasolineGeneratorEmissionsReport
{
    public string SelectionSource { get; set; } = null!;

    public string PollutionSource { get; set; } = null!;

    public int WorkHoursPerDay { get; set; }

    public int WorkDaysPerYear { get; set; }

    public int GeneratorCount { get; set; }

    public int SameGeneratorCount { get; set; }

    public List<GasolineGeneratorEmissions> Emissions { get; set; } = new();
}

public class GasolineGeneratorEmissions
{
    public PollutantInfo PollutantInfo { get; set; } = null!;

    public float MaximumEmission { get; set; }
    
    public float GrossEmission { get; set; }
}