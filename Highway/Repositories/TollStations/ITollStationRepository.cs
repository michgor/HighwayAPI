using System;
using System.Collections.Generic;
using Highway.DAL.DomainModels.Routes;
using Highway.DAL.DomainModels.TollStations;
using Highway.DAL.DomainModels.Vehicles;
using Highway.DAL.ResultObjects;

namespace Highway.DAL.Repositories.TollStations
{
    public interface ITollStationRepository
    {
        IDataResult<IReadOnlyCollection<string>> GetGateAccessTypes(string tollStationId, string entryGateId);

        IDataResult<decimal> GetPriceForRoute(string entryGateId, string exitGateId, VehicleCategory vehicleCategory);

        IDataResult<IReadOnlyCollection<RoutePrice>> GetPricesForAllRoutes(string tollStationId);
    }
}
