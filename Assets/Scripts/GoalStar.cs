using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalStar : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.HasGoalStar = true;
            AudioManager.Instance.PlaySFX(SoundType.Energy_Gathered);
        }
        Destroy(gameObject);
    }

}
