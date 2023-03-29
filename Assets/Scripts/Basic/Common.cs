
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common
{
    /// <summary>
    /// do uniform interpolation with boundary with a specific delta value
    /// </summary>
    /// <param name="boundary"></param>
    /// <param name="currentValue"></param>
    /// <param name="expectedDelta"> the value expect to do interpolation</param>
    /// <param name="deltaValue"> the correct delta value</param>
    /// <returns>return true if reach the boundary after this interpolation</returns>
    public static bool UniformInterpolationInValue(float boundary, float currentValue, float expectedDelta, out float deltaValue)
    {
        deltaValue = 0.0f;
        if (currentValue == boundary) return true;
        if (currentValue < boundary)
        {
            expectedDelta = MathF.Abs(expectedDelta);

            if (expectedDelta + currentValue < boundary) deltaValue = expectedDelta;
            else
            {
                deltaValue = boundary - currentValue;
                return true;
            }
        }

        if (currentValue > boundary)
        {
            expectedDelta = -MathF.Abs(expectedDelta);

            if (expectedDelta + currentValue > boundary) deltaValue = expectedDelta;
            else
            {
                deltaValue = boundary - currentValue;
                return true;
            }
        }

        return false;
    }

    public static float Lerp(float a, float b, float lerpSale)
    {
        return a + (1-lerpSale)*b;
    }

    public static bool IsFront(Vector3 targetPos, Vector3 selfPos, Vector3 forward, float minLimit)
    {
        Vector3 d = (targetPos - selfPos).normalized;
        float v = Vector3.Dot(d, forward.normalized);
        return v >= minLimit;
    }
}
