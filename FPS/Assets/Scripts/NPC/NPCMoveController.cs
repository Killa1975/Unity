using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMoveController : MonoBehaviour
{
    public Transform[] movePoint;

    NavMeshAgent agent;
    Animator anim;

    public bool circularRoute;
    public int curWaypoint;

    public float distance;
    public float speed;
    public Vector3 moveToPoint;
    public Transform player;

    public float runSpeed;
    public float walkSpeed;
    public float rotationSpeed;
    public int Damage;
    public bool fast;

    public EnemyStat enemyStat;
    public Stat stat;

    void Start()
    {
        moveToPoint = transform.position;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;

    }
   
    public void Patrol()
    {
        if (movePoint.Length >= 1) 
        {
            if (movePoint.Length > curWaypoint)
            {
                if (Vector3.Distance(transform.position, movePoint[curWaypoint].position) < 1f)
                {
                    Debug.Log("Way");
                    curWaypoint++;
                }
                else
                {
                    MoveToPoint(movePoint[curWaypoint].position);
                   
                }
            }
            else if (movePoint.Length == curWaypoint)
            {
                Debug.Log("0");
                if (circularRoute)
                {
                    if (movePoint.Length >= 1)
                        curWaypoint = 0;
                    else circularRoute = false;
                }
                else
                {
                    if (Vector3.Distance(transform.position, movePoint[curWaypoint - 1].position) < 1f)
                    {
                        Stop();
                    }
                    else
                    {
                        MoveToPoint(movePoint[curWaypoint - 1].position);
                    }
                }
            }

        }
        else
        {
            Stop();
            //Debug.Log("Stop");
        }
    }


    public void MoveToPoint(Vector3 position)
    {
        if(moveToPoint != position)
        {
            agent.SetDestination(position);
            moveToPoint = position;
        }
        if (agent.hasPath)
        {
            if (fast)
                speed = Mathf.MoveTowards(speed, runSpeed, Time.deltaTime * 3);
            else speed = Mathf.MoveTowards(speed, walkSpeed, Time.deltaTime * 3);

            anim.SetFloat("Speed", speed);
            agent.speed = speed;
            RotationToTarget(agent.path.corners[1]);
        }
        else RotationToTarget(moveToPoint);
    }

    public float AngleToPoint(Vector3 position)
    {
        Vector3 targetPos = position;
        targetPos.y = transform.position.y;
        Vector3 targetDir = targetPos - transform.position;
        Vector3 forward = transform.forward * 0.001f;
        float angleBetween = Vector3.SignedAngle(targetDir, forward, Vector3.up);

        return angleBetween * -1;
    }


    public bool RotationToTarget(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        //Debug.Log(direction.x);
        //Debug.Log(direction.z);
        if (transform.rotation == lookRotation)
        {
            return true;
        }
        else return false;
    }

    public void Stop()
    {
        if(speed >0)
        {
            agent.SetDestination(transform.position);
            speed = Mathf.MoveTowards(speed, 0, Time.deltaTime * 15);
            anim.SetFloat("Speed", speed);
            agent.speed = speed;
        }
    }


    public void DamageEnemy()
    {
        Damage = 10;
        stat.TakeAwayHealth();
    }

}

