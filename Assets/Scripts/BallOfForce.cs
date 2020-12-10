using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfForce : MonoBehaviour
{
    public float knockBackForce = 20f;
    public float timeToDelete = 20f;

    float deleteTimer;

    // Start is called before the first frame update
    void Start()
    {
        deleteTimer = timeToDelete;
    }

    // Update is called once per frame
    void Update()
    {
        if(deleteTimer <= 0)
        {
            DestroyBall();
        }
        else if(deleteTimer > 0)
        {
            deleteTimer -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RigidBodyFpsController rigidFPSObject = other.gameObject.GetComponent<RigidBodyFpsController>() as RigidBodyFpsController;

            if (rigidFPSObject != null)
            {
                rigidFPSObject.LockoutControls(.5f);
            }
        }
        else if (other.CompareTag("zombie"))
        {
            other.gameObject.GetComponent<ZombieController>().DisableNavmeshAgentForSeconds();
        }

        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 knockbackdir = rb.transform.position - transform.position;
            knockbackdir = knockbackdir.normalized;

            rb.AddForce(knockbackdir * knockBackForce + rb.velocity, ForceMode.Impulse);

            //Debug.Log("Hit");
        }
    }

    public void DestroyBall()
    {
        Destroy(gameObject);
    }
}