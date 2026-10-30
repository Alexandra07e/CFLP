using System;
using System.Security.AccessControl;

class Ex1
{
    public static void swap(ref int a, ref int b)
    {
        int aux = a;
        a = b;
        b = aux;
    }
}

public struct Coords
{
    public float X { get; set; }
    public float Y { get; set; }

    public Coords(float x, float y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}
class Ex2
{
    public static Coords Middle(Coords A, Coords B)
    {
        float Xmij = (A.X + B.X) / 2;
        float Ymij = (A.Y + B.Y) / 2;
        return new Coords(Xmij, Ymij);
    }
}

class Account
{
    private string Name;
    private double Balance;

    public Account(string name, double balance)
    {
        Name = name;
        Balance = balance;
    }
    
    public bool Withdraw(double amount,out double remainingBalance)
    {
        if (Balance < amount)
        {
            Console.WriteLine("Suma incorecta!");
            remainingBalance = Balance;
            return false;
        }

        remainingBalance = Balance - amount;
        return true;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        //ex1
        int a = 5;
        int b = 7;
        Ex1.swap(ref a, ref b);
        Console.WriteLine("Ex1: a = " + a + " b = " + b);

        //ex2
        Coords point1 = new Coords(2, 5);
        Coords point2 = new Coords(4, 5);
        Coords mij = Ex2.Middle(point1, point2);
        Console.Write("Ex 2: ");
        Console.WriteLine(mij.ToString());

        //ex3
        Account acc = new Account("Alexandra", 123.5);
        if(acc.Withdraw(120, out double remainingBalance) == true)
        {
            Console.WriteLine("Ex3: Suma ramasa= " + remainingBalance);
        }

    }
}