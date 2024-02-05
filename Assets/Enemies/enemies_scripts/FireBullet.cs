using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 25f;
    [SerializeField] private GameObject particleOnHitPrefabs;
    [SerializeField] private bool isEnemyProjectile = false; //? bullet cua enemy
    //private WeaponInfo weaponInfo; // thay bang projectileRange
    [SerializeField] private float projectileRange = 10f; //? pham vi dan cua enemy
    private Vector3 startPos;
    private void Start() {
        startPos = transform.position;
    }
    private void Update() {
        
        MoveProjectile();
        DetecFireRange(); // do khoang cach vi tri mui ten bat dau va realtime
    }
    public void UpdateProjectileRange(float projectileRange) //WeaponInfo weapon
    {
        //this.weaponInfo = weapon;
        this.projectileRange = projectileRange;
    }
    public void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    //todo co su va cham cua mui ten Trigger voi cac vat khac (co the trigger hoac ko trigger). 
    //todo do tai cho va cham mui ten da trigger=> co bien other sinh ra
    private void OnTriggerEnter2D(Collider2D other)
    {
        //EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        //Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if(!other.isTrigger && player) {
            if(player && isEnemyProjectile && !player.isDead) {

                Debug.Log("Player take damage");
                player?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabs, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

        // if(!other.isTrigger && (enemyHealth || indestructible || player)) // mui ten trigger va cham voi 1 vat ko trigger va (|| )
        // {
        //     if((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile) ) // neu vien dan cham vao player && vien dan cua enemy
        //     {
        //         //player take damage
        //         player?.TakeDamage(1, transform);

        //         Instantiate(particleOnHitPrefabs, transform.position, transform.rotation);
        //         Destroy(gameObject);
        //     } 
        //     else if (!other.isTrigger && indestructible) // mui ten trigger, va cham voi tang da ko trigger va co chua indestructible.cs
        //     {
        //         Instantiate(particleOnHitPrefabs, transform.position, transform.rotation);
        //         Destroy(gameObject);
        //     }
        // }
    }

    private void DetecFireRange(){
        if(Vector3.Distance(transform.position, startPos) > projectileRange) //weaponInfo.weaponRange
        {
            //tao hieu ung o day truoc khi destroy
            
            Destroy(gameObject);
        }
    }




}
