using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForward : MonoBehaviour
{
    public float speed;
    public float timeToLoop;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToLoop);
            speed *= -1;
        }
    }
}
