using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding; 

public class EnemyAIController : MonoBehaviour
{
    public UnityEvent OnAttack;

    [SerializeField] private Transform target;
    [SerializeField] private float chaseDistance = 10f;
    //[SerializeField] private float attackDistance = 0.8f;
    //[SerializeField] private float attackDelay = 1.0f;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDistance = 1f;

    Path path;
    int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    //private float timeElapsed = 1f;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            float distanceToTarget = Vector2.Distance(target.position, transform.position);

            if (distanceToTarget < chaseDistance)
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position * 10).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
