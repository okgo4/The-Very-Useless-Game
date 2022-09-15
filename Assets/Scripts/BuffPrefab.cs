using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPrefab : MonoBehaviour
{
    private float cnt;
    void Start()
    {
        cnt = 10;
    }
    void Update()
    {   
        cnt -= Time.deltaTime;
        if (cnt <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                Destroy(gameObject);
                break;
        }
    }
}
