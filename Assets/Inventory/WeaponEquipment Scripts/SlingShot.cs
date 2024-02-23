using UnityEngine;

public class SlingShot : MonoBehaviour, IWeapon
{
    [SerializeField] private ItemScriptableObject weaponScriptableObject;
    [SerializeField] private GameObject arrowPrefab;


    public ItemScriptableObject GetWeaponInfo()
    {
        return weaponScriptableObject; // dung de truy cap vao cac gia tri trong SOb
    }

    public void Attack()
    {
        Debug.Log("SlingShot duoc kich hoat tan cong tu Activeweapon.cs through Interface");
        var newArrow = Instantiate(arrowPrefab, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        newArrow.GetComponent<FireBullet>().UpdateProjectileRange(weaponScriptableObject.weaponRange);

        newArrow.GetComponent<FireBullet>().SetDir_ArrowBullet(PlayerController.Instance.GetMoveDir);

    }
}
