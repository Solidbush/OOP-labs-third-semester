using Banks.Exceptions;

namespace Banks.Models;

public class Transaction : IEquatable<Transaction>
{
    private const int MinTransactionNumber = 0;
    private readonly BankAccountPair _sender;
    private readonly BankAccountPair _recipient;
    public Transaction(int number, BankAccountPair sender, BankAccountPair recipient, decimal sum, decimal commission)
    {
        if (number < MinTransactionNumber)
            throw new UnrealNumberException($"Min number: {MinTransactionNumber}. Your number: {number}");
        Number = number;
        _sender = sender;
        _recipient = recipient;
        Sum = sum;
        Commission = commission;
    }

    public int Number { get; }
    public decimal Sum { get; }
    public decimal Commission { get; }

    public BankAccountPair GetSender()
    {
        return _sender;
    }

    public BankAccountPair GetRecipient()
    {
        return _recipient;
    }

    public bool Equals(Transaction other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Number == other.Number;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Transaction)obj);
    }

    public override int GetHashCode()
    {
        return Number;
    }
}