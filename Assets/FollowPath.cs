using UnityEngine;

public class FollowPath : MonoBehaviour
{

    public GameObject wpManager;
    GameObject[] wps;
    UnityEngine.AI.NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update()
    {
        float rh = Input.GetAxis("Horizontal");
        float rv = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(0, 0, rv) * Time.deltaTime * 5);
        transform.Rotate(new Vector3(0, rh, 0) * Time.deltaTime * 20);
    }
    public void GoToHeli()
    {
        agent.SetDestination(wps[4].transform.position);
    }

    public void GoToRuin()
    {
        agent.SetDestination(wps[0].transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
}