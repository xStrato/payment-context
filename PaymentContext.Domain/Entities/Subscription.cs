using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Shared;

namespace PaymentContext.Domain.Entities
{
    public class Subscription: Entity
    {
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            _payments = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public List<Payment> _payments;
        public IReadOnlyCollection<Payment> Payments { get => _payments.ToList(); }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
            .Requires()
            .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "Payment date must be greater than now"));

            //Deixa passar e caso seja invalido, barra na hora de comittar no banco de dados
            _payments.Add(payment);
        }

        public void Activate()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }
        public void Inativate()
        {
            Active = false;
            LastUpdateDate = DateTime.Now;
        }
    }
}