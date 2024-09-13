using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatMove : MonoBehaviour
{
    Vector3 oriPos;
    float time;
    int forward;
    float speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        oriPos = transform.position;
        forward = 0;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(forward == 0)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if (transform.position.y >= 2) forward = 1;
        if (transform.position.y <= -14) forward = 0;

    }
}
