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
    
    [SerializeField]
    private Slider healthSlider;

    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        score = health;
    }

    void Update()
    {
    
        healthSlider.value = health;
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject.Find("MainControl").GetComponent<MainControl>().gameScore += score;
            GameObject.Find("MainControl").GetComponent<MainControl>().refreshCD = 5;
        }


    }

   
}
