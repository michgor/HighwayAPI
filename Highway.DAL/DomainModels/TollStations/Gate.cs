using System.Collections.Generic;
using System.Linq;
using Highway.DAL.DomainModels;
using Highway.DAL.DomainModels.TollStations;

namespace Highway.DomainModels.TollStations
{
    public sealed class Gate : Entity
    {
        public string Name { get; }

        public IReadOnlyCollection<GateAccessType> GateAccessTypes { get; }

        public IReadOnlyCollection<GatePaymentOption> PaymentOptions { get; }

        public TollStation TollStation { get; private set; }

        public string PublicId { get; }

        public Gate(
            string name,
            IEnumerable<GateAccessType> gateAccessType,
            IEnumerable<GatePaymentOption> paymentOptions,
            string publicId) 
            : this(name, null, gateAccessType, paymentOptions, publicId)
        {
        }

        public Gate(
            string name,
            TollStation tollStation,
            IEnumerable<GateAccessType> gateAccessType,
            IEnumerable<GatePaymentOption> paymentOptions,
            string publicId)
        {
            this.GateAccessTypes = gateAccessType == null ? new List<GateAccessType>() : gateAccessType.ToList();
            this.Name = name;
            this.TollStation = tollStation;
            this.PublicId = publicId;
            this.PaymentOptions = paymentOptions == null ? new List<GatePaymentOption>() : paymentOptions.ToList();
        }

        public void AssignToolStation(TollStation tollStation)
        {
            this.TollStation = tollStation;

            // In that case I could raise a Domain Event that a new Toll station  was assigned to the gate and perform then next actions.
        }
    }
}
