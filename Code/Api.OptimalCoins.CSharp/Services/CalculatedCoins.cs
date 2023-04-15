using Challenge.Api.CSharp.Domain;

namespace Challenge.Api.CSharp.Services;

public class CalculatedCoins
{
    public int SilverDollars { get; set; }

    public int HalfDollars { get; set; }

    public int Quarters { get; set; }

    public int Dimes { get; set; }

    public int Nickels { get; set; }

    public int Pennies { get; set; }

    public ValidateCoinsResult ValidateCoinsResult { internal get; set; }
}
