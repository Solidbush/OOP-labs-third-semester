using Banks.Entities;
using Banks.Entities.Account;
using Banks.Models;
using Banks.Services;
using Xunit;

namespace Banks.Test;

public class Banks
{
    private readonly CentralBank _centralBank;
    public Banks()
    {
        _centralBank = new CentralBank();
    }

    [Fact]
    public void AddFreeClients_CentralBankContainsFreeClients()
    {
        var firstClient = new Client("Alex", "Gavrilov");
        var secondClient = new Client("Egor", "Belyacov");
        var thirdClient = new Client("Danil", "Muzikus");
        _centralBank.AddClient(firstClient);
        _centralBank.AddClient(secondClient);
        _centralBank.AddClient(thirdClient);

        Assert.Contains(new Client("Alex", "Gavrilov"), _centralBank.GetClients());
        Assert.Contains(new Client("Egor", "Belyacov"), _centralBank.GetClients());
        Assert.Contains(new Client("Danil", "Muzikus"), _centralBank.GetClients());
    }

    [Fact]
    public void AddBankInCentralBank_CentralBankContainsBank()
    {
        Bank bank = new Bank("Alfa-Bank", 123456, 50000, 0.001m);
        _centralBank.AddBank(bank);
        Assert.Contains(new Bank("Alfa-Bank", 123456, 50000, 0.001m), _centralBank.GetBanks());
    }

    [Fact]
    public void AddFreeAccountsInBank_BankContainsAccounts()
    {
        var firstClient = new Client("Alex", "Gavrilov");
        var secondClient = new Client("Egor", "Belyacov");
        var thirdClient = new Client("Danil", "Muzikus");
        _centralBank.AddClient(firstClient);
        _centralBank.AddClient(secondClient);
        _centralBank.AddClient(thirdClient);
        Bank bank = new Bank("Alfa-Bank", 123456, 50000, 0.001m);
        IAccount debitAccount = new DebitAccount(123456, firstClient, 0.001m);
        IAccount creditAccount = new CreditAccount(23456, secondClient, 3.5m, 50000);
        IAccount depositAccount = new DepositAccount(45678, thirdClient, 4.5m, 90);
        _centralBank.AddBank(bank);
        _centralBank.AddAccountInBank(bank, debitAccount);
        _centralBank.AddAccountInBank(bank, creditAccount);
        _centralBank.AddAccountInBank(bank, depositAccount);

        Assert.Contains(debitAccount, bank.GetAccounts());
        Assert.Contains(creditAccount, bank.GetAccounts());
        Assert.Contains(depositAccount, bank.GetAccounts());
    }

    [Fact]
    public void AddAccountAndChangeClientPassport_DoubtfulChanged()
    {
        var firstClient = new Client("Alex", "Gavrilov");
        _centralBank.AddClient(firstClient);
        Bank bank = new Bank("Alfa-Bank", 123456, 50000, 0.001m);
        IAccount debitAccount = new DebitAccount(123456, firstClient, 0.001m);
        _centralBank.AddBank(bank);
        _centralBank.AddAccountInBank(bank, debitAccount);
        _centralBank.ChangeClientPassport(firstClient, 1234567891);

        Assert.Equal(1234567891, _centralBank.GetAccount(bank, debitAccount).Client.GetPassport());
    }

    [Fact]
    public void AddDebitAndCreditAccountsMakeTransaction_BalancesChanged()
    {
        var firstClient = new Client("Alex", "Gavrilov");
        var secondClient = new Client("Egor", "Belyacov");
        Bank bank = new Bank("Alfa-Bank", 123456, 50000, 50);
        _centralBank.AddClient(firstClient);
        _centralBank.AddClient(secondClient);
        IAccount debitAccount = new DebitAccount(123456, firstClient, 0.001m);
        IAccount creditAccount = new CreditAccount(23456, secondClient, 3.5m, 50000);
        _centralBank.AddBank(bank);
        _centralBank.AddAccountInBank(bank, debitAccount);
        _centralBank.AddAccountInBank(bank, creditAccount);
        decimal payment = 30000;
        _centralBank.ChangeClientPassport(secondClient, 1234567891);
        Assert.Equal(0, debitAccount.GetBalance());
        _centralBank.MakeTransaction(new BankAccountPair(bank, creditAccount), new BankAccountPair(bank, debitAccount), payment);
        Assert.Equal(payment, _centralBank.GetAccount(bank, debitAccount).GetBalance());
        Assert.Equal(-(payment + bank.GetCommission()), _centralBank.GetAccount(bank, creditAccount).GetBalance());
        Assert.Equal(1234567891, _centralBank.GetAccount(bank, creditAccount).Client.GetPassport());
    }

