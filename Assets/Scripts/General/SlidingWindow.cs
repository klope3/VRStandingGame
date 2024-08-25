using System.Collections.Generic;
using UnityEngine;

public class SlidingWindow<T>
{
    private List<T> elements;
    private int maxLength;

    public SlidingWindow(int maxLength)
    {
        this.maxLength = maxLength;
        elements = new List<T>();
    }

    public void Push(T element)
    {
        if (elements.Count == maxLength)
        {
            elements.RemoveAt(0);
        }
        elements.Add(element);
    }

    public List<T> GetElements()
    {
        return elements;
    }
}
