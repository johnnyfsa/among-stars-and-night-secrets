using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour, IChargeable
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    [SerializeField]
    bool isActive;
    bool IsActive
    {
        get { return isActive; }
        set
        {
            isActive = value;
            animator.SetBool(AnimationStrings.isActive, isActive);
        }
    }

    [SerializeField]
    float springForce = 10;

    [SerializeField]
    float springForceCharge = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            if (collision.GetContact(0).normal == Vector2.down)
            {
                IsActive = true;
                float correctionFactor = 1.5f;
                rb.AddForce((Vector2.up * springForce * correctionFactor), ForceMode2D.Impulse);
            }
            else
            {
                IsActive = true;
                rb.AddForce((Vector2.up * springForce), ForceMode2D.Impulse);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsActive = false;
    }

    public void Charge()
    {
        springForce += springForceCharge;
    }

    public void Discharge()
    {
        springForce -= springForceCharge;
    }
}
