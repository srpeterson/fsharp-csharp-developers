using System;

namespace CSharp.Examples
{
    public class Customer //good 'ol DTO or POCO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class ImmutableCustomer
    {
        public ImmutableCustomer(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        private string FirstName { get; }
        private string LastName { get; }
        private int Age { get; }
    }

    public class EquatableCustomer : IEquatable<EquatableCustomer>
    {
        public EquatableCustomer(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        private string FirstName { get; }
        private string LastName { get; }
        private int Age { get; }

        public bool Equals(EquatableCustomer other)
        {
            if (other == null) return false;

            return string.Equals(FirstName, other.FirstName)
                && string.Equals(LastName, other.LastName)
                && Age == other.Age;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as EquatableCustomer);
        }

        public override int GetHashCode()
        {
            unchecked
            {

                var firstNameHashCode = !string.IsNullOrEmpty(FirstName) ? FirstName.GetHashCode() : 0;
                var lastNameHashCode = !string.IsNullOrEmpty(LastName) ? LastName.GetHashCode() : 0;
                var ageHashCode = (13 * 397) ^ Age;
                return firstNameHashCode ^ lastNameHashCode ^ ageHashCode;
            }
        }

    }

}
