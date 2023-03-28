using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public CharacterStatus characterStatus;
    public CharacterInventory characterInventory;
    public CharacterIK characterIK;
    public Weapon weapon;
    public Transform targetLook;
    public CameraHandler cameraHandler;


    public Animator anim;
    public bool debugAiming;
    public bool isAiming;
    public bool isSprint;

    public bool rifleshotauto;

    public bool opportunityToAim;
    public float distance;

    bool reload;
    float vertical;
    public int SelWeapon;
    public AudioSource source;
    public SoundManager soundManager;
    public bool BreathA = true;
    
    public int count;
    public float timer;

    public GameObject crosshair;


    public void Update()
    {
        if ((Input.GetMouseButtonDown(1)) && (anim.GetBool("FPS")))
        {
            count++;  
        }

        //if ((Input.GetMouseButtonUp(1)) && (anim.GetBool("FPS")))
        //{
        //    count++;

        //}

        if (count > 0)
        {
            timer += Time.deltaTime;
        }

        if (timer > 0.5)
        {
            count = 0;
            timer = 0;
        }

        if (timer < 0.5 && count > 1)
        {
            count = 0;
            timer = 0;
            anim.SetBool("zoom", true);
            characterStatus.isAiming = true;
            characterStatus.isAimingMove = true;
            crosshair.SetActive(false);
        }
        if ((Input.GetMouseButtonUp(1)) && (anim.GetBool("FPS")) && (anim.GetBool("Weapon")))
        {
            anim.SetBool("zoom", false);
            crosshair.SetActive(true);
        }

    }


    public void InputUpdate()
    {
        
        RayCastAiming();
        InputAiming();
        InputSelectWeapon();
        
       
    }

    
    public void InputAiming()
    {
        if (characterInventory.activeWeapon != null && !reload && (anim.GetBool("Weapon")))
        {

            if (Input.GetMouseButton(1) && opportunityToAim  )
            {
                characterStatus.isAiming = true;
                characterStatus.isAimingMove = true;
            }

            if (Input.GetMouseButton(1) && !opportunityToAim )
            {
                characterStatus.isAiming = false;
                characterStatus.isAimingMove = true;
            }

            if (!Input.GetMouseButton(1))
            {
                characterStatus.isAiming = false;
                characterStatus.isAimingMove = false;
            }

            //if (Input.GetKeyDown(KeyCode.B))
            //{
            //    rifleshotauto = !rifleshotauto;
            //}

            if (Input.GetKeyDown(KeyCode.R) && !reload)
            {
                //characterInventory.activeWeapon.firstShootl = true;
                characterInventory.activeWeapon.shooing = false;

                characterStatus.isAiming = false;
                characterStatus.isAimingMove = false;
                characterIK.activeLeftHand = false;
                anim.SetTrigger("Reload");
                reload = true;
            }

        }

        //if ((Input.GetKey(KeyCode.Mouse0)) && (Input.GetKey(KeyCode.Mouse1)))
        //{
        //    if (rifleshotauto == true && opportunityToAim)

        //    {
        //        weapon.AutoShout();
        //    }
        //}

        if ((rifleshotauto == false) && (opportunityToAim) && (Input.GetMouseButtonDown(0)) && (Input.GetMouseButton(1)))
        {

            weapon.ShootManager();
          
        }



        //if (!debugAiming)
        //    characterStatus.isAiming = Input.GetMouseButton(1);
        //else
        //    characterStatus.isAiming = isAiming;

        vertical = anim.GetFloat("vertical");

        if ((vertical > 0) && Input.GetKey(KeyCode.LeftShift))
        {
            characterStatus.isSprint = true;

            while (BreathA == true)
            {
                source.PlayOneShot(soundManager.Breath);
                BreathA = false;
            }

            if (source.volume < 0.04)
            {
                source.volume = source.volume + ((Time.deltaTime + 1) / 10000);
            }
        }
        else
        {
            characterStatus.isSprint = false;
            
            if (source.volume < 0.05)
            {
                source.volume = source.volume - ((Time.deltaTime + 1) / 5000);
            }
            if (source.volume == 0)
            {
                source.Stop();
                BreathA = true;
            }


            //source.Stop();
            //source.volume = 0;
        }
        

        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    characterStatus.isSprint = false;
        //}

    }
    //public void Breath()
    //{
    //    

    //}


    public void Reload()
    {
        characterInventory.activeWeapon.curAmmo = characterInventory.activeWeapon.weaponProperties.maxAmmo;
        reload = false;
        weapon.firstShootl = true;
        characterIK.activeLeftHand = true;
        anim.SetBool("aiming", false);
    }

    public void InputSelectWeapon()
    {
        if(!anim.GetBool("aiming"))
        {
             if(Input.GetKeyDown(KeyCode.Alpha1) && SelWeapon !=1)
            {
                SelWeapon = 1;
                anim.SetTrigger("Select");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && SelWeapon != 2)
            {
                SelWeapon = 2;
                anim.SetTrigger("Select");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && SelWeapon != 3)
            {
                SelWeapon = 3;
                anim.SetTrigger("Select");
            }
        }
    }

    public void SelectWeapon()
    {
        characterInventory.DestroyWeapon();
        characterInventory.SelectWeapon(SelWeapon);
    }

    public void RayCastAiming()
    {
        //луч при котором убирается ствол
        Debug.DrawLine(transform.position + transform.up * 1.4f, targetLook.position, Color.green) ;

        distance = Vector3.Distance(transform.position + transform.up * 1f, targetLook.position);
            if (distance > 1.1f)
                opportunityToAim = true;
            else opportunityToAim = false;

    }
    
}