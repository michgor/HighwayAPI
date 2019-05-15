using System;
using Highway.DAL.DomainModels.Vehicles;

namespace Highway.DAL.DomainModels.Routes
{
    public sealed class RoutePrice : Entity
    {       
        public decimal Price { get; }

        public Route Route { get; }
        
        public Vehicle Vehicle { get; }

        public DateTime ValidFrom { get; }

        public DateTime ValidTo { get; }

        public RoutePrice(
            decimal price,
            Route route,
            DateTime validFrom,
            DateTime validTo,
            Vehicle vehicle)
        {
            Price = price;
            Route = route;
            ValidFrom = validFrom;
            ValidTo = validTo;
            Vehicle = vehicle;
        }
    }
}
