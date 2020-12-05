using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{    
    public static T GetRandomElement<T>(IList<T> list)
    {
        // If there are no elements in the collection, return the default value of T
        if (list.Count == 0)
            return default(T);
 
        return list[Random.Range(0, list.Count)];
    }

}
