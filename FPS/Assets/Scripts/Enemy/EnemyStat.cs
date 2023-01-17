using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour

{
    public Animator anim;
    public Rigidbody[] rigid;
    public int health;
    public bool Live;
    public AudioSource audio;
    

    public NPCMoveController npcMoveController;

    void Start()
    {
       
    }

    public void TakeAwayHealth(int TakeAway)
    {
        health -= TakeAway;

        if (health <= 0)
            Dead();
        Live = false;
    }
    public void Dead()
    {   
        anim.enabled = false;
        //npcMoveController.movePoint[0] = null;
        npcMoveController.Stop();
        audio.enabled = false;
        foreach (Rigidbody rb in rigid)
        {
            rb.isKinematic = false;

        }

       

    }
}