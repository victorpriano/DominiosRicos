using System;
using Flunt.Notifications;

namespace PaymentContext.Shared.Entities
{
    // Id das entidades
    public abstract class Entity : Notifiable
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}