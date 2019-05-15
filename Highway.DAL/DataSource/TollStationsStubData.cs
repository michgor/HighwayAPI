using System.Collections.Generic;
using Highway.DAL.DomainModels.Routes;
using Highway.DAL.DomainModels.TollStations;

namespace Highway.DAL.DataSource
{
    public class TollStationsStubData
    {
        private readonly IReadOnlyCollection<TollStation> tollStations;


        public TollStationsStubData()
        {
            this.CreateToolsStations();
        }

        private void CreateToolsStations()
        {
            var tollStationWroclawWschod = new TollStation(
                "Wrocław Wschód",
                new Address("wiejska 1", "Kobierzyce", "51-124", "Polska", "11,12", "51,14")
                );

            var wroclawWschodGates = new List<Gate>
            {
                new Gate("Gate-1", tollStationWroclawWschod, new []{ GateAccessType.Ticket, GateAccessType.ViaToll } , null),
                new Gate("Gate-2", tollStationWroclawWschod, new []{ GateAccessType.Ticket, GateAccessType.ViaToll }, null)
            };

            tollStationWroclawWschod.AddGates(wroclawWschodGates);

            var tollStationWroclawPoludnie = new TollStation(
                "Wrocław Południe",
                new Address("miejska 1", "Wrocław", "55-124", "Polska", "12,87", "51,32")
            );

            var wroclawPoludnieGates = new List<Gate>
            {
                new Gate("Gate-1", tollStationWroclawPoludnie, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.Card, GatePaymentOption.Cash }),
                new Gate("Gate-2", tollStationWroclawPoludnie, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.Card, GatePaymentOption.Cash }),
                new Gate("Gate-3", tollStationWroclawPoludnie, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.PrePaid }),
                new Gate("Gate-4", tollStationWroclawPoludnie, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.PrePaid })
            };

            var tollStationKatowice = new TollStation(
                "Katowice Station",
                new Address("górnicza 1", "Katowice", "18-124", "Polska", "14,87", "53,32")
            );

            var katowiceGates = new List<Gate>
            {
                new Gate("Gate-1", tollStationKatowice, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.Card, GatePaymentOption.Cash }),
                new Gate("Gate-2", tollStationKatowice, new []{ GateAccessType.Payment }, new []{ GatePaymentOption.PrePaid })
            };

            tollStationKatowice.AddGates(katowiceGates);
        }
    }
}
