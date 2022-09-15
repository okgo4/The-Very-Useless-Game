using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField]
    private GameObject buffPrefab;
    [SerializeField]
    private GameObject anchor;
    private bool exist;
    public float renewCount;

    // Start is called before the first frame update
    void Start()
    {
        renewCount = 15;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!exist)
        {
            renewCount -= Time.deltaTime;
            if (renewCount <= 0)
            {
                renew();
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            renew();
        }
    }

    private void renew()
    {
        GameObject buff = Instantiate(buffPrefab);
        buff.transform.position = anchor.transform.position;
        exist = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && (exist == true))
        {
            renewCount=15;
            exist=false;
        }
    }
}
