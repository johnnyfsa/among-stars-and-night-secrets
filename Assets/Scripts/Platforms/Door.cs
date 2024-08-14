using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IChargeable
{
    [SerializeField]
    private Animator doorAnimator;
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
        doorAnimator.SetBool(AnimationStrings.isActive, true);
    }

    public void Discharge()
    {
        doorAnimator.SetBool(AnimationStrings.isActive, false);
    }

}
