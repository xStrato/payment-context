using System;
using Flunt.Notifications;

namespace PaymentContext.Shared
{
    public abstract class Entity: Notifiable
    {
        public Guid Id { get; private set; }

        // public Entity(Guid id)
        // {
        //     Id = Guid.NewGuid();
        // }
    }
}