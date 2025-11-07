using CodePlayground.Interfaces;

namespace CodePlayground.Services;

public class AlgorithmService : IAlgorithmService
{
    public string GetColumnTitleByColumnNumber(int columnNumber)
    {
        var letters = new[]
        {
            "A", "B", "C", "D", "E", "F", "G", "H",
            "I", "J", "K", "L", "M", "N", "O", "P",
            "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };

        var result = string.Empty;

        while (columnNumber > letters.Length)
        {
            var remains = 0;

            while (columnNumber > letters.Length)
            {
                columnNumber -= letters.Length;
                remains++;
            }

            result = letters[columnNumber - 1] + result;

            columnNumber = remains;
        }

        return letters[columnNumber - 1] + result;
    }

    public int[] GetArrayLeaders(int[] array)
    {
        var result = new List<int>();
        
        for (var currentIndex = 0; currentIndex < array.Length; currentIndex++)
        {
            var isLeader = true;
            for (var nextIndex = currentIndex + 1; nextIndex < array.Length; nextIndex++)
            {
                if (array[currentIndex] <= array[nextIndex])
                {
                    isLeader = false;
                    break;
                }
            }

            if (isLeader)
            {
                result.Add(array[currentIndex]);
            }
        }

        return result.ToArray();
    }

    public int GetTotalPriceWithDiscounts(int[] prices, int[] discounts)
    {
        if (prices.Length != discounts.Length)
        {
            return -1;
        }

        return prices.Select((price, arrayIndex) => Math.Max(price - discounts[arrayIndex], 0)).Sum();
    }

    public bool IsPositiveDominant(int[] numbers)
    {
        var positiveCount = numbers.Distinct().Count(number => number >= 0);
        var negativeCount = numbers.Distinct().Count() - positiveCount;
        
        return positiveCount > negativeCount;
    }
}