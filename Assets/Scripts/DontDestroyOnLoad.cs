using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    private GameObject instance;

    private void Awake()
    {
        int count = GameObject.FindGameObjectsWithTag(this.tag).Length;     

        if (count > 1)
        {
            Object.Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(this);
        }
        
    }
}
