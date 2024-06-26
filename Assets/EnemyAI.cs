using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Animator animator;

    public float speed = 2f;
    public float nextWaypointDistance = 3f;
    public int maxHealth = 100;
    public int currentHealth;
    int dd;

    public Transform enemyGraph;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public HealthHP health;
    
    Seeker seeker;
    Rigidbody2D rb;

    public PlayerMovement PlayerMovement;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = currentHealth;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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
    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath =true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (force.x >= 0.01f)
        {
            enemyGraph.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= 0.01f)
        {
            enemyGraph.localScale = new Vector3(-1f, 1f, 1f);
        }
        float dd = enemyGraph.position.x - target.position.x;
        if (dd < 1.5f && dd > -1.5f)
        {
            animator.SetBool("Attack", true);
            PlayerMovement.TakeDamage(20);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
    
    public void TakeDamage(int amoint)
    {
        currentHealth -= amoint;
        if(currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }


}
