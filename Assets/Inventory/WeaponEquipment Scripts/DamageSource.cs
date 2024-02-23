
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    //! gan tren vat se cham enemy va tru mau enemy
    [SerializeField] int damageAmount = 1;

    private void Start() {
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrenActiveWeapon;
        damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().damage;
    }
    
    //? kiem tao ra collision slash(damageSource.cs) -> collision cham enemy - check trigger (collison on of khi chem vs enemy)
    //? goi ham TakeDmage(tru mau + getnockback + kiem tra chet chua) trong enemyHealth.cs
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(damageAmount);
    }
}
