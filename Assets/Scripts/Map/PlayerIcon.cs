using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Vector2 move;
    PlayerController playerController;
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (!playerController.IsMoving()) return;
        move = playerController.GetMoveAmount();
        playerRigidbody.MovePosition(playerRigidbody.position + move * moveSpeed * Time.deltaTime);
    }
}
