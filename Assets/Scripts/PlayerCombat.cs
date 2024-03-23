using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Animator animator;
    private float timePassed = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            animator.SetBool("IsAttacking", true);
        }
        timePassed += Time.deltaTime;
        if (timePassed >= 1.5f)
        {
            animator.SetBool("IsAttacking", false);
            timePassed = 0f;
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().Die();
            Debug.Log("Hit enemy.");
        }
    }

}
