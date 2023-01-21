using Banks.Entities;
using Banks.Entities.Account;

namespace Banks.Models;

public class BankAccountPair
{
    private readonly Bank _bank;
    private readonly IAccount _account;

    public BankAccountPair(Bank bank, IAccount account)
    {
        _bank = bank;
        _account = account;
    }

    public Bank GetBank()
    {
        return _bank;
    }

    public IAccount GetAccount()
    {
        return _account;
    }
}