using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    List<Transform> waypoints;
    Vector3 nextPosition;
    [SerializeField]
    private int location;
    Vector3 startingPosition;

    [SerializeField]
    Charger charger;

    [SerializeField]
    float moveSpeed = 2f;

    [SerializeField]
    private bool canMove = false;

    [SerializeField]
    int maxNumberOfCharges;

    void Awake()
    {
        charger.OnChargesCarriedNumberIncreased += SetDestinationForward;
        charger.OnChargesCarriedNumberDecreased += SetDestinationBackward;
        maxNumberOfCharges = waypoints.Count;
    }

    private void SetDestinationBackward()
    {
        /*if the charger currently has more charges than the max number of charges this platform can carry, 
        then this should not move. Otherwise the player can decrease the amount of charges in the charger,
        and this platform will move even if it still contains the max amount of charges! 
        The platform should only move when the amount of charges in it is less then it can carry*/
        if (charger.NumberOfChargesCarried >= maxNumberOfCharges)
        {
            return;
        }
        location--;
        if (location < 0)
        {
            location = -1;
            nextPosition = startingPosition;
        }
        else if (location >= 0)
        {
            nextPosition = waypoints[location].position;
        }
        canMove = true;
    }

    private void SetDestinationForward()
    {
        location++;
        if (location >= waypoints.Count)
        {
            location--;
        }
        nextPosition = waypoints[location].position;
        canMove = true;

    }

    void OnDestroy()
    {
        charger.OnChargesCarriedNumberIncreased -= SetDestinationForward;
        charger.OnChargesCarriedNumberDecreased -= SetDestinationBackward;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        location = -1;
        canMove = false;
        nextPosition = waypoints[0].position;
    }

    void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
            if (transform.position == nextPosition)
            {
                canMove = false;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }


}
