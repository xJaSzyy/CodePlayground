using CodePlayground.Models;

namespace CodePlayground.Interfaces;

public interface IOutputService
{
    /// <summary>
    /// Создать в виде Excel-файла отчет по расчету выбросов ЗВ от бензогенератора
    /// </summary>
    /// <param name="report">Отчет по расчету выбросов ЗВ от бензогенератора (входные + выходные данные)</param>
    public void CreateGasolineGeneratorEmissionsReport(GasolineGeneratorEmissionsReport report);
}