using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    private Animator enemyAnimator;
    public AnimatorOverrideController overrideControllers;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.runtimeAnimatorController = overrideControllers;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
