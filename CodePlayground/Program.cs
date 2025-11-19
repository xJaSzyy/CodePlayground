using CodePlayground;
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
            Emissions = new List<GasolineGeneratorEmissionsResult>
            {
                emissionService.CalculateGasolineGeneratorEmissions(Pollutant.CO, 1, 365, 1, 1),
                emissionService.CalculateGasolineGeneratorEmissions(Pollutant.CH, 1, 365, 1, 1),
                emissionService.CalculateGasolineGeneratorEmissions(Pollutant.NO2, 1, 365, 1, 1),
                emissionService.CalculateGasolineGeneratorEmissions(Pollutant.NO, 1, 365, 1, 1),
                emissionService.CalculateGasolineGeneratorEmissions(Pollutant.SO2, 1, 365, 1, 1)
            }.OrderBy(e => e.PollutantInfo.Code).ToList()
        };

        outputService.CreateGasolineGeneratorEmissionsReport(gasolineGeneratorEmissionsReport);

        var reservoirsEmissionsReport = new ReservoirsEmissionsReport
        {
            SelectionSource = "001",
            PollutionSource = "0016",
            ReservoirVolume = 25,
            ReservoirCount = 6,
            WorkHoursPerYear = 8760,
            ReservoirType = ReservoirType.Ground,
            OilProduct = OilProduct.DieselFuel,
            ClimateZone = ClimateZone.Second,
            AutumnWinterOilAmount = 100f,
            SpringSummerOilAmount = 50f,
            DrainedVolume = 150f,
            AverageDrainTime = 1200f,
        };
        reservoirsEmissionsReport.VaporConcentration = DataStorage.VaporConcentration[reservoirsEmissionsReport.ReservoirType][reservoirsEmissionsReport.ClimateZone][reservoirsEmissionsReport.OilProduct];
        reservoirsEmissionsReport.Emissions = new List<ReservoirsEmissionsResult>
        {
            emissionService.CalculateReservoirsEmissions(Pollutant.RPK240280, reservoirsEmissionsReport.VaporConcentration, 100f, 50f, 150f),
            emissionService.CalculateReservoirsEmissions(Pollutant.H2S, reservoirsEmissionsReport.VaporConcentration, 100f, 50f, 150f)
        }.OrderBy(e => e.PollutantInfo.Code).ToList();

        outputService.CreateReservoirsEmissionsReport(reservoirsEmissionsReport);
    }
}