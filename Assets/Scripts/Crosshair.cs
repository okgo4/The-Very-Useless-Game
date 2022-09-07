using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject turret;
    void Update()
    {
        Vector3 mousePos = turret.GetComponent<Turret>().hitPosition;
        Vector3 mouseNormal = turret.GetComponent<Turret>().hitNormal;
        transform.position = mousePos;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, mouseNormal);
    }
}
