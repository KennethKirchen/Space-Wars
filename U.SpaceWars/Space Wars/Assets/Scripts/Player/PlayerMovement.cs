using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
        EventManager.Instance.InvertControlsEvent += SetControls;
        SetControls();
    }

    private void OnDestroy()
    {
        EventManager.Instance.InvertControlsEvent -= SetControls;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 movement = Vector2.zero;
        movement.x = InputReader.Instance.MoveCompostion * moveSpeed;
        movement.y = 0;

        rig.velocity = movement;
    }

    void SetControls()
    {
        if (PlayerPrefs.GetInt("Invert Controls") == 0)
        {
            moveSpeed = Mathf.Abs(moveSpeed);
        }
        else
        {
            moveSpeed = -moveSpeed;
        }
    }
}
