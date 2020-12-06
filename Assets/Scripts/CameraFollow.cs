using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float height;

    CameraShake camshake;
    // Start is called before the first frame update
    void Start()
    {
        camshake = GetComponent<CameraShake>();
        moveToOffsetNow();
    }
    public void OnValidate()
    {
        moveToOffsetNow();   
    }

    void moveToOffsetNow()
    {
        if(target != null)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y+height, target.transform.position.z) + (camshake==null?Vector3.zero:camshake.getOffset());
        }        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        moveToOffsetNow();
    }
}
