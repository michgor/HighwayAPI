using System;
using System.Collections.Generic;
using System.Linq;
using Highway.DAL.DomainModels.Routes;
using Highway.DAL.DomainModels.TollStations;
using Highway.DAL.DomainModels.Vehicles;
using Highway.DomainModels.TollStations;

namespace Highway.DAL.DataSource
{
    public class StubDataRepository : IStubDataRepository
    {
        private IList<Gate> wroclawWschodGates;
        private IList<Gate> wroclawPoludnieGates;
        private IList<Gate> katowiceGates;
        private IList<Route> routes;
        private IList<Vehicle> vehicles;

        public IList<TollStation> TollStations { get; private set; }
        public IList<RoutePrice> RoutePrices { get; private set; }



        public StubDataRepository()
        {
            this.CreateTollStations();
            this.CreateVehicles();
            this.CreateRoutes();
        }

        private void CreateVehicles()
        {
            var motorcycle = new Vehicle(VehicleCategory.Motorcycle);
            var car = new Vehicle(VehicleCategory.Car);
            var truck = new Vehicle(VehicleCategory.Truck);
            var longVehicle = new Vehicle(VehicleCategory.LongVehicle);

            this.vehicles = new List<Vehicle>
            {
                motorcycle,
                car,
                truck,
                longVehicle
            };
        }

        private void CreateTollStations()
        {
            var tollStationWroclawWschod = new TollStation(
                "Wrocław Wschód",
                new Address("Wiejska 1", "Kobierzyce", "51-124", "Polska", "11,12", "51,14"),
                "wro-w"
                );

            this.wroclawWschodGates = new List<Gate>
            {
                new Gate("Gate-1", tollStationWroclawWschod, new []{ GateAccessType.Ticket, GateAccessType.ViaToll }, null, "wro-w-g1"),
                new Gate("Gate-2", tollStationWroclawWschod, new []{ GateAccessType.Ticket, GateAccessType.ViaToll }, null, "wro-w-g2")
            };

            tollStationWroclawWschod.AddGates(wroclawWschodGates);

            var tollStationWroclawPoludnie = new TollStation(
                "Wrocław Południe",
                new Address("Miejska 1", "Wrocław", "55-124", "Polska", "12,87", "51,32"),
                "wro-p"
            );

            this.wroclawPoludnieGates = new List<Gate>
            {
                new Gate("Gate-1", tollStationWroclawPoludnie, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.Card, GatePaymentOption.Cash }, "wro-p-g1"),
                new Gate("Gate-2", tollStationWroclawPoludnie, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.Card, GatePaymentOption.Cash }, "wro-p-g2"),
                new Gate("Gate-3", tollStationWroclawPoludnie, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.PrePaid }, "wro-p-g3"),
                new Gate("Gate-4", tollStationWroclawPoludnie, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.PrePaid }, "wro-p-g4")
            };

            tollStationWroclawPoludnie.AddGates(this.wroclawPoludnieGates);

            var tollStationKatowice = new TollStation(
                "Katowice Station",
                new Address("Górnicza 1", "Katowice", "18-124", "Polska", "14,87", "53,32"),
                "kat"
            );

            this.katowiceGates = new List<Gate>
            {
                new Gate("Gate-1", tollStationKatowice, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.Card, GatePaymentOption.Cash }, "kat-g1"),
                new Gate("Gate-2", tollStationKatowice, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.PrePaid }, "kat-g2")
            };

            tollStationKatowice.AddGates(katowiceGates);

            this.TollStations = new List<TollStation> { tollStationWroclawWschod, tollStationWroclawPoludnie, tollStationKatowice };
        }

        private void CreateRoutes()
        {
            var routeWroclawEastWroclawSouth1 = new Route(this.wroclawWschodGates.ElementAt(0), this.wroclawPoludnieGates.ElementAt(0));
            var routeWroclawEastWroclawSouth2 = new Route(this.wroclawWschodGates.ElementAt(0), this.wroclawPoludnieGates.ElementAt(1));
            var routeWroclawEastWroclawSouth3 = new Route(this.wroclawWschodGates.ElementAt(0), this.wroclawPoludnieGates.ElementAt(2));
            var routeWroclawEastWroclawSouth4 = new Route(this.wroclawWschodGates.ElementAt(0), this.wroclawPoludnieGates.ElementAt(3));

            var routeWroclawEastWroclawSouth5 = new Route(this.wroclawWschodGates.ElementAt(1), this.wroclawPoludnieGates.ElementAt(0));
            var routeWroclawEastWroclawSouth6 = new Route(this.wroclawWschodGates.ElementAt(1), this.wroclawPoludnieGates.ElementAt(1));
            var routeWroclawEastWroclawSouth7 = new Route(this.wroclawWschodGates.ElementAt(1), this.wroclawPoludnieGates.ElementAt(2));
            var routeWroclawEastWroclawSouth8 = new Route(this.wroclawWschodGates.ElementAt(1), this.wroclawPoludnieGates.ElementAt(3));

            var routeKatowice1 = new Route(this.katowiceGates.ElementAt(0));
            var routeKatowice2 = new Route(this.katowiceGates.ElementAt(0));
            var routeKatowice3 = new Route(this.katowiceGates.ElementAt(0));
            var routeKatowice4 = new Route(this.katowiceGates.ElementAt(0));

            this.routes = new List<Route>
            {
                routeWroclawEastWroclawSouth1,
                routeWroclawEastWroclawSouth2,
                routeWroclawEastWroclawSouth3,
                routeWroclawEastWroclawSouth4,
                routeWroclawEastWroclawSouth5,
                routeWroclawEastWroclawSouth6,
                routeWroclawEastWroclawSouth7,
                routeWroclawEastWroclawSouth8,
                routeKatowice1,
                routeKatowice2,
                routeKatowice3,
                routeKatowice4
            };

            var validFrom = new DateTime(2019, 5, 15);
            var validTo = new DateTime(2020, 5, 15);

            decimal priceValue = new decimal(0.00);
            this.RoutePrices = new List<RoutePrice>();

            foreach (var vehicle in this.vehicles)
            {
                priceValue += ((decimal)5.00);

                foreach (var route in this.routes)
                {
                    this.RoutePrices.Add(new RoutePrice(priceValue, validFrom, validTo, route, vehicle));
                }
            }
        }
    }
}
