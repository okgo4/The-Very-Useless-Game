using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellDirection : MonoBehaviour
{
    public float damage;
    public GameObject smokePrefab;
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
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyTankProperiy>().health -= damage;
            
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ControlTank>().health -= damage;
        }
        Instantiate(smokePrefab, gameObject.transform.position, gameObject.transform.rotation);

        Destroy(gameObject);
    }
}
