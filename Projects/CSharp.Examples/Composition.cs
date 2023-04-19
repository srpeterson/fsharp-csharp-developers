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

    // Imperative way but all squished together..
    // Is it readable for the next guy or does he have to go dust off
    // his 8th grade algebra book?
    public static int SubtractOneSquareAddTen2(int value)
    {
        return ((value - 1) * (value - 1)) + 10;
    }

    // Better - a C# way of doing F# piping..
    public static int SubtractOneSquareAddTen3(int input)
    {
        return input
            .SubtractOne()
            .Square()
            .AddTen();
    }
}
