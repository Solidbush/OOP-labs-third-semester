using Banks.Entities;
using Banks.Entities.Account;
using Banks.Models;
using Banks.Services;

var centralBank = new CentralBank();

void PrintMune()
{
    Console.WriteLine("1: Add new Client");
    Console.WriteLine("2: Add new Bank");
    Console.WriteLine("3: Add new Account");
    Console.WriteLine("4: Print all active Clients");
    Console.WriteLine("5: Print all active Banks");
    Console.WriteLine("6: Print all Active Accounts");
    Console.WriteLine("7: Change Client Passport");
    Console.WriteLine("8 Print all transactions");
    Console.WriteLine("9: Make transaction");
    Console.WriteLine("10: Cancel transaction");
    Console.WriteLine("11: Change interest for bank");
    Console.WriteLine("12: Time to future");
}

void PrintAllTransaction()
{
    int number = 0;
    foreach (var transaction in centralBank.GetTransactions())
    {
        Console.WriteLine($"{number}. {transaction.Number} {transaction.GetSender().GetBank().Name} {transaction.GetRecipient().GetBank().Name}");
        number++;
    }

    Console.WriteLine("Finished!");
}

void PrintAccountsInBank(int bankNumber)
{
    int number = 0;
    foreach (var account in centralBank.GetBanks().ToList()[bankNumber].GetAccounts())
    {
        Console.WriteLine(
            $"{number} {account.Client.GetName()} {account.Client.GetSurname()} {account.Type} {account.Number}");
        number++;
    }

    Console.WriteLine("Finished");
}

void MakeTransaction()
{
    int bankNumber;
    int accountNumber;
    PrintActiveBanks();
    Console.WriteLine("Input number of sender bank: ");
    bankNumber = int.Parse(Console.ReadLine() ?? string.Empty);
    PrintAccountsInBank(bankNumber);
    Console.WriteLine("Input number of sender account: ");
    accountNumber = int.Parse(Console.ReadLine() ?? string.Empty);
    var sender = new BankAccountPair(
        centralBank.GetBanks().ToList()[bankNumber],
        centralBank.GetBanks().ToList()[bankNumber].GetAccounts().ToList()[accountNumber]);
    PrintActiveBanks();
    Console.WriteLine("Input number of recipient bank: ");
    bankNumber = int.Parse(Console.ReadLine() ?? string.Empty);
    PrintAccountsInBank(bankNumber);
    Console.WriteLine("Input number of recipient account: ");
    accountNumber = int.Parse(Console.ReadLine() ?? string.Empty);
    var resipient = new BankAccountPair(
        centralBank.GetBanks().ToList()[bankNumber],
        centralBank.GetBanks().ToList()[bankNumber].GetAccounts().ToList()[accountNumber]);
    decimal payment;
    Console.WriteLine("Input transaction payment: ");
    payment = decimal.Parse(Console.ReadLine());
    centralBank.MakeTransaction(sender, resipient, payment);
}

void ChangeClientPassport()
{
    PrintActiveClients();
    Console.WriteLine("Input client number");
    int clientNumber;
    clientNumber = int.Parse(Console.ReadLine() ?? string.Empty);
    long newPassport;
    Console.WriteLine("Input new client's passport");
    newPassport = long.Parse(Console.ReadLine() ?? string.Empty);
    centralBank.ChangeClientPassport(centralBank.GetClients().ToList()[clientNumber], newPassport);
}

void AddNewClient()
{
    Console.WriteLine("Input Client's name: ");
    string name = Console.ReadLine();
    Console.WriteLine("Input Client's surname: ");
    string surname = Console.ReadLine();
    Client tempClient = new Client(name, surname);
    centralBank.AddClient(tempClient);
    Console.WriteLine($"Client {name} {surname} added!");
}

void PrintActiveClients()
{
    int counter = 0;
    foreach (var client in centralBank.GetClients())
    {
        Console.WriteLine($"{counter}. {client.GetName()} {client.GetSurname()} {client.GetPassport()}");
        counter++;
    }

    Console.WriteLine("Finished");
}

void AddNewBank()
{
    Console.WriteLine("Input Bank's name: ");
    string name = Console.ReadLine();
    Console.WriteLine("Input bank's number: ");
    int number = int.Parse(Console.ReadLine() ?? string.Empty);
    Console.WriteLine("Input payment limit for bank: ");
    decimal paymentLimit = decimal.Parse(Console.ReadLine() ?? string.Empty);
    Console.WriteLine("Input commission for bank: ");
    decimal commission = decimal.Parse(Console.ReadLine() ?? string.Empty);
    var tempBank = new Bank(name, number, paymentLimit, commission);
    centralBank.AddBank(tempBank);
    Console.WriteLine($"Added bank {name} {number} {paymentLimit} {commission}");
}

