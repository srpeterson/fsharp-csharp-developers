namespace CSharp.Examples;

public class Composition
{
    public int SubtractOneSquareAddTen(int input)
    {
        return input
            .SubtractOne()
            .Square()
            .AddTen();
    }
}
