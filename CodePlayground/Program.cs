using CodePlayground.Extensions;
using CodePlayground.Services;

class Program
{
    private static async Task Main()
    {
        var algorithmService = new AlgorithmService();

        var columnTitle = algorithmService.GetColumnTitleByColumnNumber(24568);
        Console.WriteLine($"Column title: {columnTitle};");
        
        var arrayLeaders = algorithmService.GetArrayLeaders(new[] { 16, 17, 4, 3, 5, 2 });
        Console.WriteLine($"Array leaders: {arrayLeaders.ToFormattedString()};");

        var totalPriceWithDiscounts = algorithmService.GetTotalPriceWithDiscounts(new[] { 10, 15 }, new[] { 10, 10 });
        Console.WriteLine($"Total price: {totalPriceWithDiscounts};");

        var isPositiveDominant = algorithmService.IsPositiveDominant(new[] { 1, 2, 3, -3, -4 });
        Console.WriteLine($"Is positive dominant? {isPositiveDominant};");
    }
}