using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
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
        animator = GetComponent<Animator>();
    }
    private void OnCollisionStay2D(Collision2D other) //? neu co va cham voi collison2D + other getcomponent cua enemyAi => - mau
    {
        EnemyAI enemyAI = other.gameObject.GetComponent<EnemyAI>();
        if(enemyAI) {
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

        CheckPlayerDeath();
    }
    private void CheckPlayerDeath() {
        if(currentHealth <= 0 && !isDead) {

            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject); // fix loi player khi die tro ve town vi mat vu khi 27 activeInventory.cs
            currentHealth = 0;
            //GameController.Instance.GameOverSendData(); //todo collum36 Gamecontroller
            
        }
    }
    IEnumerator DamageReoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

}
