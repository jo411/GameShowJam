using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class ZombieController : MonoBehaviour
{

    private enum AIState
    {
        Chasing,
        Fleeing
    }

    GameObject target;
    NavMeshAgent agent;
    Rigidbody rb;

    public float minSpeed = 5;
    public float maxSpeed = 13;

    public float minTurn = 100;
    public float maxTurn = 200;

    public float minAcceleration = 5;
    public float maxAcceleration = 10;

    public GameObject tail;

    public LayerMask groundedLayers;

    bool tryAttatchToNavmesh = false;

    bool grounded = true;

    public float health = 1f;

    bool hasTail = false;
    bool isDead = false;

    float FleeDistance = 15.0f;

    AIState currentState = AIState.Chasing;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        agent.speed = Random.Range(minSpeed, maxSpeed);
        agent.angularSpeed = Random.Range(minTurn, maxTurn);
        agent.acceleration = Random.Range(minAcceleration, maxAcceleration);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (tryAttatchToNavmesh)
        {
            checkGrounded();
            EnableAgent();
        }

        if(agent.enabled && agent.isOnNavMesh)
        {
            switch(currentState)
            {
                case AIState.Chasing:
                    TargetPlayer();
                    break;
                case AIState.Fleeing:
                    FleePlayer();
                    break;
            }
        }            
    }



    void TargetPlayer()
    {
        agent.SetDestination(target.transform.position);
    }

    void FleePlayer()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(distance<FleeDistance)
        {
            Vector3 dirToPlayer = transform.position - target.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            agent.SetDestination(newPos);
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

    public void damageZombie(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            KillZombie();
        }
    }

    public void KillZombie()
    {
        if (hasTail)
        {
            RemoveTail();
        }

        Destroy(gameObject, 5);

        isDead = true;
    }

    public bool AddTail()
    {
        if (!isDead)
        {
            hasTail = true;
            currentState = AIState.Fleeing;
            tail.SetActive(true);
            return true;
        }
        return false;

    }

    public void RemoveTail()
    {
        hasTail = false;
        currentState = AIState.Chasing;
        tail.SetActive(false);
        target.GetComponent<TailController>().AddTail();
    }
}
