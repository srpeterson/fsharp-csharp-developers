namespace CSharp.Examples;

public class Composition
{
    // 'Normal' imperative way
    public static int SubtractOneSquareAddTen1(int value)
    {
        int valueMinusOne = value - 1; 
        int valueSquared = valueMinusOne * valueMinusOne; 
        int result = valueSquared + 10; 
        return result;
    }
    
    // Better - a C# way of doing F# piping..
    public static int SubtractOneSquareAddTen2(int input)
    {
        return input
            .SubtractOne()
            .Square()
            .AddTen();
    }
}
