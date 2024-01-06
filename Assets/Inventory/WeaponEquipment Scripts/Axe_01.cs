using UnityEngine;

public class Axe_01 : MonoBehaviour,IWeapon
{
    [SerializeField] private ItemScriptableObject weaponScriptableObject;

    
    public ItemScriptableObject GetWeaponInfo()
    {
        return weaponScriptableObject; // dung de truy cap vao cac gia tri trong SOb
    }
    public void Attack()
    {
        Debug.Log("Axe duoc kich hoat tan cong tu Activeweapon.cs through Interface");
        // gi de override animator cua Sword len Player
    }
}
