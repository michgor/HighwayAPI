using System.Collections.Generic;
using System.Linq;
using Highway.DAL.DataSource;
using Highway.DAL.DomainModels.Routes;
using Highway.DAL.DomainModels.TollStations;
using Highway.DAL.DomainModels.Vehicles;
using Highway.DAL.Helpers;
using Highway.DAL.Helpers.Results;
using Highway.DAL.Repositories.TollStations;
using Highway.DAL.ResultObjects;

namespace Highway.Repositories.TollStations
{
    public class TollStationRepository : ITollStationRepository
    {
        private readonly IStubDataRepository stubDataRepository;
        private readonly IDateTimeProvider dateTimeProvider;

        public TollStationRepository(IStubDataRepository stubDataRepository, IDateTimeProvider dateTimeProvider)
        {
            this.stubDataRepository = stubDataRepository;
            this.dateTimeProvider = dateTimeProvider;
        }

        public IDataResult<IReadOnlyCollection<string>> GetGateAccessTypes(string tollStationId, string entryGateId)
        {
            var tollStation = this.stubDataRepository.TollStations.FirstOrDefault(ts => ts.PublicId.Equals(tollStationId));

            if (tollStation == null)
            {
                return new DataFailedResult<IReadOnlyCollection<string>>();;
            }

            var result = tollStation.Gates.Single(g => g.PublicId.Equals(entryGateId)).GateAccessTypes.Select(x => x.ToString()).ToList();

            return new DataSuccessResult<IReadOnlyCollection<string>>(result);
        }

        public IDataResult<decimal> GetPriceForRoute(string entryGateId, string exitGateId, VehicleCategory vehicleCategory)
        {
            var routePrice = this.stubDataRepository.RoutePrices.Where(rp =>
                rp.Route.Entry.PublicId.Equals(entryGateId)
                && rp.Vehicle.VehicleCategory == vehicleCategory);

            routePrice = string.IsNullOrEmpty(exitGateId) ? 
                routePrice.Where(rp => rp.Route.Exit == null) 
                : routePrice.Where(rp => rp.Route.Exit.PublicId.Equals(exitGateId));

            var result = routePrice.FirstOrDefault(rp => dateTimeProvider.Now <= rp.ValidTo);

            if (result == null)
            {
                return new DataFailedResult<decimal>();
            }

            return new DataSuccessResult<decimal>(result.Price);
        }

        public IDataResult<IReadOnlyCollection<RoutePrice>> GetPricesForAllRoutes(string tollStationId)
        {
            var tollStation = this.stubDataRepository.TollStations.FirstOrDefault(ts => ts.PublicId.Equals(tollStationId));

            if (tollStation == null)
            {
                return new DataFailedResult<IReadOnlyCollection<RoutePrice>>();
            }

            var gatesInTollStation = tollStation.Gates;
            var routePrices = new List<RoutePrice>();

            foreach (var gate in gatesInTollStation)
            {
                var gatePrices = this.stubDataRepository.RoutePrices.Where(rp => 
                    rp.Route.Entry.Id.Equals(gate.Id)
                    && dateTimeProvider.Now <= rp.ValidTo);

                gatePrices.ToList().ForEach(gp => routePrices.Add(gp));
            }

            return new DataSuccessResult<IReadOnlyCollection<RoutePrice>>(routePrices);
        }
    }
}