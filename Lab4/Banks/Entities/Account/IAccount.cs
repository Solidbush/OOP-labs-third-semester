using Banks.Models;

namespace Banks.Entities.Account;

public interface IAccount : IEquatable<IAccount>
{
    string Type { get; }
    int Number { get; }
    Client Client { get; }
    decimal DailyIncome();
    void AddIncome();
    Client GetClient(Client client);
    void AddTransaction(Transaction transaction);
    void DeleteTransaction(Transaction transaction);
    decimal GetBalance();
    void Payment(decimal payment, decimal commission, decimal paymentLimit);
    void Receipt(decimal receipt);
    void ChangeClientStatus(bool newStatus);
    void ChangeInterestRate(decimal newInterestRate);
    void ChangeTempPayments(decimal payment);
    IReadOnlyCollection<Transaction> GetTransactions();
    bool ReturnClientStatus();
}