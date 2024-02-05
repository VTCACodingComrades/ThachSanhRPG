using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyHealthBar enemyHealthBar;
    [SerializeField] private GameObject pfEnemyDeathAnimation;
    [SerializeField] int startHealth = 3;
    [SerializeField] int currentHealth;

    private void Start() {
        currentHealth = startHealth;
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    public void TakeDamage(int damage) // tre nmui ten co damage source de tru mau eneymy
    {
        currentHealth -= damage;

        enemyHealthBar.SetHealthBarEnemyPercent((float)currentHealth / startHealth);

        // knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        // StartCoroutine(flash.FlashRoutine()); //todo change white - defaultMat
        StartCoroutine(CheckDetecDeathRoutine());
        Debug.Log("enemy health: " + currentHealth);
    }

    private IEnumerator CheckDetecDeathRoutine() {
        yield return new WaitForSeconds(0.1f); // detect die cham lai
        DetecDeath();
    }

    private void DetecDeath(){
        
        if(currentHealth <=0){
            enemyHealthBar.gameObject.SetActive(false);

            Instantiate(pfEnemyDeathAnimation, transform.position, transform.rotation, this.gameObject.transform.parent);
            
            //Instantiate(deathVFXPrefab,transform.position + Vector3.up,Quaternion.identity);

            //GetComponent<PickupInstantiate>().ItemsDrop(); // truoc khi enemy die se vang ra item

            // if(TryGetComponent(out PickupInstantiate pickupInstantiate)) {
            //     pickupInstantiate.ItemsDrop();
            // }
            Destroy(this.gameObject);
        } 
    }
}
