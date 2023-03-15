
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
        if (expectedDelta == 0) return true;
        if (expectedDelta < 0)
        {
            if (expectedDelta < boundary - currentValue)
            {
                deltaValue = boundary - currentValue;
                return true;
            }
            deltaValue = expectedDelta;
        }

        if (expectedDelta > 0)
        {
            if (expectedDelta > boundary - currentValue)
            {
                deltaValue = boundary - currentValue;
                return true;
            }
            deltaValue = expectedDelta;
        }

        return false;
    }
}
