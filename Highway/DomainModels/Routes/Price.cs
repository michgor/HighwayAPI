using System;

namespace Highway.DAL.DomainModels.Routes
{
    public class Price
    {
        public decimal Value { get; }

        public DateTime ValidFrom { get; }

        public DateTime ValidTo { get; }

        public Price(
            decimal price,
            DateTime validFrom,
            DateTime validTo)
        {
            this.Value = price;
            this.ValidFrom = validFrom;
            this.ValidTo = validTo;
        }
    }
}
