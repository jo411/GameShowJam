using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class KnockupTrigger : MonoBehaviour
{    
    public float knockBackForce = 20f;
    public float height = .5f;
    public List<AudioClip> sounds;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            RigidBodyFpsController rigidFPSObject = other.gameObject.GetComponent<RigidBodyFpsController>() as RigidBodyFpsController;

            if(rigidFPSObject != null)
            {
                rigidFPSObject.LockoutControls();
            }
            playSound();
        }
        else if(other.CompareTag("zombie"))
        {
            other.gameObject.GetComponent<ZombieController>().DisableNavmeshAgentForSeconds();
            playSound();
        }
      
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if(rb!=null)
        {           
            Vector3 knockbackdir =  rb.transform.position- transform.position + new Vector3(0,height,0);
            knockbackdir = knockbackdir.normalized;
            
            rb.AddForce(knockbackdir * knockBackForce, ForceMode.Impulse);
            
        }
    }


    void playSound()
    {
        source.PlayOneShot(Extensions.GetRandomElement(sounds));
    }
}
