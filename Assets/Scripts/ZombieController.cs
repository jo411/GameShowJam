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
        if(agent.enabled && agent.isOnNavMesh)
        {
            agent.SetDestination(target.transform.position);
        }
        
    }

    public void EnableAgent()
    {
        agent.enabled = true;
        rb.isKinematic = true;
    }

    public void DisableNavmeshAgentForSeconds(float seconds = 2)
    {
        agent.enabled = false;
        rb.isKinematic = false;
        Invoke("EnableAgent", seconds);
    }


}
