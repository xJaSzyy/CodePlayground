namespace CodePlayground.Models;

public class DryCementMixtureConsumptionResult
{
    /// <summary>
    /// Объем возводимой безврубовой взрывоустойчивой изолирующей перемычки, м3
    /// </summary>
    public double Volume { get; set; }
    
    /// <summary>
    /// Расход сухой цементной смеси «УГМ-П» на возведение безврубовой взрывоустойчивой перемычки
    /// </summary>
    public double DryCementMixtureConsumption { get; set; }
    
    /// <summary>
    /// Итоговый расход сухой цементной смеси «УГМ-П» на возведение проектируемой безврубовой взрывоустойчивой изолирующей перемычки
    /// </summary>
    public double TotalDryCementMixtureConsumption { get; set; }
}