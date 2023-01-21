using Backups.Exceptions;
using Banks.Models;

namespace Banks.Entities.Account;

public class DepositAccount : IAccount
{
    private const int MinAccountPeriod = 0;
    private const decimal MinPaymentValue = 0;
    private const decimal MinPaymentCommission = 0;
    private const decimal LowBalance = 50000;
    private const decimal MiddleBalance = 100000;
    private const int CountDays = 365;
    private const int PeriodLength = 30;
    private const int CommonValue = 0;
    private const decimal MinInterestRate = 0;
    private const int MinAccountNumber = 0;
    private static readonly decimal InterestRateUp = Convert.ToDecimal(0.5);
    private decimal _interestRate;
    private decimal _income = CommonValue;
    private decimal _paymentSum = CommonValue;
    private decimal _balance = CommonValue;
    private int _tempDays = CommonValue;
    private int _accountPeriod = MinAccountPeriod;
    private bool _doubtful;
    private List<Transaction> _transactions;
    public DepositAccount(int number, Client client, decimal interestRate, int accountPeriod)
    {
        if (number <= MinAccountNumber)
            throw new AccountNumberException($"Min number of account {MinAccountNumber}, your number {number}");
        if (interestRate <= MinInterestRate)
            throw new Exception();
        if (accountPeriod < MinAccountPeriod || ((accountPeriod % 30) != 0))
            throw new Exception();
        Number = number;
        Client = client;
        if (Client.GetPassport() != 0)
            _doubtful = true;
        _interestRate = interestRate;
        _accountPeriod = accountPeriod;
        _transactions = new List<Transaction>();
    }

    public string Type => nameof(DepositAccount);
    public int Number { get; }
    public Client Client { get; }

    public decimal DailyIncome()
    {
        return _balance switch
        {
            < LowBalance => _balance * (_interestRate / CountDays),
            < MiddleBalance => _balance * ((_interestRate + InterestRateUp) / CountDays),
            _ => _balance * ((_interestRate + InterestRateUp + InterestRateUp) / CountDays)
        };
    }

    public void AddIncome()
    {
        if (_accountPeriod <= 0) return;
        switch (_tempDays)
        {
            case < PeriodLength:
                _income += DailyIncome();
                _tempDays++;
                _accountPeriod--;
                return;
            case PeriodLength:
                _income += DailyIncome();
                _balance += _income;
                _income = 0;
                _tempDays = 0;
                _accountPeriod--;
                return;
        }
    }

    public Client GetClient(Client client)
    {
        return Client;
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
        if (_accountPeriod != 0)
            throw new Exception();
        decimal paymentAndCommission = payment + commission;
        if ((_balance - paymentAndCommission) < 0)
            throw new Exception();
        if (!_doubtful && ((_paymentSum > paymentLimit) || ((_paymentSum + paymentAndCommission) >= paymentLimit)))
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

    public void Prolongation(int prolongationPeriod)
    {
        if (_accountPeriod != 0)
            throw new Exception();
        if ((prolongationPeriod % PeriodLength) != 0)
            throw new Exception();
        _accountPeriod = prolongationPeriod;
    }

    public void ChangeTempPayments(decimal payment)
    {
        _paymentSum -= payment;
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