    [Fact]
    public void MakeTransactionCancelTransaction_BalancesChanged()
    {
        var firstClient = new Client("Alex", "Gavrilov");
        var secondClient = new Client("Egor", "Belyacov");
        Bank bank = new Bank("Alfa-Bank", 123456, 50000, 50);
        _centralBank.AddClient(firstClient);
        _centralBank.AddClient(secondClient);
        IAccount debitAccount = new DebitAccount(123456, firstClient, 0.001m);
        IAccount creditAccount = new CreditAccount(23456, secondClient, 3.5m, 50000);
        _centralBank.AddBank(bank);
        _centralBank.AddAccountInBank(bank, debitAccount);
        _centralBank.AddAccountInBank(bank, creditAccount);
        decimal payment = 30000;
        _centralBank.ChangeClientPassport(secondClient, 1234567891);
        Assert.Equal(0, debitAccount.GetBalance());
        var transaction = _centralBank.MakeTransaction(new BankAccountPair(bank, creditAccount), new BankAccountPair(bank, debitAccount), payment);
        Assert.Equal(payment, _centralBank.GetAccount(bank, debitAccount).GetBalance());
        Assert.Equal(-(payment + bank.GetCommission()), _centralBank.GetAccount(bank, creditAccount).GetBalance());
        Assert.Equal(1234567891, _centralBank.GetAccount(bank, creditAccount).Client.GetPassport());
        _centralBank.CancelTransaction(transaction);
        Assert.Equal(0, _centralBank.GetAccount(bank, debitAccount).GetBalance());
        Assert.Equal(0, _centralBank.GetAccount(bank, creditAccount).GetBalance());
        Assert.Empty(_centralBank.GetAccount(bank, creditAccount).GetTransactions());
        Assert.Empty(_centralBank.GetTransactions());
    }

    [Fact]
    public void CreatTwoAccountsMakeTimeToFutureOnThirtyDays_BalancesChanged()
    {
        var firstClient = new Client("Alex", "Gavrilov");
        var secondClient = new Client("Egor", "Belyacov");
        Bank bank = new Bank("Alfa-Bank", 123456, 50000, 50);
        _centralBank.AddClient(firstClient);
        _centralBank.AddClient(secondClient);
        IAccount debitAccount = new DebitAccount(123456, firstClient, 0.001m);
        IAccount creditAccount = new CreditAccount(23456, secondClient, 3.5m, 50000);
        _centralBank.AddBank(bank);
        _centralBank.AddAccountInBank(bank, debitAccount);
        _centralBank.AddAccountInBank(bank, creditAccount);
        decimal payment = 30000;
        _centralBank.ChangeClientPassport(secondClient, 1234567891);
        Assert.Equal(0, debitAccount.GetBalance());
        _centralBank.MakeTransaction(new BankAccountPair(bank, creditAccount), new BankAccountPair(bank, debitAccount), payment);
        Assert.Equal(payment, _centralBank.GetAccount(bank, debitAccount).GetBalance());
        Assert.Equal(-(payment + bank.GetCommission()), _centralBank.GetAccount(bank, creditAccount).GetBalance());
        _centralBank.TimeToFuture(30);
        Assert.Equal(30002.547945205479452054794518m, _centralBank.GetAccount(bank, debitAccount).GetBalance());
        Assert.Equal(-38982.671232876712328767123297m, _centralBank.GetAccount(bank, creditAccount).GetBalance());
    }
}