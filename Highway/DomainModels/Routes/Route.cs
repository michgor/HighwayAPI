using Highway.DAL.DomainModels.TollStations;
using Highway.DomainModels.TollStations;

namespace Highway.DAL.DomainModels.Routes
{
    public sealed class Route : Entity
    {            
        public Gate Entry { get; }

        public Gate Exit { get; }

        public Route(Gate entry)
            : this(entry, null)
        {
        }

        public Route(Gate entry, Gate exit)
            : base()
        {
            this.Entry = entry;
            this.Exit = exit;
        }
    }
}
