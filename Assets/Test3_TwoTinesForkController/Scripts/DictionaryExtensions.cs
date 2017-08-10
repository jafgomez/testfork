using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryExtensions {
    public static void AddOrModify<T,L>(this Dictionary<T, L> dictionary , T key, L val)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = val;
        }
        else
        {
            dictionary.Add(key, val);
        }
    }

}

public class InstanceCounterDictionary<T>
{
    private Dictionary<T, int> storage;

    public InstanceCounterDictionary()
    {
        storage = new Dictionary<T, int>();
    }

    public int Add(T key)
    {
        return Add(key, 1);
    }

    public int Add(T key, int qty)
    {
        int v;
        if (storage.ContainsKey(key))
        {
            v = storage[key] + qty;
            storage[key] = v;
        }
        else
        {
            v = qty;
            storage.Add(key, v);

        }

        return v;
    }

    public int Remove(T key)
    {
        return Remove(key, 1);
    }

    public int Remove(T key, int qty)
    {
        int v;
        if (storage.ContainsKey(key))
        {
            v = storage[key] - qty;
            storage[key] = v;
        }
        else
        {
            v = qty;
            storage.Add(key, v);

        }

        return v;
    }

    public void Clear()
    {
        storage.Clear();
        storage = null;
    }
}
