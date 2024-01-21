using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RandomUtil : MonoBehaviour
{
    private static readonly System.Random random = new System.Random();

    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;

        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public static T SelectRandomElement<T>(List<T> list)
    {
        return list[random.Next(list.Count)];
    }

    public static System.Drawing.Color GenerateRandomColor()
    {
        return System.Drawing.Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
    }



    /// <summary>
    /// minValue is inclusive.
    /// maxValue is exclusive.
    /// values towards the min and max value are less likely
    /// </summary>
    /// <param name="minValue">inclusive</param>
    /// <param name="maxValue">exclusive</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static int RangeTriangularDistribution(int minValue, int maxValue)
    {
        // Ensure minValue is inclusive and maxValue is exclusive
        if (minValue >= maxValue)
        {
            throw new ArgumentException("minValue must be less than maxValue");
        }

        // Calculate the range and the midpoint
        int range = maxValue - minValue;
        int midpoint = minValue + range / 2;

        // Generate a random value within the triangular distribution
        int randomValue = (int)(midpoint + ((random.NextDouble() - random.NextDouble()) * range / 2));

        // Ensure the result is within the specified range
        return Math.Clamp(randomValue, minValue, maxValue - 1);
    }
}
