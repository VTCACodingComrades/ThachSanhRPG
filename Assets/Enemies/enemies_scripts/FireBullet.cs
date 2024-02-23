using UnityEngine;

public class FireBullet : MonoBehaviour
{
    //todo gameObject = vien dan cua enemy + vien dan cua player
    [SerializeField] float moveSpeed = 25f;
    [SerializeField] private GameObject particleOnHitPrefabs;
    [SerializeField] private bool isEnemyProjectile = false; //? bullet cua enemy
    //private WeaponInfo weaponInfo; // thay bang projectileRange
    [SerializeField] private float projectileRange = 10f; //? pham vi dan cua enemy
    [SerializeField] private Vector2 dir;
    private Vector3 startPos;
    private void Start() {
        startPos = transform.position;
    }
    private void Update() {
        
        MoveProjectile();
        DetecFireRange(); // do khoang cach vi tri mui ten bat dau va realtime
    }
    public void SetDir_ArrowBullet(Vector2 dir) {
        this.dir = dir;
    }

    public void UpdateProjectileRange(float projectileRange) //WeaponInfo weapon
    {
        //this.weaponInfo = weapon;
        this.projectileRange = projectileRange;
    }
    public void MoveProjectile()
    {
        if(isEnemyProjectile) transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        
        if(!isEnemyProjectile) {
            transform.Translate(new Vector3(dir.x, dir.y, 0) * Time.deltaTime * moveSpeed);
        }
    }

    //todo co su va cham cua mui ten Trigger voi cac vat khac (co the trigger hoac ko trigger). 
    //todo do tai cho va cham mui ten da trigger=> co bien other sinh ra
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        InDestructible indestructible = other.gameObject.GetComponent<InDestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        /* if(!other.isTrigger && player) {
            if(player && isEnemyProjectile && !player.isDead) {

                Debug.Log("Player take damage");
                player?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabs, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        } */

        if(!other.isTrigger && (enemyHealth || player || indestructible)) // mui ten trigger va cham voi 1 vat ko trigger va (|| indestructible )
        {
            if((player && isEnemyProjectile && !player.isDead) || (enemyHealth && !isEnemyProjectile) ) // neu vien dan cham vao player && vien dan cua enemy
            {
                //player take damage
                Debug.Log("Player take damage");
                player?.TakeDamage(1, transform);

                Instantiate(particleOnHitPrefabs, transform.position, transform.rotation);
                Destroy(gameObject);
            } 
            else if (!other.isTrigger && indestructible) // mui ten trigger, va cham voi tang da ko trigger va co chua indestructible.cs
            {
                Instantiate(particleOnHitPrefabs, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void DetecFireRange(){
        if(Vector3.Distance(transform.position, startPos) > projectileRange) //weaponInfo.weaponRange
        {
            //tao hieu ung o day truoc khi destroy

            Destroy(gameObject);
        }
    }




}
