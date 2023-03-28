using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Manager/Sound")]
public class SoundManager : ScriptableObject
{
    public AudioClip[] footStepsStone;
    public AudioClip[] footStepsWood;

    public AudioClip[] footStepsWoodMan;

    public AudioClip[] meatHit;
    public AudioClip[] metalHit;
    public AudioClip[] stoneHit;


    public AudioClip[] shell;
    public AudioClip Breath;

    public AudioClip Inventory;
}
