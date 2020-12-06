using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public GameObject checkpointSpawn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DeathScript deathscript = other.gameObject.GetComponent<DeathScript>() as DeathScript;
            deathscript.SetCheckpoint(checkpointSpawn);
        }
    }
}
