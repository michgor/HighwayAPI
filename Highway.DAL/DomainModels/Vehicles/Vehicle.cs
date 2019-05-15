namespace Highway.DAL.DomainModels.Vehicles
{
    public sealed class Vehicle : Entity
    {
        public Vehicle(string type)
            : base()
        {
            this.Type = type;
        }

        public string Type { get; }
    }
}
