namespace Highway.DAL.DomainModels.Vehicles
{
    public sealed class Vehicle : Entity
    {
        public Vehicle(VehicleCategory vehicleCategory)
            : base()
        {
            this.VehicleCategory = vehicleCategory;
        }

        public VehicleCategory VehicleCategory { get; }
    }
}
