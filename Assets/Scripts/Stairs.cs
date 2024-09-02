using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<Player>().IsOnStairsArea = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player p))
        {
            if (p.IsOnStairs && !p.IsGrounded)
            {
                p.transform.position = new Vector3(transform.position.x, p.transform.position.y, p.transform.position.z);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<Player>().IsOnStairsArea = false;
        }
    }

}
