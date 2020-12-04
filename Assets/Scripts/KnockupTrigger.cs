using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockupTrigger : MonoBehaviour
{    
    float knockBackForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
      
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if(rb!=null)
        {           
            Vector3 knockbackdir =  rb.transform.position- transform.position + new Vector3(0,.5f,0);
            knockbackdir = knockbackdir.normalized;
          
            Debug.Log(knockbackdir * knockBackForce);
            
            rb.AddForce(knockbackdir * knockBackForce, ForceMode.Impulse);
        }
    }
}
