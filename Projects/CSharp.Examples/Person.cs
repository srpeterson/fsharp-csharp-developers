﻿namespace CSharp.Examples;

public class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Person(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public string GetFullName()
    {
        string fullname = $"{this.LastName}, {this.FirstName}";
        return fullname;
    }
}