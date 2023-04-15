using System;

namespace Challenge.Api.CSharp.Services;

public static class Validation
{
    public static bool DecimalLessThanZero(decimal d) => d < 0m;

    public static bool DecimalGreaterThanIntMax(decimal d) => d > int.MaxValue;

    public static bool ValidateDecimal(this decimal d, Func<decimal, bool> func) => func(d);

    //later on in real life we could add something like this very easily..
    //public static bool ValidateString(this string s, Func<string, bool> func) => func(s);
}