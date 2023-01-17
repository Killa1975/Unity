using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/properties")]
public class WeaponProperties : ScriptableObject
{
    public Vector3 rHandPos;
    public Vector3 rHandRot;

    public GameObject weaponPrefab;

    public Vector3 Weapon_pos;
    public Vector3 Weapon_rot;


    public int maxAmmo;
    public int damage;
    public int shootSpeed;
}
