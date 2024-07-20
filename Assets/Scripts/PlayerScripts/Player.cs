using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField]

    [Header("Movement")]
    float moveSpeed;
    private float direction;

    [Header("Jumping")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);

    [SerializeField]
    private bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }

    public LayerMask groundLayer;

    [SerializeField]
    private float jumpForce = 5.0f;

    private Rigidbody2D rb;

    [Header("Charges")]

    [SerializeField]
    int numberOfChargesCarried;

    public int NumberOfChargesCarried
    {
        get { return numberOfChargesCarried; }
        set { numberOfChargesCarried = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext callback)
    {
        direction = callback.ReadValue<Vector2>().normalized.x;
    }

    public void Jump(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (IsGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
