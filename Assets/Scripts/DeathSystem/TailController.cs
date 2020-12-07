using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TailController : MonoBehaviour
{
    public bool hasTail = true;
    public float FFLTime = 10f;
    public float StealRadias = 5f;
    public float TheftCheckCoolDown = 2f;
    public AnimationCurve NumberOfZombiesToStealChance = AnimationCurve.Linear(0, 0, 50, 80);
    
    private float FFLTimePassed = 0f;
    private float TimeSinceLastTheft = 0f;


    [SerializeReference]
    private List<GameObject> Zombies = null;


    private DeathScript deathScript = null;
    private ZombieController ZombieWithTail = null;

    // Start is called before the first frame update
    void Start()
    {
        deathScript = this.GetComponent<DeathScript>() as DeathScript;
        Random.InitState((int)System.DateTime.Now.Ticks);
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasTail)
        {
            FFLTimePassed += Time.deltaTime;

            if(FFLTimePassed >= FFLTime)
            {
                deathScript.KillPlayer();
                
                if (ZombieWithTail != null)
                {
                    ZombieWithTail.RemoveTail();
                    ZombieWithTail = null;
                }

                AddTail();
            }
        } else
        {
            TimeSinceLastTheft += Time.deltaTime;

            if (TimeSinceLastTheft >= TheftCheckCoolDown)
            {
                GetCloseZombies();
                CalculateTheft();
                TimeSinceLastTheft = 0f;
            }
        }
    }

    public void AddTail()
    {
        hasTail = true;
    }

    private void RemoveTail()
    {
        hasTail = false;
        FFLTimePassed = 0f;
        AddTailToZombie();
    }

    private void GetCloseZombies()
    {
        Zombies.Clear();

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, StealRadias);

        foreach (Collider col in colliders)
        {
            if(col.gameObject.tag == "zombie")
            {
                Zombies.Add(col.gameObject);
            }
        }
    }

    private void CalculateTheft()
    {
        if(Zombies.Count > 0)
        {
           int chance = (int)NumberOfZombiesToStealChance.Evaluate(Zombies.Count);

           if (checkPercentage(chance))
           {
                RemoveTail();
           }
        }
    }

    private bool checkPercentage(int chanceTrue)
    {
        return Random.Range(0, 101) <= chanceTrue;
    }

    private void AddTailToZombie()
    {
       GameObject zombie = Zombies[Random.Range(0, Zombies.Count)];
       ZombieWithTail = zombie.GetComponent<ZombieController>() as ZombieController;
       ZombieWithTail.AddTail();
    }
}
