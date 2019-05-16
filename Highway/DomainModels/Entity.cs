using System;

namespace Highway.DAL.DomainModels
{
    public abstract class Entity
    {
        protected Entity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; }        
    }
}
