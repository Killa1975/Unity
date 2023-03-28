using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public Animator anim;
    public Rigidbody[] rigid;
    public int health;
    public bool Live;
    public NPCMoveController npcMoveController;
    public void TakeAwayHealth()
    {
        health -= npcMoveController.Damage;

        if (health <= 0)
            Dead();
        Live = false;
    }
    public void Dead()
    {
        anim.enabled = false;
        //npcMoveController.movePoint[0] = null;

        foreach (Rigidbody rb in rigid)
        {
            rb.isKinematic = false;
        }



    }
}
