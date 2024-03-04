using UnityEngine;

public class FireShooter : MonoBehaviour, IEnemy
{
    //todo gameobject la enemy can ban vie ndan ve phia nhan vat
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform newBulletPawnPoint;
    private EnemyAnimation enemyAnimation;
    [SerializeField] private int damageBullet_FireShotter = 2;

    private void Awake() {
        enemyAnimation = GetComponent<EnemyAnimation>();
    }

    public void Attack()
    {
        Debug.Log("FireShooter Attack");
        enemyAnimation.SetState(EnemyState.Attack);
        enemyAnimation.animator.SetTrigger("Attack");

        // huong vector tu player - gameobject hien tai
        Vector2 targetDirectionPlayer = PlayerController.Instance.transform.position - transform.position;
        bulletPrefab.GetComponent<FireBullet>().damageBullet_FireShotter = this.damageBullet_FireShotter;
        GameObject newBullet = Instantiate(bulletPrefab, newBulletPawnPoint.transform.position, Quaternion.identity);
        newBullet.transform.right = targetDirectionPlayer;
    }

}
