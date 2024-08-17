using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IChargeable
{
    [SerializeField]
    private Animator doorAnimator;
    [SerializeField]
    private int numberOfChargesToOpen;
    [SerializeField]
    private int currentNumberOfCharges;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Charge()
    {
        currentNumberOfCharges++;
        if (currentNumberOfCharges >= numberOfChargesToOpen)
        {
            Open();
        }
    }

    public void Open()
    {
        doorAnimator.SetBool(AnimationStrings.isActive, true);
    }

    public void Close()
    {
        doorAnimator.SetBool(AnimationStrings.isActive, false);
    }

    public void Discharge()
    {
        currentNumberOfCharges--;
        if (currentNumberOfCharges < numberOfChargesToOpen)
        {
            Close();
        }
    }

}
