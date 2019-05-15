using Highway.DAL.DomainModels.TollStations;

namespace Highway.DAL.DomainModels.Routes
{
    public sealed class Route : Entity
    {     
        public Route(Gate from, Gate to)
            : base()
        {
            this.From = from;
            this.To = to;
        }

        public Gate From { get; }

        public Gate To { get; }

        public Route(Gate from)
            : this(from, null)
        {
        }
    }
}
