using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject shellSpawn;
    public GameObject shellPrefab;
    public GameObject smokePrefab;
    public GameObject recoilTank;
    public GameObject shellSmoke;
    public AudioClip shellShot;
    public AudioClip shellExplosion;
    public AudioSource shellSE;
    private float CD = 0;
    public float coolDown;
    public float damage;
    float speed;
    void Start()
    {
        speed = GetComponent<Turret>().speed;
    }
    void fire()
    {
        GameObject shell = Instantiate(shellPrefab, shellSpawn.transform.position, shellSpawn.transform.rotation);
        shell.GetComponent<shellDirection>().damage = damage;
        GameObject smoke = Instantiate(smokePrefab, shellSmoke.transform.position, shellSmoke.transform.rotation);
        shellSE.clip = shellShot;
        shellSE.Play();
        AudioSource.PlayClipAtPoint(shellShot, transform.position);
        shell.GetComponent<Rigidbody>().velocity = speed * shellSpawn.transform.forward;
        Vector3 recoil = new Vector3(-shellSpawn.transform.forward.x, 0, -shellSpawn.transform.forward.z);
        recoilTank.GetComponent<Rigidbody>().AddForce(recoil * 11, ForceMode.Impulse);
        Destroy(shell, 5);
        shellSE.clip = shellExplosion;
        shellSE.Play();

    }
    void Update()
    {
        damage = GameObject.Find("/Tank").gameObject.GetComponent<ControlTank>().damage;
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
