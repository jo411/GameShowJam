using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DeathScript deathscript = other.gameObject.GetComponent<DeathScript>() as DeathScript;
            deathscript.ResetPlayer();
        } 
        else if (other.gameObject.tag == "zombie")
        {

            ZombieController zombiScript = other.gameObject.GetComponent<ZombieController>() as ZombieController;
            //zombiScript.KillZombie();
        }
    }
}
