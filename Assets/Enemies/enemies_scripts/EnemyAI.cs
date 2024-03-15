using System.Collections;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    [SerializeField] private groundOrAir type;
	private enum groundOrAir {
		ground,
		air
	}

	private enum State{
		Roaming,
		Attacking
	}
	State state;
	EnemyPathFinding enemyPathFinding;

    private Vector2 roamPosition; // luu gia tri random
    private float timeRoaming = 0f;
    private Ray ray;
    [SerializeField] bool enemyFacingLeft = true;
    [SerializeField] private GameObject raycastPoint;
    [SerializeField] float distanceToChangeDir = 2f;
    [SerializeField] private bool isTouchObtacle = false;
    public float checkDistance;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] float roamingChangeDirFloat = 1;
    private bool canAttack = true;
    [SerializeField] private bool stopMovingWhileAttacking = false;
    [SerializeField] private float attackCoolDown = 2f;
    [SerializeField] private MonoBehaviour enemyType; // scrip loai enemy
	private EnemyHealth enemyHealth;

    private void Awake() {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }

    private void Start() {
		enemyHealth = GetComponent<EnemyHealth>();
        roamPosition = GetRoamingPos();
    }
    private void Update() {
        //todo kiem tra direction cua enemy
		CheckDirectionLeftRight();
        //todo ve raycast do khoang cach attackRange + raycast va cham doi huong
		if(type == groundOrAir.ground) {
            DrawRaycast();
			CheckObtacleLeftright();
            CheckObtacleBottom();
		}

		if(!PlayerHealth.Instance.isDead) {
			checkDistance = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);
			MovementStateControl();
		}
		
    }
    private void MovementStateControl() {
		
		switch (state)
		{
			default:
			case State.Roaming :
			{
				Roaming();
				break;
			}
			case State.Attacking :
			{
				Attacking();
				break;
			}

		}
	}


    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
	
		enemyPathFinding.MoveTo(roamPosition); // dang di chuyen roaming

		if(checkDistance < attackRange) {

			if(type == groundOrAir.ground) {
				//todo phai cho facing thi moi tan cong
				float diff = PlayerController.Instance.transform.position.x - gameObject.transform.position.x;
				if((enemyFacingLeft && (diff < 0)) || (!enemyFacingLeft && (diff > 0)))
				{
					state = State.Attacking;
				}
			} else if(type == groundOrAir.air) {
				//todo neu la air thi tan cong khong can xet facing voi nhau
				state = State.Attacking;
			}
		}

		if(isTouchObtacle || timeRoaming > roamingChangeDirFloat) {

			isTouchObtacle = false;
			roamPosition = GetRoamingPos(); // timeRoaming xet ve 0 + nhan toa do random moi de di tiep
		}
    }

    private void Attacking() {

		if(checkDistance > attackRange) {
			state = State.Roaming;
		}

		if(attackRange != 0 && canAttack && !enemyHealth.IsDie()) {
			canAttack = false;
			// goi ham attqack thong qua interface
			(enemyType as IEnemy). Attack();

			if(stopMovingWhileAttacking) enemyPathFinding.StopMoving();
			else enemyPathFinding.MoveTo(roamPosition);

			StartCoroutine(AttackCoolDownRoutine()); // delay va tan cong theo attackCoolDOwn time
		}
	}

	private IEnumerator AttackCoolDownRoutine() {
		yield return new WaitForSeconds(attackCoolDown);
		canAttack = true;
	}

    private void CheckDirectionLeftRight()
    {
        if(roamPosition.x < 0) enemyFacingLeft =true;
		else if(roamPosition.x > 0) enemyFacingLeft = false;
    }

    private Vector2 GetRoamingPos()
    {
        timeRoaming = 0; // xet ve 0 => khi bat dau roaming dem tiep
		
		if(type == groundOrAir.ground) {
			return new Vector2(Random.Range(-5,5), 0).normalized;
		} else
		{
			return new Vector2(Random.Range(-5,5), Random.Range(-5,5)).normalized;
		}
    }

    private void CheckObtacleLeftright() {
		RaycastHit hit;
		Vector3 direction;

		if(enemyFacingLeft) direction = Vector3.left;
		else direction = Vector3.right;

		if(Physics.Raycast(raycastPoint.transform.position , direction, out hit, distanceToChangeDir)) {
			Debug.DrawLine(raycastPoint.transform.position, hit.point, Color.yellow);
			if(hit.transform != null) {
				isTouchObtacle = true;
			} else isTouchObtacle = false;
		}
	}

    private void CheckObtacleBottom() {
		RaycastHit hit;

		if(Physics.Raycast(raycastPoint.transform.position , Vector3.down, out hit, distanceToChangeDir)) {
			Debug.DrawLine(raycastPoint.transform.position, hit.point, Color.yellow);
			if(hit.transform != null) {
				isTouchObtacle = true;
			} else isTouchObtacle = false;
		}
	}

    private void DrawRaycast() {

        ray.origin = raycastPoint.transform.position;
		ray.direction = Vector3.left;
		if(enemyFacingLeft) Debug.DrawRay(ray.origin, ray.direction * attackRange, Color.red, 1f);
		else if(!enemyFacingLeft) Debug.DrawRay(ray.origin, -ray.direction * attackRange, Color.red, 1f);
	}



    //todo
}
