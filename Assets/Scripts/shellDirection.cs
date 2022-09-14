using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellDirection : MonoBehaviour
{
    public float damage;
    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>().velocity.magnitude > 0.9)
        {
            transform.forward = GetComponent<Rigidbody>().velocity;
            
        }
        damage = GameObject.Find("Tank").gameObject.GetComponent<ControlTank>().damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
