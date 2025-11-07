using CodePlayground.Interfaces;
using CodePlayground.Services;

namespace Tests;

public class AlgorithmServiceTest
{
    private IAlgorithmService _service;
    
    [SetUp]
    public void Setup()
    {
        _service = new AlgorithmService();
    }

    [TestCase(666, "YP")]
    [TestCase(1, "A")]
    [TestCase(26, "Z")]
    [TestCase(2458, "CPN")]
    [TestCase(24568, "AJHX")]
    public void GetColumnTitleByColumnNumber_ShouldReturnCorrectColumnTitle(int input, string expected)
    {
        // Act
        var result = _service.GetColumnTitleByColumnNumber(input);
        
        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(new[] { 16, 17, 4, 3, 5, 2 }, new[] { 17, 5, 2 })]
    [TestCase(new[] { 4, 3, 7, 12, 6, 67, 5, 45, 34, 35, 2, 8 }, new[] { 67, 45, 35, 8 })]
    [TestCase(new[] { 12, 10, 12, 8, 7, 6 }, new[] { 12, 8, 7, 6 })]
    [TestCase(new[] { 1, 2, 3, 4, 5, 4 }, new[] { 5, 4 })]
    public void GetArrayLeaders_ShouldReturnCorrectLeaders(int[] input, int[] expected)
    {
        // Act
        var result = _service.GetArrayLeaders(input);
        
        // Assert
        Assert.That(result, Has.Length.EqualTo(expected.Length));
        
        for (var arrayIndex = 0; arrayIndex < result.Length; arrayIndex++)
        {
            Assert.That(result[arrayIndex], Is.EqualTo(expected[arrayIndex]));
        }
    }

    [TestCase(new[] { 10, 15 }, new[] { 10, 10 }, 5)]
    [TestCase(new[] { 2, 4, 6, 10 }, new[] { 1, 5, 7, 9 }, 2)]
    [TestCase(new[] { 10, 20, 40, 100 }, new[] { 9, 18, 40, 200 }, 3)]
    [TestCase(new[] { 10, 20, 40, 100 }, new[] { 1, 2, 30, 50 }, 87)]
    public void GetTotalPriceWithDiscounts_ShouldReturnCorrectTotalPrice(int[] prices, int[]  discounts, int expected)
    {
        // Act
        var result = _service.GetTotalPriceWithDiscounts(prices, discounts);
        
        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}