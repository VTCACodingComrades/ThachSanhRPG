using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyHealthBar enemyHealthBar;
    [SerializeField] private GameObject pfEnemyDeathAnimation;
    [SerializeField] int startHealth = 3;
    [SerializeField] int currentHealth;
    [SerializeField] private float timeToDestroyEnemies = 0.5f;
    private bool isDie = false;
    public UnityEvent OnDie;
    public TakeDamageEvent OnTakeDamage;

    [Serializable]
    public class TakeDamageEvent : UnityEvent<float>
    {

    }
    private void Start() {
        currentHealth = startHealth;
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    public void TakeDamage(int damage) // tre nmui ten co damage source de tru mau eneymy
    {
        currentHealth -= damage;

        enemyHealthBar.SetHealthBarEnemyPercent((float)currentHealth / startHealth);

        OnTakeDamage?.Invoke(damage);
        // knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        // StartCoroutine(flash.FlashRoutine()); //todo change white - defaultMat
        StartCoroutine(CheckDetecDeathRoutine());
        //Debug.Log("enemy health: " + currentHealth);
    }

    public bool IsDie()
    {
        return isDie;
    }

    private IEnumerator CheckDetecDeathRoutine() {
        yield return new WaitForSeconds(0f); // detect die cham lai
        DetecDeath();
    }

    private void DetecDeath(){
        
        if(currentHealth <= 0 && !isDie){
            isDie = true;
            enemyHealthBar.gameObject.SetActive(false);
            StartCoroutine(SpawnDieEffect());
            //GameObject effect = Instantiate(pfEnemyDeathAnimation, transform.position, transform.rotation, this.gameObject.transform.parent);

            //Destroy(effect, 1f);

            OnDie?.Invoke();
            //Instantiate(deathVFXPrefab,transform.position + Vector3.up,Quaternion.identity);

            //GetComponent<PickupInstantiate>().ItemsDrop(); // truoc khi enemy die se vang ra item

            // if(TryGetComponent(out PickupInstantiate pickupInstantiate)) {
            //     pickupInstantiate.ItemsDrop();
            // }
            Destroy(this.gameObject, timeToDestroyEnemies); //todo xoa enemies object de thay duoc death animation cua enemies tam_comment
        } 
    }

    IEnumerator SpawnDieEffect()
    {
        yield return new WaitForSeconds(0);
        GameObject effect = Instantiate(pfEnemyDeathAnimation, transform.position, transform.rotation, this.gameObject.transform.parent);

        Destroy(effect, 3f);

    }
}
