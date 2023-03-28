 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    
     AudioSource source;
    public SoundManager soundManager;
    public LayerManager layerManager;


 Animator anim;

    private void Start()
    {
    anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void Step(string leg)
    {
        Vector3 startPoint;

        if(leg == "Left")
            startPoint = anim.GetBoneTransform(HumanBodyBones.LeftLowerLeg).position;
        else startPoint = anim.GetBoneTransform(HumanBodyBones.RightLowerLeg).position;

        RaycastHit hit;
        if (Physics.Linecast(startPoint, startPoint + -Vector3.up, out hit, layerManager.footStepMask ))
        {

            if (hit.collider.sharedMaterial != null)
            {

                string materialName = hit.collider.sharedMaterial.name;
              

                switch (materialName)
                {

                    case "Stone":
                        if(anim.GetBool("Man"))
                        {
                        source.PlayOneShot(soundManager.footStepsStone[Random.Range(0, soundManager.footStepsStone.Length)]);
                        }
                        else
                        source.PlayOneShot(soundManager.footStepsStone[Random.Range(0, soundManager.footStepsStone.Length)]);

                        break;

                    case "Wood":
                        if (anim.GetBool("Man"))
                        {
                           
                        source.PlayOneShot(soundManager.footStepsWoodMan[Random.Range(0, soundManager.footStepsWood.Length)]);
                        }
                        source.PlayOneShot(soundManager.footStepsWood[Random.Range(0, soundManager.footStepsWood.Length)]);

                        break;
                }
            }

        }
    }
}
