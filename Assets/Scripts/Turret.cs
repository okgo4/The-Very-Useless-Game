using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
   
    public GameObject target;
    public GameObject shellSpawn;
    float speed = 15;

    
    // Update is called once per frame
    void Update()
    {
        float? upAngle = calculateAngle();
        //Debug.Log(upAngle);
        float yAngle = 0;
        if(upAngle!= null)
        {
            yAngle = (float)upAngle;
        }
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, yAngle, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }
    float? calculateAngle()
    {
        Vector3 targetDirection = target.transform.position - shellSpawn.transform.position;
        float y = targetDirection.y;
        float x = targetDirection.magnitude;
        float gravity = 9.8f;
        float speedSqr = speed * speed;
        float squareRoot = (speedSqr * speedSqr) - gravity * (gravity * x * x + 2 * y * speedSqr);
        
        if (squareRoot >= 0)
        {
            float root = Mathf.Sqrt(squareRoot);
            float highAngle = speedSqr + root;
            float lowAngle = speedSqr - root;
            return (Mathf.Atan2(lowAngle, gravity * x) );
        }
        return null;
    }
    
}
