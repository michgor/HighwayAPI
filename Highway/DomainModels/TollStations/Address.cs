namespace Highway.DAL.DomainModels.TollStations
{
    public class Address    
    {       
        public string Street { get; }

        public string City { get; }

        public string PostCode { get; }

        public string Country { get; }

        public string HorizontalCoordinate { get; }

        public string VerticalCoordinate { get; }

        public Address(
            string street,
            string city,
            string postCode,
            string country,
            string horizontalCoordinate,
            string verticalCoordinate)
        {
            this.Street = street;
            this.City = city;
            this.PostCode = postCode;
            this.Country = country;
            this.HorizontalCoordinate = horizontalCoordinate;
            this.VerticalCoordinate = verticalCoordinate;
        }
    }
}