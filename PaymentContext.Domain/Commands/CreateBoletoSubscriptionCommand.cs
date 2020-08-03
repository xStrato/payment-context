using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Command;

namespace PaymentContext.Domain.Commands
{
    public class CreateBoletoSubscriptionCommand: Notifiable, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string BarCode { get; set; }
        public string BoletoNumber { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string PayerEmail { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

    public void Validate()
    {
        //Fail Fast Validation - Validate JSON
        //
        AddNotifications(new Contract()
            .Requires()
            .HasMinLen(FirstName, 3, "FirstName", "Property must be at least 3 characters long")
            .HasMinLen(FirstName, 3, "LastName", "Property must be at least 3 characters long")
            .HasMaxLen(FirstName, 40, "FirstName", "Property must not contain more than 40 characters")
            );
    }
  }
}