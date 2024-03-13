
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] GameObject destroyVFX;

    
    private void OnTriggerEnter2D(Collider2D other) // other chi sinh ra khi gameobject tac dong this.gameobject nay la trigger tai noi va cham
    {
        if(other.gameObject.GetComponent<DamageSource>() || 
        other.gameObject.GetComponent<PlayerHealth>()) // bien other sinh ra khi co va cham trigger(circle trigger tren dau player va cham this.gameobject)
        {
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
