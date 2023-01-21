using Banks.Entities;
using Banks.Entities.Account;
using Banks.Models;

namespace Banks.Services;

public interface ICentralBank
{
    void AddClient(Client client);
    void AddClientInBank(Bank bank, Client client);
    void AddAccountInBank(Bank bank, IAccount account);
    void ChangeClientPassport(Client client, long newPassport);
    void DeleteAccount(Bank bank, IAccount account);
    void DeleteClient(Bank bank, Client client);
    void AddBank(Bank bank);
    Transaction MakeTransaction(BankAccountPair sender, BankAccountPair recipient, decimal sum);
    void CancelTransaction(Transaction transaction);
    void ChangeInterestForCreditAccount(Bank bank, decimal newInterest);
    void ChangeInterestForDebitAccount(Bank bank, decimal newInterest);
    void ChangeInterestForDepositAccount(Bank bank, decimal newInterest);
    void TimeToFuture(int daysToFuture);
    IAccount GetAccount(Bank bank, IAccount account);
}