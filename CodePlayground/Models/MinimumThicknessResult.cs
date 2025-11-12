namespace CodePlayground.Models;

public class MinimumThicknessResult
{
    /// <summary>
    /// Толщина плиты безврубовой перемычки, обеспечивающая ее прочность на изгиб под действием эквивалентного давления, м
    /// </summary>
    public double BendingStrengthThickness { get; set; }
    
    /// <summary>
    /// Толщина плиты безврубовой перемычки, обеспечивающая прочность ее закрепления по контуру, м
    /// </summary>
    public double AnchoringStrengthThickness { get; set; }
    
    /// <summary>
    /// Расчетная толщина безврубовой перемычки, м
    /// </summary>
    public double MinimumThickness { get; set; }
}