using Banks.Entities.Account;
using Banks.Exceptions;

namespace Banks.Entities;

public class Client : IEquatable<Client>
{
    private const long MinPassportValue = 1000000000;
    private long _passport;
    private string _name;
    private string _surname;
    private List<IAccount> _accounts;
    public Client(string name, string surname)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException(nameof(surname));
        _name = name;
        _surname = surname;
        _passport = 0;
        _accounts = new List<IAccount>();
    }

    public void SetNewName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentNullException(nameof(newName));
        _name = newName;
    }

    public void SetNewSurname(string newSurname)
    {
        if (string.IsNullOrWhiteSpace(newSurname))
            throw new ArgumentNullException(nameof(newSurname));
        _surname = newSurname;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetSurname()
    {
        return _surname;
    }

    public void SetPassport(long passportNumber)
    {
        if (passportNumber < MinPassportValue)
        {
            throw new PassportLengthException(
                $"Passport's number should nas {MinPassportValue} digits, your passport number is {passportNumber}");
        }

        _passport = passportNumber;
    }

    public long GetPassport()
    {
        return _passport;
    }

    public void AddAccount(IAccount newAccount)
    {
        if (_accounts.Contains(newAccount))
            throw new AddAccountException($"Account with name: {newAccount} and number: {newAccount} already exists");
        _accounts.Add(newAccount);
    }

    public IReadOnlyCollection<IAccount> GetClientAccounts()
    {
        return _accounts;
    }

    public bool Equals(Client other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _passport == other._passport && _name == other._name && _surname == other._surname;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Client)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_passport, _name, _surname);
    }
}