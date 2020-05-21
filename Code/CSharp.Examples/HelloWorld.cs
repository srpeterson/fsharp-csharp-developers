using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.Examples
{
    public class HelloWorld
    {
        private const string Greeting = "Hello World!";

        private const int Ten = 10;

        private List<string> _firstNames = new List<string> { "Steve", "Bob", "Mary" };

        private string[] _lastNames = { "Jones", "Smith", "Adams" };

        private Dictionary<string, string> _myDictionary = new Dictionary<string, string> {{"key1", "Bob"}, {"key2", "Bob"}};

        private HashSet<string> _hashNames = new HashSet<string> { "Steve", "Bob", "Mary" };

        private (int, string) _myTuple = (5, "five");

        private int? _eleven = 11;

        private int? _twelve = null;

        private string LessThanTen(int x) => x < 10 ? "Less than ten" : "Greater than ten";

        private int GetMyTupleFirst()
        {
            var (first, _) = _myTuple; //variable
            return first; //5
        }

        private int SwitchOnInt(int x)
        {
            return x switch
            {
                5 => x + 10,
                _ when x < 10 => x,
                _ => x + 100
            };
        }

        private readonly List<int> _oneToFive = new List<int> { 1, 2, 3, 4, 5 };

        private void IterExample() => _oneToFive.ForEach(Console.Write);

        private List<int> FilterExample() => _oneToFive.Where(e => e < 3).ToList();

        private int SumExample() => _oneToFive.Sum();

        private List<int> SelectExample() => _oneToFive.Select(e => e + 10).ToList();

        private int SumByExample() => _oneToFive.Select(e => e * 2).Sum();

        private string TellMeABoutYourYou(string name, int weight) => $"My name is {name} and I weigh {weight} pounds!";

        private string TellMeABoutYourPet(string pet, int age) => $"My pets name is {pet} and is {age} years old!";

        private string TellMeABoutYourDog(int age) => TellMeABoutYourPet("dog", age);

        private Func<string, int,string> PetFunc = (animal, age) => $"My pet is a {animal} and is {age} years old!";

        private Func<string, int, string> PersonFunc = (name, weight) => $"My name is {name} and I weigh {weight} pounds!";

        private string TellMe(string parm1, int parm2, Func<string, int, string> func) => func(parm1, parm2);
    }
}
