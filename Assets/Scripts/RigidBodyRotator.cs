using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyRotator : MonoBehaviour
{


    public float rotateSpeed;
    public Vector3 rotationAngles;
    Quaternion currentRotation;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentRotation = rb.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        rotate();

    }

    public void rotate()
    {
        currentRotation *= Quaternion.Euler(rotationAngles * Time.deltaTime * rotateSpeed);
        rb.MoveRotation(currentRotation);

    }
}
