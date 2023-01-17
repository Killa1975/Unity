using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponProperties weaponProperties;
    public Transform shotPoint;
    public Transform targetLook;
    public Transform lHandTarget;

    //public GameObject cameraMain;
    public GameObject decal;
    public GameObject bullet;

    public ParticleSystem muzzleFlash;
    public AudioSource audioSource;
    public AudioClip shootClip;
    public AudioClip noammoclip;

    public GameObject shell;
    public Transform shellPosition;
    //public Hitmarker hitmarker;

    public int curAmmo;


    public bool shooing;
    public bool firstShootl;
    public float timer;
    public float delayShoot;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //hitmarker = GameObject.Find("CrosshairManager").GetComponent<Hitmarker>();

        delayShoot = 60f / weaponProperties.shootSpeed;
        Debug.Log(delayShoot);

    }

    void Update()
    {
        timer += Time.deltaTime;
    }

   public void ShootManager()

    {
       
            if (firstShootl)
            {
                Debug.Log("paw");
                firstShootl = false;
                Shoot();
                timer = 0;
            }
            else
            {
                
                if (timer >= delayShoot)
                {
                    Shoot();
                    timer = 0;
                }
            
    }


        RaycastHit hit;
    }
    public void Shoot()
    {

        {
            if (curAmmo > 0)
            {
                AddShell();
                timer = 0;
                curAmmo = curAmmo - 1;
                muzzleFlash.Play();
                shotPoint.LookAt(targetLook);
                audioSource.PlayOneShot(shootClip);
                GameObject newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
                newBullet.GetComponent<Bullet>().damage = weaponProperties.damage;
               
                //newBullet.GetComponent<Bullet>().hitmarker = hitmarker;
            }

            else
            {
                audioSource.PlayOneShot(noammoclip);
            }




            
        }
    }

    //public void AutoShout()
    //{
    //    StartCoroutine(Auto());

    //}
    //IEnumerator Auto()
    //{
        
    //    Instantiate(bullet, shotPoint.position, shotPoint.rotation);
    //    audioSource.PlayOneShot(shootClip);
    //    muzzleFlash.Play();
    //    AddShell();
    //    yield return new WaitForSeconds(15.5f);

    //}

    void AddShell()
    {
        GameObject newShell = Instantiate(shell);
        newShell.transform.position = shellPosition.position;

        Quaternion rot = shellPosition.rotation;
        newShell.transform.rotation = rot;

        newShell.transform.parent = null;
        newShell.GetComponent<Rigidbody>().AddForce(-newShell.transform.forward * Random.Range(80, 120));
        Destroy(newShell, 20);
    }


}
