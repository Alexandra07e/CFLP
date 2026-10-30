using System;
using System.Linq.Expressions;

public class Stack<T>
{
    private List<T> list = new List<T>();
    public void Push(T elem)
    {
        list.Add(elem);
    }

    public T Pop()
    {
        if (list.Count==0)
            throw new ArgumentNullException("List is null");

        T element = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        return element;
    }

    public bool TryPop(out T output_elem)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Stack is empty");
            output_elem = default(T)!; 
            return false;
        }
        output_elem = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        return true;
    }

    public T Peek()
    {
        if (list.Count == 0)
        {
            throw new ArgumentNullException("List is null");
        }
        return list[list.Count - 1];
    }
    
    public void PrintStack()
    {
        Console.Write("Stack: ");
        foreach (T elem in list)
        {
            Console.Write(elem + " ");
        }
        Console.WriteLine();
    }
}
public class Program
{
    public static void RemoveAt(int[] array, ref int length, int n)
    {
        if (n >= length)
        {
            Console.WriteLine("Index out of range");
            return;
        }
        for (int i = n; i < length - 1; i++)
            array[i] = array[i + 1];
        length--;
    }

    public static void PrintArray(int[] array, int length)
    {
        for (int i = 0; i < length; i++)
            Console.Write(array[i] + " ");
        Console.WriteLine();
    }

    public static List<int> Intersection(List<int> l1, List<int> l2)
    {
        if (l1 == null || l2 == null)
        {
            throw new ArgumentNullException("One of the lists is null");
        }
        List<int> result = new List<int>();
        foreach (int elem1 in l1)
        {
            if (l2.Contains(elem1) && !result.Contains(elem1))
            {
                result.Add(elem1);
            }
        }
        return result;
    }
    
    public static void PrintList(List<int> list)
    {
        foreach (int elem in list)
        {
            Console.Write(elem + " ");
        }
        Console.WriteLine();
    }
    static void Main(string[] args)
    {
        //ex1
        Console.WriteLine("////////////////////////EX 1:");
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int length = 9;
        PrintArray(array, length);
        int n = 4;
        RemoveAt(array, ref length, n);
        PrintArray(array, length);

        //ex2
        Console.WriteLine("////////////////////////EX 2:");
        List<int> l1 = new List<int>();
        List<int> l2 = new List<int>();
        l1.Add(1);
        l1.Add(2);
        l1.Add(3);
        l1.Add(3);
        Console.Write("List 1: ");
        PrintList(l1);
        l2.Add(3);
        l2.Add(4);
        l2.Add(2);
        l2.Add(5);
        l2.Add(6);
        Console.Write("List 2: ");
        PrintList(l2);
        List<int> result = Intersection(l1, l2);
        if (result == null)
        {
            Console.Write("List empty");
        }
        else
            PrintList(result);

        //ex3
        Console.WriteLine("////////////////////////EX 3:");
        Stack<int> stack = new Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Pop();
        stack.Push(3);
        stack.Push(4);
        stack.PrintStack();
        stack.TryPop(out int rez);
        Console.WriteLine("Try Pop: " + rez);
        Console.WriteLine("Peek: " + stack.Peek());
        stack.Pop();
        stack.PrintStack();
        stack.Pop();
        stack.Peek();
        stack.Pop();
        stack.TryPop(out int res);
        Console.WriteLine("TryPop when empty: " + res);
    }
}