using System.Collections.Generic;
using System.Linq;

namespace Highway.DAL.DomainModels.TollStations
{
    public class TollStation
    {
        public string Name { get; }

        public IReadOnlyCollection<Gate> Gates { get; private set; }

        public Address Address { get; }

        public TollStation(
            string name,
            Address address)
            : this(name, null, address)
        {
        }

        public TollStation(
            string name,
            IReadOnlyCollection<Gate> gates,
            Address address)
        {
            this.Name = name;
            this.Gates = gates;
            this.Address = address;
        }

        public void AddGate(Gate gate)
        {
            var newGatesList = this.Gates.ToList();
            newGatesList.Add(gate);
            this.Gates = newGatesList.ToList();
        }

        public void AddGates(IEnumerable<Gate> gates)
        {
            var newGatesList = this.Gates.ToList();
            newGatesList.AddRange(gates);
            this.Gates = newGatesList.ToList();
        }
    }
}
