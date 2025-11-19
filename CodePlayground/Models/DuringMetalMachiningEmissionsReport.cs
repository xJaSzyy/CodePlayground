namespace CodePlayground.Models;

/// <summary>
/// Отчет по расчету выбросов ЗВ при механической обработке металлов (входные + выходные данные)
/// </summary>
public class DuringMetalMachiningEmissionsReport
{
    /// <summary>
    /// Номер источника выделения
    /// </summary>
    public string SelectionSource { get; set; } = null!;

    /// <summary>
    /// Номер источника загрязнения
    /// </summary>
    public string PollutionSource { get; set; } = null!;
}