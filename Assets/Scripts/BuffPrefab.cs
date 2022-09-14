using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPrefab : MonoBehaviour
{
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
