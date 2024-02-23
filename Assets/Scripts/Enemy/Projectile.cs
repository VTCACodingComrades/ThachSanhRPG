using UnityEngine;

public class Projectile : MonoBehaviour
{
    //[SerializeField] float moveSpeed = 25f;
    [SerializeField] private GameObject particleOnHitPrefabs;
    [SerializeField] private bool isEnemyProjectile = false; //? bullet cua enemy
    [SerializeField] private float movingSpeed;
    public float lifeTime;
    public float initialLifeTime;
    Rigidbody2D rb;

    void Start()
    {
        lifeTime = initialLifeTime;
        rb = GetComponent<Rigidbody2D>();
        Transform target = FindObjectOfType<PlayerController>().transform;
        Vector2 movingDirection = (target.transform.position - gameObject.transform.position).normalized;
        LaunchProjectile(movingDirection);
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void LaunchProjectile(Vector2 direction)
    {
        rb.velocity = direction * movingSpeed;
    }

    /* private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            Destroy(gameObject, 0.2f);
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            Destroy(gameObject, 0.1f);
        }
        else
        {
            Destroy(gameObject);
        }
    } */

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        //neu other cham gameobject ko phai trigger (player dang !isTrigger)
        //co the dung vien dan nay de so sanh voi nhung game object khac de tao hieu ung khac nhau
        if(!other.isTrigger && playerHealth)
        {
            if(playerHealth && isEnemyProjectile && !playerHealth.isDead) {

                Debug.Log("Player take damage");
                playerHealth?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabs, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

    }
}
