using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Layer/Sound")]
public class LayerManager : ScriptableObject

{

    [Header("Footstep")]
    public LayerMask footStepMask;
}
