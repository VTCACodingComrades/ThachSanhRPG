using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    //? gameobject = player
    [SerializeField] private Animator playerAnimator;
    //public static ActiveWeapon Instance;
    [SerializeField] public MonoBehaviour CurrenActiveWeapon {get; private set;}
    [SerializeField] ItemScriptableObject DefaultWeapon;
    public AnimatorOverrideController overrideControllers;
    private string weaponName;
    private int weaponDamage;
    bool attackButton, isAttacking = false;
    public bool AttackButtonPress { get; private set; }

    private ThachSanh thachSanh;
    [SerializeField] private float timeBetweenAttacks = .5f;
    private AudioSource audioSource;
    private AudioClip weaponSound;
    protected override void Awake() 
    {
        base.Awake();
        thachSanh = new ThachSanh();
    }
    private void OnEnable() 
    {
		thachSanh.Enable();
    }

    private void OnDisable()
    {
        thachSanh.Player.Attack.started -= _ => StartAttacking();
        thachSanh.Disable();
    }

    private void Start() 
    {
        thachSanh.Player.Attack.started += _ => StartAttacking();
        thachSanh.Player.Attack.canceled += _ => StopAttacking();
        attackButton = false;

        playerAnimator = GetComponentInParent<Animator>();
        CurrenActiveWeapon = DefaultWeapon.pfSword.GetComponent<MonoBehaviour>();
        NewWeapon(CurrenActiveWeapon);
        audioSource = GetComponent<AudioSource>();
    }

    private void StopAttacking()
    {
        attackButton = false;
        isAttacking = false;
    }

    private void StartAttacking()
    {
        Debug.Log("Attack button kich hoat ne");
        attackButton = true;
        AttackCurrentWeapon();
    }

    //private void Update() {
    //    AttackCurrentWeapon(); //?Phuc comment || su dung tai day de khi acctack se goi ham Attack() SlingShot.cs coll 16
    //    //Debug.Log(weaponName);
    //}
    public void NewWeapon(MonoBehaviour newWeapon) // ham nay duoc goi ben ActiveInventory khi Instite vu khi va bo class weapon vao day
    {
        //Debug.Log("Co chay");
        CurrenActiveWeapon = newWeapon;
        // AttackCoolDown();
        // timeBetweenAttacks = (CurrenActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
        weaponName = (CurrenActiveWeapon as IWeapon).GetWeaponInfo().itemName;
        weaponDamage = (CurrenActiveWeapon as IWeapon).GetWeaponInfo().damage; //Phuc them
        overrideControllers = (CurrenActiveWeapon as IWeapon).GetWeaponInfo().animatorOverrideController;
        //Debug.Log(overrideControllers.name);
        playerAnimator.runtimeAnimatorController = overrideControllers; //-> Phuc them
        weaponSound = (CurrenActiveWeapon as IWeapon).GetWeaponInfo().sound;

    }
    public void WeaponNull() {
        CurrenActiveWeapon = null;
    }

    private void AttackCurrentWeapon()
    {
        // if (attackButton && !isAttacking && CurrenActiveWeapon)
        // {
        //     isAttacking = true;
        //     AttackCoolDown();
        //     (CurrenActiveWeapon as IWeapon).Attack(); //? vu khi dang equip se Attack() as IWeapon

        // }Input.GetKeyDown(KeyCode.Mouse0)
        if(attackButton && !isAttacking && CurrenActiveWeapon) {
            isAttacking = true;
            StopAllCoroutines();
            AttackCoolDown();
            (CurrenActiveWeapon as IWeapon).Attack();
            //playerAnimator.runtimeAnimatorController = overrideControllers;
            playerAnimator.SetTrigger("Attack");
            audioSource.PlayOneShot(weaponSound);
        }
    }
    private void SetAnimation() {
        //Debug.Log("Co chay");
        playerAnimator.runtimeAnimatorController = (CurrenActiveWeapon as IWeapon).GetWeaponInfo().animatorOverrideController;
    }
    
    public int GetWeaponDamage()
    {     
        return weaponDamage;
    }
    IEnumerator TimeBetweenAttackRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }
    private void AttackCoolDown() //? gia tri float coolDown quyet dinh khoang cach thuc hien hanh dong attack moi loai vu khi
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttackRoutine()); //isAttacking = false;
    }

    public string GetCurrentWeapon()
    {
        return weaponName;
    }

    public void ResetAttack()
    {
        StopAttacking();
    }
    
}
