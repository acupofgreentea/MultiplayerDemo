using UnityEngine;

public static class Helpers
{
    public static Vector3 GetRandomPosition(float minValue, float maxValue, float height)
    {
        return new Vector3(Random.Range(minValue, maxValue), height, Random.Range(minValue, maxValue));
    }
}