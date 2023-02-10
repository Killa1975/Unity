using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public Animator anim;
    public Transform targetLook;
    public Transform rHand;

    public WeaponProperties firstWeapon;
    public WeaponProperties secondWeapon;

    public CharacterIK characterIK;
    public CharacterInput characterInput;

    public GameObject objWeapon;
    public Weapon activeWeapon; 

    public void SelectWeapon (int selectWeapon)
    {
        if(selectWeapon == 1)
        {
            DestroyWeapon();
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
           

        }
        if (selectWeapon == 3)
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
            Debug.Log("1");
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

    }
}