namespace Cardoni;

using System;
using System.Collections.Generic;

public class PriorityQueue<T>
    where T : IComparable
{
    List<T> elements = new List<T>();

    public int Count
    {
        get { return elements.Count; }
    }

    public bool Empty
    {
        get { return elements.Count == 0; }
    }

    public T Top
    {
        get { return elements[0]; }
    }

    public void Push(T item)
    {
        elements.Add(item);

        sortDown(elements.Count - 1);
    }

    public T Pop()
    {
        int last = elements.Count - 1;
        T item = elements[last];
        elements.RemoveAt(last--);

        if (last == -1)
        {
            return item;
        }

        T least = elements[0];
        int i = 0;

        while (true)
        {
            int j = (i + 1) * 2 - 1;
            if (j == last)
            {
                elements[i] = elements[j];
                i = j;
                break;
            }
            else if (j > last)
            {
                break;
            }

            if (LessThan(elements[j], elements[j + 1]))
            {
                elements[i] = elements[j];
                i = j;
            }
            else
            {
                elements[i] = elements[j + 1];
                i = j + 1;
            }
        }
        elements[i] = item;
        sortDown(i);

        return least;
    }

    private void sortDown(int i)
    {
        while (i > 0)
        {
            int j = (i + 1) / 2 - 1;
            if (LessThan(elements[i], elements[j]))
            {
                T temp = elements[i];
                elements[i] = elements[j];
                elements[j] = temp;
            }
            else
            {
                break;
            }
            i = j;
        }
    }

    private bool LessThan(T a, T b)
    {
        return (a as IComparable).CompareTo(b) < 0;
    }
}
