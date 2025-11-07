namespace CodePlayground.Interfaces;

public interface IAlgorithmService
{
    /// <summary>
    /// Получить название столбца страницы Excel
    /// </summary>
    /// <param name="columnNumber">Номер столбца страницы Excel</param>
    /// <returns>Название столбца страницы Excel</returns>
    public string GetColumnTitleByColumnNumber(int columnNumber);

    /// <summary>
    /// Получить всех лидеров в массиве
    /// </summary>
    /// <param name="array">Массив чисел</param>
    /// <returns>Массив всех лидеров</returns>
    public int[] GetArrayLeaders(int[] array);

    /// <summary>
    /// Получить суммарную цену с учетом скидок
    /// </summary>
    /// <param name="prices">Массив цен;</param>
    /// <param name="discounts">Массив скидок;</param>
    /// <returns>Суммарная цена с учетом скидок или -1, если длины массивов не совпадают.</returns>
    public int GetTotalPriceWithDiscounts(int[] prices, int[] discounts);

    /// <summary>
    /// Проверить больше ли положительных чисел в массиве?
    /// </summary>
    /// <param name="numbers">Массив чисел;</param>
    /// <returns>True - больше положительных чисел, False - больше отрицательных чисел.</returns>
    public bool IsPositiveDominant(int[] numbers);
}