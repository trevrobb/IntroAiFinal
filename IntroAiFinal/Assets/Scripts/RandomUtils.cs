using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class RandomUtils 
{
    public static T GetRandomEnumValue<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Range(0, values.Length));
    }
   
    
}
