using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TailController : MonoBehaviour
{
    public bool hasTail = true;
    public float FFLTime = 10f;
    public float StealRadias = 5f;
    public float TheftCheckCoolDown = 2f;
    public AnimationCurve NumberOfZombiesToStealChance = AnimationCurve.Linear(0, 0, 50, 80);
    
    private float FFLTimeLeft = 0f;
    private float TimeSinceLastTheft = 0f;

    public GameObject FFLObject;
    public Text FFLtext;

    private List<GameObject> Zombies;


    private DeathScript deathScript = null;
    private ZombieController ZombieWithTail = null;

    // Start is called before the first frame update
    void Start()
    {
        Zombies = new List<GameObject>();
        deathScript = GetComponent<DeathScript>() as DeathScript;
        Random.InitState((int)System.DateTime.Now.Ticks);
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasTail)
        {
            FFLTimeLeft -= Time.deltaTime;

            FFLtext.text = FFLTimeLeft.ToString("00");


            if (FFLTimeLeft < 0)
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
        FFLObject.SetActive(false);
    }

    private void RemoveTail()
    {
        hasTail = false;
        FFLTimeLeft = FFLTime;
        AddTailToZombie();
        FFLObject.SetActive(true);
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
