using System;
using System.Data;

class Program
{
    public delegate Boolean MyDelegate(int element);

    public static Boolean DelegateMethod(int element)
    {
        if (element >= 0)
            return true;
        else
            return false;
    }

    public static List<int> Filter(MyDelegate Conditie, List<int> lista)
    {
        List<int> result = new List<int>();
        foreach (int element in lista)
        {
            if (Conditie(element) == true)
                result.Add(element);
        }
        return result;
    }

    public static void PrintList(List<int> lista)
    {
        if (lista.Count == 0)
        {
            throw new ArgumentNullException("Lista e goala");
        }
        foreach (int element in lista)
        {
            Console.Write(element + " ");
        }
        Console.WriteLine();
    }
    
    public delegate float MyDelegate1(float x);
    public static float f(float x)
    {
        return x*3;
    }

    public static float g(float x)
    {
        return x-2;
    }
    public static float FGX(MyDelegate1 del1, MyDelegate1 del2, float x)
    {
        //functii compuse in x, valoarea este f(g(x))
        return del1(del2(x));
    }

    
    //nu trebuie cu delegates, si trebuie returnata tot o functie, nu o valoare, ATENTIE!!
    public static int a(int y)
    {
        return y*3;
    }

    public static int b(int y)
    {
        return y-2;
    }

    //scriem Func<int,int> , echivalentul fiind int Func(int x) !!!!!
    public static Func<int,int> Comp(Func<int,int> del3, Func<int,int> del4)
    {
        //lambda expression, functie anonima cu un parametru y 
        //SINTAXA LAMBDA EXPRESSION: parametru=>functie !!!!!
        return y=>del3(del4(y));
    }

    static void Main(string[] args)
    {
        //ex1
        Console.WriteLine("////////////EXERCITIUL 1");
        MyDelegate Conditie = DelegateMethod;
        List<int> lista = new List<int>();
        lista.Add(1);
        lista.Add(-1);
        lista.Add(2);
        lista.Add(-2);
        lista.Add(-3);
        lista.Add(3);
        Console.Write("Lista initiala: ");
        PrintList(lista);
        List<int> result = Filter(Conditie, lista);
        Console.Write("Lista obtinuta: ");
        PrintList(result);

        //ex2
        Console.WriteLine();
        Console.WriteLine("////////////EXERCITIUL 2");
        MyDelegate1 del1=f;
        MyDelegate1 del2=g;
        float x=5;
        Console.WriteLine("Rezultat obtinut: "+FGX(del1,del2,x));

        //ex3
        Console.WriteLine();
        Console.WriteLine("////////////EXERCITIUL 3");
        Func<int,int> compus=Comp(a,b);
        int y=5;
        int rezultat=compus(y);
        Console.WriteLine("Rezultat obtinut: "+rezultat);
    }
}