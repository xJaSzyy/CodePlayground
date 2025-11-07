using CodePlayground.Extensions;
using CodePlayground.Services;

class Program
{
    private static async Task Main()
    {
        var algorithmService = new AlgorithmService();
        
        var arrayLeaders = algorithmService.GetArrayLeaders(new[] { 16, 17, 4, 3, 5, 2 });
        Console.WriteLine(arrayLeaders.ToFormattedString());
    }
}