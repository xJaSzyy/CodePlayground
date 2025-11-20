using CodePlayground.Models;

namespace CodePlayground.Interfaces;

public interface IOutputService
{
    /// <summary>
    /// Создать в виде Excel-файла отчет по расчету выбросов ЗВ от бензогенератора
    /// </summary>
    /// <param name="report">Отчет по расчету выбросов ЗВ от бензогенератора (входные + выходные данные)</param>
    /// <param name="outputFile">Путь к Excel-файлу отчета</param>
    public void CreateGasolineGeneratorEmissionsReport(GasolineGeneratorEmissionsReport report, string outputFile);

    /// <summary>
    /// Создать в виде Excel-файла отчет по расчету выбросов ЗВ от резервуаров
    /// </summary>
    /// <param name="report">Отчет по расчету выбросов ЗВ от резервуаров (входные + выходные данные)</param>
    /// <param name="outputFile">Путь к Excel-файлу отчета</param>
    public void CreateReservoirsEmissionsReport(ReservoirsEmissionsReport report, string outputFile);

    /// <summary>
    /// Создать в виде Excel-файла отчет по расчету выбросов ЗВ при механической обработке металлов
    /// </summary>
    /// <param name="report">Отчет по расчету выбросов ЗВ при механической обработке металлов (входные + выходные данные)</param>
    /// <param name="outputFile">Путь к Excel-файлу отчета</param>
    public void CreateDuringMetalMachiningEmissionsReport(DuringMetalMachiningEmissionsReport report,
        string outputFile);

    /// <summary>
    /// Создать в виде Excel-файла отчет по расчету выбросов ЗВ при сварочных работах
    /// </summary>
    /// <param name="report">Отчет по расчету выбросов ЗВ при сварочных работах (входные + выходные данные)</param>
    /// <param name="outputFile">Путь к Excel-файлу отчета</param>
    public void CreateDuringWeldingOperationsEmissionsReport(DuringWeldingOperationsEmissionsReport report,
        string outputFile);
}