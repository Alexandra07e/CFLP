using System;
using System.Security.AccessControl;
using System.Security.Principal;


class Bank
{
    public readonly string Name;
    public readonly string Swift;
    public readonly List<Account> array = new List<Account>();

    public Bank(string name, string swift)
    {
        Name = name;
        Swift = swift;
    }

    public Boolean deschidereCont(Account acc)
    {
        //iban unic!
        foreach (Account c in array)
        {
            if (c.Iban.Equals(acc.Iban))
            {
                Console.WriteLine("Contul exista deja!");
                return false;
            }
        }
        array.Add(acc);
        return true;
    }

    public void cautareIban(string iban)
    {
        foreach (Account acc in array)
        {
            if (acc.Iban.Equals(iban))
            {
                Console.Write("Cont gasit -> ");
                Console.WriteLine(acc.toString());
                return;
            }
        }
        Console.WriteLine("Nu s-a gasit contul");
    }

    public void depunereNumerarBanca(double sum, string iban)
    {
        foreach(Account acc in array)
        {
            if (acc.Iban.Equals(iban))
            {
                acc.depunereNumerar(sum);
                return;
            }
        }
    }

    public void retragereNumerarBanca(double sum, string iban)
    {
        foreach(Account acc in array)
        {
            if (acc.Iban.Equals(iban))
            {
                acc.retragereNumerar(sum);
                return;
            }
        }
    }

    public void transfer(string iban_sender, string iban_receiver, double sum)
    {
        if (iban_sender.Equals(iban_receiver))
        {
            Console.WriteLine("E acelasi cont");
            return;
        }

        foreach (Account acc in array)
        {
            if (acc.Iban.Equals(iban_sender))
                acc.retragereNumerar(sum);
            if (acc.Iban.Equals(iban_receiver))
                acc.depunereNumerar(sum);
        }
    }

    public void afisare()
    {
        foreach(Account acc in array)
        {
            Console.WriteLine(acc.toString());
        }
    }
}

public enum AccountType
{
    person,
    company
}
class Account
{
    public readonly string AccountHolder;
    public readonly string Iban;
    public double Amount;
    public readonly AccountType Type;

    public Account(string name, string iban, double sum, AccountType type)
    {
        AccountHolder = name;
        Iban = iban;
        Amount = sum;
        Type = type;
    }

    public Boolean depunereNumerar(double sum)
    {
        if (sum <= 0)
        {
            Console.WriteLine("Suma e negativa, ups");
            return false;
        }
        this.Amount += sum;
        Console.WriteLine("Suma depusa");
        return true;
    }

    public Boolean retragereNumerar(double sum)
    {
        if (sum <= 0 || this.Amount<sum)
        {
            Console.WriteLine("Nu se poate");
            return false;
        }
        this.Amount -= sum;
        Console.WriteLine("Suma retrasa");
        return true;
    }

    public string toString()
    {
        return "Account holder: " + this.AccountHolder + ", Account type: " + this.Type + ", Iban: " + this.Iban + ", Amount: " + this.Amount;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Account acc1 = new Account("AC001", "ROBRD876317517", 400, AccountType.person);
        Account acc2 = new Account("AC002", "ROBCR673652176", 300, AccountType.company);
        Account acc3 = new Account("AC003", "ROBT8761324251", 800,AccountType.person);
        Account acc4 = new Account("AC004", "REVO8543298623", 900, AccountType.company);

        Bank bank1 = new Bank("Alexandra", "BNCFROBUXXX");

        bank1.deschidereCont(acc1);
        bank1.deschidereCont(acc2);
        bank1.deschidereCont(acc3);
        bank1.deschidereCont(acc4);

        Console.Write("Cautare IBAN corect: ");
        bank1.cautareIban("ROBT8761324251");
        Console.Write("Cautare IBAN incorect: ");
        bank1.cautareIban("RO72762176387");
        Console.Write("Depunere in cont: ");
        bank1.depunereNumerarBanca(100, "ROBRD876317517");
        Console.Write("Retragere din cont: ");
        bank1.retragereNumerarBanca(100, "REVO8543298623");
        Console.Write("Transfer intre 2 conturi: ");
        bank1.transfer("ROBCR673652176", "ROBT8761324251", 200);
        Console.WriteLine("\nAfisarea tuturor conturilor: ");
        bank1.afisare();

    }
}
