namespace Payscrow.MarketPlace.Application.Domain.Enumerations
{
    public enum PaymentType
    {
        Credit = 5,
        Debit = 10
    }

    public enum TransactionPaymentStatus
    {
        Pending = 5,
        Paid = 10
    }

    public enum ChargeMethod
    {
        Percentage = 5,
        FixedRate = 10,
        Combination = 15
    }
}
