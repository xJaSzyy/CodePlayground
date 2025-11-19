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
            SameGeneratorCount = 1
        };
        gasolineGeneratorEmissionsReport.Emissions = new List<GasolineGeneratorEmissionsResult>
        {
            emissionService.CalculateGasolineGeneratorEmissions(Pollutant.CO,
                gasolineGeneratorEmissionsReport.WorkHoursPerDay, gasolineGeneratorEmissionsReport.WorkDaysPerYear,
                gasolineGeneratorEmissionsReport.GeneratorCount, gasolineGeneratorEmissionsReport.SameGeneratorCount),
            emissionService.CalculateGasolineGeneratorEmissions(Pollutant.CH,
                gasolineGeneratorEmissionsReport.WorkHoursPerDay, gasolineGeneratorEmissionsReport.WorkDaysPerYear,
                gasolineGeneratorEmissionsReport.GeneratorCount, gasolineGeneratorEmissionsReport.SameGeneratorCount),
            emissionService.CalculateGasolineGeneratorEmissions(Pollutant.NO2,
                gasolineGeneratorEmissionsReport.WorkHoursPerDay, gasolineGeneratorEmissionsReport.WorkDaysPerYear,
                gasolineGeneratorEmissionsReport.GeneratorCount, gasolineGeneratorEmissionsReport.SameGeneratorCount),
            emissionService.CalculateGasolineGeneratorEmissions(Pollutant.NO,
                gasolineGeneratorEmissionsReport.WorkHoursPerDay, gasolineGeneratorEmissionsReport.WorkDaysPerYear,
                gasolineGeneratorEmissionsReport.GeneratorCount, gasolineGeneratorEmissionsReport.SameGeneratorCount),
            emissionService.CalculateGasolineGeneratorEmissions(Pollutant.SO2,
                gasolineGeneratorEmissionsReport.WorkHoursPerDay, gasolineGeneratorEmissionsReport.WorkDaysPerYear,
                gasolineGeneratorEmissionsReport.GeneratorCount, gasolineGeneratorEmissionsReport.SameGeneratorCount)
        }.OrderBy(e => e.PollutantInfo.Code).ToList();

        outputService.CreateGasolineGeneratorEmissionsReport(gasolineGeneratorEmissionsReport, "/home/xjasz/Desktop");

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
        reservoirsEmissionsReport.VaporConcentration =
            DataStorage.VaporConcentration[reservoirsEmissionsReport.ReservoirType][
                reservoirsEmissionsReport.ClimateZone][reservoirsEmissionsReport.OilProduct];
        reservoirsEmissionsReport.Emissions = new List<ReservoirsEmissionsResult>
        {
            emissionService.CalculateReservoirsEmissions(Pollutant.RPK240280,
                reservoirsEmissionsReport.VaporConcentration, reservoirsEmissionsReport.AutumnWinterOilAmount,
                reservoirsEmissionsReport.SpringSummerOilAmount, reservoirsEmissionsReport.DrainedVolume,
                reservoirsEmissionsReport.AverageDrainTime),
            emissionService.CalculateReservoirsEmissions(Pollutant.H2S, reservoirsEmissionsReport.VaporConcentration,
                reservoirsEmissionsReport.AutumnWinterOilAmount, reservoirsEmissionsReport.SpringSummerOilAmount,
                reservoirsEmissionsReport.DrainedVolume, reservoirsEmissionsReport.AverageDrainTime)
        }.OrderBy(e => e.PollutantInfo.Code).ToList();

        outputService.CreateReservoirsEmissionsReport(reservoirsEmissionsReport, "/home/xjasz/Desktop");
    }
}