void PrintActiveBanks()
{
    int bankNumber = 0;
    foreach (var bank in centralBank.GetBanks())
    {
        Console.WriteLine($"{bankNumber} {bank.Name} {bank.Number}");
        bankNumber++;
    }

    Console.WriteLine("Finished!");
}

void AddNewAccount()
{
    PrintActiveBanks();
    Console.WriteLine("Input number of bank in list: ");
    int bankNumber = int.Parse(Console.ReadLine());
    Console.WriteLine("Input  account type:");
    string accountType = Console.ReadLine();
    int number;
    int clientNumber;
    decimal interestRate;
    decimal paymentCredit;
    int accountPeriod;
    switch (accountType)
    {
        case "credit":
            Console.WriteLine("Input account number: ");
            number = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Input client number: ");
            PrintActiveClients();
            clientNumber = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Input account interest rate: ");
            interestRate = decimal.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Input account payment credit: ");
            paymentCredit = decimal.Parse(Console.ReadLine() ?? string.Empty);
            var creditAccount = new CreditAccount(number, centralBank.GetClients().ToList()[clientNumber], interestRate, paymentCredit);
            centralBank.AddAccountInBank(centralBank.GetBanks().ToList()[bankNumber], creditAccount);
            break;
        case "debit":
            Console.WriteLine("Input account number: ");
            number = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Input client number: ");
            clientNumber = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Input account interest rate: ");
            interestRate = decimal.Parse(Console.ReadLine() ?? string.Empty);
            var debitAccount = new DebitAccount(number, centralBank.GetClients().ToList()[clientNumber], interestRate);
            centralBank.AddAccountInBank(centralBank.GetBanks().ToList()[bankNumber], debitAccount);
            break;
        default:
            Console.WriteLine("Input account number: ");
            number = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Input client number: ");
            clientNumber = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Input account interest rate: ");
            interestRate = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Input account period: ");
            accountPeriod = int.Parse(Console.ReadLine() ?? string.Empty);
            var depositAccount = new DepositAccount(number, centralBank.GetClients().ToList()[clientNumber], interestRate, accountPeriod);
            centralBank.AddAccountInBank(centralBank.GetBanks().ToList()[bankNumber], depositAccount);
            break;
    }
}

void CancelTransaction()
{
    PrintAllTransaction();
    Console.WriteLine("Input number of transaction: ");
    int number;
    number = int.Parse(Console.ReadLine() ?? string.Empty);
    centralBank.CancelTransaction(centralBank.GetTransactions().ToList()[number]);
}

void DayToFuture()
{
    Console.WriteLine("Input days to future: ");
    int days = int.Parse(Console.ReadLine() ?? string.Empty);
    centralBank.TimeToFuture(days);
}

void AllActiveAccounts()
{
    int number = 0;
    foreach (var bank in centralBank.GetBanks())
    {
        foreach (var account in bank.GetAccounts())
        {
            if (centralBank.GetClients().Contains(account.Client))
                Console.WriteLine($"{number}. {account.Client.GetName()} {account.Client.GetSurname()} {account.Number} {account.Type} {account.GetBalance()}");
        }

        number++;
    }

    Console.WriteLine("Finished!");
}

void ChangeInterestForBank()
{
    int bankNumber;
    decimal newIntrest;
    string accountType;
    PrintActiveBanks();
    Console.WriteLine("Input number of bank: ");
    bankNumber = int.Parse(Console.ReadLine() ?? string.Empty);
    Console.WriteLine("Input new interest for bank: ");
    newIntrest = decimal.Parse(Console.ReadLine() ?? string.Empty);
    Console.WriteLine("Input type of account: ");
    accountType = Console.ReadLine();
    switch (accountType)
    {
        case "credit":
            centralBank.ChangeInterestForCreditAccount(centralBank.GetBanks().ToList()[bankNumber], newIntrest);
            break;
        case "debit":
            centralBank.ChangeInterestForDebitAccount(centralBank.GetBanks().ToList()[bankNumber], newIntrest);
            break;
        case "deposit":
            centralBank.ChangeInterestForDepositAccount(centralBank.GetBanks().ToList()[bankNumber], newIntrest);
            break;
    }
}

PrintMune();
while (true)
{
    Console.WriteLine("Input a command: ");
    int command = int.Parse(Console.ReadLine() ?? string.Empty);
    switch (command)
    {
        case 1:
            AddNewClient();
            break;
        case 2:
            AddNewBank();
            break;
        case 3:
            AddNewAccount();
            break;
        case 4:
            PrintActiveClients();
            break;
        case 5:
            PrintActiveBanks();
            break;
        case 6:
            AllActiveAccounts();
            break;
        case 7:
            ChangeClientPassport();
            break;
        case 8:
            PrintAllTransaction();
            break;
        case 9:
            MakeTransaction();
            break;
        case 10:
            CancelTransaction();
            break;
        case 11:
            ChangeInterestForBank();
            break;
        case 12:
            DayToFuture();
            break;
        default:
            PrintMune();
            break;
    }
}