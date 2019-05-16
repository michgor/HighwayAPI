using System.Collections.Generic;
using System.Linq;
using Highway.DomainModels.TollStations;

namespace Highway.DAL.DomainModels.TollStations
{
    public class TollStation : Entity
    {
        public string Name { get; }

        public string PublicId { get; }

        public IReadOnlyCollection<Gate> Gates { get; private set; }

        public Address Address { get; }

        public TollStation(
            string name,
            Address address,
            string publicId)
            : this(name, null, address, publicId)
        {
        }

        public TollStation(
            string name,
            IEnumerable<Gate> gates,
            Address address,
            string publicId)
        {
            this.Name = name;
            this.Gates = gates == null ? new List<Gate>() : gates.ToList();
            this.Address = address;
            this.PublicId = publicId;
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
