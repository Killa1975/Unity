using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Сamera/Pricel")]
public class PricelConfig : ScriptableObject
{

    public float turnSmooth;
    public float pivotSpeed;
    public float Y_rot_speed;
    public float X_rot_speed;
    public float minAngle;
    public float maxAngle;
    public float normalZ;
    public float normalX;
    public float normalY;
    public float aimZ;
    public float aimX;
    public float aimY;

    public GameObject pricel;

    public void Update()
    {
        pricel = GameObject.FindGameObjectWithTag("Pricel");

        aimX = pricel.transform.position.x;
        aimY = pricel.transform.position.y;
        aimZ = pricel.transform.position.z;

    }
}
