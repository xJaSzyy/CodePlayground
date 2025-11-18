using CodePlayground.Models;

namespace CodePlayground.Interfaces;

public interface IOutputService
{
    public void CreateGasolineGeneratorEmissionsReport(GasolineGeneratorEmissionsReport report);
}