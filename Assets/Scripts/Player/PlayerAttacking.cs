using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    [SerializeField] private MonoBehaviour enemyType; // scrip loai enemy
    //public AnimationEvents AnimationEvents;
    public event Action<string> OnCustomEvent = s => { };
    public Transform Edge;
    public int thurstForce;
    private int damage;
    //private int hitCount = 0;
    //[SerializeField] GameObject slashEffect;
    //[SerializeField] GameObject axeWeapon;
    //[SerializeField] Transform axePos;

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
                Collider2D[] hitColliders = Physics2D.OverlapBoxAll(Edge.position, Edge.localScale, 0);
                
                foreach (Collider2D hitCollider in hitColliders)
                {
                    Pot combatTarget = hitCollider.gameObject.GetComponent<Pot>();
                    if (combatTarget != null)
                        combatTarget.GetComponent<Pot>().Smash();

                    if (hitCollider.gameObject.CompareTag("CombatTarget"))
                    {
                        damage = ActiveWeapon.Instance.GetWeaponDamage();
                        Debug.Log(damage);
                        Rigidbody2D enemyRb = hitCollider.gameObject.GetComponent<Rigidbody2D>();
                        Vector2 direction = (hitCollider.transform.position - transform.position).normalized * thurstForce;
                        enemyRb.AddForce(direction, ForceMode2D.Impulse);
                        hitCollider.GetComponent<EnemyHealth>().TakeDamage(damage);
                    }

                    if(hitCollider.gameObject.GetComponent<EnemyAI>() != null) {
                        damage = ActiveWeapon.Instance.GetWeaponDamage();
                        Debug.Log("player hit enemyHealth");
                        hitCollider.GetComponent<EnemyHealth>().TakeDamage(damage);
                    }

                }
                break;
            //case "Throw":
            //    //Debug.Log("Throw axe ne");
            //    Instantiate(axeWeapon, axePos.position, Quaternion.identity);
            //    break;
            default: return;
        }
    }    
}
