using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGuy : MonoBehaviour
{
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
        if (other.CompareTag("zombie"))
        {
            other.gameObject.GetComponent<ZombieController>().DisableNavmeshAgentForSeconds();
        }
    }
}
