using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public GameObject target;
    public GameObject shellSpawn;
    public float speed;
    public float rotateSpeed = 20;
    float yAngle = 0;
    void LateUpdate()
    {
        target = GameObject.Find("Tank");

        float? upAngle = calculateAngle(target.transform.position);
        if (upAngle != null)
        {
            yAngle = (float)upAngle;
        }


        //Debug.Log(yAngle);               -0.1 ---- 0.3
        yAngle = Mathf.Clamp(yAngle, -0.1f, 0.3f);
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, yAngle, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
    float? calculateAngle(Vector3 targetPos)
    {
        Vector3 targetDirection = targetPos - shellSpawn.transform.position;
        float y = targetDirection.y;
        float x = targetDirection.magnitude;
        float gravity = 9.8f;
        float speedSqr = speed * speed;
        float squareRoot = (speedSqr * speedSqr) - gravity * (gravity * x * x + 2 * y * speedSqr);
        if (squareRoot >= 0)
        {
            float root = Mathf.Sqrt(squareRoot);
            //float highAngle = speedSqr + root;
            float lowAngle = speedSqr - root;
            return (Mathf.Atan2(lowAngle, gravity * x));
        }
        return null;

    }
}
