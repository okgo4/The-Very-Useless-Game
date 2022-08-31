using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    float gravity = -9.8f;
    float mass = 20;
    float zVel = 0;
    float yVel = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        yVel = yVel + gravity / mass * Time.deltaTime;

        transform.Translate(0, yVel, 20 * Time.deltaTime);
    }
}
