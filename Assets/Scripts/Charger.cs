using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charger : MonoBehaviour
{
    public event Action OnCharge;
    public event Action OnDischarge;

    public event Action OnChargesCarriedNumberIncreased;
    public event Action OnChargesCarriedNumberDecreased;

    [SerializeField]
    GameObject[] chargeableObjects;
    [SerializeField]
    GameObject[] chargeVisuals;
    private List<IChargeable> chargeables;

    [SerializeField]
    private bool isInChargingArea;
    [SerializeField]
    Player player;

    [SerializeField]
    int numberOfChargesCarried;

    public int NumberOfChargesCarried { get => numberOfChargesCarried; set => numberOfChargesCarried = value; }

    void Awake()
    {

        player.OnCharge += Charge;
        player.OnDischarge += Discharge;
        chargeables = new List<IChargeable>();
    }

    void OnDestroy()
    {
        player.OnCharge -= Charge;
        player.OnDischarge -= Discharge;
    }

    void Start()
    {
        for (int i = 0; i < chargeableObjects.Length; i++)
        {
            chargeables.Add(chargeableObjects[i].GetComponent<IChargeable>());
        }
        numberOfChargesCarried = 0;
    }


    public void Charge()
    {
        if (isInChargingArea && player.NumberOfChargesCarried > 0)
        {

            foreach (IChargeable chargeable in chargeables)
            {
                chargeable.Charge();
                OnCharge?.Invoke();
            }
            player.NumberOfChargesCarried = Mathf.Max(player.NumberOfChargesCarried - 1, 0);
            OnChargesCarriedNumberIncreased?.Invoke();
            numberOfChargesCarried++;
            chargeVisuals[numberOfChargesCarried - 1].SetActive(true);
        }
    }

    public void Discharge()
    {
        if (isInChargingArea && numberOfChargesCarried > 0)
        {
            //player gets 1 charge back but it cannot be more than the max the player can carry
            if (player.NumberOfChargesCarried + 1 <= player.MaxNumberOfCharges)
            {
                player.NumberOfChargesCarried++;
                numberOfChargesCarried = Mathf.Max(numberOfChargesCarried - 1, 0);
                OnChargesCarriedNumberDecreased?.Invoke();
                foreach (IChargeable chargeable in chargeables)
                {
                    chargeable.Discharge();
                }
                chargeVisuals[numberOfChargesCarried].SetActive(false);
                if (numberOfChargesCarried == 0)
                {
                    OnDischarge?.Invoke();
                }

            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isInChargingArea = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isInChargingArea = false;
    }


}
