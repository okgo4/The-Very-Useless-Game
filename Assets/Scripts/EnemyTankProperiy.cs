using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTankProperiy : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public float moveSpeed;
    public float rotationSpeed;
    public float health;
    public float score;
    public GameObject deathEffect;
    
    [SerializeField]
    private Slider healthSlider;

    private float time;
    private float timeDelay;

    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        time = 0f;
        timeDelay = 3f;
        score = health;

    }

    void Update()
    {
    
        healthSlider.value = health;
        if (health <= 0)
        {

            if(time == 0f){
                GameObject turrent = this.transform.Find("TankRenderers").gameObject.transform.Find("TankTurret").gameObject;
                turrent.AddComponent(typeof(Rigidbody));
                turrent.AddComponent(typeof(BoxCollider));
                Rigidbody turrentrigid = turrent.GetComponent<Rigidbody>();
                turrentrigid.AddTorque(0, 0.05f, 0, ForceMode.Impulse);
            } 
            time = time + 1f*Time.deltaTime;
            if(time > timeDelay){
                time = 0f;
                Instantiate(deathEffect, this.transform.position, this.transform.rotation);
                Destroy(gameObject);
                Destroy(gameObject);
                GameObject.Find("MainControl").GetComponent<MainControl>().gameScore += score;
                GameObject.Find("MainControl").GetComponent<MainControl>().refreshCD = 5;
            }   
        }


    }
}

   