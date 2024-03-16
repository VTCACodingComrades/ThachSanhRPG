using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    const string SLIDER_HEALTH =  "Slider Health";
    private Slider healthSlider;
    
    public bool isDead {get; private set;}
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private float delayTimeToShowGameOverPanel = 1f;

    private Animator animator;
    private bool canTakeDamage = true;
    public int CurrentHealth {get{return currentHealth;}}
    public TakeDamageEvent OnTakeDamage;

    [Serializable]
    public class TakeDamageEvent : UnityEvent<float>
    {

    }
    public int SetCurrentHealth(int health) {
        return this.currentHealth = health;
    }
    public bool SetIsPlayerDeath(bool isPlayerDeath) => isDead = isPlayerDeath;

    protected override void Awake() {
        base.Awake();

    }

    private void Start() {
        isDead = false;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
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
        OnTakeDamage?.Invoke(damageAmount);
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
            canTakeDamage = false;
            //Destroy(PlayerHealth.Instance.gameObject);
            
            //xet Die animaiton
            animator.SetBool("IsDie", true);
    
            //? hien bang gameover
            //GameController.Instance.GameOverSendData();
            StartCoroutine(ShowGameOver());
        }
    }

    IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(delayTimeToShowGameOverPanel);
        GameController.Instance.GameOverSendData();
    }
    IEnumerator DamageReoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    public void ResetAnimation()
    {
        animator.SetBool("IsDie", false);
        canTakeDamage = true;
    }

}
