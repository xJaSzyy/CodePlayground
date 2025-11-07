namespace CodePlayground.Extensions;

public static class ArrayExtensions
{
    public static string ToFormattedString(this int[] array)
    {
        return "[" + string.Join(", ", array) + "]";
    }
}