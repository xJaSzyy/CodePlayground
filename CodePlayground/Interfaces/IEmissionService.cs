using CodePlayground.Enums;
using CodePlayground.Models;

namespace CodePlayground.Interfaces;

public interface IEmissionService
{
    /// <summary>
    /// Расчет выбросов загрязняющих веществ от бензогенератора
    /// </summary>
    /// <param name="pollutant">Загрязняющее вещество</param>
    /// <param name="workHoursPerDay">Время работы в день, ч</param>
    /// <param name="workDaysPerYear">Кол-во рабочих дней в году</param>
    /// <param name="generatorCount">Кол-во генераторов, шт</param>
    /// <param name="sameGeneratorCount">Кол-во одновременно работающих генераторов, шт</param>
    /// <returns></returns>
    public GasolineGeneratorEmissionsResult CalculateGasolineGeneratorEmissions(Pollutant pollutant, int workHoursPerDay,
        int workDaysPerYear, int generatorCount, int sameGeneratorCount);

    /// <summary>
    /// Расчет выбросов загрязняющих веществ от резервуаров
    /// </summary>
    /// <param name="maximumConcentration">Максимальная концентрация паров нефтепродуктов в резервуаре, г/м3</param>
    /// <param name="drainedVolume">Объем слитого нефтепродукта в резервуар, м3</param>
    /// <param name="averageDrainTime">Среднее время слива, с</param>
    /// <param name="oilAmountInAutumnWinter">Кол-во закачиваемого в резервуар нефтепродукта в осенне-зимний период, м3</param>
    /// <param name="oilAmountInSpringSummer">Кол-во закачиваемого в резервуар нефтепродукта в весенне-летний период, м3</param>
    /// <param name="fillingConcentrationInAutumnWinter">Концентрация паров нефтепродуктов при заполнении резервуаров в осенне-зимний период, г/м3</param>
    /// <param name="fillingConcentrationInSpringSummer">Концентрация паров нефтепродуктов при заполнении резервуаров в весенне-летний период, г/м3</param>
    /// <param name="pollutantConcentration">Концентрация загрязняющего вещества</param>
    /// <param name="precision">Количество знаков после запятой</param>
    /// <returns></returns>
    public (float, float) CalculateReservoirsEmissions(float maximumConcentration, float drainedVolume,
        float averageDrainTime, float oilAmountInAutumnWinter, float oilAmountInSpringSummer,
        float fillingConcentrationInAutumnWinter, float fillingConcentrationInSpringSummer,
        float pollutantConcentration, int precision = 6);

    /// <summary>
    /// Расчет выбросов загрязняющих веществ при механической обработке металлов
    /// </summary>
    /// <param name="type">Тип станка для обработки металла</param>
    /// <param name="annualEquipmentOperatingTimeFund">Годовой фонд времени работы оборудования, ч</param>
    /// <param name="precision">Количество знаков после запятой</param>
    /// <returns></returns>
    public (float, float) CalculateDuringMetalMachiningEmissions(MetalMachiningMachineType type,
        float annualEquipmentOperatingTimeFund, int precision = 6);
}