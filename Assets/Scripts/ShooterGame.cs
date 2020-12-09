using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShooterGame : MonoBehaviour
{
    public float damage = 1f;
    public float reloadSpeed = 1;
    public float timeBetweenShots = .3f;
    public int magazineSize = 2;
    public float spread = 0f;
    public int shotCount = 1;
    public float range = 5f;
    public float knockback;
    public GameObject cam;
    public LayerMask IgnoreMe;
    public ParticleSystem flashFX;
    public AudioClip emptySound;
    public List<AudioClip> shotSounds;

    public Rigidbody playerRb;

    CameraShake camShake;

    public AudioSource audioSource;
    public Image ammo1;
    public Image ammo2;

    bool interShotCoolDown = false;
    bool canFire = true;
    float fireRate;
    int magazineCount;
    //float bulletSmokeTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = reloadSpeed;
        magazineCount = magazineSize;
        camShake = cam.GetComponent<CameraShake>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
        if(!canFire)
        {           
            audioSource.PlayOneShot(emptySound);
        }

        if (canFire && !interShotCoolDown)
        {
            audioSource.PlayOneShot(Extensions.GetRandomElement(shotSounds));
            interShotCoolDown = true;
            Invoke("resetInterShotCooldown", timeBetweenShots);
            magazineCount -= 1;
            spawnShootFx();

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

                    //Debug.Log("Fired: " + i.ToString() + " " + face.ToString() + " " + dir.ToString() + hit.transform.position.ToString());
                }
            }
            camShake.shakeCam();
            UpdateAmmoUI();
        }
    }

    void resetInterShotCooldown()
    {
        interShotCoolDown = false;
    }
    void CanFire()
    {
        canFire = true;
        UpdateAmmoUI();
    }


    void spawnShootFx()
    {

        ParticleSystem.MainModule newMain = flashFX.main;
        float min = playerRb.velocity.magnitude + newMain.startSpeed.constantMin;
        float max = playerRb.velocity.magnitude + newMain.startSpeed.constantMax;
        newMain.startSpeed = new ParticleSystem.MinMaxCurve(min, max);
        flashFX.Play();
    }


    void UpdateAmmoUI()
    {
        switch(magazineCount)
        {
            case 0:
                ammo1.enabled = false;
                ammo2.enabled = false;
                break;
            case 1:
                ammo1.enabled = false;
                ammo2.enabled = true;
                break;
            case 2:
                ammo1.enabled = true;
                ammo2.enabled = true;
                break;
        }
    }

    void FireHide()
    {
        
    }
}