using Banks.Entities.Account;

namespace Banks.Entities;

public class Bank : IEquatable<Bank>
{
    private const decimal MinPaymentLimit = 0;
    private const decimal MinCommission = 0;
    private const int MinBankNumber = 0;
    private List<Client> _clients;
    private List<IAccount> _accounts;
    private List<Client> _mailingList;
    private decimal _paymentLimit;
    private decimal _commission;

    public Bank(string name, int number, decimal paymentLimit, decimal commission)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        if (paymentLimit < MinPaymentLimit)
            throw new Exception();
        if (number < MinBankNumber)
            throw new Exception();
        if (commission < MinCommission)
            throw new Exception();
        Name = name;
        Number = number;
        _paymentLimit = paymentLimit;
        _clients = new List<Client>();
        _accounts = new List<IAccount>();
        _mailingList = new List<Client>();
        _commission = commission;
    }

    public string Name { get; }
    public int Number { get; }

    public bool ContainsClient(Client client)
    {
        return _clients.Contains(client);
    }

    public bool ContainsAccount(IAccount account)
    {
        return _accounts.Contains(account);
    }

    public bool ContainsClientInMailingList(Client client)
    {
        return _mailingList.Contains(client);
    }

    public void ChangePaymentLimit(decimal newPaymentLimit)
    {
        if (newPaymentLimit < MinPaymentLimit)
            throw new Exception();
        _paymentLimit = newPaymentLimit;
    }

    public void ChangeClientPassport(Client client, long newPassport)
    {
        if (_clients.Contains(client))
        {
            foreach (var account in _accounts)
            {
                if (account.Client != client) continue;
                account.Client.SetPassport(newPassport);
                account.ChangeClientStatus(true);
            }

            Client tempClient = client;
            _clients.Remove(client);
            tempClient.SetPassport(newPassport);
            _clients.Add(tempClient);
            if (_mailingList.Contains(client))
            {
                _mailingList.Remove(client);
                _mailingList.Add(tempClient);
            }
        }
    }

    public void AddClient(Client client)
    {
        if (!ContainsClient(client))
            _clients.Add(client);
    }

    public void DeleteClient(Client client)
    {
        if (_clients.Contains(client))
        {
            foreach (var account in _accounts)
            {
                if (account.Client == client)
                    _accounts.Remove(account);
            }

            _clients.Remove(client);
        }
    }

    public void AddClientInMailingList(Client client)
    {
        if (!ContainsClientInMailingList(client))
            _mailingList.Add(client);
    }

    public void AddAccount(IAccount account)
    {
        if (!ContainsAccount(account))
            _accounts.Add(account);
    }

    public void DeleteAccount(IAccount account)
    {
        if (ContainsAccount(account))
            _accounts.Remove(account);
    }

    public decimal GetCommission()
    {
        return _commission;
    }

    public decimal GetPaymentLimit()
    {
        return _paymentLimit;
    }

    public IReadOnlyCollection<IAccount> GetAccounts()
    {
        return _accounts;
    }

    public IReadOnlyCollection<Client> GetClients()
    {
        return _clients;
    }

    public IReadOnlyCollection<Client> GetMailingList()
    {
        return _mailingList;
    }

    public void PayInterests()
    {
        foreach (var account in _accounts)
        {
            account.AddIncome();
        }
    }

    public bool Equals(Bank other)
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
        return Equals((Bank)obj);
    }

    public override int GetHashCode()
    {
        return Number;
    }
}