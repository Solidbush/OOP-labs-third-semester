namespace Banks.Models;

public class PaymentCommissionPair
{
    public PaymentCommissionPair(decimal sum, decimal commission)
    {
        Sum = sum;
        Commission = commission;
    }

    public decimal Sum { get; }
    public decimal Commission { get; }
}