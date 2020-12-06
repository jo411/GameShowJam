using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterGame : MonoBehaviour
{
    public float damage = 1f;
    public float reloadSpeed = 1;
    public int magazineSize = 2;
    public float spread = 0f;
    public int shotCount = 1;
    public float range = 5f;
    public float knockback;
    public GameObject cam;
    public LayerMask IgnoreMe;
    public GameObject fire;

    bool canFire = true;
    float fireRate;
    int magazineCount;
    float bulletSmokeTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = reloadSpeed;
        magazineCount = magazineSize;
        fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){

            Fire();                  
                       
            if (magazineCount <= 0)
            {
                canFire = false;

                Invoke("CanFire", fireRate);

                magazineCount = magazineSize;
            }
        }
    }

    void Fire()
    {
        if (canFire)
        {
            magazineCount -= 1;

            fire.SetActive(true);

            Invoke("FireHide", .5f);

            for (int i = 0; i < shotCount; i++)
            {
                Vector3 forward = cam.transform.forward;
                Vector3 face = cam.transform.position;

                Vector3 dir = (forward + UnityEngine.Random.insideUnitSphere * spread);

                RaycastHit hit;

                if (Physics.Raycast(face, dir, out hit, range, ~IgnoreMe))
                {
                    Debug.DrawLine(face, hit.point, Color.grey, 2);
                                
                    if (hit.collider.CompareTag("zombie"))
                    {
                        ZombieController zombie = hit.transform.gameObject.GetComponent<ZombieController>();

                        zombie.damageZombie(damage);

                        zombie.DisableNavmeshAgentForSeconds();

                        Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();

                        rb.AddForce(forward * knockback, ForceMode.Impulse);
                    }

                    Debug.Log("Fired: " + i.ToString() + " " + face.ToString() + " " + dir.ToString() + hit.transform.position.ToString());
                }
            }
        }
    }

    void CanFire()
    {
        canFire = true;
    }

    void FireHide()
    {
        fire.SetActive(false);
    }
}