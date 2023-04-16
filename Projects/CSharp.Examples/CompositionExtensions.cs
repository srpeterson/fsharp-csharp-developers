namespace CSharp.Examples;

public static class CompositionExtensions
{
    public static int SubtractOne(this int input) => input - 1;

    public static int Square(this int input) => input * input;

    public static int AddTen(this int input) => input + 10;
}
