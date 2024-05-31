using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    public Transform attackPoint;

    public float attackRange = 0.5f;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public LayerMask enemyLayers;
    public EnemyAI enemyAI;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        

        animator.SetFloat("Horizontal", movement.x);
        
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemyAI.TakeDamage(20);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    

}
