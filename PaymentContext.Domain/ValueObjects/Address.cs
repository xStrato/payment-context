using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Address: ValueObject
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract()
            .Requires()
            .HasMinLen(Street, 3, "Street", "Property must be at least 3 characters long")
            .HasMinLen(Neighborhood, 3, "Neighborhood", "Property must be at least 3 characters long")
            .HasMaxLen(State, 40, "State", "Property must not contain more than 40 characters")
            );
        }
    }
}