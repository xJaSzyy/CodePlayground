namespace CodePlayground.Interfaces;

public interface IAlgorithmService
{
    public string GetColumnTitleByColumnNumber(int columnNumber);

    public int[] GetArrayLeaders(int[] array);
}