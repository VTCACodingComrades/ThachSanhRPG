using System;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float walkSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 moveDir;
	private EnemyAnimation enemyAnimation;


    private void Awake() {
		enemyAnimation = GetComponent<EnemyAnimation>();
        rb = GetComponent<Rigidbody2D>();
        speed = 0;
    }

    private void FixedUpdate() {
        EnemyMove();
        FlipEnemy();
    }
    public void MoveTo(Vector2 targetPos) {
		moveDir = targetPos;
	}

    private void FlipEnemy() {
		if(moveDir.x <= 0) {
			transform.localScale = new Vector3(-1,1,1);
		}
		else {
			transform.localScale = new Vector3(1,1,1);
		}
	}
    private void EnemyMove()
    {
        if(moveDir.magnitude == 0)
		{
			speed = 0f;
			enemyAnimation.SetState(EnemyState.Ready);
		}
		else if(moveDir.magnitude >= 0.1f) {
			speed = walkSpeed; // toc do di chuyen
			enemyAnimation.SetState(EnemyState.Walk); // xet trang thai animation trong animator
		}

		rb.MovePosition(rb.position + moveDir * (speed * Time.deltaTime));
    }
    public void StopMoving() // khi dung lai tan ocng, xet vi tri tuon gdoi player va enemy de enemy dao chieu
	{
		float diff = PlayerController.Instance.transform.position.x - gameObject.transform.position.x;

		if(diff <0) {
			moveDir = Vector3.zero; // cho dung lai
			transform.localScale = new Vector3(1,1,1); //cho quay dung chieu
		}
		else if(diff>0) {
			moveDir = Vector3.right * 0.01f;
			transform.localScale = new Vector3(-1,1,1);
		}
	}
}
