using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject shellSpawn;
    public GameObject shellPrefab;
    public GameObject recoilTank;
    float speed;
    void Start()
    {
        speed = GetComponent<Turret>().speed;
    }
    void fire()
    {
        GameObject shell = Instantiate(shellPrefab, shellSpawn.transform.position, shellSpawn.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * shellSpawn.transform.forward;
        recoilTank.GetComponent<Rigidbody>().AddForce(-shellSpawn.transform.forward * 10,ForceMode.Impulse);
        Destroy(shell, 4);
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
        }
    }
   

}
