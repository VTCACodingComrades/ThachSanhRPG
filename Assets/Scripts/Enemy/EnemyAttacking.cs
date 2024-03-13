using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    public event Action<string> OnCustomEvent = s => { };
    public Transform Edge;
    public int thurstForce;
    public int damage;


    /// <summary>
    /// Listen animation events to determine hit moments.
    /// </summary>
    public void Start()
    {
        OnCustomEvent += OnAnimationEvent;
    }

    public void OnDestroy()
    {
        OnCustomEvent -= OnAnimationEvent;
    }

    private void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "Hit":
                Debug.Log("Boss enemy hit something ne");
                Collider2D[] hitColliders = Physics2D.OverlapBoxAll(Edge.position, Edge.localScale, 0);

                foreach (Collider2D hitCollider in hitColliders)
                {

                    if (hitCollider.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("Boss Enemy hit player ne");
                        Rigidbody2D enemyRb = hitCollider.gameObject.GetComponent<Rigidbody2D>();
                        Vector2 direction = (hitCollider.transform.position - transform.position).normalized * thurstForce;
                        enemyRb.AddForce(direction, ForceMode2D.Impulse);
                        hitCollider.GetComponent<PlayerHealth>().TakeDamage(damage, transform);
                    }

                }
                break;
            default: return;
        }
    }
}
