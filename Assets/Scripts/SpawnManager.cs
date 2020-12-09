using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{

    public float spawnRate = .5f;
    public float max_Spawns;

    public float minSpawnRadius = 10;
    public float maxSpawnRadius = 20;

    public float checkRadius= 20f;

    public float maxSpawnTries = 20f;

    float minDelta;
    public GameObject spawnPrefab;


    float spawnTimer = 0f;
    GameObject player;

    List<GameObject> spawns;


    // Start is called before the first frame update
    void Start()
    {
        spawns = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        minDelta = maxSpawnRadius - minSpawnRadius;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer>=spawnRate)
        {
            spawnTimer = 0;
            spawns.RemoveAll(e => e == null);
            if(spawns.Count<max_Spawns)
            {
                SpawnUnit();
            }            
        }

    }

    void SpawnUnit()
    {
        bool valid;
        //Debug.Log("TrySpawn");
        Vector3 pos = GetSpawnLocation(out valid);

        if(pos!=Vector3.positiveInfinity && valid)
        {
            GameObject spawned = Instantiate(spawnPrefab,pos,Quaternion.identity);
            spawns.Add(spawned);
        }
    }

    Vector3 GetSpawnLocation(out bool valid)
    {
        bool foundLoc = false;
        int tries = 0;

        while(!foundLoc)
        {
            //Debug.Log("Try");
            if (tries > maxSpawnTries) { break; }

            var dir = Random.insideUnitSphere;
            Vector3 spawnpos = dir * minDelta;
            

            spawnpos += dir * minSpawnRadius;

            spawnpos = player.transform.position + spawnpos;

            NavMeshHit hit;

            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(spawnpos, out hit, checkRadius, 1))
            {
                if(Vector3.Distance(player.transform.position,finalPosition)>minSpawnRadius)
                {
                    finalPosition = hit.position;
                    valid = true;
                    return finalPosition;
                }               
            }

            tries++;
        }
        valid = false;
        return Vector3.positiveInfinity;
    } 
    
}
