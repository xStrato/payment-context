using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name: ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
            .Requires()
            .HasMinLen(FirstName, 3, "FirstName", "Property must be at least 3 characters long")
            .HasMinLen(FirstName, 3, "LastName", "Property must be at least 3 characters long")
            .HasMaxLen(FirstName, 40, "FirstName", "Property must not contain more than 40 characters")
            );
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public override string ToString() => $"{FirstName} {LastName}";
    }
}