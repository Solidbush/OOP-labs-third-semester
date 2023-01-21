using Backups.Exceptions;
using Banks.Models;

namespace Banks.Entities.Account;

public class CreditAccount : IAccount
{
    private const decimal MinPaymentValue = 0;
    private const decimal MinPaymentCredit = 0;
    private const decimal MinPaymentCommission = 0;
    private const int CountDays = 365;
    private const int PeriodLength = 30;
    private const int CommonValue = 0;
    private const decimal MinInterestRate = 0;
    private const int MinAccountNumber = 0;
    private decimal _interestRate;
    private decimal _income = CommonValue;
    private decimal _paymentSum = CommonValue;
    private decimal _balance = CommonValue;
    private decimal _paymentCredit = CommonValue;
    private int _tempDays = CommonValue;
    private bool _doubtful;
    private List<Transaction> _transactions;

    public CreditAccount(int number, Client client, decimal interestRate, decimal paymentCredit)
    {
        if (number <= MinAccountNumber)
            throw new AccountNumberException($"Min number of account {MinAccountNumber}, you number: {number}");
        if (interestRate <= MinInterestRate)
            throw new Exception();
        if (paymentCredit < MinPaymentCredit)
            throw new Exception();
        Number = number;
        Client = client;
        if (Client.GetPassport() != 0)
            _doubtful = true;
        _interestRate = interestRate;
        _paymentCredit = paymentCredit;
        _transactions = new List<Transaction>();
    }

    public string Type => nameof(DebitAccount);
    public int Number { get; }
    public Client Client { get; }

    public decimal DailyIncome()
    {
        return -_balance * (_interestRate / CountDays);
    }

    public void AddIncome()
    {
        if (_balance >= CommonValue) return;
        switch (_tempDays)
        {
            case < PeriodLength:
                _income += DailyIncome();
                _tempDays++;
                return;
            case PeriodLength:
                _income += DailyIncome();
                _balance -= _income;
                _income = 0;
                _tempDays = 0;
                return;
        }
    }

    public Client GetClient(Client client)
    {
        return Client;
    }

    public void ChangeTempPayments(decimal payment)
    {
        _paymentSum -= payment;
    }

    public void AddTransaction(Transaction transaction)
    {
        if (_transactions.Contains(transaction))
            throw new Exception();
        _transactions.Add(transaction);
    }

    public void DeleteTransaction(Transaction transaction)
    {
        if (!_transactions.Contains(transaction))
            throw new Exception();
        _transactions.Remove(transaction);
    }

    public decimal GetBalance()
    {
        return _balance;
    }

    public void Payment(decimal payment, decimal commission, decimal paymentLimit)
    {
        if (payment < MinPaymentValue)
            throw new Exception();
        if (commission < MinPaymentCommission)
            throw new Exception();
        decimal paymentAndCommission = payment + commission;
        if ((!_doubtful && (_paymentSum > paymentLimit)) || ((_paymentSum + paymentAndCommission) >= paymentLimit) ||
            ((_balance - paymentAndCommission) < -_paymentCredit))
            throw new Exception();
        _balance -= paymentAndCommission;
        _paymentSum += payment;
    }

    public void Receipt(decimal receipt)
    {
        if (receipt < MinPaymentValue)
            throw new Exception();
        _balance += receipt;
    }

    public void ChangeClientStatus(bool newStatus)
    {
        _doubtful = newStatus;
    }

    public bool ReturnClientStatus()
    {
        return _doubtful;
    }

    public void ChangeInterestRate(decimal newInterestRate)
    {
        _interestRate = newInterestRate;
    }

    public IReadOnlyCollection<Transaction> GetTransactions()
    {
        return _transactions;
    }

    public bool Equals(IAccount other)
    {
        return Number == other.Number;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((DepositAccount)obj);
    }

    public override int GetHashCode()
    {
        return Number;
    }
}