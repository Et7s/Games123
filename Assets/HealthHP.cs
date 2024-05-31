using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHP : MonoBehaviour
{
    public int health;
    public int maxHealth;

    private void Start()
    {
        health = maxHealth;

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}
