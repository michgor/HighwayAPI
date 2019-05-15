namespace Highway.DAL.DomainModels.TollStations
{
    public enum GatePaymentOption
    {
        Cash,

        Card,

        // this can be ViaToll, a4Go or any other system which supports prepaid transactions
        PrePaid
    }
}