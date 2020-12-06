using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
    public static bool hasTail = false;
    public float FFLTime = 5f;

    private float FFLTimePassed = 0f;
    private DeathScript deathScript;

    // Start is called before the first frame update
    void Start()
    {
        hasTail = true;
        AddTail();
        deathScript = this.GetComponent<DeathScript>() as DeathScript;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasTail)
        {
            FFLTimePassed += Time.deltaTime;

            if(FFLTimePassed >= FFLTime)
            {
                deathScript.KillPlayer();
                AddTail();
            }
        }
    }

    private void AddTail()
    {
        hasTail = true;
    }

    public bool RemoveTail()
    {
        if (hasTail)
        {
            hasTail = false;
            FFLTimePassed = 0f;
            return true;
        } else
        {
            return false;
        }
    }
}
