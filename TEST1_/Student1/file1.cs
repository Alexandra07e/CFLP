using System;
using System.ComponentModel.DataAnnotations;
//STUDENT 1

abstract class Cadru_didactic
{
    public string nume{get; protected set;}
    public string prenume{get; protected set;}
    public int varsta{get;protected set;}

    public Cadru_didactic(string nume, string prenume, int varsta)
    {
        this.nume=nume;
        this.prenume=prenume;
        this.varsta=varsta;
    }

    public abstract void Salut();

    public override string ToString()
    {
        return nume+" "+prenume+" "+varsta+" ";
    }
}

class Cadru_didactic_profesor : Cadru_didactic
{
    public int nr_doctoranzi{get; protected set;}

    public Cadru_didactic_profesor(string nume, string prenume, int varsta, int nr_doctoranzi) : base(nume, prenume, varsta)
    {
        this.nr_doctoranzi=nr_doctoranzi;
    }

    public override void Salut()
    {
        Console.WriteLine("Salut, sunt Cadru_didactic_profesor");
    }

    public override string ToString()
    {
        return base.ToString()+nr_doctoranzi;
    }
}

class Cadru_didactic_conferentiar : Cadru_didactic
{
    public int nr_masteranzi{get; protected set;}

    public Cadru_didactic_conferentiar(string nume, string prenume, int varsta, int nr_masteranzi) : base(nume, prenume, varsta)
    {
        this.nr_masteranzi=nr_masteranzi;
    }

    public override void Salut()
    {
        Console.WriteLine("Salut, sunt Cadru_didactic_conferentiar");
    }

    public override string ToString()
    {
        return base.ToString()+nr_masteranzi;
    }
}

class Student1
{
    public static void Whiskey34(ref double a, ref double b)
    {
        if(a > 70 && b > 70)
        {
            double aux = a;
            a = b;
            b = aux;
        } else Console.WriteLine("Numbers are not greater than 70");
    }

    public static bool Condition(short x)
    {
        if(x%2==0)
            return true;
        else
            return false;
    }

    public static short Transform(short x)
    {
        return 32;
    }

    public static List<short> Romeo30(List<short> list, Func<short,bool> f1, Func<short,short> f2)
    {
        if(list.Count==0)
            throw new ArgumentNullException("List is empty");
        List <short> result = new List<short>();
        foreach (short elem in list)
        {
            if(f1(elem))
            {
                result.Add(f2(elem));
            } else result.Add(elem);

        }
        return result;
    }

    public static void PrintList(List<short> list)
    {
        foreach(short elem in list)
        {
            Console.Write(elem+" ");
        }
        Console.WriteLine();
    }

    public static void Main(string[] args)
    {
        //ex1
        Console.WriteLine("Exercitiul 1: ");
        double a = 70;
        double b = 90;
        Console.WriteLine("a = "+a+", b = "+b);
        Whiskey34(ref a, ref b);
        Console.WriteLine("a = "+a+", b = "+b);

        //ex2
        Console.WriteLine("\nExercitiul 2: ");
        List<short> list=new List<short>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.Add(5);
        list.Add(6);
        PrintList(list);
        List<short> result = Romeo30(list, Condition, Transform);
        PrintList(result);

        //ex3
        Console.WriteLine("\nExercitiul 3: ");
        List<Cadru_didactic> lista=new List<Cadru_didactic>();
        Cadru_didactic_conferentiar conf1 = new Cadru_didactic_conferentiar("Matei", "Mihai", 34, 2);
        Cadru_didactic_conferentiar conf2 = new Cadru_didactic_conferentiar("Pintea", "Ilie", 40, 5);
        Cadru_didactic_profesor prof1 = new Cadru_didactic_profesor("Micea", "Marcel", 45, 10);
        Cadru_didactic_profesor prof2 = new Cadru_didactic_profesor("Marin", "Razvan", 41, 14);

        lista.Add(conf1);
        lista.Add(prof1);
        lista.Add(conf2);
        lista.Add(prof2);

        foreach(Cadru_didactic p in lista)
        {
            p.Salut();
        }

        foreach(Cadru_didactic p in lista)
        {
            Console.WriteLine(p.ToString());
        }
    }
}