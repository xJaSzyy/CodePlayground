using ClosedXML.Excel;
using CodePlayground.Enums;
using CodePlayground.Models;
using CodePlayground.Services;

class Program
{
    private static async Task Main()
    {
        var emissionService = new EmissionService();
        var outputService = new OutputService();
        
        var gasolineGeneratorEmissionsReport = new GasolineGeneratorEmissionsReport
        {
            SelectionSource = "001",
            PollutionSource = "0007",
            WorkHoursPerDay = 1,
            WorkDaysPerYear = 365,
            GeneratorCount = 1,
            SameGeneratorCount = 1,
            Emissions = new List<GasolineGeneratorEmissions>()
            {
                emissionService.CalculateGasolineGeneratorEmissions(Pollutant.CO, 1, 365, 1, 1),
                emissionService.CalculateGasolineGeneratorEmissions(Pollutant.CH, 1, 365, 1, 1),
                emissionService.CalculateGasolineGeneratorEmissions(Pollutant.NO2, 1, 365, 1, 1),
                emissionService.CalculateGasolineGeneratorEmissions(Pollutant.NO, 1, 365, 1, 1),
                emissionService.CalculateGasolineGeneratorEmissions(Pollutant.SO2, 1, 365, 1, 1),
            }
        };
        
        outputService.CreateGasolineGeneratorEmissionsReport(gasolineGeneratorEmissionsReport);
    }
}