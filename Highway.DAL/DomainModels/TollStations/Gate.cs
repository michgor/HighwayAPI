using System.Collections.Generic;
using System.Linq;

namespace Highway.DAL.DomainModels.TollStations
{
    public sealed class Gate : Entity
    {
        public string Name { get; }

        public IReadOnlyCollection<GateAccessType> GateAccessType { get; }

        public IReadOnlyCollection<GatePaymentOption> PaymentOptions { get; }

        public TollStation TollStation { get; private set; }

        public Gate(
            string name,
            IEnumerable<GateAccessType> gateAccessType,
            IEnumerable<GatePaymentOption> paymentOptions
        ) : this(name, null, gateAccessType, paymentOptions)
        {
        }

        public Gate(
            string name,
            TollStation tollStation,
            IEnumerable<GateAccessType> gateAccessType,
            IEnumerable<GatePaymentOption> paymentOptions
        )
        {
            this.GateAccessType = gateAccessType.ToList();
            this.Name = name;
            TollStation = tollStation;
            this.PaymentOptions = paymentOptions.ToList();
        }

        public void AssignToolStation(TollStation tollStation)
        {
            this.TollStation = tollStation;

            // In that case I could raise a Domain Event that a new Toll station  was assigned to the gate and perform then next actions.
        }
    }
}
