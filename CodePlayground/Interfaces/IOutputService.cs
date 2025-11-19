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
}