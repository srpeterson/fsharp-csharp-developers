using System;

namespace CSharp.Examples
{
    public class Person //good 'ol DTO or POCO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
    }

    public class ImmutablePerson
    {
        public ImmutablePerson (string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private string FirstName { get; }
        private string LastName { get; }
       
    }

    public class EquatablePerson : IEquatable<EquatablePerson>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals(obj as EquatablePerson);
        }

        public bool Equals(EquatablePerson other)
        {
            if (other == null) return false;

            return string.Equals(FirstName, other.FirstName)
                   && string.Equals(LastName, other.LastName);

        }

        public override int GetHashCode()
        {
            var firstNameHashCode = !string.IsNullOrEmpty(FirstName) ? FirstName.GetHashCode() : 0;
            var lastNameHashCode = !string.IsNullOrEmpty(LastName) ? LastName.GetHashCode() : 0;
            return firstNameHashCode ^ lastNameHashCode;
        }

    }

}
