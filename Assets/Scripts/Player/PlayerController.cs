using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public enum PlayerState
{
    idle,
    attack,
    interact,
    push
}

public class PlayerController : Singleton<PlayerController>
{
    private Inventory inventory; // se duoc Awake() goi de khoi tao new inventory
    [SerializeField] UI_Inventory ui_Inventory; // dung de goi ham SetInventoy()
    [SerializeField] private float moveSpeed;
    [SerializeField] WeaponSO defaultWeapon;

    private Vector2 moveAmount;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private PlayerState currentState;
    private WeaponSO currentWeapon;

    protected override void Awake()
    {
        base.Awake();
        
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        
        inventory = new Inventory(UseItem); // => khoi tao Inventory() => itemList
        
        //inventory = new Inventory();

        ui_Inventory.SetPlayerPos(this); // uiInventory lay vi tri player

        CameraController.Instance.SetPlayerCameraFollow();
    }

    private void Start()
    {
        //inventory.useItemAction += UseItem;
        currentState = PlayerState.idle;
        if (currentWeapon == null)
        {
            EquipWeapon(defaultWeapon);
        }
        ui_Inventory.SetInventory(inventory); // bien inventory in doi tuong UI_Inventory duoi canvas da duoc gan gia tri
    }

    private void Update() {
        //Debug.Log("test: " + inventory.GetItemList().Count);
        //Debug.Log("in gia tri inventory "+inventory);
    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.idle)
            Move();
        UpdateAnimation();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started && currentState != PlayerState.attack)
            Attack();
    }
    
    private void Move()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + moveAmount * moveSpeed * Time.deltaTime);
    }

    public void EquipWeapon(WeaponSO weapon)
    {
        currentWeapon = weapon;
        Animator animator = GetComponent<Animator>();
        weapon.Spawn(animator);
    }
    private void Attack()
    {
        currentState = PlayerState.attack;
        playerAnimator.SetTrigger("Attack");
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.3f);
        currentState = PlayerState.idle;
    }

    private void UpdateAnimation()
    {
        if (moveAmount != Vector2.zero && currentState == PlayerState.idle)
        {
            playerAnimator.SetBool("Walk", true);
            playerAnimator.SetFloat("MoveX", moveAmount.x);
            playerAnimator.SetFloat("MoveY", moveAmount.y);
        }   
        else
        {
            playerAnimator.SetBool("Walk", false);
        }    
    }

    //? Pushable
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            currentState = PlayerState.push;
            playerAnimator.SetBool("Pushed", true);          
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            //currentState = PlayerState.push;
            //animator.SetTrigger("Push");
            //animator.SetBool("Pushed", true);
            Move();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerAnimator.SetBool("Pushed", false);
        StartCoroutine(ResetAttack());
    }


    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void UseItem(Item item)
    {
        switch (item.itemScriptableObject.itemType) //? ~ item.itemType
        {
            case Item.ItemType.HealthPotion: // itemType ben ngoai Item de kiem tra itemtype ben trong SOb
                {
                    Debug.Log("su dung HealthPotion");
                    inventory.RemoveItem(new Item { itemScriptableObject = item.itemScriptableObject, amount = 1 });
                    break;
                }
            case Item.ItemType.ManaPotion:
                {
                    Debug.Log("su dung ManaPotion");
                    inventory.RemoveItem(new Item { itemScriptableObject = item.itemScriptableObject, amount = 1 });
                    break;
                }
            case Item.ItemType.Sword_01:
                {
                    //? trang bi sword len nguoi player, kho cho trang bi nua
                    Debug.Log("su dung Sword_01");
                    ChangeWeapon(item);
                    break;
                }
            case Item.ItemType.Sword_02:
                {
                    //? trang bi sword len nguoi player, kho cho trang bi nua
                    Debug.Log("su dung Sword_02");
                    ChangeWeapon(item);
                    break;
                }
            case Item.ItemType.Axe:
                {
                    //? trang bi sword len nguoi player, kho cho trang bi nua
                    Debug.Log("su dung Axe");
                    ChangeWeapon(item);
                    break;
                }
            case Item.ItemType.Hand:
                {
                    //? trang bi sword len nguoi player, kho cho trang bi nua
                    Debug.Log("su dung Hand");
                    ChangeWeapon(item);
                    break;
                }

        }
    }
    private void ChangeWeapon(Item item)
    {
        // if(ActiveWeapon.Instance.CurrenActiveWeapon != null) {
        //     Destroy(ActiveWeapon.Instance.CurrenActiveWeapon.gameObject);
        // }

        foreach (Transform child in ActiveWeapon.Instance.transform)
        {
            Destroy(child.gameObject);
            Debug.Log("co xoa");
        }

        GameObject newWeapon = item.itemScriptableObject.pfSword;
        Instantiate(newWeapon, ActiveWeapon.Instance.transform);
        //newWeapon.GetComponentInChildren<SpriteRenderer>().sprite = null;

        //? tao image loai vu khi dang trang bi tren gnuoi player
        newWeapon.transform.GetChild(0).transform.localPosition = new Vector3(0, 0.4f, 0);
        newWeapon.transform.GetChild(0).transform.localEulerAngles = new Vector3(0,0,45f);
        newWeapon.transform.GetChild(0).transform.localScale = new Vector3(.1f, .1f, .1f);


        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>()); // bo script vao
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            // lay ve doi tuong item Item.cs ( game object = vat pham pfItemWord vua louch)
            inventory.AddItem(itemWorld.GetItem()); //todo add item vat pham vao trong itemsList => tang them 1 vat pham
            itemWorld.DestroySelf();
        }

        // WeaponInfoWorld weaponInfoWorld = other.GetComponent<WeaponInfoWorld>();
        // if(weaponInfoWorld != null) {
        //     inven.AddWeaponInfo(weaponInfoWorld.GetItem(), weaponInfoWorld.amount);
        //     weaponInfoWorld.DestroySelf();
        // }
    }

    public Inventory GetPlayerInventory()
    {
        return inventory;
    }

}

