using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShell : MonoBehaviour
{
    public GameObject shellSpawn;
    public GameObject shellPrefab;
    public GameObject smokePrefab;
    public GameObject recoilTank;
    public GameObject shellSmoke;
    public float damage;
    public AudioClip shellShot;
    public AudioClip shellExplosion;
    public AudioSource shellSE;
    private float CD = 0;
    public float coolDown;
    public bool canFire = true;
    float speed;
    void Start()
    {
        speed = GetComponent<EnemyAim>().speed;
    }
    public void fire()
    {


        if(canFire)
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
            CD = coolDown;
            shellSE.clip = shellExplosion;
            shellSE.Play();
        }
        


    }
    void Update()
    {
        canFire = !(CD > 0); 
        if (CD >= 0)
        {
            CD -= Time.deltaTime;
        }
    }

}
