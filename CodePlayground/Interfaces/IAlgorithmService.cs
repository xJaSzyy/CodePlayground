namespace CodePlayground.Interfaces;

public interface IAlgorithmService
{
    public string GetColumnTitleByColumnNumber(int columnNumber);

    public int[] GetArrayLeaders(int[] array);

    public int GetTotalPriceWithDiscounts(int[] prices, int[] discounts);

    public bool IsPositiveDominant(int[] numbers);
}