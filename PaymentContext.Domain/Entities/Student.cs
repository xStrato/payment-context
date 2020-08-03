using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared;

namespace PaymentContext.Domain.Entities
{
    public class Student: Entity
    {
        public Name Name { get; set; }
        public Document Document { get; set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        private List<Subscription> _subscriptions;
        public IReadOnlyCollection<Subscription> Subscriptions { get => _subscriptions.ToList();}

        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }
        public void AddSubscription(Subscription subscription)
        {
            // 1. Se tiver assinatura vigente, cancela
            // OU
            // 2. Cancela todas as outras assinaturas, e coloca esta como a principal
            var hasSubcriptionsActive = _subscriptions.Where(sub => sub.Active);

            AddNotifications(new Contract()
            .Requires()
            .IsFalse(hasSubcriptionsActive.Any(), "Student.Subscriptions", "You already have an active subscription")
            .AreNotEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "This subscription doesn't have any payments"));

            if(Valid) _subscriptions.Add(subscription);

            //Implementação alternativa que requer test:
            // if(hasSubcriptionsActive.Any()) AddNotification("Student.Subscriptions", "You already have an active subscription");
        }
    }
}