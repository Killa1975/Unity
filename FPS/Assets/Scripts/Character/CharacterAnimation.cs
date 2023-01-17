using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator anim;
    public CharacterMovement characterMovement;
    public CharacterStatus characterStatus;
    public Transform mainTransform;

    public void AnimationUpdate()
    {
        anim.SetBool("sprint", characterStatus.isSprint);
        anim.SetBool("aiming", characterStatus.isAiming);
        anim.SetBool("aiming move", characterStatus.isAimingMove);


        if (!characterStatus.isAiming)
            AnimationNormal();
        else
            AnimationAiming();
    }
    void AnimationNormal()
    {
        anim.SetFloat("vertical", characterMovement.moveAmount);
    }
    
    void AnimationAiming()
    {
        float v = characterMovement.vertical;
        float h = characterMovement.horizontal;

        anim.SetFloat("vertical", v);
        anim.SetFloat("horizontal", h);
    }
}
