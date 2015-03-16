using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OrderedList<T> : List<T> 
{
    IComparer<T> comparer=Comparer<T>.Default;
    public OrderedList()
    {

    }
    public OrderedList(IComparer<T> comp)
    {
        comparer = comp;
    }
    public void Add(T item)
    {
        int start = 0;
        int end = this.Count - 1;
        T mid;
        int comp;
        while (true)
        {
            if (end < 0)
            {
                this.Insert(0, item);
                break;
            }
            if (start==end)
            {
                if (comparer.Compare(item, this[start])<0)
                {
                    this.Insert(start, item);
                }
                else
                {
                    this.Insert(start + 1, item);
                }
                break;
            }

            mid = this[(start + end) / 2];
            comp=comparer.Compare(item,mid);
            if (comp<0)
            {
                end = (start + end) / 2;
            }
            else if (comp>0)
            {
                start = (start + end) / 2 + 1;
            }
            else
            {
                this.Insert((start + end) / 2, item);
                break;
            }

        }
    }
}
