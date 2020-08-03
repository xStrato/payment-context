using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Student _student;
        private readonly Name _name;
        private readonly Email _email;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Subscription _subscription;
        public StudentTests()
        {
            _name = new Name("Elon", "Musk");
            _document = new Document("123456789", EDocumentType.CPF);
            _email = new Email("elon@musk.com");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
            _address = new Address("Street 1", "123", "whatever",  "São Francisco", "CA", "US", "0000-00");
        }
        
        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PaypalPayment("696969", DateTime.Now, DateTime.Now.AddDays(-5), 10, 10, "SpaceX", _document, _address, _email);

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHadNoPayment()
        {
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenAddSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PaypalPayment("696969", DateTime.Now, DateTime.Now.AddDays(-5), 10, 10, "SpaceX", _document, _address, _email);

            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}