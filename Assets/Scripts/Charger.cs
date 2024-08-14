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
    [SerializeField]
    private GameObject chargeVisualOff;
    private List<GameObject> chargesVisualsOff;
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

        EventManager.OnPlayerCharge += Charge;
        EventManager.OnPlayerDischarge += Discharge;
        chargeables = new List<IChargeable>();
    }

    void OnDestroy()
    {
        EventManager.OnPlayerCharge -= Charge;
        EventManager.OnPlayerDischarge -= Discharge;
    }

    void Start()
    {
        for (int i = 0; i < chargeableObjects.Length; i++)
        {
            chargeables.Add(chargeableObjects[i].GetComponent<IChargeable>());
        }
        numberOfChargesCarried = 0;
        chargesVisualsOff = new List<GameObject>();
        for(int i = 0; i < chargeVisuals.Length; i++)
        {
            var temp = Instantiate(chargeVisualOff, chargeVisuals[i].transform.position, Quaternion.identity, transform);
            chargesVisualsOff.Add(temp);
            temp.SetActive(true);
        }
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
            chargesVisualsOff[numberOfChargesCarried - 1].SetActive(false);
            AudioManager.Instance.PlaySFX(SoundType.Energy_Deposited);
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
                chargesVisualsOff[numberOfChargesCarried].SetActive(true);
                if (numberOfChargesCarried == 0)
                {
                    OnDischarge?.Invoke();
                }
                AudioManager.Instance.PlaySFX(SoundType.Energy_Gathered);
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
