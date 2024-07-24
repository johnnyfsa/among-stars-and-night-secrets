using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{
    private float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeDirection());
    }

    //the sea must go left and right with some time in between
    void Update()
    {
        transform.Translate(speed * Vector3.left * Time.deltaTime * 0.5f);

    }

    //make an ienumerator to change the direction of the sea
    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            speed *= -1;
        }
    }

}
