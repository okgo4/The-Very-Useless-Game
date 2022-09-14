using UnityEngine;

public class ControlTank : MonoBehaviour
{

    public GameObject wpManager;
    GameObject[] wps;
    UnityEngine.AI.NavMeshAgent agent;
    public float moveSpeed;
    public float rotationSpeed;

    public float moveSpeedOri;
    public float moveSpeedAcc;
    public float accCount;

    public AudioClip tankMove;
    public AudioSource backgroundMusic;
    public AudioSource tankState;

    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        
    }
    void Update()
    {
        // Acceleration Buff Counting
        if (accCount > 0)
        {
            moveSpeed = moveSpeedAcc;
            accCount -= Time.deltaTime;
        }
        else
        {
            moveSpeed = moveSpeedOri;
            accCount = 0;
        }

        float rh = Input.GetAxis("Horizontal");
        float rv = Input.GetAxis("Vertical");
        if (rh !=null || rv != null)
        {
            tankState.clip = tankMove;
            tankState.Play();
            // AudioSource.PlayClipAtPoint(tankMove, transform.position);
        }
        else
        {
            tankState.Stop();
        }
        transform.Translate(new Vector3(0, 0, rv) * Time.deltaTime * moveSpeed);
        transform.Rotate(new Vector3(0, rh, 0) * Time.deltaTime * rotationSpeed);
    }
    public void GoToHeli()
    {
        agent.SetDestination(wps[4].transform.position);
    }

    public void GoToRuin()
    {
        agent.SetDestination(wps[0].transform.position);
    }

    // Buff System
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Accelerate":
                Destroy(other.gameObject);
                accCount=15;
                break;
        }
    }
}