using System.Collections.Generic;
using UnityEngine;

public class LoopingPlatform : MonoBehaviour, IChargeable
{
    [SerializeField]
    private List<Transform> wayPoints;

    [SerializeField]
    Charger charger;

    Vector3 nextPosition;
    Vector3 startingPosition;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int currentIndex;
    private bool ascending = true;

    [SerializeField]
    private int numberOfChargesCarried = 0;

    private bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        nextPosition = wayPoints[0].position;
        startingPosition = transform.position;
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckNumberOfCarriedCharges();
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * moveSpeed);
            if (transform.position == nextPosition)
            {
                if (ascending)
                {
                    currentIndex++;
                    if (currentIndex == numberOfChargesCarried)
                    {
                        ascending = false;
                    }
                }
                else
                {
                    currentIndex--;
                    if (currentIndex == 0)
                    {
                        ascending = true;
                    }
                }
                nextPosition = wayPoints[currentIndex].position;
            }


        }
    }


    private void CheckNumberOfCarriedCharges()
    {
        if (numberOfChargesCarried > 0)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
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

    public void Charge()
    {
        numberOfChargesCarried++;
    }

    public void Discharge()
    {
        numberOfChargesCarried--;
    }
}
