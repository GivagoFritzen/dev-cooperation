using System;

[Serializable]
public class IntRange
{
    public int min;
    public int max;

    public IntRange(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public int Random
    {
        get { return UnityEngine.Random.Range(min, max); }
    }
}
