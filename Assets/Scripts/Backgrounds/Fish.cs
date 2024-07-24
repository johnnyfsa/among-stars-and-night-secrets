using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //the background must go left until x=-17 then it should reset to x=35
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        if (transform.position.x <= -26.24f)
        {
            transform.position = new Vector3(35, transform.position.y, transform.position.z);
        }
    }
}
