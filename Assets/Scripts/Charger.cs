using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charger : MonoBehaviour
{
    public event Action OnCharge;
    public event Action OnDischarge;

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
            numberOfChargesCarried++;
            chargeVisuals[numberOfChargesCarried - 1].SetActive(true);
        }
    }

    public void Discharge()
    {
        if (isInChargingArea && numberOfChargesCarried > 0)
        {
            foreach (IChargeable chargeable in chargeables)
            {
                chargeable.Discharge();
                OnDischarge?.Invoke();
            }
            player.NumberOfChargesCarried = Mathf.Min(player.NumberOfChargesCarried + 1, numberOfChargesCarried + player.NumberOfChargesCarried);
            numberOfChargesCarried = Mathf.Max(numberOfChargesCarried - 1, 0);
            chargeVisuals[numberOfChargesCarried].SetActive(false);
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
