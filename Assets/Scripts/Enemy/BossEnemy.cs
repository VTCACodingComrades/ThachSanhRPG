using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    private Animator enemyAnimator;
    public AnimatorOverrideController overrideControllers;
    public Transform target;
    public float detectRadius = 6;
    public float attackRadius = 3;
    public float moveSpeed = 1;
    private bool isSleeping = true;
    private Vector3 initialPosition;
    private float attackCooldown = 1f;
    private float timeSinceLastAttack;
    private EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.runtimeAnimatorController = overrideControllers;
        target = GameObject.Find("Player").transform;
        timeSinceLastAttack = attackCooldown;
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.OnDie.AddListener(Die);
    }

    //private void OnEnable()
    //{
        
    //}

    private void OnDisable()
    {
        enemyHealth.OnDie.RemoveListener(Die);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (target.GetComponent<PlayerHealth>().isDead) return;
        if (enemyHealth.IsDie()) return;
        if (isSleeping)
        {
            enemyAnimator.SetBool("Walk", false);
        }
        if (Vector3.Distance(transform.position, target.position) <= detectRadius)
        {
            isSleeping = false;
            //enemyAnimator.SetBool("WakeUp", true);
            if (Vector3.Distance(transform.position, target.position) > attackRadius)
            {
                //MoveTo(target.position);
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                Vector2 moveDirction = (target.position - transform.position).normalized;
                if (moveDirction != Vector2.zero)
                {
                    
                    enemyAnimator.SetBool("Walk", true);
                    enemyAnimator.SetFloat("MoveX", moveDirction.x);
                    enemyAnimator.SetFloat("MoveY", moveDirction.y);
                }
                else
                {
                   enemyAnimator.SetBool("Walk", false);
                }
            }
            else
            {
                if (timeSinceLastAttack >= 0)
                {
                    timeSinceLastAttack -= Time.deltaTime;
                    enemyAnimator.SetBool("Walk", false);
                }
                else
                {
                    timeSinceLastAttack = attackCooldown;
                    enemyAnimator.SetTrigger("Attack");
                }
               
            }
        }
        else if (Vector3.Distance(transform.position, target.position) > detectRadius && !isSleeping)
        {
            MoveTo(initialPosition);
            if (Vector3.Distance(transform.position, initialPosition) < 0.01)
            {
                isSleeping = true;
            }
        }
    }

    private void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        Vector2 moveDirction = (targetPosition - transform.position).normalized;
        enemyAnimator.SetFloat("MoveX", moveDirction.x);
        enemyAnimator.SetFloat("MoveY", moveDirction.y);
    }

    private void Die()
    {
        enemyAnimator.SetBool("IsDie", true);
    }
}
