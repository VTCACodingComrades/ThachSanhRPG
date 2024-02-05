using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    const string SLIDER_HEALTH =  "Slider Health";
    private Slider healthSlider;
    
    public bool isDead {get; private set;}
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damageRecoveryTime = 1f;
    private Animator animator;
    private bool canTakeDamage = true;
    public int CurrentHealth {get{return currentHealth;}}
    public int SetCurrentHealth(int health) {
        return this.currentHealth = health;
    }

    protected override void Awake() {
        base.Awake();

    }

    private void Start() {
        isDead = false;
        currentHealth = maxHealth;
        //animator = GetComponent<Animator>();
        UpdateHealthSilder();
    }
    private void Update() {
        UpdateHealthSilder();
    }

    public void AddHealthPlayer() {
        if(currentHealth < maxHealth) {
            currentHealth += 1;
            UpdateHealthSilder();
        }
    }
        
    private void UpdateHealthSilder()
    {
        if(healthSlider == null) {
            healthSlider = GameObject.Find(SLIDER_HEALTH).GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    //? neu co va cham voi collison2D + other getcomponent cua enemyAi => - mau
    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemyAI = other.gameObject.GetComponent<EnemyAI>();
        LogEnemy logEnemy = other.gameObject.GetComponent<LogEnemy>();
        if(enemyAI || logEnemy) {
            Debug.Log("player touch enemyAi.cs || logEnemy.cs");
            TakeDamage(1, other.transform);
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform) {
        if(!canTakeDamage) return; // neu con delay time chua bi tru mau
        //goi ham playerKnockBack 3Dcontroller

        // goi ham flash sang len chop tat

        canTakeDamage = false;
        currentHealth -= damageAmount;
        Debug.Log("currenthealth = "+ currentHealth);
        StartCoroutine(DamageReoveryRoutine());

        UpdateHealthSilder();
        CheckPlayerDeath();
    }
    private void CheckPlayerDeath() {
        if(currentHealth <= 0 && !isDead) {

            isDead = true;
            currentHealth = 0;
            
            //xet Die animaiton
            //animator.SetTrigger("Die");
        }
    }
    IEnumerator DamageReoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

}
