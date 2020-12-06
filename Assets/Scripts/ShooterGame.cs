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
    public GameObject cam;
    public LayerMask IgnoreMe;
    public bool showBullets = false;

    bool canFire = true;
    float fireRate;
    int magazineCount;
    LineRenderer bullet;
    float bulletSmokeTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1 / reloadSpeed;
        magazineCount = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){

            Fire();

            magazineCount -= 1;

            if (magazineCount <= 0)
            {
                Invoke("CanFire", fireRate);

                magazineCount = magazineSize;
            }
        }
    }

    void Fire()
    {
        if (canFire)
        {
            for (int i = 0; i < shotCount; i++)
            {
                Vector3 forward = cam.transform.forward;
                Vector3 face = cam.transform.position;

                Vector3 dir = (forward + UnityEngine.Random.insideUnitSphere * spread);

                RaycastHit hit;

                if (Physics.Raycast(face, dir, out hit, range, ~IgnoreMe))
                {
                    Debug.DrawLine(face, hit.point, Color.grey, 2);

                    if (showBullets)
                    {
                        bullet.SetPosition(0, face);
                        bullet.SetPosition(1, hit.transform.position);

                        Invoke("BulletHide", bulletSmokeTime);
                    }

                    if (hit.collider.CompareTag("zombie"))
                    {
                        ZombieController zombie = hit.transform.gameObject.GetComponent<ZombieController>();

                        //TODO: Add to Zombie Controller
                        zombie.damageZombie(damage);
                    }

                    Debug.Log("Fired: " + i.ToString() + " " + face.ToString() + " " + dir.ToString() + hit.transform.position.ToString());
                }
            }
        }

        canFire = false;
    }

    void CanFire()
    {
        canFire = true;
    }

    void BulletHide()
    {

    }
}