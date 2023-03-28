using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellSound : MonoBehaviour
{ 
    public AudioSource source;
    public SoundManager soundManager;
    void Start()
    {
       
    }

    public void OnCollisionEnter(Collision coll)
    {
        string materialName = coll.gameObject.GetComponent<Collider>().sharedMaterial.name;

        switch (materialName)
        {


            case "Stone":
                source.PlayOneShot(soundManager.shell[Random.Range(0, soundManager.shell.Length)]);

                break;

        }

    }
}
    

