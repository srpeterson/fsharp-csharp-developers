using System;

namespace CSharp.Examples
{
    public class CompositionChainingExtensions
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

    public static class ChainingExtensions
    {
        public static int SubtractOne(this int input) => input - 1;

        public static int Square(this int input) => input * input;

        public static int AddTen(this int input) => input + 10;

        public static string TheAnswer(this int input, string prefix) => $"{prefix} {input}";
    }

    public class CompositionFuncExtensions
    {
        private readonly Func<int, int> _subtractOne = input => input - 1;
        private readonly Func<int, int> _square = input => input * input;
        private readonly Func<int, int> _addTen = input => input + 10;
        private readonly Func<int, string> _theAnswer = input => $"The function composing answer: {input}";

        public string WhatIsTheAnswer(int input)
        {
            //this is opposite of F#. The last function on the 'Compose' chain
            //is the first executed
            return _theAnswer.Compose(_addTen.Compose(_square).Compose(_subtractOne))(input);
        }
    }

    public static class FuncExtensions
    {
        public static Func<T, TReturn2> Compose<T, TReturn1, TReturn2>(this Func<TReturn1, TReturn2> func1, Func<T, TReturn1> func2)
        {
            return x => func1(func2(x));
        }
    }
}
