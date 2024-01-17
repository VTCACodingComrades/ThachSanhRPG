using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogEnemy : Enemy
{
    public float detectRadius;
    public float attackRadius;
    public Transform target;
    public float moveSpeed;

    private Animator enemyAnimator;
    private Vector3 initialPosition;
    private bool isSleeping = true;

    [SerializeField] Transform firePos;

    [SerializeField] GameObject projectilePf;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
        target = GameObject.Find("Player").transform;
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (isSleeping)
        {
            enemyAnimator.SetBool("WakeUp", false);
        }
        if (Vector3.Distance(transform.position, target.position) <= detectRadius)
        {
            isSleeping = false;
            enemyAnimator.SetBool("WakeUp", true);
            if (Vector3.Distance(transform.position, target.position) > attackRadius)
            {
                //MoveTo(target.position);
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                Vector2 moveDirction = (target.position - transform.position).normalized;
                enemyAnimator.SetFloat("MoveX", moveDirction.x);
                enemyAnimator.SetFloat("MoveY", moveDirction.y);
                //enemyAnimator.SetBool("Attack", false);
            }
            else
            {
                FacingTo(target.position);
                enemyAnimator.SetTrigger("Attack");
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

    private void FacingTo(Vector3 targetPosition)
    {
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        Vector2 moveDirction = (targetPosition - transform.position).normalized;
        enemyAnimator.SetFloat("MoveX", moveDirction.x);
        enemyAnimator.SetFloat("MoveY", moveDirction.y);
    }

    public void Fire()
    {
        GameObject projectile = Instantiate(projectilePf, firePos.transform.position, Quaternion.identity);
        //projectile.GetComponent<Projectile>().LaunchProjectile();
    }
}

//if (Vector3.Distance(transform.position, target.position) <= detectRadius)
//{
//    enemyAnimator.SetBool("WakeUp", true);
//    transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
//    Vector2 moveDirction = (target.position - transform.position).normalized;
//    enemyAnimator.SetFloat("MoveX", moveDirction.x);
//    enemyAnimator.SetFloat("MoveY", moveDirction.y);
//}
//else if (Vector3.Distance(transform.position, target.position) < attackRadius)
//{           
//    Debug.Log("Attack ne");
//}

