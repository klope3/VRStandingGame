using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedCollection<T>
{
    [SerializeField] private Dictionary<T, int> weights;

    public WeightedCollection()
    {
        weights = new Dictionary<T, int>();
    }

    public T ChooseWeightedRandom()
    {
        //refactor with LINQ
        int sum = 0;
        foreach (KeyValuePair<T, int> pair in weights)
        {
            sum += pair.Value;
        }

        float rand = Random.Range(0, (float)sum);
        float min = 0;
        foreach (KeyValuePair<T, int> pair in weights)
        {
            float max = min + pair.Value;
            bool match = rand > min && rand <= max;
            if (match) return pair.Key;
            min = max;
        }
        return default;
    }

    public void DebugWeights()
    {
        int runs = 1000;
        Dictionary<T, int> results = new Dictionary<T, int>();
        for (int i = 0; i < runs; i++)
        {
            T result = ChooseWeightedRandom();
            if (results.ContainsKey(result)) results[result]++;
            else results.Add(result, 1);
        }
        string output = $"Ran {runs} times. Results: ";
        foreach (KeyValuePair<T, int> pair in results)
        {
            output += $"{pair.Key}: {pair.Value} ({(float)pair.Value / runs * 100}%) | ";
        }
        Debug.Log(output);
    }
}
