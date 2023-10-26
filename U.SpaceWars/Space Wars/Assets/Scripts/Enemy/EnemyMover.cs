using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rig;

    const string BOUNDS_TAG_STRING = "Bounds";

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 movement = new Vector2();
        movement.x = moveSpeed;
        movement.y = 0;

        rig.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == BOUNDS_TAG_STRING)
        {
            moveSpeed = -moveSpeed;
        }
    }
}
