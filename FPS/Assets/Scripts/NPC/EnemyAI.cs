using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    public GameObject headd;

    public NPCMoveController npcMoveController;
    public Collider[] targetsInViewRadius;

    public int calmVisible;
    public int agrVisible;
    public int angleView;
    public LayerMask findEnemyMask;
    public LayerMask trackEnemyMask;
    public Collider col;

    public Transform target;
    public Transform targetMemory;


    Transform head;
    Animator anim;
    EnemyStat enemyStat;

    public Vector3 lastPositionTarget;
    public Vector3 enemy;
    public Vector3 player;
    public Transform mainTransform;
    public bool detectedFirst;


    void Update()
    {
        //if(target == null)
        //player = target.position;
        

        if (target != null)
        {
            
            lastPositionTarget = targetMemory.position;
            headd.transform.LookAt(target);
        }
        enemy = mainTransform.position;

        if (target == null && targetMemory == null && enemyStat.Live == true)
        {
            npcMoveController.Patrol();
            
        }

        if (target != null && targetMemory && enemyStat.Live == true)
        {
            Detected();
            Fight();

        }
        if (target == null && targetMemory && enemyStat.Live == true)
        {

            Persuit();
        }

        void Detected()
        {
            if (detectedFirst == true)
            {
                anim.SetTrigger("Detected");
                npcMoveController.Stop(); 
               
                detectedFirst = false;
               

            }

        }

    }


    void Start()
    {
        StartCoroutine("FindTarget", Random.Range(0.2f, 0.3f));
        enemyStat = GetComponent<EnemyStat>();

        anim = enemyStat.anim;
        head = anim.GetBoneTransform(HumanBodyBones.Head).transform;
        
    }



    #region  Поиск врага
    IEnumerator FindTarget(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
                findTargetRayCast();
            
        }
    }
   
    void findTargetRayCast()
    {
        targetsInViewRadius = Physics.OverlapSphere(transform.position, calmVisible, findEnemyMask);

        Collider temp;
        for(int i=0; i < targetsInViewRadius.Length; i++)
        {
            for(int j = i + 1; j < targetsInViewRadius.Length; j++)
            {
                float dist1 = Vector3.Distance(transform.position, targetsInViewRadius[i].transform.position);
                float dist2 = Vector3.Distance(transform.position, targetsInViewRadius[j].transform.position);
                if (dist1 > dist2)
                {
                    temp = targetsInViewRadius[i];
                    targetsInViewRadius[i] = targetsInViewRadius[j];
                    targetsInViewRadius[j] = temp;
                }
            }
        }

        if(target == null)
        {
            
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                if(npcMoveController.AngleToPoint(targetsInViewRadius[i].transform.position) <= angleView && npcMoveController.AngleToPoint(targetsInViewRadius[i].transform.position) >= -angleView)
                    {
                    Ray ray = new Ray(head.position, targetsInViewRadius[i].transform.position - head.position);

                    RaycastHit[] hit = Physics.RaycastAll(ray, trackEnemyMask).OrderBy(h => h.distance).ToArray();

                    for (int j = 0; j < hit.Length; j++)
                    {

                        Debug.DrawRay(head.position, (targetsInViewRadius[i].transform.position) - head.position, Color.red, 2);

                        if (hit[j].transform == transform)

                            continue;
                        else if (hit[j].transform.tag !="Player")
                        {
                            break;
                        }
                        else if (hit[j].transform.tag == "Player")
                        {
                                target = targetsInViewRadius[i].transform;
                                targetMemory = target;
                            
                                                        
                                break;
                        }
                    }
                }
            }
        }
        else
        {
            if(Vector3.Distance(target.position, head.position) <= agrVisible)
            {
                if (npcMoveController.AngleToPoint(target.position) <=angleView && npcMoveController.AngleToPoint(target.position) >= - angleView)
                {
                    Debug.DrawRay(head.position, (target.position) - head.position, Color.red, 2f);

                    Ray ray = new Ray(head.position, (target.position) - head.position);
                    RaycastHit[] hit = Physics.RaycastAll(ray, 5, trackEnemyMask).OrderBy(h => h.distance).ToArray();

                    for(int j = 0; j < hit.Length; j++)
                    {
                        if (hit[j].transform == transform)
                            continue;
                        else if (hit[j].transform == target)
                        
                                {
                                    break;
                                }
                        else
                        {
                            //targetMoment = target;
                            targetMemory = target;
                            target = null;
                            break;
                            Debug.Log("Pox2");
                        }
                    }
                }
                else
                {
                    target = null;
                    Debug.Log("Pox2");
                }
            }
            else
            {
                target = null;
                Debug.Log("Pox3");
            }
        }
    }

    #endregion
    void Persuit()
    {
        if (Vector3.Distance(mainTransform.position, lastPositionTarget) > 1.3f)
        {
            npcMoveController.MoveToPoint(lastPositionTarget);
            if (targetMemory != null && target == null)
            {
                if (enemy.x == lastPositionTarget.x)
                {
                    if (enemy.z == lastPositionTarget.z)
                    targetMemory = null;
                    Debug.Log("No");
                }
            }

        }
        else
        {
            targetMemory = null;
            Debug.Log("Pox4");
        }


    }
           
    public void Fight()
    {

        player = target.position ;
        if(target != null)
        npcMoveController.MoveToPoint(player);
        {
            if (Vector3.Distance(mainTransform.position, player) < 1.8f)
            {
                npcMoveController.Stop();
                anim.SetTrigger("Attack");
                angleView = 360;
                npcMoveController.MoveToPoint(player);

            }
             else
            {
                npcMoveController.MoveToPoint(player);
                angleView = 90;
            }
            
        }
    }
  
}