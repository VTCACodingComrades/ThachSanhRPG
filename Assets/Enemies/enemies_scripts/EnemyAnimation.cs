using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;

	private void Awake() {
		animator = GetComponent<Animator>();
	}
	private void Start() {
		SetState(EnemyState.Idle);
	}
    private void Update() {

    }
    public void GetAction() => animator.SetBool("Action", false);
	
	public void ResetAnimation() // tra ve trang thai Idle
	{
		SetState(EnemyState.Idle);
		//UpdateAnimation();
	}
    public void SetSpeed(float speed) {
        animator.SetFloat("Speed",speed);
    }

	//public void GetReady() => animator.SetBool("Ready", true);

	//public void Relax() => animator.SetBool("Ready", false);
	
	//public bool IsReady() => animator.GetBool("Ready"); // kiem tra trang thai ready hay khong
	

	public void SetState(EnemyState state)
	{
		switch (state)
		{
			case EnemyState.Ready:
				//GetReady();
				state = EnemyState.Ready;
				break;
			case EnemyState.Relax:
				//Relax();
				state = EnemyState.Idle;
				break;
		}
		animator.SetInteger("State", (int) state); // chuyen qua kieu so gan vao bien State ben animator
	}

	public EnemyState GetState() => (EnemyState) animator.GetInteger("State");
	
}
