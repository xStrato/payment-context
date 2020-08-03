using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.Commands;
using PaymentContext.Tests.Mocks;
using System;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Elon";
            command.LastName = "Musk";
            command.Document = "1234";
            command.BarCode = "1234";
            command.BoletoNumber = "1234";
            command.PaymentNumber = "1234";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 150;
            command.TotalPaid = 150;
            command.Payer = "SpaceX";
            command.PayerDocument = "1234";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "email@email.com";
            command.Street = "Second street";
            command.Number = "1234";
            command.Neighborhood = "Nobody";
            command.City = "São Francisco";
            command.State = "CA";
            command.Country = "US";
            command.ZipCode = "0313100";

            handler.Handle(command);
            Assert.IsFalse(handler.Valid);
        }
    }
}