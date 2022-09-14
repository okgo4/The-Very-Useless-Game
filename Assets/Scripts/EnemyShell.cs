using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShell : MonoBehaviour
{
    public GameObject shellSpawn;
    public GameObject shellPrefab;
    public GameObject recoilTank;
    private float CD = 0;
    public float coolDown;
    float speed;
    void Start()
    {
        speed = GetComponent<EnemyAim>().speed;
    }
    void fire()
    {
        GameObject shell = Instantiate(shellPrefab, shellSpawn.transform.position, shellSpawn.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * shellSpawn.transform.forward;
        Vector3 recoil = new Vector3(-shellSpawn.transform.forward.x, 0, -shellSpawn.transform.forward.z);
        recoilTank.GetComponent<Rigidbody>().AddForce(recoil * 11, ForceMode.Impulse);
        Destroy(shell, 5);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CD <= 0)
            {
                fire();
                CD = coolDown;
            }
        }
        if (CD > 0)
        {
            CD -= Time.deltaTime;
        }
    }

}
