using Banks.Entities;
using Banks.Entities.Account;
using Banks.Models;

namespace Banks.Services;

public class CentralBank : ICentralBank
{
    private List<Bank> _banks;
    private List<Transaction> _transactions;
    private List<Client> _clients;

    public CentralBank()
    {
        _banks = new List<Bank>();
        _transactions = new List<Transaction>();
        _clients = new List<Client>();
    }

    public delegate void CentralBankHandler(string message);
    public event CentralBankHandler Notify;

    public void DisplayMessage(string message) => Console.WriteLine(message);
    public void DisplayRedMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void AddClient(Client client)
    {
        if (!_clients.Contains(client))
            _clients.Add(client);
    }

    public void AddClientInBank(Bank bank, Client client)
    {
        if (!_banks.Contains(bank)) return;
        if (_clients.Contains(client))
        {
            bank.AddClient(client);
        }
    }

    public void AddAccountInBank(Bank bank, IAccount account)
    {
        if (!_banks.Contains(bank)) return;
        if (_clients.Contains(account.Client))
            bank.AddAccount(account);
    }

    public void ChangeClientPassport(Client client, long newPassport)
    {
        if (_clients.Contains(client))
        {
            Client tempClient = client;
            foreach (var bank in _banks)
            {
                bank.ChangeClientPassport(client, newPassport);
            }

            _clients.Remove(client);
            tempClient.SetPassport(newPassport);
            _clients.Add(tempClient);
        }
    }

    public void DeleteAccount(Bank bank, IAccount account)
    {
        if (_banks.Contains(bank))
        {
            bank.DeleteAccount(account);
        }
    }

    public IAccount GetAccount(Bank bank, IAccount account)
    {
        if (_banks.Contains(bank))
            if (bank.ContainsAccount(account)) return account;
        return null;
    }

    public void DeleteClient(Bank bank, Client client)
    {
        if (_banks.Contains(bank))
        {
            if (_clients.Contains(client))
            {
                bank.DeleteClient(client);
            }
        }
    }

    public void AddBank(Bank bank)
    {
        if (!_banks.Contains(bank))
            _banks.Add(bank);
    }

    public Transaction MakeTransaction(BankAccountPair sender, BankAccountPair recipient, decimal sum)
    {
        var rnd = new Random();
        int transactionNumber = rnd.Next(0, int.MaxValue);
        var tempTransaction =
            new Transaction(transactionNumber, sender, recipient, sum, sender.GetBank().GetCommission());
        while (_transactions.Contains(tempTransaction))
        {
            transactionNumber = rnd.Next(0, int.MaxValue);
            tempTransaction =
                new Transaction(transactionNumber, sender, recipient, sum, sender.GetBank().GetCommission());
        }

        if (_banks.Contains(sender.GetBank()) && _banks.Contains(recipient.GetBank()))
        {
            if (_clients.Contains(sender.GetAccount().Client) && _clients.Contains(recipient.GetAccount().Client))
            {
                if (sender.GetBank().ContainsAccount(sender.GetAccount()) &&
                    sender.GetBank().ContainsAccount(sender.GetAccount()))
                {
                    sender.GetAccount().Payment(tempTransaction.Sum, tempTransaction.Commission, sender.GetBank().GetPaymentLimit());
                    recipient.GetAccount().Receipt(tempTransaction.Sum);
                    if (sender.GetAccount().GetTransactions().Contains(tempTransaction))
                        sender.GetAccount().DeleteTransaction(tempTransaction);
                    if (recipient.GetAccount().GetTransactions().Contains(tempTransaction))
                        recipient.GetAccount().DeleteTransaction(tempTransaction);
                    sender.GetAccount().AddTransaction(tempTransaction);
                    recipient.GetAccount().AddTransaction(tempTransaction);
                    _transactions.Add(tempTransaction);
                }
            }
        }

        return tempTransaction;
    }

    public void CancelTransaction(Transaction transaction)
    {
        if (_transactions.Contains(transaction))
        {
            transaction.GetSender().GetAccount().Receipt(transaction.Sum + transaction.Commission);
            transaction.GetSender().GetAccount().ChangeTempPayments(transaction.Sum + transaction.Commission);
            transaction.GetRecipient().GetAccount().Payment(transaction.Sum, 0, decimal.MaxValue);
            transaction.GetRecipient().GetAccount().ChangeTempPayments(transaction.Sum);
            if (transaction.GetSender().GetAccount().GetTransactions().Contains(transaction))
                transaction.GetSender().GetAccount().DeleteTransaction(transaction);
            if (transaction.GetRecipient().GetAccount().GetTransactions().Contains(transaction))
                transaction.GetRecipient().GetAccount().DeleteTransaction(transaction);
            _transactions.Remove(transaction);
        }
    }

    public void ChangeInterestForCreditAccount(Bank bank, decimal newInterest)
    {
        if (_banks.Contains(bank))
        {
            foreach (var account in bank.GetAccounts())
            {
                if (account.Type is nameof(CreditAccount))
                    account.ChangeInterestRate(newInterest);
            }
        }
    }

    public void ChangeInterestForDebitAccount(Bank bank, decimal newInterest)
    {
        if (_banks.Contains(bank))
        {
            foreach (var account in bank.GetAccounts())
            {
                if (account.Type is nameof(DebitAccount))
                    account.ChangeInterestRate(newInterest);
            }
        }
    }

    public void ChangeInterestForDepositAccount(Bank bank, decimal newInterest)
    {
        if (_banks.Contains(bank))
        {
            foreach (var account in bank.GetAccounts())
            {
                if (account.Type is nameof(DepositAccount))
                {
                    account.ChangeInterestRate(newInterest);
                    Notify += DisplayRedMessage;
                    Console.WriteLine($"Interest rate for Deposit Accounts in bank {bank.Name} {bank.Number} changed, new interest rate: {newInterest}");
                    Notify -= DisplayRedMessage;
                    Notify += DisplayMessage;
                    if (bank.ContainsClientInMailingList(account.Client))
                    {
                        Console.WriteLine(
                            $"Hello {account.Client.GetName()} {account.Client.GetSurname()} interest rate for your account {account.Type} {account.Number} has been changed, new interest rate {newInterest}");
                    }
                }
            }
        }
    }

    public void TimeToFuture(int daysToFuture)
    {
        for (int i = 0; i <= daysToFuture; i++)
        {
            foreach (Bank bank in _banks)
            {
                bank.PayInterests();
            }
        }
    }

    public IReadOnlyCollection<Bank> GetBanks()
    {
        return _banks;
    }

    public IReadOnlyCollection<Transaction> GetTransactions()
    {
        return _transactions;
    }

    public IReadOnlyCollection<Client> GetClients()
    {
        return _clients;
    }

    protected virtual void OnNotify(string message)
    {
        Notify?.Invoke(message);
    }
}