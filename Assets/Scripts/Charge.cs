using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charge : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.NumberOfChargesCarried += 1;
            AudioManager.Instance.PlaySFX(SoundType.Energy_Gathered);
        }
        Destroy(gameObject);
    }
}
