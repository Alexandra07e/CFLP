using System;

abstract class Animal_de_casa
{
    public int an_nastere{get; protected set;}
    public string rasa{get; protected set;}
    public string nume{get; protected set;}

    public Animal_de_casa(int an_nastere, string rasa, string nume)
    {
        this.an_nastere=an_nastere;
        this.rasa=rasa;
        this.nume=nume;
    }

    public abstract void Salut();

    public override string ToString()
    {
        return an_nastere+" "+nume+" "+rasa+" ";
    }
}

class Animal_de_casa_de_acvariu : Animal_de_casa
{
    public double volum_dm_cubi{get; protected set;}

    public Animal_de_casa_de_acvariu(int an_nastere, string rasa, string nume, double volum_dm_cubi) : base(an_nastere, rasa, nume)
    {
        this.volum_dm_cubi=volum_dm_cubi;
    }

    public override void Salut()
    {
        Console.WriteLine("Salut, sunt Animal_de_casa_de_acvariu");
    }

    public override string ToString()
    {
        return base.ToString()+volum_dm_cubi+" dm^3";
    }
}

class Animal_de_casa_de_terariu : Animal_de_casa
{
    public double suprafata_dm_patrati{get; protected set;}

    public Animal_de_casa_de_terariu(int an_nastere, string rasa, string nume, double suprafata_dm_patrati) : base(an_nastere, rasa, nume)
    {
        this.suprafata_dm_patrati=suprafata_dm_patrati;
    }

    public override void Salut()
    {
     Console.WriteLine("Salut, sunt Animal_de_casa_de_terariu");
    }

    public override string ToString()
    {
        return base.ToString()+suprafata_dm_patrati+" dm^2";
    }
}

class Student2
{

    public static void Victor21(ref ulong a, ref ulong b)
    {
        if(a%2==0 && b % 2 == 0)
        {
            ulong aux=a;
            a=b;
            b=aux;
        } else Console.WriteLine("Numbers are not even");
    }

    public static bool Condition(ushort x)
    {
        if(x>5)
            return true;
        else return false;
    }

    public static ushort Transform(ushort x)
    {
        return 0;
    }
    public static List<ushort> Lima32(List<ushort> list, Func<ushort, bool> Condition, Func<ushort,ushort> Transform)
    {
        if(list.Count==0)
            throw new ArgumentNullException("List is empty");
        List<ushort> result=new List<ushort>();
        foreach(ushort elem in list)
        {
            if(Condition(elem))
                result.Add(Transform(elem));
            else result.Add(elem);
        }
        return result;
    }

    public static void PrintList(List<ushort> lista)
    {
        foreach(ushort elem in lista)
        {
            Console.Write(elem+" ");
        }
        Console.WriteLine();
    }
    public static void Main(string[] args)
    {
        //ex1
        Console.WriteLine("EXERCITIUL 1: ");
        ulong a = 23;
        ulong b = 35;
        Console.WriteLine(a+" "+b);
        Victor21(ref a, ref b);
        Console.WriteLine(a+" "+b);

        //ex2
        Console.WriteLine("EXERCITIUL 2: ");
        List<ushort> lista = new List<ushort>();
        lista.Add(1);
        lista.Add(3);
        lista.Add(4);
        lista.Add(7);
        lista.Add(8);
        lista.Add(2);
        PrintList(lista);
        List<ushort> result=Lima32(lista, Condition, Transform);
        PrintList(result);

        //ex3
        Console.WriteLine("EXERCITIUL 2: ");
        List<Animal_de_casa> list=new List<Animal_de_casa>();
        Animal_de_casa_de_acvariu p1=new Animal_de_casa_de_acvariu(2020, "peste1", "Nemo", 1234);
        Animal_de_casa_de_acvariu p2=new Animal_de_casa_de_acvariu(2022, "peste2", "Squidward", 12821);
        Animal_de_casa_de_terariu p3=new Animal_de_casa_de_terariu(2015, "pisica", "Matzel", 18);
        Animal_de_casa_de_terariu p4=new Animal_de_casa_de_terariu(2016, "caine", "Azor", 198);

        list.Add(p1);
        list.Add(p2);
        list.Add(p3);
        list.Add(p4);

        foreach(Animal_de_casa elem in list)
        {
            elem.Salut();
        }

        foreach(Animal_de_casa elem in list)
        {
            Console.WriteLine(elem.ToString());
        }


    }
}
