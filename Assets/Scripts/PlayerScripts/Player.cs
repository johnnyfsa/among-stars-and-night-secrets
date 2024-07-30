using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public event System.Action OnCharge;
    public event System.Action OnDischarge;


    [Header("Animation")]
    private Animator animator;


    [Header("Movement")]

    [SerializeField]
    float moveSpeed;
    private float horizontalMovement;
    private float verticalMovement;
    [SerializeField]
    private bool isFacingRight = true;

    [SerializeField]
    private bool isMovingOnStairs;

    public bool IsMovingOnStairs
    {
        get { return isMovingOnStairs; }
        set
        {
            animator.SetBool(AnimationStrings.isMovingOnStairs, value);
            isMovingOnStairs = value;
        }
    }

    [SerializeField]
    private bool isMoving;
    public bool IsMoving
    {
        get { return isMoving; }
        set
        {
            animator.SetBool(AnimationStrings.isMoving, value);
            isMoving = value;
        }
    }

    [Header("Jumping")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);

    [SerializeField]
    private bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
        set
        {
            animator.SetBool(AnimationStrings.isGrounded, value);
            isGrounded = value;
        }
    }

    public LayerMask groundLayer;
    public LayerMask platformLayer;

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

    public int MaxNumberOfCharges { get { return maxNumberOfCharges; } set { maxNumberOfCharges = value; } }
    [SerializeField]
    private int maxNumberOfCharges;

    [SerializeField]
    private bool isCharged;
    public bool IsCharged
    {
        get { return isCharged; }
        set
        {
            animator.SetBool(AnimationStrings.isCharged, value);
            isCharged = value;
        }
    }

    [SerializeField]
    private bool hasGoalStar;

    public bool HasGoalStar
    {
        get { return hasGoalStar; }
        set { hasGoalStar = value; }
    }

    [Header("StairsMovement")]

    Collider2D playerCollider;
    [SerializeField]
    bool isOnStairs;
    [SerializeField]
    bool isOnStairsArea;
    public bool IsOnStairsArea
    {
        get { return isOnStairsArea; }
        set { isOnStairsArea = value; }
    }
    public bool IsOnStairs
    {
        get { return isOnStairs; }
        set
        {
            isOnStairs = value;
            animator.SetBool(AnimationStrings.isOnStairs, value);
        }
    }

    private float originalMoveSpeed;
    private float reducedMoveSpeed;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        horizontalMovement = 0;
        originalMoveSpeed = moveSpeed;
        reducedMoveSpeed = moveSpeed * 0.5f;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        CheckHorizontalVelocityModifierWhileJumping();
        if (!IsOnStairs)
        {
            rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        }
        else if (IsOnStairs && !IsGrounded)
        {
            rb.velocity = new Vector2(0, verticalMovement * moveSpeed);
        }
        else if (IsGrounded && IsOnStairs)
        {
            rb.velocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
        }
        MovementCheck();
        Flip();
    }

    private void CheckHorizontalVelocityModifierWhileJumping()
    {

        if (!IsGrounded)
        {
            moveSpeed = reducedMoveSpeed;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
        }
    }

    void Update()
    {
        ChargeCheck();
        ChechStairs();
    }

    private void ChechStairs()
    {
        if (!IsOnStairsArea)
        {
            IsOnStairs = false;
        }
        if (IsOnStairs && !IsGrounded)
        {
            rb.gravityScale = 0;
            playerCollider.excludeLayers = LayerMask.GetMask("Platforms");
        }
        else
        {
            rb.gravityScale = 1;
            playerCollider.excludeLayers = LayerMask.GetMask("Nothing");
        }
    }

    private void ChargeCheck()
    {
        if (numberOfChargesCarried > 0 || HasGoalStar)
        {
            IsCharged = true;
        }
        else
        {
            IsCharged = false;
        }
    }

    private void MovementCheck()
    {
        if (horizontalMovement != 0 && IsGrounded)
        {
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
        if (verticalMovement != 0 && IsOnStairs)
        {
            IsMovingOnStairs = true;
        }
        else
        {
            IsMovingOnStairs = false;
        }
    }

    public void Move(InputAction.CallbackContext callback)
    {
        horizontalMovement = callback.ReadValue<Vector2>().normalized.x;
        verticalMovement = callback.ReadValue<Vector2>().normalized.y;
        if (callback.performed)
        {
            if (callback.ReadValue<Vector2>().normalized.y > 0)
            {
                OnCharge?.Invoke();
            }
            else if (callback.ReadValue<Vector2>().normalized.y < 0)
            {
                OnDischarge?.Invoke();
            }
        }
        if (callback.performed && callback.ReadValue<Vector2>().normalized.y != 0)
        {
            if (isOnStairsArea)
            {
                IsOnStairs = true;
            }
        }

    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0f || !isFacingRight && horizontalMovement > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Jump(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (IsGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            AudioManager.Instance.PlaySFX(SoundType.Normal_Jump);
        }
    }

    private void GroundCheck()
    {
        if (!IsOnStairs)
        {
            if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer) || Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, platformLayer))
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }
        else
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

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
