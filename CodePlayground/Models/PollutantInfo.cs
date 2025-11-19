using CodePlayground.Enums;

namespace CodePlayground.Models;

public class PollutantInfo
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;
    
    public string ShortName { get; set; } = null!;

    public Pollutant Pollutant { get; set; }
    
    public float SpecificEmission { get; set; }
}