using System;

class Student4
{
    public static void Zulu22(ref ushort a, ref ushort b)
    {
        if(a>77 && b > 77)
        {
            ushort aux=a;
            a=b;
            b=aux;
        } else Console.WriteLine("Numbers aren't greater than 77");
    }

    public delegate bool MyDelegate1(byte x);
    public delegate byte MyDelegate2(byte x);

    public static bool Cond(byte x)
    {
        if(x%2==0)
            return true;
        else return false;
    }

    public static byte Transform(byte x)
    {
        return 0;
    }

    public static List<byte> Mike34(List<byte> list, MyDelegate1 f1, MyDelegate2 f2)
    {
        List<byte> result = new List<byte>();
        if(list.Count==0)
            throw new ArgumentNullException("List empty");
        
        foreach(byte elem in list)
        {
            if(!f1(elem))
                result.Add(f2(elem));
            else result.Add(elem);
        }
        return result;
    }

    public static void PrintList(List<byte> list)
    {
        foreach(byte elem in list)
        {
            Console.Write(elem+" ");
        }
        Console.WriteLine();
    }
    public static void Main(string[] args)
    {
        //ex1
        ushort a=80;
        ushort b=90;
        Console.WriteLine(a+" "+b);
        Zulu22(ref a, ref b);
        Console.WriteLine(a+" "+b);
        Console.Write("\n\n");

        //ex2
        List<byte> list=new List<byte>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.Add(5);
        list.Add(7);
        PrintList(list);

        List<byte> result=Mike34(list, Cond, Transform);
        PrintList(result);
    }
}
