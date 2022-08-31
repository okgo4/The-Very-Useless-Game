using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellDirection : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>().velocity.magnitude > 0.7)
        {
            transform.forward = GetComponent<Rigidbody>().velocity;
        }
    }
}
