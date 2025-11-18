using System.Globalization;
using ClosedXML.Excel;
using CodePlayground.Interfaces;
using CodePlayground.Models;

namespace CodePlayground.Services;

public class OutputService : IOutputService
{
    public void CreateGasolineGeneratorEmissionsReport(GasolineGeneratorEmissionsReport report)
    {
        var fileName = $"ИЗА {report.PollutionSource}_{report.SelectionSource} Бензогенератор";
        
        var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(fileName);

        var row = 1;
        
        SetCell(worksheet, $"A{row}:I{row}", "Расчет выбросов загрязняющих веществ от бензогенератора", true, XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"A{row + 2}:I{row + 2}", $"Источник загрязнения № {report.PollutionSource}, Бензогенератор", true);
        SetCell(worksheet, $"A{row + 3}:I{row + 3}", $"Источник выделения № {report.SelectionSource}", true);
        SetCell(worksheet, $"A{row + 5}:I{row + 5}", "Литература: Методика проведения инвентаризации выбросов загрязняющих веществ в атмосферу автотранспортных предприятий (расчетным методом). Москва, 1998, с дополнениями и изменениями к Методике проведения инвентаризации выбросов загрязняющих веществ в атмосферу автотранспортных предприятий (расчетным методом). М, 1999");
        SetCell(worksheet, $"A{row + 7}:I{row + 7}", "В соответствии с п.12 \"Методического пособия по расчету, нормированию и контролю выбросов загрязняющих веществ в атмосферный воздух\", СПб., 2012 расчет выбросов от бензогенераторов выполняем по \"Методике проведения инвентаризации выбросов загрязняющих веществ в атмосферу автотранспортных предприятий (расчетным методом). Москва, 1998, с дополнениями и изменениями к Методике проведения инвентаризации выбросов загрязняющих веществ в атмосферу автотранспортных предприятий (расчетным методом). М, 1999\", принимая за выброс - 0,25 от величины выброса легкового карбюраторнорго автомобиля с объемом двигателя  до 1,2 л при движении по территории со скоростью 5 км/ч.");
        row += 9;
        
        SetCell(worksheet, $"A{row}:I{row}", "Валовый выброс определяется по формуле:", true);
        SetCell(worksheet, $"A{row + 2}:I{row + 2}", "Mi = 0,25 \u00d7 gi \u00d7 5,0 \u00d7 ti \u00d7 b \u00d7 Nk  / 1000000, т/г", true);
        SetCell(worksheet, $"A{row + 4}:I{row + 4}", "где  gi - удельный выброс, г/км (удельные выбросы - пробеговые выбросы, г/км) (табл. 2.5)");
        SetCell(worksheet, $"A{row + 5}:I{row + 5}", "ti - время работы в день, ч;");
        SetCell(worksheet, $"A{row + 6}:I{row + 6}", "b - количество рабочих дней в году;");
        SetCell(worksheet, $"A{row + 7}:I{row + 7}", "Nk - количество генераторов, k-вида, шт;");
        SetCell(worksheet, $"A{row + 8}:I{row + 8}", "5.0 - скорость движения км/ч;");
        SetCell(worksheet, $"A{row + 9}:I{row + 9}", "1000000 - перевод г на тонны.");
        row += 11;
        
        SetCell(worksheet, $"A{row}:I{row}", "Максимально разовый выброс определяется по формуле:", true);
        SetCell(worksheet, $"A{row + 2}:I{row + 2}", "Gi = 0,25 \u00d7 gi \u00d7 5 \u00d7 nk / 3600, г/с", true);
        SetCell(worksheet, $"A{row + 4}:I{row + 4}", "где nk - количество одновременно работающих генераторов k-вида;");
        SetCell(worksheet, $"A{row + 5}:I{row + 5}", "3600 - перевод г/ч на г/с.");
        row += 7;
        
        SetCell(worksheet, $"A{row}:I{row}", "Расчет выбросов", true);
        SetCell(worksheet, $"A{row + 1}:A{row + 2}", "Наименование генератора", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"B{row + 1}:B{row + 2}", "Кол-во, nk, шт.", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"C{row + 1}:C{row + 2}", "Кол-во, Nk, шт.", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"D{row + 1}:D{row + 2}", "Время работы в день, ti ,ч", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"E{row + 1}:E{row + 2}", "Кол-во рабочих дней в год, b", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"F{row + 1}:F{row + 2}", "Наименование ЗВ", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"G{row + 1}:G{row + 2}", "Удельный выброс, gi, г/км", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"H{row + 1}:I{row + 1}", "Выбросы в атмосферу", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"H{row + 2}", "Максимально-разовый выброс, г/с", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"I{row + 2}", "Валовый выброс, т/г", horizontal: XLAlignmentHorizontalValues.Center);
        row += 3;
        
        SetCell(worksheet, $"A{row}", 1, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"B{row}", 2, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"C{row}", 3, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"D{row}", 4, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"E{row}", 5, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"F{row}", 6, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"G{row}", 7, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"H{row}", 8, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"I{row}", 9, horizontal: XLAlignmentHorizontalValues.Center);
        
        SetCell(worksheet, $"A{row + 1}:A{row + 5}", "Бензиновый генератор", horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"B{row + 1}:B{row + 5}", report.SameGeneratorCount, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"C{row + 1}:C{row + 5}", report.GeneratorCount, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"D{row + 1}:D{row + 5}", report.WorkHoursPerDay, horizontal: XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"E{row + 1}:E{row + 5}", report.WorkDaysPerYear, horizontal: XLAlignmentHorizontalValues.Center);
        
        foreach (var emission in report.Emissions)
        {
            row++;
            SetCell(worksheet, $"F{row}", emission.PollutantInfo.Pollutant.ToString(), horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"G{row}", emission.PollutantInfo.SpecificEmission, horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"H{row}", emission.MaximumEmission, horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"I{row}", emission.GrossEmission, horizontal: XLAlignmentHorizontalValues.Center);
        }
        
        SetBorder(worksheet, $"A{row - report.Emissions.Count - 2}:I{row - report.Emissions.Count - 1}", XLBorderStyleValues.Medium);
        SetBorder(worksheet, $"A{row - report.Emissions.Count}:I{row - report.Emissions.Count}", XLBorderStyleValues.Medium);
        SetBorder(worksheet, $"A{row - report.Emissions.Count + 1}:I{row}", XLBorderStyleValues.Medium);
        row += 2;
        
        SetCell(worksheet, $"A{row}:I{row}", $"Итого выбросов от источника {report.PollutionSource}", true, XLAlignmentHorizontalValues.Center);
        row++;
        
        SetCell(worksheet, $"A{row}", "Код ЗВ", true, XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"B{row}:E{row}", "Наименование ЗВ", true, XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"F{row}:G{row}", "г/с", true, XLAlignmentHorizontalValues.Center);
        SetCell(worksheet, $"H{row}:I{row}", "т/г", true, XLAlignmentHorizontalValues.Center);

        SetBorder(worksheet, $"A{row}", XLBorderStyleValues.Medium);
        SetBorder(worksheet, $"B{row}:I{row}", XLBorderStyleValues.Medium);
        SetBorder(worksheet, $"A{row + 1}:A{row + report.Emissions.Count}", XLBorderStyleValues.Medium);
        SetBorder(worksheet, $"B{row + 1}:I{row + report.Emissions.Count}", XLBorderStyleValues.Medium);

        report.Emissions = report.Emissions.OrderBy(e => e.PollutantInfo.Code).ToList();
        foreach (var emission in report.Emissions)
        {
            row++;
            SetCell(worksheet, $"A{row}", emission.PollutantInfo.Code, horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"B{row}:E{row}", emission.PollutantInfo.Name, horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"F{row}:G{row}", emission.MaximumEmission, horizontal: XLAlignmentHorizontalValues.Center);
            SetCell(worksheet, $"H{row}:I{row}", emission.GrossEmission, horizontal: XLAlignmentHorizontalValues.Center);
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