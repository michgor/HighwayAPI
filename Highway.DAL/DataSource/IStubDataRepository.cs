using System.Collections.Generic;
using Highway.DAL.DomainModels.Routes;
using Highway.DAL.DomainModels.TollStations;

namespace Highway.DAL.DataSource
{
    public interface IStubDataRepository
    {
        IList<TollStation> TollStations { get; }

        IList<RoutePrice> RoutePrices { get; }
    }
}