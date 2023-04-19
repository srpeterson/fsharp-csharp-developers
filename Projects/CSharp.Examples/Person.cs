namespace CSharp.Examples;

public class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int Age { get; set; }
    
    public string GetFullName()
    {
        string fullname = $"{this.LastName}, {this.FirstName}";
        return fullname;
    }
}