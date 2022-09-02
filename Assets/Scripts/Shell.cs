using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject shellSpawn;
    public GameObject shellPrefab;
    float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<Turret>().speed;
    }

    void fire()
    {
        GameObject shell = Instantiate(shellPrefab, shellSpawn.transform.position, shellSpawn.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * shellSpawn.transform.forward;
        Destroy(shell, 5);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
        }
    }
   

}
