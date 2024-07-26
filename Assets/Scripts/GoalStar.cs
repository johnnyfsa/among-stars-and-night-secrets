using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalStar : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.HasGoalStar = true;
        }
        Destroy(gameObject);
    }
}
