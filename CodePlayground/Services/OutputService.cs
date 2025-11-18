using System.Globalization;
using ClosedXML.Excel;
using CodePlayground.Interfaces;
using CodePlayground.Models;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CodePlayground.Services;

public class OutputService : IOutputService
{
    public void CreateGasolineGeneratorEmissionsReport(GasolineGeneratorEmissionsReport report)
    {
        var fileName = $"ИЗА {report.PollutionSource}_{report.SelectionSource} Бензогенератор";
        
        var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(fileName);

        SetCell(worksheet, "A1:I1", "Расчет выбросов загрязняющих веществ от бензогенератора", true, XLAlignmentHorizontalValues.Center);
        
        SetCell(worksheet, "A3:I3", $"Источник загрязнения № {report.PollutionSource}, Бензогенератор", true);
        SetCell(worksheet, "A4:I4", $"Источник выделения № {report.SelectionSource}", true);
        
        SetCell(worksheet, "A6:I6", "Литература: Методика проведения инвентаризации выбросов загрязняющих веществ в атмосферу автотранспортных предприятий (расчетным методом). Москва, 1998, с дополнениями и изменениями к Методике проведения инвентаризации выбросов загрязняющих веществ в атмосферу автотранспортных предприятий (расчетным методом). М, 1999");
        SetCell(worksheet, "A8:I8", "В соответствии с п.12 \"Методического пособия по расчету, нормированию и контролю выбросов загрязняющих веществ в атмосферный воздух\", СПб., 2012 расчет выбросов от бензогенераторов выполняем по \"Методике проведения инвентаризации выбросов загрязняющих веществ в атмосферу автотранспортных предприятий (расчетным методом). Москва, 1998, с дополнениями и изменениями к Методике проведения инвентаризации выбросов загрязняющих веществ в атмосферу автотранспортных предприятий (расчетным методом). М, 1999\", принимая за выброс - 0,25 от величины выброса легкового карбюраторнорго автомобиля с объемом двигателя  до 1,2 л при движении по территории со скоростью 5 км/ч.");
        
        SetCell(worksheet, "A10:I10", "Валовый выброс определяется по формуле:", true);
        
        SetCell(worksheet, "A12:I12", "Mi = 0,25 \u00d7 gi \u00d7 5,0 \u00d7 ti \u00d7 b \u00d7 Nk  / 1000000, т/г", true);
        SetCell(worksheet, "A14:I14", "где  gi - удельный выброс, г/км (удельные выбросы - пробеговые выбросы, г/км) (табл. 2.5)");
        SetCell(worksheet, "A15:I15", "ti - время работы в день, ч;");
        SetCell(worksheet, "A16:I16", "b - количество рабочих дней в году;");
        SetCell(worksheet, "A17:I17", "Nk - количество генераторов, k-вида, шт;");
        SetCell(worksheet, "A18:I18", "5.0 - скорость движения км/ч;");
        SetCell(worksheet, "A19:I19", "1000000 - перевод г на тонны.");
        
        SetCell(worksheet, "A21:I21", "Максимально разовый выброс определяется по формуле:", true);
        SetCell(worksheet, "A23:I23", "Gi = 0,25 \u00d7 gi \u00d7 5 \u00d7 nk / 3600, г/с", true);
        SetCell(worksheet, "A25:I25", "где nk - количество одновременно работающих генераторов k-вида;");
        SetCell(worksheet, "A26:I26", "3600 - перевод г/ч на г/с.");
        
        SetCell(worksheet, "A28:I28", "Расчет выбросов", true);
        SetCell(worksheet, "A29:A30", "Наименование генератора", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "B29:B30", "Кол-во, nk, шт.", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "C29:C30", "Кол-во, Nk, шт.", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "D29:D30", "Время работы в день, ti ,ч", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "E29:E30", "Кол-во рабочих дней в год, b", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "F29:F30", "Наименование ЗВ", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "G29:G30", "Удельный выброс, gi, г/км", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "H29:I29", "Выбросы в атмосферу", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "H30", "Максимально-разовый выброс, г/с", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "I30", "Валовый выброс, т/г", horizontal: XLAlignmentHorizontalValues.Center);
        
        SetCell(worksheet, "A31", 1, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "B31", 2, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "C31", 3, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "D31", 4, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "E31", 5, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "F31", 6, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "G31", 7, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "H31", 8, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "I31", 9, horizontal: XLAlignmentHorizontalValues.Center);
        
        SetCell(worksheet, "A32:A36", "Бензиновый генератор", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "B32:B36", report.SameGeneratorCount, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "C32:C36", report.GeneratorCount, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "D32:D36", report.WorkHoursPerDay, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, "E32:E36", report.WorkDaysPerYear, horizontal: XLAlignmentHorizontalValues.Center);
        
        for (var emissionIndex = 0; emissionIndex < report.Emissions.Count; emissionIndex++)
        {
            var row = 32 + emissionIndex;
            SetCell(worksheet, $"F{row}", report.Emissions[emissionIndex].PollutantInfo.Pollutant.ToString(), horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"G{row}", report.Emissions[emissionIndex].PollutantInfo.SpecificEmission, horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"H{row}", report.Emissions[emissionIndex].MaximumEmission, horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"I{row}", report.Emissions[emissionIndex].GrossEmission, horizontal: XLAlignmentHorizontalValues.Center);
        }
        
        var lastRow = 32 + report.Emissions.Count - 1;
        
        SetBorder(worksheet, $"A29:I{lastRow}", XLBorderStyleValues.Medium);
        SetBorder(worksheet, "A31:I31", XLBorderStyleValues.Medium);
        SetBorder(worksheet, $"A32:I{lastRow}", XLBorderStyleValues.Medium);

        var nextRow = lastRow + 2;
        
        SetCell(worksheet, $"A{nextRow}:I{nextRow}", $"Итого выбросов от источника {report.PollutionSource}", true, XLAlignmentHorizontalValues.Center);
        nextRow++;
        
        SetCell(worksheet, $"A{nextRow}", "Код ЗВ", true, XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"B{nextRow}:E{nextRow}", "Наименование ЗВ", true, XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"F{nextRow}:G{nextRow}", "г/с", true, XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"H{nextRow}:I{nextRow}", "т/г", true, XLAlignmentHorizontalValues.Center);

        SetBorder(worksheet, $"A{nextRow}", XLBorderStyleValues.Medium);
        SetBorder(worksheet, $"B{nextRow}:I{nextRow}", XLBorderStyleValues.Medium);
        SetBorder(worksheet, $"A{nextRow + 1}:A{nextRow + report.Emissions.Count}", XLBorderStyleValues.Medium);
        SetBorder(worksheet, $"B{nextRow + 1}:I{nextRow + report.Emissions.Count}", XLBorderStyleValues.Medium);
        nextRow++;

        report.Emissions = report.Emissions.OrderBy(e => e.PollutantInfo.Code).ToList();
        for (var emissionIndex = 0; emissionIndex < report.Emissions.Count; emissionIndex++)
        {
            var row = nextRow + emissionIndex;
            SetCell(worksheet, $"A{row}", report.Emissions[emissionIndex].PollutantInfo.Code, horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"B{row}:E{row}", report.Emissions[emissionIndex].PollutantInfo.Name, horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"F{row}:G{row}", report.Emissions[emissionIndex].MaximumEmission, horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"H{row}:I{row}", report.Emissions[emissionIndex].GrossEmission, horizontal: XLAlignmentHorizontalValues.Center);
        }

        workbook.SaveAs($"/home/xjasz/Desktop/{fileName}.xlsx");
    }

    private static void SetCell(IXLWorksheet worksheet, string address, object value, bool bold = false, XLAlignmentHorizontalValues horizontal = XLAlignmentHorizontalValues.Left)
    {
        if (address.Contains(':'))
        {
            worksheet.Range(address).Merge();
            address = address.Split(':')[0];
        }
        
        var cell = worksheet.Cell(address);

        cell.Value = value switch
        {
            string stringValue => stringValue,
            int intValue => intValue,
            float floatValue => floatValue,
            _ => cell.Value
        };

        cell.Style.NumberFormat.Format = "0.######";
        cell.Style.Alignment.Horizontal = horizontal;
        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        cell.Style.Font.Bold = bold;
        cell.Style.Alignment.WrapText = true;
    }

    private static void SetBorder(IXLWorksheet worksheet, string address, XLBorderStyleValues style = XLBorderStyleValues.Thin)
    {
        var range = worksheet.Range(address);
        range.Style.Border.OutsideBorder = style;
        range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
    }
}