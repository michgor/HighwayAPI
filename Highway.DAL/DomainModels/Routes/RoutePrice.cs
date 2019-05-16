using System;
using Highway.DAL.DomainModels.Vehicles;

namespace Highway.DAL.DomainModels.Routes
{
    public sealed class RoutePrice : Entity
    {              
        public Route Route { get; }
        
        public Vehicle Vehicle { get; }

        public decimal Price { get; }

        public DateTime ValidFrom { get; }

        public DateTime ValidTo { get; }


        public RoutePrice(
            decimal price,
            DateTime validFrom, 
            DateTime validTo,
            Route route,            
            Vehicle vehicle)
        {
            Price = price;
            Route = route;
            Vehicle = vehicle;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }
    }
}
