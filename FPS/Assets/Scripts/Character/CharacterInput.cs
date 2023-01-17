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

    public Animator anim;
    public bool debugAiming;
    public bool isAiming;
    public bool isSprint;

    public bool rifleshotauto;

    public bool opportunityToAim;
    public float distance;

    bool reload;

    public int SelWeapon;

    void Start()
    {
       
    }

    public void InputUpdate()
    {
        
        RayCastAiming();
        InputAiming();
        InputSelectWeapon();
    }

    public void InputAiming()
    {
        if (characterInventory.activeWeapon != null && !reload)
        {

            if (Input.GetMouseButton(1) /*&& opportunityToAim*/ )
            {
                characterStatus.isAiming = true;
                characterStatus.isAimingMove = true;
            }

            //if (Input.GetMouseButton(1)/* && !opportunityToAim */)
            //{
            //    characterStatus.isAiming = false;
            //    characterStatus.isAimingMove = true;
            //}

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
                characterInventory.activeWeapon.firstShootl = true;
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


        if (characterStatus.isSprint = Input.GetKey(KeyCode.LeftShift)) ;
    }


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
                Debug.Log("3");
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
        Debug.DrawLine(transform.position + transform.up * 1.4f, targetLook.position, Color.green) ;

        distance = Vector3.Distance(transform.position + transform.up * 1.4f, targetLook.position);
            if (distance > 1.5f)
                opportunityToAim = true;
            else opportunityToAim = false;

    }
    
}