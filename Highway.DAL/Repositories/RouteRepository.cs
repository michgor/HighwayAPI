using System;
using System.Collections.Generic;
using Highway.DAL.DomainModels.Routes;

namespace Highway.DAL.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        public IReadOnlyCollection<RoutePrice> GetPriceForRoute(Guid @from, Guid to)
        {
            throw new NotImplementedException();
        }
    }

    public interface IRouteRepository
    {
        IReadOnlyCollection<RoutePrice> GetPriceForRoute(Guid from, Guid to);
    }
}
