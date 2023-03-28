using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    //public Transform camera;
    public Animator anim;
    public Transform targetLook;
    public Transform rHand;
    //public Transform pointzoom;

    public WeaponProperties firstWeapon;
    public WeaponProperties secondWeapon;

    public CharacterIK characterIK;
    public CharacterInput characterInput;

    public GameObject objWeapon;
    //public GameObject Deagle;
    public Weapon activeWeapon;

    public GameObject pistol;
    public GameObject rifle;
    public AudioSource source;
    public SoundManager soundManager;

    public int selectWeapon;

    public GameObject crosshair;

    public void Update()
    {
        if (anim.GetInteger("WeaponType") == 1)
        {
            if (!anim.GetBool("zoom"))
            {

                characterIK.r_Hand.localPosition = secondWeapon.rHandPos;
                Quaternion rotRight = Quaternion.Euler(secondWeapon.rHandRot.x, secondWeapon.rHandRot.y, secondWeapon.rHandRot.z);
                characterIK.r_Hand.localRotation = rotRight;

            }
            if (anim.GetBool("zoom"))
            {


                characterIK.r_Hand.localPosition = secondWeapon.Aim_pos;
                Quaternion rotRight = Quaternion.Euler(secondWeapon.Aim_rot.x, secondWeapon.Aim_rot.y, secondWeapon.Aim_rot.z);
                characterIK.r_Hand.localRotation = rotRight;


            }
        }
    }


    public void SelectWeapon (int selectWeapon)
    {

        source.PlayOneShot(soundManager.Inventory);

        if (selectWeapon == 1)
        {
            DestroyWeapon();
            crosshair.SetActive(false);

        }
        if (selectWeapon == 2)
        {
            objWeapon = Instantiate(firstWeapon.weaponPrefab);
            activeWeapon = objWeapon.GetComponent<Weapon>();
            objWeapon.transform.parent = rHand;
            objWeapon.transform.localPosition = firstWeapon.Weapon_pos;
            objWeapon.transform.localRotation = Quaternion.Euler(firstWeapon.Weapon_rot);

            characterIK.r_Hand.localPosition = firstWeapon.rHandPos;
            Quaternion rotRight = Quaternion.Euler(firstWeapon.rHandRot.x, firstWeapon.rHandRot.y, firstWeapon.rHandRot.z);
            characterIK.r_Hand.localRotation = rotRight;

            activeWeapon.targetLook = targetLook;
            characterInput.weapon = activeWeapon;
            characterIK.l_Hand_Target = activeWeapon.lHandTarget;
            characterIK.activeLeftHand = true;

            anim.SetBool("Weapon", true);
            anim.SetInteger("WeaponType", 2);
            pistol.SetActive(true);
            rifle.SetActive(false);
            crosshair.SetActive(true);



        }
        if  (selectWeapon == 3)
        {
            if (!anim.GetBool("FPS"))
            {
                objWeapon = Instantiate(secondWeapon.weaponPrefab);
                activeWeapon = objWeapon.GetComponent<Weapon>();
                objWeapon.transform.parent = rHand;
                objWeapon.transform.localPosition = secondWeapon.Weapon_pos;
                objWeapon.transform.localRotation = Quaternion.Euler(secondWeapon.Weapon_rot);

                characterIK.r_Hand.localPosition = secondWeapon.rHandPos;
                Quaternion rotRight = Quaternion.Euler(secondWeapon.rHandRot.x, secondWeapon.rHandRot.y, secondWeapon.rHandRot.z);
                characterIK.r_Hand.localRotation = rotRight;

                activeWeapon.targetLook = targetLook;
                characterInput.weapon = activeWeapon;
                characterIK.l_Hand_Target = activeWeapon.lHandTarget;
                characterIK.activeLeftHand = true;

                anim.SetBool("Weapon", true);
                anim.SetInteger("WeaponType", 1);
                pistol.SetActive(false);
                rifle.SetActive(true);
                crosshair.SetActive(true);
            }
            if (anim.GetBool("FPS"))
            {
                objWeapon = Instantiate(secondWeapon.weaponPrefab);
                activeWeapon = objWeapon.GetComponent<Weapon>();
                objWeapon.transform.parent = rHand;
                objWeapon.transform.localPosition = secondWeapon.Weapon_pos;
                objWeapon.transform.localRotation = Quaternion.Euler(secondWeapon.Weapon_rot);

                characterIK.r_Hand.localPosition = secondWeapon.Aim_pos;
                Quaternion rotRight = Quaternion.Euler(secondWeapon.Aim_rot.x, secondWeapon.Aim_rot.y, secondWeapon.Aim_rot.z);
                characterIK.r_Hand.localRotation = rotRight;

                activeWeapon.targetLook = targetLook;
                characterInput.weapon = activeWeapon;
                characterIK.l_Hand_Target = activeWeapon.lHandTarget;
                characterIK.activeLeftHand = true;

                anim.SetBool("Weapon", true);
                anim.SetInteger("WeaponType", 1);
                pistol.SetActive(false);
                rifle.SetActive(true);
                crosshair.SetActive(true);
            }
            
        }
        
        
    }

    public void DestroyWeapon()
    {
        characterInput.weapon = null;
        characterIK.l_Hand_Target = null;
        Destroy(objWeapon);
        objWeapon = null;
        anim.SetBool("Weapon", false);
        anim.SetInteger("WeaponType", 0);
        characterIK.activeLeftHand = false;
        pistol.SetActive(true);
        rifle.SetActive(true);
        crosshair.SetActive(false);
    }
}