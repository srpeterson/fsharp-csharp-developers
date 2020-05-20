namespace CSharp.Examples
{
    public class CompositionExample
    {
        public string WhatIsTheAnswer(int input)
        {
            return input
                .SubtractOne()
                .Square()
                .AddTen()
                .TheAnswer("The extension chaining answer:");
        }
    }

    public static class CompositionExtensions
    {
        public static int SubtractOne(this int input) => input - 1;

        public static int Square(this int input) => input * input;

        public static int AddTen(this int input) => input + 10;

        public static string TheAnswer(this int input, string prefix) => $"{prefix} {input}";
    }

}
