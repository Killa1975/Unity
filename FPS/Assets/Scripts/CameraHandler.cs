using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class CameraHandler : MonoBehaviour
{
    public Transform camTrans;
    
    public Animator anim;
    public Transform pivot;
    public Transform Character;
    public Transform mTransform;
    public Animator FurnitureAnim;

    public CharacterStatus characterStatus;
    public CameraConfig cameraConfig;
    public CameraConfig PistoletFps;
    public CameraConfig M4Fps;
    public CameraConfig fps;
    public CameraConfig tps;
    public bool leftPivot;
    public bool FPS;
    
    public float delta;

    public Transform targetLook;

    public float mouseX;
    public float mouseY;
    public float smoothX;
    public float smoothY;
    public float smoothXVelocity;
    public float smoothYVelocity;
    public float lookAngle;
    public float titlAngle;

    public float vertical;
    public bool move;
    public bool WisibleHead;
    Transform MainCharacter;
    public GameObject pricel;
    public GameObject[] Head;

    public GameObject Text;
    public GameObject Furniture;
    public GameObject ChilderFurniture;
    public GameObject LightChilderFurniture;
    public AudioSource SwitchAudio;
    public void Start()
    {

        //GameObject[] Head = GameObject.FindGameObjectsWithTag("Head");

        MainCharacter = Character;
    }




    public void LateUpdate()
    {
        pricel = GameObject.FindGameObjectWithTag("Pricel");

        
        Tick();

        

        if(Input.GetKeyDown(KeyCode.E))
        {
            
            if (FPS == false) 
            {
               
                cameraConfig = fps;
                anim.SetBool("FPS", true);
               
                for (int i = 0; i < Head.Length; i++)
                {
                    Head[i].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;


                }


                FPS = true;
            }
            else
            {
                
                cameraConfig = tps;
                anim.SetBool("FPS", false);


                for (int i = 0; i < Head.Length; i++)
                {

                    Head[i].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.On;
                }

                FPS = false;
            }
        }




        //интересно но я не понял 
        //cameraConfig = FPS ? fps : tps;
        //anim.SetBool("FPS", cameraConfig == fps ? true : false);



        if ((anim.GetBool("zoom")) && (anim.GetBool("FPS")) && (anim.GetInteger("WeaponType") == 1))
        {
            cameraConfig = PistoletFps;
            Character = pricel.transform;

        }
        if ((anim.GetBool("zoom")) && (anim.GetBool("FPS")) && (anim.GetInteger("WeaponType") == 2))
        {
            cameraConfig = M4Fps;
            Character = pricel.transform;

        }
        if ((!anim.GetBool("zoom")) && (anim.GetBool("FPS")))
        {
            Character = MainCharacter;
            cameraConfig = fps;

        }
    }

    void Tick()
    {
        delta = Time.deltaTime;

        HandlePosition();
        HandleRotation();

        Vector3 targetPosition = Vector3.Lerp(mTransform.position, Character.position, 1);
        mTransform.position = targetPosition;

        TargetLook();
    }


    void TargetLook()
    {
            Ray ray = new Ray(camTrans.position, camTrans.forward * 2000);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetLook.position = Vector3.Lerp(targetLook.position, hit.point, Time.deltaTime * 40);
            }
            else
            {
                targetLook.position = Vector3.Lerp(targetLook.position, targetLook.transform.forward * 200, Time.deltaTime * 5);
            }

        {
            if (hit.collider.gameObject.tag == "SwitcherONOF")
            {
                Furniture = hit.collider.gameObject;
                ChilderFurniture = Furniture.transform.GetChild(0).gameObject;
                LightChilderFurniture = ChilderFurniture.transform.GetChild(0).gameObject;
                FurnitureAnim = Furniture.transform.GetChild(1).gameObject.GetComponent<Animator>();
                SwitchAudio = ChilderFurniture.GetComponent<AudioSource>();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SwitchAudio.Play();
                    LightChilderFurniture.SetActive(!LightChilderFurniture.activeSelf);
                    if (FurnitureAnim.GetBool("ON"))
                    {
                        FurnitureAnim.SetBool("ON", false);
                    }
                    else
                    {
                        FurnitureAnim.SetBool("ON", true);
                    }

                }


            }
            

        
            if (hit.collider.gameObject.tag == "Door")
            {
                Furniture = hit.collider.gameObject;
                FurnitureAnim = Furniture.GetComponent<Animator>();
                SwitchAudio = Furniture.GetComponent<AudioSource>();

                if (Input.GetKeyDown(KeyCode.F))
                {
                    SwitchAudio.Play();
                    if (FurnitureAnim.GetBool("DoorOpen"))
                    {   
                        FurnitureAnim.SetBool("DoorOpen", false);
                    }
                    else
                    {   
                        FurnitureAnim.SetBool("DoorOpen", true);
                    }

                }


            }
           


        }

        if ((hit.collider.gameObject.tag == "SwitcherONOF" || hit.collider.gameObject.tag == "Door"))
        {
            Text.SetActive(true);
        }
        else
        {
            Text.SetActive(false);
        }
    }


    void HandlePosition()
    {
        float targetX = cameraConfig.normalX;
        float targetY = cameraConfig.normalY;
        float targetZ = cameraConfig.normalZ;


        if (characterStatus.isAiming)
        {
            targetX = cameraConfig.aimX;
            targetZ = cameraConfig.aimZ;
            targetY = cameraConfig.aimY;
        }

        if (leftPivot)
        {
            targetX = -targetX;
        }

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = camTrans.localPosition;
        newCameraPosition.z = targetZ;
        newCameraPosition.y = targetZ;
        newCameraPosition.x = targetX;

        float t = delta * cameraConfig.pivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, newCameraPosition, t);
        vertical = anim.GetFloat("vertical");


        if (!anim.GetBool("sprint") && !anim.GetBool("FPS"))
        {
            cameraConfig.normalZ = -0.95f;
        }
        if (!anim.GetBool("sprint") && anim.GetBool("FPS"))
        {
            cameraConfig.normalZ = 0f;
        }

        if (move == true && anim.GetBool("sprint"))
        {
            cameraConfig.normalZ = cameraConfig.normalZ + 0.10f;
            move = false;
        }
        if (move == false && !anim.GetBool("sprint"))
            {
                move = true;
                cameraConfig.normalZ = cameraConfig.normalZ - 0.10f;
            }

        }
       
    void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (cameraConfig.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, cameraConfig.turnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, cameraConfig.turnSmooth);
        }
        else
        {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        lookAngle += smoothX * cameraConfig.Y_rot_speed;
        Quaternion targetRot = Quaternion.Euler(0, lookAngle, 0);
        mTransform.rotation = targetRot;

        titlAngle -= smoothY * cameraConfig.X_rot_speed;
        titlAngle = Mathf.Clamp(titlAngle, cameraConfig.minAngle, cameraConfig.maxAngle);
        pivot.localRotation = Quaternion.Euler(titlAngle, 0, 0);
    }
}