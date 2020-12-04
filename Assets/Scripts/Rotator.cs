using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{


    public float rotateSpeed;
    public Vector3 rotationAngles;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        rotate();    

    }

    public void rotate()
    {
        transform.Rotate(rotationAngles * Time.deltaTime * rotateSpeed);
        
    }

}


