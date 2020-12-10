using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    public GameObject prefab;
    public float spawnInterval = 0f;

    bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            canSpawn = false;

            Invoke("SpawnTimer", spawnInterval);
        }

    }

    void SpawnTimer()
    {
        canSpawn = true;
    }
}
