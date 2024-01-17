using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float movingSpeed;
    public float lifeTime;
    public float initialLifeTime;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = initialLifeTime;
        rb = GetComponent<Rigidbody2D>();
        Transform target = FindObjectOfType<PlayerController>().transform;
        Vector2 movingDirection = (target.transform.position - gameObject.transform.position).normalized;
        LaunchProjectile(movingDirection);
    }

    // Update is called once per frame
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

    private void OnCollisionEnter2D(Collision2D collision)
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
}
