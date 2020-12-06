using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class ZombieController : MonoBehaviour
{
    GameObject target;
    NavMeshAgent agent;
    Rigidbody rb;

    public LayerMask groundedLayers;

    bool tryAttatchToNavmesh = false;

    bool grounded = true;

    public float health = 1f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (tryAttatchToNavmesh)
        {
            checkGrounded();
            EnableAgent();
        }

        if(agent.enabled && agent.isOnNavMesh)
        {
            agent.SetDestination(target.transform.position);
        }

        if(health <= 0)
        {
            KillZombie();
        }        
    }

    void checkGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitinfo;
        if(Physics.Raycast(ray, out hitinfo,100,groundedLayers))
        {
             if(hitinfo.distance<.8)
            {
                grounded = true;
            }else
            {
                grounded = false;
            }
        }
    }
  
    public void EnableAgent()
    {
        if(tryAttatchToNavmesh == false)
        {
            tryAttatchToNavmesh = true;
        }
        else
        {
            if(grounded)
            {
                agent.enabled = true;
                rb.isKinematic = true;
                tryAttatchToNavmesh = false;
            }
           
        }
        
    }

    public void DisableNavmeshAgentForSeconds(float seconds = 2)
    {
        agent.enabled = false;
        rb.isKinematic = false;
        Invoke("EnableAgent", seconds);
        grounded = false;

    }

    public void KillZombie()
    {
        Debug.Log("Git Gud Scrub");
    }
}
