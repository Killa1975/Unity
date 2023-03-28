using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMaterialSwitch : MonoBehaviour
{
    public Material Red;
    public Material Green;
    public Animator anim;
    public GameObject Button;

    void Update()
    {
        if (anim.GetBool("ON"))
        {
            Button.GetComponent<Renderer>().material = Green;
        }
        else
        {
            Button.GetComponent<Renderer>().material = Red;
        }
    }
}
